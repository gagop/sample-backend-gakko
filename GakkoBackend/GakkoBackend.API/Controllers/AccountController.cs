using GakkoBackend.Application.Account;
using GakkoBackend.Application.Account.Commands.AddRefreshToken;
using GakkoBackend.Application.Account.Commands.RegisterEmployee;
using GakkoBackend.Application.Account.Queries.CheckRefreshToken;
using GakkoBackend.Application.Account.Queries.LoginEmployee;
using GakkoBackend.Application.DTOs;
using GakkoBackend.Shared.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace GakkoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        #region Class configs

        public IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        #endregion

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginEmployeeQuery loginCredentials)
        {
            AddRefreshTokenCommand user = await Mediator.Send(loginCredentials);

            if (user == null)
            {
                return NotFound("User does not exist");
            }

            return Ok(await GenerateTokensPair(user));
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterEmployeeCommand registerUser)
        {
            bool res = await Mediator.Send(registerUser);

            if (!res)
            {
                return BadRequest("User with this email already exists");
            }

            return NoContent();
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(TokensPair tokens)
        {
            try
            {
                GetPrincipalFromExpiredToken(tokens.Token);
            }
            catch (Exception)
            {
                return Unauthorized("Incorrect JWT token");
            }

            AddRefreshTokenCommand res = await Mediator.Send(new RefreshTokenQuery { IdPerson = GetIdPerson(), RefreshToken = tokens.RefreshToken });

            if (res == null)
            {
                return NotFound("Refresh token was not found");
            }

            if (Helpers.IsRefreshTokenExpired(res.Employee.RefreshTokenExpDate ?? DateTime.Now))
            {
                return BadRequest("Refresh token has expired");
            }

            return Ok(GenerateTokensPair(res));
        }

        #region Private methods
        private async Task<TokensPair> GenerateTokensPair(AddRefreshTokenCommand response)
        {
            return new TokensPair
            {
                Token = new JwtSecurityTokenHandler().WriteToken(CreateToken(response)),
                RefreshToken = await CreateRefreshToken(response)
            };
        }
        private JwtSecurityToken CreateToken(AddRefreshTokenCommand response)
        {
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Role, GlobalConsts.USER_ROLE),
                new Claim(ClaimTypes.NameIdentifier, response.Employee.IdEmployee.ToString()),
                new Claim(ClaimTypes.Name, response.Person.Name),
                new Claim(ClaimTypes.Surname, response.Person.Surname)
            };

            JwtSecurityToken token = new JwtSecurityToken(
                "",
                "",
                claims,
                expires: DateTime.UtcNow.AddMinutes(GlobalConsts.TOKEN_EXP_TIME_IN_MINS),
                signingCredentials: creds
            );

            return token;
        }

        private async Task<string> CreateRefreshToken(AddRefreshTokenCommand response)
        {
            return await Mediator.Send(response);
        }

        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"])),
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        #endregion
    }
}