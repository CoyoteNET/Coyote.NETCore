using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.RequestInput
{
    public class LoginInput
    {
        private LoginInput()
        {

        }

        public LoginInput(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; set; }

        public string Password { get; set; }
    }
}
