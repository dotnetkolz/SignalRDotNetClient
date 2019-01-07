using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace SignalRDotNetClient
{
    public class ChatClient
    {
        HubConnection connection;
        public ChatClient()
        {
            connection = new HubConnectionBuilder()
                                .WithUrl("wss://localhost:5001/chatHub")
                                .Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };

            connection.On<string, string>(
                "ReceiveMessage", 
                (user, message) => 
                {

                });

        }
        public async Task SendMessage()
        {
            try
            {
                await connection.StartAsync();
                await connection.InvokeAsync(
                    "SendMessage",
                    "hey",
                    "ok");
            }
            catch(Exception ex)
            {

            }

        }
    }
}
