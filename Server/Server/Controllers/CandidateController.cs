using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;


namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidateController : ControllerBase
    {
        // TODO: add services for candidate management and skill management
        public CandidateController()
        {
            // TODO: add dependency injection for services
        }

        [HttpPost]
        public ActionResult<string> AddCandidate()
        {
            // TODO: implement
            return StatusCode(500, new { message = "Not implemented yet" });
        }

        [HttpPatch("add-skill")]
        public ActionResult<string> AddSkillToCandidate()
        {
            // TODO: implement
            return StatusCode(500, new { message = "Not implemented yet" });
        }

        [HttpPatch("remove-skill")]
        public ActionResult<string> RemoveSkillFromCandidate()
        {
            // TODO: implement
            return StatusCode(500, new { message = "Not implemented yet" });
        }

        [HttpGet]
        public ActionResult<string> GetCandidate()
        {
            // TODO: implement
            return StatusCode(500, new { message = "Not implemented yet" });
        }
    }
}
