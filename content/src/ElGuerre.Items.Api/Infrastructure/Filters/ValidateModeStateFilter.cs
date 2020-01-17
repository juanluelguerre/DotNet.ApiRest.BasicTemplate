using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics.CodeAnalysis;

namespace ElGuerre.Items.Api.Infrastructure.Filters
{
    [SuppressMessage("NDepend",
        "ND2005:AttributeClassNameShouldBeSuffixedWithAttribute",
        Justification = "Use 'Filter' suffix instead of Attribute")]
    public class ValidateModelStateFilter : ActionFilterAttribute
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
