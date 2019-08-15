using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNETCore.Shared.RequestInput
{
    public class LoginInput
    {
        public LoginInput(string name, string password)
        {
            Name = name;
            Password = password;
        }

        public string Name { get; set; }

        public string Password { get; set; }
    }
}
