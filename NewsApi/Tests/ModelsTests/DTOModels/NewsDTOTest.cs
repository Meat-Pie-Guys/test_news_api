using System;
using NewsApi.Models.DTOModels;
using Tests.MockData.EntityModels;
using Xunit;

namespace Tests.ModelTests.DTOModels
{
    public class NewsDTOTest
    {
        
        [Fact]
        public void NewsDTONoArgConstructorAndGetterTest()
        {
            // Act
            var newsDTO = new NewsDTO{ Id = 3, Title = "X", ReleaseDate = new DateTime(1999, 5, 12), Content = "Y" };

            // Assert
            Assert.Equal("X", newsDTO.Title);
            Assert.Equal("Y", newsDTO.Content);
            Assert.Equal(3, newsDTO.Id);
            Assert.Equal(new DateTime(1999, 5, 12), newsDTO.ReleaseDate);
        }

        [Fact]
        public void NewsDTOConstructFromNewsTest()
        {
            // Arrange
            var news = MockNews.Get(3);

            // Act
            var newsDTO = new NewsDTO(news);

            // Assert
            Assert.Equal(news.Title, newsDTO.Title);
            Assert.Equal(news.Content, newsDTO.Content);
            Assert.Equal(news.Id, newsDTO.Id);
            Assert.Equal(news.ReleaseDate, newsDTO.ReleaseDate);
        }

        [Fact]
        public void NewsDTOSetterTest()
        {
            // Arrange
            var newsDTO = new NewsDTO{ Id = 3, Title = "X", ReleaseDate = DateTime.Now, Content = "Y" };

            // Act
            newsDTO.Id = 55;
            newsDTO.Title = "A";
            newsDTO.Content = "B";
            newsDTO.ReleaseDate = new DateTime(1793, 5, 3);

            // Assert
            Assert.Equal("A", newsDTO.Title);
            Assert.Equal("B", newsDTO.Content);
            Assert.Equal(55, newsDTO.Id);
            Assert.Equal(new DateTime(1793, 5, 3), newsDTO.ReleaseDate);
        }
    }
}