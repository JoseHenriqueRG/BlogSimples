using Microsoft.AspNetCore.SignalR.Client;
using Web.Interfaces;

namespace Web.Services
{
    public class HubConnectionService : IHubConnectionService
    {
        private readonly HubConnection _connection;

        public HubConnectionService()
        {
            _connection = new HubConnectionBuilder()
                .WithUrl("https://localhost:7011/NotificationHub")
                .WithAutomaticReconnect()
                .Build();
        }

        public async Task SendNotificationAsync(string message)
        {
            try
            {
                if (_connection.State == HubConnectionState.Disconnected)
                {
                    await _connection.StartAsync();
                }

                await _connection.InvokeAsync("SendMessage", message);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar notificação: {ex.Message}");
            }
        }
    }
}
