using Eclipseworks.Dominio.Notifier.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Eclipseworks.Api.Validation.ValidateAttribute;

public class ValidateModelAttribute : ActionFilterAttribute
{
    private readonly Type _modelType;


    public ValidateModelAttribute(Type modelType)
    {
        _modelType = modelType;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var model = context.ActionArguments.Values.FirstOrDefault(_modelType.IsInstanceOfType);

        if (model != null)
        {
            var validatorType = typeof(IValidator<>).MakeGenericType(_modelType);
            var validator = (IValidator)context.HttpContext.RequestServices.GetService(validatorType);

            var validationContext = new ValidationContext<object>(model);
            var validationResult = validator.Validate(validationContext);

            if (!validationResult.IsValid)
            {
                var notificador = (INotifier)context.HttpContext.RequestServices.GetService(typeof(INotifier));
                notificador.Add(validationResult);

                context.Result = new BadRequestObjectResult(notificador.ListNotifications);
            }
        }

        base.OnActionExecuting(context);
    }
}