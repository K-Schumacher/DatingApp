using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BugController : BaseApiController
    {
        private readonly DataContext _context;
        public BugController(DataContext context)
        {
            _context = context;
        }

    // Test 401 Unauthorized response
    [Authorize]
    [HttpGet("auth")]
    public ActionResult<string> GetSecret()
    {
        return "secret text";
    }

    // Generate Not Found error
    [HttpGet("not-found")]
    public ActionResult<AppUser> GetNotFound()
    {
        // Search for something that we know doesn't exist
        var thing = _context.Users.Find(-1);

        if (thing == null) return NotFound();

        return Ok(thing);
    }

    // Generate server error
    [HttpGet("server-error")]
    public ActionResult<string> GetServerError()
    {
        var thing = _context.Users.Find(-1);

        // Used to generate null reference exception
        var thingToReturn = thing.ToString();

        return thingToReturn;
    }

    [HttpGet("bad-request")]
    public ActionResult<string> GetBadRequest()
    {
        return BadRequest("This was a bad request");
    }

    }
}