using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Web.Http.ModelBinding;
using System.Linq;
using System.Net.Http.Formatting;

namespace CustomizedApi.Api.Filters
{
    public class ModelStateFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (!actionContext.ModelState.IsValid)
            {
                var response = actionContext.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
                response.Content =
                    new ObjectContent<ApiResourceValidationErrorWrapper>(
                        new ApiResourceValidationErrorWrapper(actionContext.ModelState),
                        new JsonMediaTypeFormatter());
                actionContext.Response = response;
            }
        }
    }

    public class ApiResourceValidationErrorWrapper
    {
        private const string MissingPropertyError = "Undefined error.";

        public string Message { get; private set; }

        public IDictionary<string, IEnumerable<string>> Errors { get; private set; }

        public ApiResourceValidationErrorWrapper(ModelStateDictionary modelState)
        {
            SerializeModelState(modelState);
        }

        private void SerializeModelState(ModelStateDictionary modelState)
        {
            Errors = new Dictionary<string, IEnumerable<string>>();

            modelState.ToList().ForEach(state =>
            {
                var key = state.Key;

                var errors = state.Value.Errors;

                if (errors != null && errors.Count > 0)
                {
                    IEnumerable<string> errorMessages = errors.Select(
                        error => string.IsNullOrEmpty(error.ErrorMessage)
                                     ? MissingPropertyError
                                     : error.ErrorMessage).ToArray();

                    Errors.Add(key, errorMessages);
                }
            });
        }
    }
}