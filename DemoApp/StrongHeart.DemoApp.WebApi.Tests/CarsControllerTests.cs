using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using StrongHeart.DemoApp.Business.Features.Commands.CreateCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using Xunit;

namespace StrongHeart.DemoApp.WebApi.Tests
{
    public class CarsControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public CarsControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetCar()
        {
            HttpClient client = _factory.CreateClient();
            Car result = await client.GetFromJsonAsync<Car>("/Cars/242");
            Assert.NotNull(result);
            Assert.Equal("Renault", result.Model);
        }

        [Fact]
        public async Task GetCars()
        {
            HttpClient client = _factory.CreateClient();
            List<Car> result = await client.GetFromJsonAsync<List<Car>>("/Cars/Fiat");
            Assert.NotNull(result);
            Assert.Equal("Fiat", result.Single().Model);
        }

        [Fact]
        public async Task CreateCar_NoValidationError()
        {
            CreateCarDto dto = new("Skoda");
            HttpClient client = _factory.CreateClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("/Cars", dto);
            HttpStatusCode actual = response.StatusCode;
            Assert.Equal(HttpStatusCode.Accepted, actual);
        }

        [Fact]
        public async Task CreateCar_WithValidationError()
        {
            CreateCarDto dto = new("Not Skoda"); //"Skoda" is the only valid model name in this demo
            HttpClient client = _factory.CreateClient();
            HttpResponseMessage response = await client.PostAsJsonAsync("/Cars", dto);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            string content = await response.Content.ReadAsStringAsync();
            Assert.Equal(@"Validation messages: 
- Model must be Skoda", content);
        }
    }
}
