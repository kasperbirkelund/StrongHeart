using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Microsoft.AspNetCore.Mvc.Testing;
using StrongHeart.DemoApp.Business.Features.Commands.CreateCar;
using StrongHeart.DemoApp.Business.Features.Queries.GetCars;
using Xunit;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

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

        [Fact]
        public void Json()
        {
            //var features = new QueryFeature[]
            //{
            //    new QueryFeature("GetTester", new Request("string A", "string B"), new QueryResponse(false,"Car", "string Moladel", "int Age")),
            //    new QueryFeature("GetTester2", new Request("string A", "string B"), new QueryResponse(true, "Car", "string Model", "int Age")),
            //};
            //var features2 = new []
            //{
            //    new CommandFeature("GetTester", new Request("string A", "string B")),
            //    new CommandFeature("GetTester2", new Request("string A", "string B"))
            //};
            var features = new QueryFeature[]
            {
                new QueryFeature()
                {
                    Name = "ab", 
                    Request = new QueryRequest()
                    {
                        Properties = new List<string>()
                        {
                            "string A",
                            "string B"
                        }
                    },
                    Response = new QueryResponse()
                    {
                        Properties = new List<string>()
                        {
                            "string C",
                            "string D"
                        },
                        IsListResponse = true,
                        ResponseTypeName = "Person"
                    },
                },
                new QueryFeature()
                {
                    Name = "ab2",
                    Request = new QueryRequest()
                    {
                        Properties = new List<string>()
                        {
                            "string A",
                            "string B"
                        }
                    },
                    Response = new QueryResponse()
                    {
                        //Properties = new List<string>()
                        //{
                        //    "string C",
                        //    "string D"
                        //},
                        IsListResponse = true,
                        ResponseTypeName = "Person"
                    },
                }
            };
            QueryFeatures fs = new QueryFeatures()
            {
                RootNamespace = "StrongHeart.DemoApp.Business.Features",
                Items = features
            };
            var features2 = new []
            {
                new CommandFeature()
                {
                    Name = "CreateCar",
                    Request = new CommandRequest()
                    {
                        DtoProperties = new List<string>()
                        {
                            "string Model"
                        },
                        AdditionalRequestProperties = new List<string>()
                        {
                            "Guid Id"
                        }
                    }
                },
            };
            CommandFeatures fs2 = new CommandFeatures()
            {
                RootNamespace = "StrongHeart.DemoApp.Business.Features",
                Items = features2
            };

            //var json = System.Text.Json.JsonSerializer.Serialize(features);
            //var serializer = new SerializerBuilder()
            //    .WithNamingConvention(CamelCaseNamingConvention.Instance)
            //    .Build();

            XmlSerializer serializer2 = new XmlSerializer(typeof(CommandFeatures));
            StringBuilder sb = new StringBuilder();
            TextWriter writer = new StringWriter(sb);
            serializer2.Serialize(writer, fs2);
            var s = sb.ToString();

            //var yaml = serializer.Serialize(features);
            //var yaml2 = serializer.Serialize(features2);

            
        }
    }

    public class QueryFeatures
    {
        public string RootNamespace { get; set; }
        public QueryFeature[] Items { get; set; }
    }

    public class CommandFeatures
    {
        public string RootNamespace { get; set; }
        public CommandFeature[] Items { get; set; }
    }

    public class QueryFeature
    {
        public string Name { get; set; }
        public QueryRequest Request { get; set; }
        public QueryResponse Response { get; set; }
    }

    public class CommandFeature
    {
        public string Name { get; set; }
        public CommandRequest Request { get; set; }
    }

    public class QueryRequest
    {
        public List<string> Properties { get; set; }
    }

    public class CommandRequest 
    {
        public List<string> AdditionalRequestProperties { get; set; }
        public List<string> DtoProperties { get; set; }
    }

    public class QueryResponse
    {
        public bool IsListResponse { get; set; }
        public string ResponseTypeName { get; set; }
        public List<string> Properties { get; set; }

    }
}
