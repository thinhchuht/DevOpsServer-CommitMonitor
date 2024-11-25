using Microsoft.AspNetCore.SignalR.Client;

namespace Pusher.Example.ConsoleApp
{
    internal class Program
    {
        const string _url = "https://localhost:7256/notifications";
        static void Main(string[] args)
        {
            var connection = new HubConnectionBuilder()
               .WithUrl(_url).Build();

            connection.Closed += async (error) =>
            {
                Console.WriteLine("Connection closed. Attempting to reconnect...");
                await Task.Delay(new Random().Next(0, 5) * 1000);
                try
                {
                    await connection.StartAsync();
                    Console.WriteLine("Reconnected successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to reconnect: {ex.Message}");
                }
            };

            connection.On<string>("p", noti =>
            {
                Console.WriteLine(noti);
            });

            try
            {
                connection.StartAsync().Wait();
                Console.WriteLine("Connection started. Press any key to exit...");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to start the connection: {ex.Message}");
            }
        }
    }
}
