using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using ReservationAPI.DTOs.Write;
using ReservationAPI.DTOs.Read;
using DAL.Models;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace ReservationAPI.Tests.Controllers
{
    public class PaymentControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient client;

        public PaymentControllerTests(WebApplicationFactory<Program> factory)
        {
            client = factory.CreateClient();
        }

        [Fact]
        public async Task GetAllPayments_ReturnsUnauthorized()
        {
            var response = await client.GetAsync("/api/Payment");
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task CreatePayment_ReturnsCreated()
        {
            var payment = new PaymentCreateUpdateDto
            {
                UserId = 6, // Changed from 1 to 6
                Type = PaymentType.Card
            };

            var response = await client.PostAsJsonAsync("/api/Payment", payment);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }

        [Fact]
        public async Task GetPaymentById_ReturnsOk_IfExists()
        {
            var newPayment = new PaymentCreateUpdateDto
            {
                UserId = 6, // Changed from 1 to 6
                Type = PaymentType.PayPal
            };

            var createResponse = await client.PostAsJsonAsync("/api/Payment", newPayment);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);

            var location = createResponse.Headers.Location?.ToString();
            Assert.NotNull(location);

            var id = int.Parse(location.Split('/').Last());

            var getResponse = await client.GetAsync($"/api/Payment/{id}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
        }

        [Fact]
        public async Task UpdatePayment_ReturnsOk_IfExists()
        {
            var payment = new PaymentCreateUpdateDto
            {
                UserId = 6, // Changed from 1 to 6
                Type = PaymentType.GooglePay
            };

            var createResponse = await client.PostAsJsonAsync("/api/Payment", payment);
            Assert.Equal(HttpStatusCode.Created, createResponse.StatusCode);
            var id = int.Parse(createResponse.Headers.Location?.ToString().Split('/').Last());

            var updated = new PaymentCreateUpdateDto
            {
                Id = id,
                UserId = 6, // Changed from 1 to 6
                Type = PaymentType.ApplePay
            };

            var updateResponse = await client.PutAsJsonAsync("/api/Payment", updated);
            Assert.Equal(HttpStatusCode.OK, updateResponse.StatusCode);
        }

        //[Fact]
        //public async Task DeletePayment_ReturnsNoContent_IfExists()
        //{
        //    var payment = new PaymentCreateUpdateDto
        //    {
        //        UserId = 6, // Changed from 1 to 6
        //        Type = PaymentType.Cash
        //    };

        //    var createResponse = await client.PostAsJsonAsync("/api/Payment", payment);
        //    var id = int.Parse(createResponse.Headers.Location?.ToString().Split('/').Last());

        //    var deletePayload = new PaymentDto { Id = id };

        //    var deleteResponse = await client.SendAsync(new HttpRequestMessage
        //    {
        //        Method = HttpMethod.Delete,
        //        RequestUri = new Uri("/api/Payment", UriKind.Relative),
        //        Content = JsonContent.Create(deletePayload)
        //    });

        //    Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
        //}

        [Fact]
        public async Task GetPaymentById_ReturnsNotFound_IfMissing()
        {
            var response = await client.GetAsync("/api/Payment/99999");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task UpdatePayment_ReturnsNotFound_IfMissing()
        {
            var updatePayment = new PaymentCreateUpdateDto
            {
                Id = 99999,
                UserId = 6, // Changed from 1 to 6
                Type = PaymentType.ApplePay
            };

            var response = await client.PutAsJsonAsync("/api/Payment", updatePayment);
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Fact]
        public async Task DeletePayment_ReturnsNotFound_IfMissing()
        {
            var deletePayload = new PaymentDto { Id = 99999 };
            var response = await client.SendAsync(new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri("/api/Payment", UriKind.Relative),
                Content = JsonContent.Create(deletePayload)
            });

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}