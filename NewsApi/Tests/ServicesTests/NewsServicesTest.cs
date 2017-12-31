using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models.DTOModels;
using NewsApi.Models.EntityModels;
using NewsApi.Repositories;
using NewsApi.Services.NewsServices;
using Xunit;
using Tests.MockData;

namespace Tests.ServicesTests
{
    public class NewsServicesTest
    {
        private readonly AppDataContext _context;
        private readonly NewsService _service;

        public NewsServicesTest()
        {
            _context = new AppDataContext(GetInMemoryOptions());
            _context.Database.OpenConnection();
            _context.Database.EnsureCreated();
            PopulateInMemoryDatabase();
            _context.SaveChanges();
            _service = new NewsService(_context);
        }

        [Fact]
        public void DemoTest()
        {
            List<NewsDTO> lis = new List<NewsDTO>();
            foreach (var n in _service.GetNews())
            {
                lis.Add(n);
            }
            Assert.Equal(2, lis.Count);
        }

        private DbContextOptions<AppDataContext> GetInMemoryOptions()
        {
            //Set up for in-memory SQLite database
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
        
            var builder = new DbContextOptionsBuilder<AppDataContext>();
            builder.UseSqlite(connection);
            var options = builder.Options;
            return options;
        }

        private void PopulateInMemoryDatabase()
        {
            PopulateNewsTable();
            // Add more if needed
        }

        private void PopulateNewsTable()
        {
            foreach (News n in MockNews.GetAll())
            {
                _context.Add(n);
            }
        }
    }
}
