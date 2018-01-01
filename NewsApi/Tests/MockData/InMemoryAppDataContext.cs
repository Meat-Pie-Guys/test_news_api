using NewsApi.Models.EntityModels;
using System;
using System.Collections.Generic;
using NewsApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;

namespace Tests.MockData
{
    public class InMemoryAppDataContext : AppDataContext
    {
        public InMemoryAppDataContext() : base(GetInMemoryOptions()) 
        {
            this.Database.OpenConnection();
            this.Database.EnsureCreated();
        }

        public void AddNews(IEnumerable<News> news)
        {
            foreach (News n in news)
            {
                this.Add(n);
            }
        }

        private static DbContextOptions<AppDataContext> GetInMemoryOptions()
        {
            var connectionString = new SqliteConnectionStringBuilder { DataSource = ":memory:" }.ToString();
            var connection = new SqliteConnection(connectionString);
            var builder = new DbContextOptionsBuilder<AppDataContext>();
            return builder.UseSqlite(connection).Options;
        }
    }
}