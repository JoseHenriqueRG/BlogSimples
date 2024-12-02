namespace Web.Interfaces
{
    public interface IHubConnectionService
    {
        Task SendNotificationAsync(string message);
    }
}
