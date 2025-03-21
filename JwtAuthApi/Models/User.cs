using System;

namespace JwtAuthApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Role { get; set; }
    }

    public class UserLogin
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    public class UserToken
    {
        public string? Token { get; set; }
        public DateTime Expiration { get; set; }
    }
} 