using Eclipseworks.Dominio.Notifier.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eclipseworks.Api.Validation.ValidateAttribute;

public class IdMaiorQueZeroAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var id = context.ActionArguments["id"];

        if (id != null && id is int pagina && pagina <= 0)
        {
            var notificador = (INotifier)context.HttpContext.RequestServices.GetService(typeof(INotifier));
            notificador.Add("O valor do id deve ser maior que zero.");

            context.Result = new BadRequestObjectResult(notificador.ListNotifications);
            return;
        }

        base.OnActionExecuting(context);
    }
}