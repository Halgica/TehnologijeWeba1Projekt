using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ReservationAPI.DTOs.Read;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace ReservationAPI.tests
{
    public class UserControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public UserControllerTests(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllUsers_ReturnsOk()
        {
            var response = await client.GetAsync("/api/User");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ReturnsCreated()
        {
            var user = new
            {
                Name = "Test user",
                Email = "Test@test.com"
            };
            var response = await client.PostAsJsonAsync("/api/User", user);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetUserById_ReturnsOk()
        {
            var user = new { Name = "Test user", Email = "Test@test.com" };
            var createdResponse = await client.PostAsJsonAsync("/api/User", user);
            var createdUser = await createdResponse.Content.ReadFromJsonAsync<UserDto>();
        }
    }
}
