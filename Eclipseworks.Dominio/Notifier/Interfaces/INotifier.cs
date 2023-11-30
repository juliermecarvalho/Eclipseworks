

namespace Eclipseworks.Dominio.Notifier.Interfaces;

public interface INotifier
{
    void Add(Exception exception);
    void Add(string mensagem);
    void Add(string[] mensagem);
    //void Add(ValidationResult validationResult);
    void Clear();
    List<Notifications> ListNotifications { get; }
    bool HasNotification { get; }
}