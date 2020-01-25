using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace ElGuerre.Items.Api.Infrastructure.Filters
{

    /// <summary>
    /// Audit purpose in each requests and responses
    /// </summary>
    /// <!-- ServiceFilterAttribute or TypeFilterAttribute are other options to audit porposes. -->
    public class ActionLoggingFilter : IActionFilter
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Create instance of ActionLoggingFilter.
        /// </summary>
        /// <param name="logger">Generic interfaz Microsoft.Extensions.Logging.ILogger for logging.</param>
        public ActionLoggingFilter(ILogger<ActionLoggingFilter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Called after the action executes, before the action result.
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext.</param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // TODO: Do something after the action executes or leave it empty.                  
        }

        /// <summary>
        /// Called before the action executes, after model binding is complete
        /// </summary>
        /// <param name="context">The Microsoft.AspNetCore.Mvc.Filters.ActionExecutedContext.</param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            var controllerName = context.RouteData.Values["controller"];
            var request = context.HttpContext.Request;
            var path = request.Path;
            var method = request.Method;
            var user = context.HttpContext.User;

            // TODO: Do something to log it
            _logger.LogTrace($"Request: ({controllerName}): {method} {path}");
        }
    }
}
