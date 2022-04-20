using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WorkManagement.Services;
using WorkManagement.ViewModels;

namespace WorkManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthenticate _authenticate;

        public AccountController(IConfiguration configuration, IAuthenticate authenticate)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            _authenticate = authenticate ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpPost("CreateUser")]
        public async Task<ActionResult<UserToken>> CreateUser([FromBody] RegisterModel model)
        {
            if (model.Password != model.ConfirmPassword)
            {
                ModelState.AddModelError("Confirm Password", "As senhas não conferem");
                return BadRequest(ModelState);
            }

            var result = await _authenticate.RegisterUser(model.LoginCNPJ, model.Password);
            if (result)
            {
                return Ok($"Usuario {model.LoginCNPJ} criado");
            }
            else
            {
                ModelState.AddModelError("CreateUser", "Registro invalido");
                return BadRequest(ModelState);
            }
        }

        [HttpPost("LoginUser")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel user)
        {
            var result = await _authenticate.Authenticate(user.LoginCNPJ, user.Password);
            if (result)
            {
                return GenerateToken(user);
            }
            else
            {
                ModelState.AddModelError("LoginUser", "Login inválido");
                return BadRequest(ModelState);
            }
        }

        private ActionResult<UserToken> GenerateToken(LoginModel user)
        {
            var claims = new[]
            {
                new Claim("login", user.LoginCNPJ),
                new Claim("meuToken", "tokendatamires"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(20);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: expiration,
                signingCredentials: creds
                );
            return new UserToken()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration,
            };
        }
    }
}
