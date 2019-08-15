using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.RequestInput
{
    public class RegisterInput
    {
        public RegisterInput(string username, string email, string password)
        {
            Username = username;
            Email = email;
            Password = password;
        }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}
