using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LGO.Backend.Test.WebSocketClient
{
    public static class Program
    {
        private const int BufferSize = 16 * 1024;
        private static Encoding MessageEncoding { get; } = Encoding.UTF8;
        
        public static async Task Main(string[] args)
        {
            var client = new ClientWebSocket();
            await client.ConnectAsync(new Uri("ws://localhost:5000/ws"), CancellationToken.None);
            Console.WriteLine("Connected.");
            
            var tokenSource = new CancellationTokenSource();

            var receiveTask = Task.Run(() => ReceiveWhileConnected(client, tokenSource.Token), tokenSource.Token);
            while (true)
            {
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    await Task.Delay(1);
                    continue;
                }

                if (message.Equals("q", StringComparison.InvariantCultureIgnoreCase))
                {
                    tokenSource.Cancel();
                    receiveTask.Wait();
                    break;
                }

                var sendBuffer = MessageEncoding.GetBytes(message);
                await client.SendAsync(sendBuffer, WebSocketMessageType.Text, true, tokenSource.Token);
            }
            
            Console.WriteLine("Shutdown.");
        }

        private static async Task ReceiveWhileConnected(WebSocket webSocket, CancellationToken cancellationToken)
        {
            var receiveBuffer = new Memory<byte>(new byte[BufferSize]);
            var message = string.Empty;
            
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    Console.WriteLine("Receiving from WebSocket connection ...");
                    var result = await webSocket.ReceiveAsync(receiveBuffer, cancellationToken);
                    
                    Console.WriteLine($"Received {result.MessageType}.");
                    if (result.MessageType != WebSocketMessageType.Text)
                    {
                        break;
                    }
                    
                    message += MessageEncoding.GetString(receiveBuffer.ToArray(), 0, result.Count);

                    if (result.EndOfMessage)
                    {
                        Console.WriteLine("Received message:");
                        Console.WriteLine(message);
                        message = string.Empty;
                    }
                }
            }
            catch
            {
                // ignore
            }

            try
            {
                await webSocket.CloseOutputAsync(WebSocketCloseStatus.Empty, null, cancellationToken);
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, null, cancellationToken);
            }
            catch
            {
                // ignore
            }
        }
    }
}