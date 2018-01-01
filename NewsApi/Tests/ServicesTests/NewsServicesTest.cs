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
        private readonly NewsService _service;

        public NewsServicesTest()
        {
            InMemoryAppDataContext _context = new InMemoryAppDataContext();
            _context.AddNews(MockNews.GetAll());
            _context.SaveChanges();
            _service = new NewsService(_context);
        }

        [Fact]
        public void DemoTest()
        {
            List<NewsDTO> lis = new List<NewsDTO>();
            foreach (var n in _service.GetAllNewsInTimeOrder())
            {
                lis.Add(n);
            }
            Assert.Equal(6, lis.Count);
        }
    }
}
