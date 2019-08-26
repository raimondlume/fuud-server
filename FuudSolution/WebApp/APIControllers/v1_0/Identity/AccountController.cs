using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Identity;
using me.raimondlu.Identity;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WebApp.Areas.Identity.Pages.Account;

namespace WebApp.APIControllers.Identity
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly IConfiguration _configuration;

        public AccountController(SignInManager<AppUser> signInManager, IConfiguration configuration,
            UserManager<AppUser> userManager, ILogger<RegisterModel> logger, IEmailSender emailSender)
        {
            _signInManager = signInManager;
            _configuration = configuration;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<string>> Login([FromBody] LoginDTO model)
        {
            var appUser = await _userManager.FindByEmailAsync(model.Email);

            if (appUser == null)
            {
                // user is not found, return 403
                _logger.LogInformation("User not found.");
                return StatusCode(403);
            }

            // do not log user in, just check that the password is ok
            var result = await _signInManager.CheckPasswordSignInAsync(appUser, model.Password, false);

            if (result.Succeeded)
            {
                // create claims based user 
                var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);
                
                var claims = new List<Claim>
                {
                    new Claim("UserID", appUser.Id.ToString()),
                    new Claim("FirstName", appUser.FirstName),
                    new Claim("LastName", appUser.LastName)
                };
                var appIdentity = new ClaimsIdentity(claims);

                claimsPrincipal.AddIdentity(appIdentity);
                
                // get the Json Web Token
                var jwt = JwtHelper.GenerateJwt(
                    claimsPrincipal.Claims,
                    Environment.GetEnvironmentVariable("JWT_ENCRYPTION_KEY"),
                    _configuration["JWT:Issuer"],
                    int.Parse(_configuration["JWT:ExpireDays"]));
                _logger.LogInformation("Token generated for user");
                return Ok(new {token = jwt});
            }

            return StatusCode(403);
        }

        [HttpPost]
        [EnableCors]
        public async Task<ActionResult<string>> Register([FromBody] RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                var appUser = new AppUser
                {
                    UserName = model.Email, 
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = await _userManager.CreateAsync(appUser, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("New user created.");
                    
                    // create claims based user 
                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(appUser);

                    // get the Json Web Token
                    var jwt = JwtHelper.GenerateJwt(
                        claimsPrincipal.Claims,
                        Environment.GetEnvironmentVariable("JWT_ENCRYPTION_KEY"),
                        _configuration["JWT:Issuer"],
                        int.Parse(_configuration["JWT:ExpireDays"]));
                    _logger.LogInformation("Token generated for user");
                    return Ok(new {token = jwt});
                }

                return StatusCode(406); //406 Not Acceptable
            }

            return StatusCode(400); //400 Bad Request
        }


        public class LoginDTO
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class RegisterDTO
        {
            public string Email { get; set; }

            public string FirstName { get; set; }
            
            public string LastName { get; set; }

            [Required] [MinLength(6)] public string Password { get; set; }
        }
    }
}