using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserApi.Models;

namespace UserApi.Services
{
    public class TokenService
    {
        internal Token CreateToken(IdentityUserToBe user)
        {
            Claim[] userClaims = new Claim[]
            {
                new Claim("email", user.Email),
                new Claim("id", user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("D4AFC1953ACF1A613D03EE4093483CB01DF130F93060B869CB937FF143B9BD48"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(claims: userClaims, signingCredentials: credentials, expires: DateTime.UtcNow.AddHours(3));
            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            return new Token(tokenString);
        }
    }
}