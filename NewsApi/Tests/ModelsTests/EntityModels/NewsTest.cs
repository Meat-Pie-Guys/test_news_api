using System;
using NewsApi.Models.EntityModels;
using Tests.MockData.ViewModels;
using Xunit;

namespace Tests.ModelTests.EntityModels
{
    
    public class NewsTest
    {
        [Fact]
        public void NewsNoArgConstructorAndGetterTest()
        {
            // Act
            var news = new News{ Id = 3, Title = "X", ReleaseDate = new DateTime(2000, 9, 9, 5, 3, 1), Content = "Y" };

            // Assert
            Assert.Equal("X", news.Title);
            Assert.Equal("Y", news.Content);
            Assert.Equal(3, news.Id);
            Assert.Equal(new DateTime(2000, 9, 9, 5, 3, 1), news.ReleaseDate);
        }

        [Fact]
        public void NewsConstructFromViewModelTest()
        {
            // Arrange
            var model = MockAddNewsViewModel.Get(3);

            // Act
            var news = new News(model);

            // Assert
            Assert.Equal(model.Title, news.Title);
            Assert.Equal(model.Content, news.Content);
            Assert.Equal(0, news.Id);
            Assert.InRange((DateTime.Now - news.ReleaseDate).TotalMilliseconds, 0, 500);
        }

        [Fact]
        public void NewsSetterTest()
        {
            // Arrange
            var news = new News{ Id = 3, Title = "X", ReleaseDate = DateTime.Now, Content = "Y" };

            // Act
            news.Id = 55;
            news.Title = "A";
            news.Content = "B";
            news.ReleaseDate = new DateTime(1793, 5, 3);

            // Assert
            Assert.Equal("A", news.Title);
            Assert.Equal("B", news.Content);
            Assert.Equal(55, news.Id);
            Assert.Equal(new DateTime(1793, 5, 3), news.ReleaseDate);
        }

        [Fact]
        public void NewsEditFromViewModelTest()
        {
            // Arrange
            var model = MockEditNewsViewModel.Get(2);
            var news = new News{ Id = 15, Title = "X", ReleaseDate = new DateTime(2010, 1, 1), Content = "Y" };

            // Act
            news.Edit(model);

            // Assert
            Assert.Equal(15, news.Id);
            Assert.Equal(model.Title, news.Title);
            Assert.Equal(model.Content, news.Content);
            Assert.Equal(new DateTime(2010, 1, 1), news.ReleaseDate);
        }
    }
}