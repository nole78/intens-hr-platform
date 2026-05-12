using Microsoft.AspNetCore.Mvc;

namespace Server.Common
{
    public static class ResultExstensions
    {
        public static ActionResult ToActionResult(this Result result)
        {
            if(result.IsSuccess)
            {
                return new OkObjectResult();
            }

            return result.ErrorType switch
            {
                ErrorType.Validation => new BadRequestObjectResult(new { message = result.Error }),
                ErrorType.NotFound => new NotFoundObjectResult(new { message = result.Error }),
                ErrorType.Conflict => new ConflictObjectResult(new { message = result.Error }),
                _ => new ObjectResult(new { message = result.Error }) { StatusCode = 500 }
            };
        }

        public static ActionResult ToActionResult<T>(this Result<T> result)
        {
            if (result.IsSuccess)
            {
                return new OkObjectResult(result.Value);
            }

            return result.ErrorType switch
            {
                ErrorType.Validation => new BadRequestObjectResult(new { message = result.Error }),
                ErrorType.NotFound => new NotFoundObjectResult(new { message = result.Error }),
                _ => new ObjectResult(new { message = result.Error }) { StatusCode = 500 }
            };
        }
    }
}
