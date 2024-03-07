using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Piranha.AspNetCore.Identity.Data;

namespace Inkzen.Api.Auth;

[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[ApiController]
[Route("api/auth")]
public class AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthController> logger, IConfiguration config) : ControllerBase
{
    [HttpGet]
    [Route("getvalue")]
    [Produces("application/json")]
    public ActionResult GetValue()
    {
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        User user = userManager.Users.FirstOrDefault(u => u.Id.ToString() == userId);
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("token")]
    public async Task<ActionResult> Token([FromBody] JwtUser model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

        if (!result.Succeeded) throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        
        logger.LogInformation("Login succeeded");
        User appUser = userManager.Users.SingleOrDefault(r => r.UserName == model.Email);

        logger.LogInformation("Logging in as user {username}.", appUser?.UserName);
        JwtToken token = new()
        {
            AccessToken = GenerateToken(model.Email, appUser)
        };
        return Ok(token);

    }

    private string GenerateToken(string userName, User user)
    {
        Claim[] jwtClaims = [
            new(JwtRegisteredClaimNames.UniqueName, userName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString())
        ];

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JwtSettings:Key"]));

        JwtSecurityToken token = new(issuer: config["JwtSettings:Issuer"],
                                    audience: config["JwtSettings:Audience"],
                                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                                    claims: jwtClaims,
                                    expires: DateTime.Now.AddDays(2));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
