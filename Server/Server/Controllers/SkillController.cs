using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SkillController : ControllerBase
    {
        // TODO: add services for skill management
        public SkillController()
        {
            // TODO: add dependency injection for services
        }

        [HttpPost]
        public ActionResult<string> AddSkill()
        {
            // TODO: implement
            return StatusCode(500, new { message = "Not implemented yet" });
        }
    }
}
