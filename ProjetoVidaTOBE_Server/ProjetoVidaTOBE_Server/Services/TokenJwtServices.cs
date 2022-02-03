using Microsoft.IdentityModel.Tokens;
using ProjetoVidaTOBE_Server.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoVidaTOBE_Server.Services
{
    public class TokenJwtServices
    {
        public static string GerarToken(Usuario usuario)
        {
            var claims = new[]
           {
                new Claim(JwtRegisteredClaimNames.Sub, usuario.Email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("api-tobe-projeto-vida-validacao-autenticacao"));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: "TobeProjetoVida",
                    audience: "projetoVida",
                    claims: claims,
                    signingCredentials: credentials,
                    expires: DateTime.Now.AddHours(2)
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
