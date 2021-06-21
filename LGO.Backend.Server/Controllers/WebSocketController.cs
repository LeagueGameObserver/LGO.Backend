using System;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LGO.Backend.Server.Controllers
{
    [Route("ws")]
    [ApiController]
    public class WebSocketController : ControllerBase
    {
        private ConcurrentDictionary<Guid, WebSocketWrapper> WebSockets { get; } = new();
        
        [HttpGet]
        public async Task Get()
        {
            if (!HttpContext.WebSockets.IsWebSocketRequest)
            {
                throw new Exception("This endpoint does only accept web socket requests.");
            }

            var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();
            if (webSocket.State != WebSocketState.Open)
            {
                throw new Exception("Unable to open new web socket connection.");
            }

            var wrapper = new WebSocketWrapper(webSocket);
            wrapper.MessageReceived += WebSocket_OnMessageReceived;
            wrapper.Start();
        }

        private void WebSocket_OnMessageReceived(object? sender, string e)
        {
            if (sender is not WebSocketWrapper webSocket)
            {
                return;
            }
            
            
        }

        private class WebSocketWrapper
        {
            private const int BufferSize = 16 * 1024;

            public event EventHandler<string>? MessageReceived; 
            
            public Guid Id { get; } = Guid.NewGuid();
            
            public bool IsRunning { get; private set; }

            private object Lock { get; } = new();
            private WebSocket Socket { get; }
            private CancellationTokenSource _cancellationTokenSource = new();
            private Task? _task;

            public WebSocketWrapper(WebSocket socket)
            {
                Socket = socket;
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
                
                while (IsRunning && Socket.State == WebSocketState.Open)
                {
                    try
                    {
                        var receiveResult = await Socket.ReceiveAsync(buffer, _cancellationTokenSource.Token);
                        message += Encoding.UTF8.GetString(buffer.ToArray(), 0, receiveResult.Count);

                        if (receiveResult.EndOfMessage)
                        {
                            MessageReceived?.Invoke(this, message);
                            message = string.Empty;
                        }
                    }
                    catch (Exception)
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
                }
            }
        }
    }
}