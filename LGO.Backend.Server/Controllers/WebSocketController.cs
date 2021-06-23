using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LGO.Backend.Server.Model.Request.WebSocket;
using LGO.Backend.Server.Model.Response.WebSocket;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace LGO.Backend.Server.Controllers
{
    internal static class WebSocketController
    {
        private static JsonSerializerSettings JsonSerializerSettings { get; } = new()
                                                                                {
                                                                                    NullValueHandling = NullValueHandling.Ignore,
                                                                                };

        private static ConcurrentDictionary<Guid, WebSocketWrapper> WebSockets { get; } = new();

        public static async void HandleWebSocketConnectionRequest(HttpContext context, TaskCompletionSource taskCompletionSource)
        {
            var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            if (webSocket.State != WebSocketState.Open)
            {
                throw new Exception("Unable to open new web socket connection.");
            }

            var wrapper = new WebSocketWrapper(webSocket, taskCompletionSource);
            WebSockets.TryAdd(wrapper.Id, wrapper);

            wrapper.Stopped += WebSocket_OnStopped;
            wrapper.MessageReceived += WebSocket_OnMessageReceived;
            wrapper.Start();
        }

        private static void WebSocket_OnStopped(object? sender, EventArgs e)
        {
            if (sender is not WebSocketWrapper wrapper || !WebSockets.TryGetValue(wrapper.Id, out _))
            {
                return;
            }

            if (wrapper.WrapperWebSocket.State != WebSocketState.Open)
            {
                WebSockets.TryRemove(wrapper.Id, out _);
                wrapper.TaskCompletionSource.SetResult();
            }
        }

        private static void WebSocket_OnMessageReceived(object? sender, string e)
        {
            if (sender is not WebSocketWrapper wrapper || !WebSockets.TryGetValue(wrapper.Id, out _))
            {
                return;
            }

            try
            {
                var message = JsonConvert.DeserializeObject<MutableWebSocketRequest>(e, JsonSerializerSettings);
                if (message == null)
                {
                    throw new Exception($"The deserialized message must not be {null}.");
                }

                switch (message.Type)
                {
                    case WebSocketRequestType.Ping:
                    {
                        OnPingRequest(wrapper);
                        return;
                    }
                    default:
                    {
                        throw new Exception($"The request type \"{message.Type}\" is not supported.");
                    }
                }
            }
            catch (Exception exception)
            {
                wrapper.WrapperWebSocket.CloseAsync(WebSocketCloseStatus.InvalidPayloadData, $"Unable to deserialize message: {exception.Message}.", CancellationToken.None);
                WebSockets.TryRemove(wrapper.Id, out _);
            }
        }

        private static async void OnPingRequest(WebSocketWrapper wrapper)
        {
            await wrapper.SendAsync(new MutableWebSocketResponse
                                    {
                                        Typ = WebSocketResponseType.Ping,
                                        Payload = new MutableWebSocketPingResponsePayload
                                                  {
                                                      ConnectionTimeoutInSeconds = Constants.WebSocketConnectionTimeout.TotalSeconds,
                                                  }
                                    });
        }

        private class WebSocketWrapper
        {
            private const int BufferSize = 16 * 1024;
            private static Encoding MessageEncoding { get; } = Encoding.UTF8;

            public event EventHandler<string>? MessageReceived;
            public event EventHandler? Stopped;

            public Guid Id { get; } = Guid.NewGuid();

            public WebSocket WrapperWebSocket { get; }

            public TaskCompletionSource TaskCompletionSource { get; }

            public bool IsRunning { get; private set; }

            private object Lock { get; } = new();
            private CancellationTokenSource _cancellationTokenSource = new();
            private Task? _task;

            public WebSocketWrapper(WebSocket wrapperWebSocket, TaskCompletionSource taskCompletionSource)
            {
                WrapperWebSocket = wrapperWebSocket;
                TaskCompletionSource = taskCompletionSource;
            }

            public void Start()
            {
                lock (Lock)
                {
                    if (IsRunning)
                    {
                        return;
                    }

                    IsRunning = true;
                    _cancellationTokenSource = new CancellationTokenSource();
                    _task = Task.Run(ReceiveWhileConnected);
                }
            }

            private async void ReceiveWhileConnected()
            {
                var buffer = new Memory<byte>(new byte[BufferSize]);
                var message = string.Empty;

                while (IsRunning)
                {
                    try
                    {
                        var receiveResult = await WrapperWebSocket.ReceiveAsync(buffer, _cancellationTokenSource.Token);
                        if (receiveResult.MessageType != WebSocketMessageType.Text)
                        {
                            break;
                        }

                        message += MessageEncoding.GetString(buffer.ToArray(), 0, receiveResult.Count);

                        if (receiveResult.EndOfMessage)
                        {
                            MessageReceived?.Invoke(this, message);
                            message = string.Empty;
                        }
                    }
                    catch (Exception exception)
                    {
                        break;
                    }
                }

                Stop();
            }

            public void Stop()
            {
                lock (Lock)
                {
                    if (!IsRunning)
                    {
                        return;
                    }

                    IsRunning = false;
                    _cancellationTokenSource.Cancel();
                    _task?.Wait();
                    Stopped?.Invoke(this, EventArgs.Empty);
                }
            }

            public async Task<bool> SendAsync(object data)
            {
                if (!IsRunning)
                {
                    return false;
                }

                var serializedData = JsonConvert.SerializeObject(data, Formatting.None, new JsonSerializerSettings {NullValueHandling = NullValueHandling.Ignore});
                var buffer = MessageEncoding.GetBytes(serializedData);
                await WrapperWebSocket.SendAsync(new ReadOnlyMemory<byte>(buffer), WebSocketMessageType.Text, true, CancellationToken.None);

                return true;
            }
        }
    }
}