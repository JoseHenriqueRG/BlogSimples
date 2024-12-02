using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string message)
        {
            await Clients.All.SendAsync("ReceiveNotification", message);
        }
    }
}
