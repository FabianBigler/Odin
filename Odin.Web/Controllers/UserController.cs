using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Odin.Core.Interfaces;
using Odin.Core.Interfaces.Services;
using Odin.Core.Model;
using Odin.Web.Model;

namespace Odin.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserLoginService userLoginService;
        private readonly IUserSignUpService userSignUpService;
        private readonly ILogger<UserController> logger;

        public UserController(IUserLoginService userLoginService, IUserSignUpService userSignUpService, ILogger<UserController> logger)
        {
            this.userLoginService = userLoginService;
            this.userSignUpService = userSignUpService;
            this.logger = logger;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        
        //[HttpPost]
        [HttpPost("[action]")]
        public async Task<IActionResult> Login([FromBody] UserLoginArgs userLoginArgs)
        {
            try
            {
                var result = await userLoginService.Login(userLoginArgs.UserName, userLoginArgs.Password);
                bool success = false;
                string message = string.Empty;
                switch(result.State)
                {
                    case UserLoginState.Success:
                        message = "Anmeldung erfolgreich!";
                        success = true;
                        break;
                    case UserLoginState.UserNotFound:
                        message = "Benutzer nicht gefunden.";
                        success = false;
                        break;
                    case UserLoginState.WrongPassword:
                        message = "Falsches Passwort eingegeben.";
                        success = false;
                        break;
                }
                
                return new JsonResult(new { success, message });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, null);
                return Json(new { success = false, message = ex.Message });
            }            
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] UserSignUpArgs userSignUpArgs)
        {
            try
            {
                var result = await userSignUpService.SignUp(userSignUpArgs.UserName, userSignUpArgs.Email, userSignUpArgs.Password);
                string message = string.Empty;
                switch(result.State)
                {
                    case SignUpState.EmailNotValid:
                        message = "E-Mail ist nicht gültig.";
                        break;
                    case SignUpState.Success:
                        message = "Registrierung erfolgreich.";
                        break; 
                    case SignUpState.UserAlreadyExists:
                        message = "Benutzer existiert bereits.";
                        break;
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, null);
                return Json(new { success = false, message = ex.Message });
            }        
        }
    }
}
