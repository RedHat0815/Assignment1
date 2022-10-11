using Assignment1;
using Assignment1.Model;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace TestProject1
{
    //Quelle: https://github.com/Naveen512/Dotnet6-API-XUnit-Testing/blob/master/Demo1.TestApi/System/Services/TestTodoService.cs
    public class TestTodoService : IDisposable
    {
        protected readonly LogbookContext _context;
        private static readonly string _connectionString =
        $"server=localhost;user={Environment.GetEnvironmentVariable("MYSQL_USER") ?? "root"};"
        + $"password={Environment.GetEnvironmentVariable("MYSQL_PASSWORD") ?? "root"};"
        + $"database={Environment.GetEnvironmentVariable("MYSQL_DATABASE") ?? "logbook"}";

        public TestTodoService()
        {


            var options = new DbContextOptionsBuilder<LogbookContext>()
            .UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString))
            .Options;

            _context = new LogbookContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task GetAllAsync_ReturnTodoCollection()
        {
            /// Arrange
            _context.Journeys.AddRange(MockData.TodoMockData.GetTodos());
            _context.SaveChanges();

            var sut = new TodoService(_context);

            /// Act
            var result = await sut.GetAllAsync();

            /// Assert
            result.Should().HaveCount(TodoMockData.GetTodos().Count);
        }

        [Fact]
        public async Task SaveAsync_AddNewTodo()
        {
            /// Arrange
            var newTodo = TodoMockData.NewTodo();
            _context.Todo.AddRange(MockData.TodoMockData.GetTodos());
            _context.SaveChanges();

            var sut = new TodoService(_context);

            /// Act
            await sut.SaveAsync(newTodo);

            ///Assert
            int expectedRecordCount = (TodoMockData.GetTodos().Count() + 1);
            _context.Todo.Count().Should().Be(expectedRecordCount);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}

