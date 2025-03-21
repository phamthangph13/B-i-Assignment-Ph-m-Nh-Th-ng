using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtAuthApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthApi.Services
{
    public class JwtService
    {
        private readonly IConfiguration _configuration;

        public JwtService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public UserToken GenerateToken(User user)
        {
            // Get JWT settings from configuration
            var jwtKey = _configuration["Jwt:Key"];
            var jwtIssuer = _configuration["Jwt:Issuer"];
            var jwtAudience = _configuration["Jwt:Audience"];
            
            if (string.IsNullOrEmpty(jwtKey) || string.IsNullOrEmpty(jwtIssuer) || string.IsNullOrEmpty(jwtAudience))
            {
                throw new InvalidOperationException("JWT configuration is missing");
            }

            // Set token expiration (1 hour)
            var expiration = DateTime.UtcNow.AddHours(1);
            
            // Create claims for the token
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Username ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Role, user.Role ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            
            // Create symmetric security key from the secret key
            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
            
            // Create signing credentials
            var signingCredentials = new SigningCredentials(symmetricKey, SecurityAlgorithms.HmacSha256);
            
            // Create JWT token
            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: expiration,
                signingCredentials: signingCredentials
            );
            
            // Return token and expiration
            return new UserToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }
} 