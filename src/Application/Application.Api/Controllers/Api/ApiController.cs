using Application.Service.Queries;
using Application.Service.SeedWork;
using Common.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Api.Controllers.Api
{
    [Route("nei/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly Messages _messages;

        public ApiController(Messages messages, IConfiguration configuration)
        {
            _configuration = configuration;
            _messages = messages;
        }

        [HttpPost]
        [Route("login")]
        public IActionResult LoginJWT([FromBody] ApiLoginVM login)
        {
            IActionResult result = Unauthorized();
            var user = AuthenticateUser(login);
            if (!string.IsNullOrEmpty(user.Role))
            {
                var tokenString = GenerateJSONWebToken(user);
                result = Ok(tokenString);
            }
            return result;
        }

        private string GenerateJSONWebToken(ApiLoginVM login)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.UserName),
                new Claim("DateRegister", login.DateRegister.ToShortDateString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            claims.Add(new Claim(ClaimTypes.Role, login.Role));

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Issuer"],
                claims.ToArray(),
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private ApiLoginVM AuthenticateUser(ApiLoginVM login)
        {
            var result = _messages.Dispatch(new GetLoginApi(login));
            if (!string.IsNullOrEmpty(result.Role))
                return new ApiLoginVM() { UserName = login.UserName, DateRegister = DateTime.Now, Role = result.Role };
            else
                return new ApiLoginVM();
        }
    }
}