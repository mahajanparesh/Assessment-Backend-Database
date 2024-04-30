using InsuranceAPIApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPIApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private readonly AngularDbContext _context;

        public LoginController(AngularDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid model");

            var user = await _context.UserAuths.FirstOrDefaultAsync(u => u.Email == model.Email);

            if (user == null)
                return Ok(new LoginResult { EmailExists = false, PasswordCorrect = false, UserID =  -1});

            if (user.Password != model.Password)
                return Ok(new LoginResult { EmailExists = true, PasswordCorrect = false, UserID = -1 });

            return Ok(new LoginResult { EmailExists = true, PasswordCorrect = true, UserID = user.Userid });
        }
        public class LoginModel
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class LoginResult
        {
            public bool EmailExists { get; set; }
            public bool PasswordCorrect { get; set; }

            public int UserID { get; set; }
        }
    }
}
