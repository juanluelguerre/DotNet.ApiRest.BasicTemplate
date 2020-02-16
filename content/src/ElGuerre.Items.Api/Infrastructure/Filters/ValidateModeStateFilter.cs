using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ElGuerre.Items.Api.Infrastructure.Filters
{
#pragma warning disable S3376 // Attribute, EventArgs, and Exception type names should end with the type being extended
    public sealed class ValidateModelStateFilter : ActionFilterAttribute
#pragma warning restore S3376 // Attribute, EventArgs, and Exception type names should end with the type being extended
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(context.ModelState);
            }
        }
    }
}
