using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Coyote.NETCore;
using CoyoteNETCore.Tests.Infrastructure;
using Shouldly;
using Xunit;

namespace CoyoteNETCore.Tests
{
    public class RegisterTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly HttpClient _client;

        public RegisterTests(CustomWebApplicationFactory<Startup> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task CreateUser_NameIsEmpty_UnsuccessfullyCreated()
        {
            var user = new { Password = "test1234test", Email = "test@test.pl" };

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.False(httpResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateUser_PasswordIsEmpty_UnsuccessfullyCreated()
        {
            var user = new { Username = "test", Email = "test@test.pl" };

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            Assert.False(httpResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateUser_EmailIsEmpty_UnsuccessfullyCreated()
        {
            var user = new { Username = "test", Password = "test1234test" };

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            Assert.False(httpResponse.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateUser_SuccessfullyCreated()
        {
            var user = new { Username = "test", Password = "test1234test", Email = "test@test.pl" };

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateUser_NameExists_UnsuccessfullyCreated()
        {
            var firstUser = new { Username = "test2", Password = "test1234test", Email = "test2@test.pl" };
            var secondUser = new { Username = "test2", Password = "test1234test", Email = "test3@test.pl" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_EmailExists_UnsuccessfullyCreated()
        {
            var firstUser = new { Username = "test3", Password = "test1234test", Email = "test4@test.pl" };
            var secondUser = new { Username = "test4", Password = "test1234test", Email = "test4@test.pl" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_NameExistsIgnoreCase_UnsuccessfullyCreated()
        {
            var firstUser = new { Username = "test5", Password = "test1234test", Email = "test5@test.pl" };
            var secondUser = new { Username = "tESt5", Password = "test1234test", Email = "test6@test.pl" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_EmailExistsIgnoreCase_UnsuccessfullyCreated()
        {
            var firstUser = new { Username = "test6", Password = "test1234test", Email = "test7@test.pl" };
            var secondUser = new { Username = "test7", Password = "test1234test", Email = "tESt7@test.pl" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.StatusCode.ShouldBe(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task CreateUser_NameContainsExisting_SuccessfullyCreated()
        {
            var firstUser = new { Username = "test8", Password = "test1234test", Email = "test8@test.pl" };
            var secondUser = new { Username = "Xtest8X", Password = "test1234test", Email = "test9@test.pl" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();

            httpResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task CreateUser_EmailContainsExisting_SuccessfullyCreated()
        {
            var firstUser = new { Username = "test9", Password = "test1234test", Email = "test10@test.pl" };
            var secondUser = new { Username = "test10", Password = "test1234test", Email = "Xtest10@test.plX" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}