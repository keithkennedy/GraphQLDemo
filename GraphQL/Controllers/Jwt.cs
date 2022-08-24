using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSGraphQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Jwt : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public Jwt(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        [Route("standard")]
        public string Standard()
        {
            return CreateToken("bilbo.baggins", "Standard");
        }

        [HttpGet]
        [Route("admin")]
        public string Admin()
        {
            return CreateToken("keith.kennedy", "Admin");
        }

        private string CreateToken(string name, string role)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, name),
                new Claim(ClaimTypes.Role, role)
            };

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["AppSettings:JwtKey"]));

            var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                "issuer",
                "audience",
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: signingCredentials);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }
    }
}
