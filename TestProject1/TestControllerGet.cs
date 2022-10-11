using Assignment1;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;


namespace TestProject1
{
    public class TestControllerGet
    {

        private static readonly string _connectionString =
        $"server=localhost;user={Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root"};"
        + $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "root"};"
        + $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "logbook"}";

        private readonly TestServer _server;


        //private static readonly string _connectionString =
        //$"server=localhost;user={Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root"};"
        //+ $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? ""};"
        //+ $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "Event_Test"}";

        //private LogbookContext _context;

        //private readonly DbContextOptions<LogbookContext> _options = new DbContextOptionsBuilder<LogbookContext>()
        //    .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
        //    .Options;


        private readonly HttpClient _client;

        public TestControllerGet()
        {
            _server = new TestServer(
                new WebHostBuilder()
                    .UseSetting("ConnectionStrings:Logbookdatabase", _connectionString)
                    //.UseStartup<Startup>()
            );

            _client = _server.CreateClient();

            using (var context = (LogbookContext)_server.Host.Services.GetService(typeof(LogbookContext))!)
            {
                context.Database.EnsureDeleted();
                context.Database.Migrate();
            }
        }

        [Fact]
        public async void PostEventAsync()
        {
            var postResponse = await _client.PostAsJsonAsync("logbook/Journeys", new { Driver = "Testdriver" });
            var createdJourney = await GetPageFromResponse(postResponse);
            var createdJourneyId = GetIdFromJourney(createdJourney);

            Assert.Equal(new Uri($"http://localhost/logbook/Journeys/{createdJourneyId}"), postResponse.Headers.Location);
            Assert.Equal(HttpStatusCode.Created, postResponse.StatusCode);
            Assert.Equal("Event title", GetDriverFromJourney(createdJourney));

            var getResponse = await _client.GetAsync($"logbook/Journeys/{createdJourneyId}");
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            var loadedJourney = await GetPageFromResponse(getResponse);

            Assert.Equal(createdJourneyId, GetIdFromJourney(loadedJourney));
            Assert.Equal("Testdriver", GetDriverFromJourney(loadedJourney));
        }

        private async Task<JsonDocument> GetPageFromResponse(HttpResponseMessage response)
        {
            return (await response.Content.ReadFromJsonAsync<JsonDocument>())!;
        }

        private Guid GetIdFromJourney(JsonDocument page)
        {
            return page.RootElement.GetProperty("id").GetGuid();
        }

        private string GetDriverFromJourney(JsonDocument page)
        {
            return page.RootElement.GetProperty("driver").GetString()!;
        }
    }
}