using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Model;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.IdentityModel.Tokens.Jwt;

namespace WebApplication2.Controllers
{
    /// <summary>
    /// Controller to get token
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly string secrectKey;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="config"></param>
        public AuthController(IConfiguration config)
        {
            secrectKey = config.GetSection("settings").GetValue<string>("secretKey").ToString();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Validate([FromBody] User user)
        {
            if (user?.UserName != null)
            {
                byte[] keyBytes = Encoding.ASCII.GetBytes(secrectKey);
                var claims = new ClaimsIdentity();
                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(20),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescription);

                string tokenCreate = tokenHandler.WriteToken(tokenConfig);
                return new OkObjectResult($"Bearer {tokenCreate}");
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
