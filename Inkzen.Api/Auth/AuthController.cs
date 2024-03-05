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
public class AuthController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AuthController> logger, IConfiguration config) : ControllerBase
{
    private readonly ILogger _logger = logger;

    [HttpGet]
    [Route("api/auth/getvalue")]
    [Produces("application/json")]
    public ActionResult GetValue()
    {
        /*var e = User.Identity.Name; var c = _userManager.GetUserAsync(HttpContext.User); var user = _userManager.FindByIdAsync(b);*/
        string userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        var user = userManager.Users.FirstOrDefault(u => u.Id.ToString() == userId);
        return Ok(user);
    }

    [HttpPost]
    [AllowAnonymous]
    [Route("api/auth/token")]
    public async Task<ActionResult> Token([FromBody] JwtUser model)
    {
        var result = await signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            var appUser = userManager.Users.SingleOrDefault(r => r.UserName == model.Email);
            var token_ = GenerateToken(model.Email, appUser);
            return Ok(token_);
        }

        throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
    }

    private string GenerateToken(string userName, User user)
    {
        Claim[] jwtClaims = [
            new(JwtRegisteredClaimNames.UniqueName, userName),
            new(JwtRegisteredClaimNames.Email, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(JwtRegisteredClaimNames.NameId, Guid.NewGuid().ToString())
        ];

        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ea1ce084b55e03c5116b186d11a76939"));

        JwtSecurityToken token = new(issuer: "https://localhost:5001",
                                    audience: "https://localhost:5001",
                                    signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256),
                                    claims: jwtClaims,
                                    expires: DateTime.Now.AddDays(2));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
