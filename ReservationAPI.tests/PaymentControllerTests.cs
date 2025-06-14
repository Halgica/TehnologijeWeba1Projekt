using DAL.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using ReservationAPI.DTOs;
using ReservationAPI.DTOs.Read;
using ReservationAPI.DTOs.Write;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace ReservationAPI.Tests.Controllers
{
    public class PaymentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly string _adminToken;
        private readonly string _userToken;

        public PaymentControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();

            // Get test tokens (you'll need to implement this based on your auth setup)
            _adminToken = GetTestToken("admin", "admin", "Admin");
            _userToken = GetTestToken("user", "user", "User");
        }

        private string GetTestToken(string email, string password, string role)
        {
            // Implement this method to get a valid JWT token for testing
            // This should call your actual authentication endpoint
            // For example:
            var loginResponse = _client.PostAsJsonAsync("/api/Auth/login", new
            {
                Email = email,
                Password = password,
                RoleId = role == "Admin" ? 1 : 2 // Assuming role IDs
            }).Result;

            if (loginResponse.IsSuccessStatusCode)
            {
                return loginResponse.Content.ReadFromJsonAsync<AuthResponse>().Result.Token;
            }
            return null;
        }

        [Fact]
        public async Task CreatePayment_ReturnsCreated()
        {
            var payment = new PaymentCreateUpdateDto
            {
                UserId = 4,
                Type = PaymentType.Card
            };

            var response = await _client.PostAsJsonAsync("/api/Payment", payment);

            // Add this debug output:
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Status: {response.StatusCode}");
            Console.WriteLine($"Response: {responseContent}");

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPayments_ReturnsUnauthorized_WhenNotAuthenticated()
        {
            var response = await _client.GetAsync("/api/Payment");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPayments_ReturnsForbidden_WhenNotAdmin()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _userToken);
            var response = await _client.GetAsync("/api/Payment");
            Assert.Equal(HttpStatusCode.Forbidden, response.StatusCode);
        }

        [Fact]
        public async Task GetAllPayments_ReturnsOk_WhenAdmin()
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _adminToken);
            var response = await _client.GetAsync("/api/Payment");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetPaymentById_ReturnsOk_IfExists()
        {
            // Create a payment first
            var newPayment = new PaymentCreateUpdateDto
            {
                UserId = 4,
                Type = PaymentType.PayPal
            };

            var createResponse = await _client.PostAsJsonAsync("/api/Payment", newPayment);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var location = createResponse.Headers.Location?.ToString();
            Assert.NotNull(location);

            var id = int.Parse(location.Split('/').Last());

            // Test getting the payment
            var getResponse = await _client.GetAsync($"/api/Payment/{id}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [Fact]
        public async Task UpdatePayment_ReturnsOk_IfExists()
        {
            // Create a payment first
            var payment = new PaymentCreateUpdateDto
            {
                UserId = 4,
                Type = PaymentType.GooglePay
            };

            var createResponse = await _client.PostAsJsonAsync("/api/Payment", payment);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var id = int.Parse(createResponse.Headers.Location?.ToString().Split('/').Last());

            // Test updating the payment
            var updated = new PaymentCreateUpdateDto
            {
                Id = id,
                UserId = 3,
                Type = PaymentType.ApplePay
            };

            var updateResponse = await _client.PutAsJsonAsync("/api/Payment", updated);
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        }

        [Fact]
        public async Task DeletePayment_ReturnsNoContent_IfExists()
        {
            // Create a payment first
            var payment = new PaymentCreateUpdateDto
            {
                UserId = 4,
                Type = PaymentType.Cash
            };

            var createResponse = await _client.PostAsJsonAsync("/api/Payment", payment);
            var id = int.Parse(createResponse.Headers.Location?.ToString().Split('/').Last());

            // Test deleting the payment
            var deletePayload = new PaymentDto { Id = id };

            var deleteResponse = await _client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/api/Payment", UriKind.Relative),
                Content = JsonContent.Create(deletePayload)
            });

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        }

        [Fact]
        public async Task GetPaymentById_ReturnsNotFound_IfMissing()
        {
            var response = await _client.GetAsync("/api/Payment/99999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePayment_ReturnsNotFound_IfMissing()
        {
            var updatePayment = new PaymentCreateUpdateDto
            {
                Id = 99999,
                UserId = 6,
                Type = PaymentType.ApplePay
            };

            var response = await _client.PutAsJsonAsync("/api/Payment", updatePayment);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeletePayment_ReturnsNotFound_IfMissing()
        {
            var deletePayload = new PaymentDto { Id = 99999 };
            var response = await _client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/api/Payment", UriKind.Relative),
                Content = JsonContent.Create(deletePayload)
            });

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }

    // Helper class for token response
    public class AuthResponse
    {
        public string Token { get; set; }
    }
}