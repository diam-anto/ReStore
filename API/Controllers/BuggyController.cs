using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    {
        [HttpGet("not-found")]
        public ActionResult GetNotFound() 
        {
            //404
            return NotFound();
        }

         [HttpGet("bad-request")]
        public ActionResult GetBadRequest() 
        {
            //400
            //return BadRequest("This is a bad request");
            //ProblemDetails to specify any problem status
            return BadRequest(new ProblemDetails{Title = "This is a bad request"});
        }

        [HttpGet("unauthorised")]
        public ActionResult GetUnauthorised() 
        {
            //401
            return Unauthorized();
        }

        [HttpGet("validation-error")]
        public ActionResult GetValidationError() 
        {
            //Vaqlidation error
            ModelState.AddModelError("Problem1", "this is the first error");
            ModelState.AddModelError("Problem2", "this is the second error");
            
            return ValidationProblem();
        }

         [HttpGet("server-error")]
        public ActionResult GetServerError() 
        {
            throw new Exception("this is a server error");
        }
        
    }
}