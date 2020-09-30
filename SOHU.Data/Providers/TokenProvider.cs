using Microsoft.IdentityModel.Tokens;
using SOHU.Data.Helpers;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SOHU.Data.Providers
{
    public class TokenProvider
    {
        public static string GenerateTokenString(Dictionary<string, string> claims)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(AppGlobal.TokenSecretKey);
            var tokenClaims = new List<Claim>();
            foreach(var claim in claims)
            {
                var tokenClaim = new Claim(claim.Key, claim.Value);
            }    
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(tokenClaims.ToArray()),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
