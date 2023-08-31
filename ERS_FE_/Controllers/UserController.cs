using ERS_FE_.Contracts.User;
using ERS_FE_.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace ERS_FE_.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISupabaseService _supabaseService;
        public UserController(ISupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }
        
        [HttpPost]
        [Route("signup-user")]
        public async Task<ActionResult<string>> CreateUser([FromBody] SignupUserRequest credential)
        {
            Console.WriteLine("test {0}", credential);
            try
            {
                var client = await _supabaseService.Client.Auth.SignUp(credential.Email, credential.Password);

                if (client != null)
                {
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message.ToString());

                return BadRequest(ex.Message.ToString());
            }
            return BadRequest(string.Empty);

        }

        [HttpPost]
        [Route("login-user")]
        public async Task<ActionResult<string>> SignInUser([FromBody] SignupUserRequest credential)
        {
           
            try
            {
                var client = await _supabaseService.Client.Auth.SignIn(credential.Email, credential.Password);



                if (client != null)
                {
                    HttpContext.Session.SetString("Token", client.AccessToken);
                    return Ok(client);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message.ToString());

                return BadRequest(ex.Message.ToString());
            }
            return BadRequest(string.Empty);
        }

        [HttpGet]
        [Route("get-session")]
        public async Task<ActionResult<string>> RetrieveSession()
        {

            try
            {
                var token = HttpContext.Session.GetString("Token");
                var client = await _supabaseService.Client.Auth.GetUser(token);


                return Ok("test");

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: {0}", ex.Message.ToString());

                return BadRequest(ex.Message.ToString());
            };
        }

        [HttpGet]
        [Authorize]
        [Route("test")]
        public async Task<ActionResult<string>> Test()
        {
            //var userId = context.HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var client = _supabaseService.Client.Auth.CurrentUser;
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            return Ok(userId);
        }

        [HttpGet]
        [Authorize]
        [Route("sign-out")]
        public async Task<ActionResult<string>> SignOut()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            var client =  _supabaseService.Client.Auth.CurrentSession;
            try
            {
                await _supabaseService.Client.Auth.SignOut();
                return Ok("Log-out");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return BadRequest(string.Empty);
            }
        }
    }
}
