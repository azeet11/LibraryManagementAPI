using LibraryManagementSystem.Interfaces;

namespace LibraryManagementSystem.Services;

public class NotificationService : INotificationService
{
    public void Notify(string message)
    {
        // Could be extended to email, SMS, etc.
        Console.WriteLine($"[Notification]: {message}");
    }
}
