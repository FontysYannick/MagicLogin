using Magic_DAL.Context;
using Magic_Logic.Classes;
using Magic_Logic.Container;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Magic.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [EnableCors]
    public class LoginController : ControllerBase
    {
        private Login_Container pc;
        private readonly IConfiguration configuration;

        public LoginController(IConfiguration ic)
        {
            configuration = ic;

            pc = new(new Login_Context(configuration["db:ConnectionString"]));

        }


        [HttpGet]
        [Route("api/[controller]")]
        public string JsonConverter()
        {
            return "q";
        }

        [HttpPost]
        [Route("api/Register")]
        public IActionResult CreateUser(Login login)
        {
            try
            {
                pc.register(login);
                return Ok(login);
            }
            catch
            {
                return Unauthorized();
            }
        }
    }
}
