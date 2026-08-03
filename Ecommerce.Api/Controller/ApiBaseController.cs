using Ecommerce.Application.common.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiBaseController : ControllerBase
    {
        public static ObjectResult ToProblem(Error error)
        {
            var first=error;
            var status = first.Type switch
            {
                ErrorType.NotFound => StatusCodes.Status404NotFound,
                ErrorType.Validation => StatusCodes.Status400BadRequest,
                ErrorType.Conflict => StatusCodes.Status409Conflict,
                ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
                ErrorType.Forbidden => StatusCodes.Status403Forbidden,
                _ => StatusCodes.Status500InternalServerError
            };
            var Problem = new ProblemDetails
            {
                Status = status,
                Title=first.Code,
                Detail=first.Message,
                Extensions = { ["errors"]=error}
            };

            return new ObjectResult(Problem) { StatusCode=status}; 
        }
        protected static ActionResult<T> ToActionResult<T>(Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            return ToProblem(result.Error);
        }
        protected static ActionResult<T> ToActionResult<T>(Result result)
        {
            if (result.IsSuccess)
            {
                return new OkResult();
            }

            return ToProblem(result.Error);
        }
    }
}
