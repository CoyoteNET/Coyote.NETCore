using System.Net.Http;
using System.Threading.Tasks;
using Coyote.NETCore;
using Newtonsoft.Json;
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
            var user = new {Password = "test1234test", Email = "test@test.pl" };

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            stringResponse.ShouldContain("Name is required.");
        }

        [Fact]
        public async Task CreateUser_PasswordIsEmpty_UnsuccessfullyCreated()
        {
            var user = new { Name = "test", Email = "test@test.pl" };

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());
            
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            stringResponse.ShouldContain("Password is required.");
        }

        [Fact]
        public async Task CreateUser_EmailIsEmpty_UnsuccessfullyCreated()
        {
            var user = new { Name = "test", Password = "test1234test"};

            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            stringResponse.ShouldContain("Email is required.");
        }
        

        [Fact]
        public async Task CreateUser_SuccessfullyCreated()
        {
            var user = new { Name = "test", Password = "test1234test", Email = "test@test.pl" };
            
            var httpResponse = await _client.PostAsync("/Account/Register", user.AsJsonString());
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(stringResponse);

            result.Success.ShouldBe(true);
            result.Result.ShouldBe("User has been registered");
        }

        [Fact]
        public async Task CreateUser_NameExists_UnsuccessfullyCreated()
        {
            var firstUser = new { Name = "test2", Password = "test1234test", Email = "test2@test.pl" };
            var secondUser = new { Name = "test2", Password = "test1234test", Email = "test3@test.pl" };
            
            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(stringResponse);

            result.Success.ShouldBe(false);
            result.Result.ShouldBe("An account with the given username already exists.");
        }

        [Fact]
        public async Task CreateUser_EmailExists_UnsuccessfullyCreated()
        {
            var firstUser = new { Name = "test3", Password = "test1234test", Email = "test4@test.pl" };
            var secondUser = new { Name = "test4", Password = "test1234test", Email = "test4@test.pl" };

            (await _client.PostAsync("/Account/Register", firstUser.AsJsonString())).EnsureSuccessStatusCode();
            var httpResponse = await _client.PostAsync("/Account/Register", secondUser.AsJsonString());
            httpResponse.EnsureSuccessStatusCode();

            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Response>(stringResponse);

            result.Success.ShouldBe(false);
            result.Result.ShouldBe("The e-mail address provided is already used.");
        }
    }

    public class Response
    {
        public bool Success { get; set; }
        public string Result { get; set; }
    }
}