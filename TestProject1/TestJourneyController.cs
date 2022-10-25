using Assignment1;
using Assignment1.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    //Quelle: https://github.com/Naveen512/Dotnet6-API-XUnit-Testing/blob/master/Demo1.TestApi/System/Services/TestTodoService.cs
    public class TestJourneyController : IDisposable
    {
        protected readonly LogbookContext _context;
        //private static readonly string _connectionString = "datasource=localhost;port=3307;database=logbook;userid=root;password=" + Environment.GetEnvironmentVariable("dbpassword");
        // private static readonly string _connectionString =
        // $"server=localhost;user={Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root"};"
        // + $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "root"};"
        // + $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "logbook"}";
        private static readonly string _connectionString = "datasource=" + Environment.GetEnvironmentVariable("server") +
            ";port=" + Environment.GetEnvironmentVariable("serverport") +
            ";database=" + Environment.GetEnvironmentVariable("database") +
            ";userid=" + Environment.GetEnvironmentVariable("userid") +
            ";password=" + Environment.GetEnvironmentVariable("dbpassword");

        public TestJourneyController()
        {


            var options = new DbContextOptionsBuilder<LogbookContext>()
            .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
            .Options;

            _context = new LogbookContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetLogbook()
        {

            Logbook logbook = new Logbook();
            logbook.journeys = await _context.Journeys.ToListAsync();


            Assert.Equal(0, logbook.distanceTotal);

        }

        [Fact]
        public async Task SaveAsync_AddNewJourney()
        {
            /// Arrange
            Journey newJourney = new Journey(new DateTime(2010, 3, 11), new DateTime(2010, 3, 11), "test1", 5, "test1");
            _context.Journeys.Add(newJourney);
            _context.SaveChanges();

            Logbook logbook = new Logbook();
            logbook.journeys = await _context.Journeys.ToListAsync();

            Journey journey = logbook.journeys.First();

            Assert.Equal("test1", journey.Driver);

        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

