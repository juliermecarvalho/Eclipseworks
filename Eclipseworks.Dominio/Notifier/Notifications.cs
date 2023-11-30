namespace Eclipseworks.Dominio.Notifier;

public class Notifications
{
    public Notifications(string message)
    {
        Message = message;
    }

    public virtual string Message { get; }
}