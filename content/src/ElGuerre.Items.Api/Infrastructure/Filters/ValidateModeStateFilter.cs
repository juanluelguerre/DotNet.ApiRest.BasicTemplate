using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace ElGuerre.Items.Api.Infrastructure.Filters
{
    [SuppressMessage("NDepend", "ND2005:AttributeClassNameShouldBeSuffixedWithAttribute",
        Justification = "Used Filter instead of Atribute suffix by convenction for AspNet Core Filters")]
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
