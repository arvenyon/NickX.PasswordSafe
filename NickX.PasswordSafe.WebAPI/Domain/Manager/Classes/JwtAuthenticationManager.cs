using Microsoft.IdentityModel.Tokens;
using NickX.PasswordSafe.WebAPI.Domain.Manager.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace NickX.PasswordSafe.WebAPI.Domain.Manager.Classes
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {
        private readonly Dictionary<string, string> _testUsers = new Dictionary<string, string>()
        {
            { "admin", "asdf" },
            { "user1", "password1" }
        };

        private string _key;

        public JwtAuthenticationManager(string privateKey)
        {
            _key = privateKey;
        }

        public string Authenticate(string username, string password)
        {
            if (!_testUsers.Any(u => u.Key == username && u.Value == password))
                return null;

            var handler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_key);
            var descriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity
                (
                    new Claim[]
                    {
                        new Claim(ClaimTypes.Name, username)
                    }
                ),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials
                (
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature
                )
            };

            var token = handler.CreateToken(descriptor);
            return handler.WriteToken(token);
        } 
    }
}
