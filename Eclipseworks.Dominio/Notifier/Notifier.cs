using Eclipseworks.Dominio.Notifier.Interfaces;
using FluentValidation.Results;

namespace Eclipseworks.Dominio.Notifier;

public class Notifier : INotifier
{
    private readonly List<Notifications> _notificacoes = new List<Notifications>();

    public List<Notifications> ListNotifications => _notificacoes;


    public void Add(string mensagem)
    {
        _notificacoes.Add(new Notifications(mensagem));

    }

    public void Add(string[] mensagem)
    {
        foreach (var msg in mensagem)
        {
            _notificacoes.Add(new Notifications(msg));
        }
    }

    public void Add(ValidationResult validationResult)
    {
        foreach (var error in validationResult.Errors)
        {
            _notificacoes.Add(new Notifications(error.ErrorMessage));
        }
    }

    public void Add(Exception exception)
    {
        string mensagemDeErro = exception.Message;
        _notificacoes.Add(new Notifications(mensagemDeErro));

        throw exception;
    }

    public void Clear()
    {
        _notificacoes.Clear();
    }

    public bool HasNotification => _notificacoes.Any();
}