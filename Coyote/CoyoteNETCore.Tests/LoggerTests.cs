using Coyote.NETCore;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace CoyoteNETCore.Tests
{
    public class LoggerTests
    {
        [Fact]
        public void RedactingPassword()
        {
            var uri = new PathString("/Account/Login");
            var payload = JsonConvert.SerializeObject(new { Username = "test6", Password = "password", Email = "test7@test.pl" });
            Assert.Contains("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("password", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload)); 

            payload = JsonConvert.SerializeObject(new { Username = "test6", Password = "asdf", Email = "test7@test.pl" });
            Assert.Contains("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("asdf", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload)); 

            payload = "{\"Password\":\"qwe\"}}";
            Assert.Contains("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = "{\"Password\":\"qwe}}";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.Contains("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = "";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = null;
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
        }

        [Fact]
        public void RedactingPassword2()
        {
            var uri = new PathString("/Account/Register");

            var payload = JsonConvert.SerializeObject(new { Username = "test6", Password = "password", Email = "test7@test.pl" });
            Assert.Contains("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("password", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload)); 

            payload = JsonConvert.SerializeObject(new { Username = "test6", Password = "asdf", Email = "test7@test.pl" });
            Assert.Contains("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("asdf", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload)); 

            payload = "{\"Password\":\"qwe\"}}";
            Assert.Contains("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = "{\"Password\":\"qwe}}";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.Contains("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = "";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = null;
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
        }

        [Fact]
        public void NotRedactingPassword()
        {
            var uri = new PathString("/Account/Asd");

            var payload = JsonConvert.SerializeObject(new { Username = "test6", Password = "password", Email = "test7@test.pl" });
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.Contains("password", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload)); 

            payload = JsonConvert.SerializeObject(new { Username = "test6", Password = "asdf", Email = "test7@test.pl" });
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.Contains("asdf", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload)); 

            payload = "{\"Password\":\"qwe\"}}";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.Contains("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = "{\"Password\":\"qwe}}";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.Contains("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = "";
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));

            payload = null;
            Assert.DoesNotContain("redacted", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
            Assert.DoesNotContain("qwe", RequestResponseLoggingMiddleware.PreventPasswordsFromBeingLogged(uri, payload));
        }
    }
}
