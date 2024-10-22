using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Enter Username: ");
        string userName = Console.ReadLine();
        Uri serverUri = new Uri($"ws://localhost:8010/ws?name={userName}"); // Adjust the port if needed
        using (var client = new ClientWebSocket())
        {
            try
            {
                await client.ConnectAsync(serverUri, CancellationToken.None);
                Console.WriteLine("Connected to the server.");

                var receiveTask = ReceiveMessages(client);
                string userInput;

                while ((userInput = Console.ReadLine()) != null)
                {
                    if (userInput.StartsWith("/join "))
                    {
                        await client.SendAsync(Encoding.UTF8.GetBytes(userInput), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else if (userInput.StartsWith("/leave "))
                    {
                        await client.SendAsync(Encoding.UTF8.GetBytes(userInput), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                    else
                    {
                        await client.SendAsync(Encoding.UTF8.GetBytes(userInput), WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static async Task ReceiveMessages(ClientWebSocket client)
    {
        var buffer = new byte[1024 * 4];

        while (client.State == WebSocketState.Open)
        {
            var result = await client.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

            if (result.MessageType == WebSocketMessageType.Close)
            {
                await client.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
                Console.WriteLine("Connection closed.");
            }
            else
            {
                var message = Encoding.UTF8.GetString(buffer, 0, result.Count);
                Console.WriteLine($"{message}");
            }
        }
    }
}
