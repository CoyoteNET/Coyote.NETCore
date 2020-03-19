using System;
using System.Collections.Generic;
using System.Text;

namespace CoyoteNET.Shared.Auth
{
    public class JsonWebToken
    {
        public JsonWebToken(string username, string token, long expires)
        {
            Username = username;
            Token = token;
            Expires = expires;
        }

        public string Username { get; set; }

        public string Token { get; set; }

        public long Expires { get; set; }
    }
}
