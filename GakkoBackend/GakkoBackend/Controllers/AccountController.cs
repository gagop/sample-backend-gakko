using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GakkoBackend.Application.Account;
using GakkoBackend.Application.Account.Commands.AddRefreshToken;
using GakkoBackend.Application.Account.Queries.CheckRefreshToken;
using GakkoBackend.Application.DTOs;
using GakkoBackend.Shared.Constants;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GakkoBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        public IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration) : base()
        {
            Configuration = configuration;
        }

        [HttpPost("refresh-token")]
        [AllowAnonymous]
        public async Task<IActionResult> RefreshToken(TokensPair tokens)
        {
            //Checking if someone is using correct JWT token - but only expired.
            ClaimsPrincipal principal = null;
            try
            {
                principal = GetPrincipalFromExpiredToken(tokens.Token);
            }
            catch (Exception)
            {
                return Unauthorized("Incorrect JWT token");
            }


            var res = await Mediator.Send(new RefreshTokenQuery { Name = principal.Identity.Name, RefreshToken = tokens.RefreshToken });

            if (res == null)
                return NotFound("Refresh token was not found");

            if (Helpers.IsRefreshTokenExpired(res.User.RefreshTokenExpDate))
                return BadRequest("Refresh token has expired");

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
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Role, GlobalConsts.USER_ROLE));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, response.User.IdPerson.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, response.User.Name));
            claims.Add(new Claim(ClaimTypes.Surname, response.User.Surname));

            var token = new JwtSecurityToken(
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
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false, 
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"])),
                ValidateLifetime = false 
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid token");

            return principal;
        }

        #endregion
    }
}