using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewsApi.Models.DTOModels;
using NewsApi.Models.EntityModels;
using NewsApi.Repositories;
using NewsApi.Services.NewsServices;
using Microsoft.Data.Sqlite;
using Xunit;
using Tests.MockData.EntityModels;
using Tests.MockData.DataContext;
using Tests.MockData.DTOModels;
using NewsApi.Exceptions;
using Tests.MockData.ViewModels;

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
        public void GetAllNewsInTimeOrder_TimeOrderTest()
        {
            // Arrange
            bool first = true;
            DateTime last = DateTime.Now;

            // Act
            var news = _service.GetAllNewsInTimeOrder();

            // Assert
            foreach (var n in news)
            {
                if (first)
                {
                    last = n.ReleaseDate;
                    first = false;
                }
                else
                {
                    Assert.True(n.ReleaseDate >= last);
                    last = n.ReleaseDate;
                }
            }
        }

        [Fact]
        public void GetAllNewsInTimeOrder_ContainsAllMocks()
        {
            // Arrange
            int counter = 0;
            var allMockNewsIds = MockNews.GetAll().Select(x => x.Id).ToHashSet();

            // Act
            var news = _service.GetAllNewsInTimeOrder();

            // Assert
            foreach (NewsDTO n in news)
            {
                counter++;
                Assert.Contains(n.Id, allMockNewsIds);
            }
            Assert.Equal(allMockNewsIds.Count, counter);
        }

        [Fact]
        public void GetAllNewsForDayInTimeOrder_TimeOrderTest()
        {
            // Arrange
            bool first = true;
            DateTime last = DateTime.Now;

            // Act
            var news = _service.GetAllNewsForDayInTimeOrder(new DateTime(2015, 4, 3));

            // Assert
            foreach (var n in news)
            {
                if (first)
                {
                    last = n.ReleaseDate;
                    first = false;
                }
                else
                {
                    Assert.True(n.ReleaseDate >= last);
                    last = n.ReleaseDate;
                }
            }
        }

        [Fact]
        public void GetAllNewsForDayInTimeOrder_ContainsAllMocks()
        {
            // Arrange
            int counter = 0;
            DateTime date = new DateTime(2015, 4, 3);
            var allMockNewsForDay = MockNews.GetAll()
                                            .Where(x => x.ReleaseDate.Date == date)
                                            .Select(x => x.Id)
                                            .ToHashSet();

            // Act 
            var news = _service.GetAllNewsForDayInTimeOrder(date);

            // Assert
            foreach (NewsDTO n in news)
            {
                counter++;
                Assert.Contains(n.Id, allMockNewsForDay);
            }
            Assert.Equal(allMockNewsForDay.Count, counter);
        }


        [Fact]
        public void GetAllNews_NullYearAndMonthAndDay_AllNewsInOrder()
        {
            // Arrange
            bool first = true;
            DateTime last = DateTime.Now;            
            var allMockNewsIds = MockNews.GetAll().Select(x => x.Id).ToHashSet();
            int counter = 0;

            // Act
            var news = _service.GetAllNews(null, null, null);

            // Assert
            foreach (var n in news)
            {
                if (first)
                {
                    last = n.ReleaseDate;
                    first = false;
                }
                else
                {
                    Assert.True(n.ReleaseDate >= last);
                    last = n.ReleaseDate;
                }
                counter++;
                Assert.Contains(n.Id, allMockNewsIds);
            }

            Assert.Equal(allMockNewsIds.Count, counter);
        }


        [Fact]
        public void GetAllNews_SpecificDay_AllNewsForSpecificDayInOrder()
        {
            // Arrange
            bool first = true;
            DateTime last = DateTime.Now;
            DateTime date = new DateTime(2015, 4, 3);
            var allMockNewsForDay = MockNews.GetAll().Where(x => x.ReleaseDate.Date == date).Select(x => x.Id).ToHashSet();
            int counter = 0;

            // Act
            var news = _service.GetAllNews("2015", "4", "3");

            // Assert
            foreach (var n in news)
            {
                if (first)
                {
                    last = n.ReleaseDate;
                    first = false;
                }
                else
                {
                    Assert.True(n.ReleaseDate >= last);
                    last = n.ReleaseDate;
                }
                counter++;
                Assert.Contains(n.Id, allMockNewsForDay);
            }
            Assert.Equal(allMockNewsForDay.Count, counter);
        }


        [Fact]
        public void GetAllNews_InvalidDate_EmptyList()
        {
            Assert.Empty(_service.GetAllNews("A", "B", "C"));
        }

        [Fact]
        public void GetNewsById_ExistingId_Success()
        {
            // Arrange
            var id = 5;
            
            // Act
            var news = _service.GetNewsById(id);

            // Assert
            Assert.Equal(MockNews.Get(id - 1).Id, news.Id);
            Assert.Equal(MockNews.Get(id - 1).Title, news.Title);
            Assert.Equal(MockNews.Get(id - 1).ReleaseDate, news.ReleaseDate);
            Assert.Equal(MockNews.Get(id - 1).Content, news.Content);
        }

        [Fact]
        public void GetNewsById_NonExistingId_Exception()
        {
            Assert.Throws<NewsNotFoundException>(() => _service.GetNewsById(0));
        }

        [Fact]
        public void AddNews_Success()
        {
            // Arrange
            var model = MockAddNewsViewModel.Get(6);
            var id = _service.AddNews(model);
            
            // Act
            var news = _service.GetNewsById(id);
            
            // Assert
            Assert.Equal(model.Title, news.Title);
            Assert.Equal(model.Content, news.Content);
        }

        [Fact]
        public void EditNewsById_ExistingId_Success()
        {
            // Arrange
            var idToEdit = 3;
            var newsToEdit = MockNews.Get(idToEdit - 1);
            var changedNewsModel = MockEditNewsViewModel.Get(6);

            // Act
            _service.EditNewsById(changedNewsModel, idToEdit);

            // Assert
            Assert.Equal(changedNewsModel.Title, _service.GetNewsById(idToEdit).Title);
            Assert.Equal(changedNewsModel.Content, _service.GetNewsById(idToEdit).Content);
        }

        [Fact]
        public void EditNewsById_NonExistingId_Exception()
        {
            Assert.Throws<NewsNotFoundException>(() => _service.EditNewsById(MockEditNewsViewModel.Get(3), 0));
        }

        [Fact]
        public void RemoveNewsById_ExistingId_Success()
        {
            // Arrange
            var idToRemove = 3;

            // Act
            _service.RemoveNewsById(idToRemove);

            // Assert
            Assert.DoesNotContain(idToRemove, _service.GetAllNewsInTimeOrder().Select(x => x.Id));
        }

        [Fact]
        public void RemoveNewsById_NonExistingId_Exception()
        {
            Assert.Throws<NewsNotFoundException>(() => _service.RemoveNewsById(0));
        }
    }
}
