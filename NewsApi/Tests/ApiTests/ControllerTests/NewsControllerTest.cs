using System.Collections.Generic;
using System.Linq;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Exceptions;
using NewsApi.Models.DTOModels;
using Tests.MockData.DTOModels;
using Tests.MockData.Services;
using Tests.MockData.ViewModels;
using Xunit;

namespace Tests.ApiTests.ControllerTets
{
    public class NewsControllerTest
    {

        [Fact]
        public void GetNews_Found_200()
        {
            // Arrange
            var service = new MockNewsService{_GetNewsById = (id) => MockNewsDTO.Get(id)};
            var controller = new NewsController(service);

            // Act
            IActionResult res = controller.GetNews(5);
            var result = controller.GetNews(5) as OkObjectResult;
            var newsResult = result.Value as NewsDTO;
            
            // Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(MockNewsDTO.Get(5).Id, newsResult.Id);
            Assert.Equal(MockNewsDTO.Get(5).Title, newsResult.Title);
            Assert.Equal(MockNewsDTO.Get(5).Content, newsResult.Content);
            Assert.Equal(MockNewsDTO.Get(5).ReleaseDate, newsResult.ReleaseDate);
        }


        [Fact]
        public void GetNews_Found_404()
        {
            // Arrange
            var service = new MockNewsService{_GetNewsById = (id) => throw new NewsNotFoundException()};
            var controller = new NewsController(service);

            // Act
            var result = controller.GetNews(15) as NotFoundResult;
            
            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void GetAllNews_TwoNewsDTObjects_200()
        {
            // Arrange
            var service = new MockNewsService
            {
                _GetAllNews = (year, month, day) => 
                {
                    return new List<NewsDTO>
                    {
                        MockNewsDTO.Get(0),
                        MockNewsDTO.Get(3)
                    };
                }
            };
            var controller = new NewsController(service);

            // Act
            var result = controller.GetAllNews("1", "2", "3") as OkObjectResult;
            var newsListResult = (result.Value as List<NewsDTO>);
            var idSet = newsListResult.Select(x => x.Id).ToHashSet();
            var titleSet = newsListResult.Select(x => x.Title).ToHashSet();
            var contentSet = newsListResult.Select(x => x.Content).ToHashSet();
            var dateSet = newsListResult.Select(x => x.ReleaseDate).ToHashSet();

            // Assert
            Assert.Equal(2, idSet.Count);
            Assert.Contains(MockNewsDTO.Get(0).Id, idSet);
            Assert.Contains(MockNewsDTO.Get(0).Title, titleSet);
            Assert.Contains(MockNewsDTO.Get(0).Content, contentSet);
            Assert.Contains(MockNewsDTO.Get(0).ReleaseDate, dateSet);
            Assert.Contains(MockNewsDTO.Get(3).Id, idSet);
            Assert.Contains(MockNewsDTO.Get(3).Title, titleSet);
            Assert.Contains(MockNewsDTO.Get(3).Content, contentSet);
            Assert.Contains(MockNewsDTO.Get(3).ReleaseDate, dateSet);
        }

        [Fact]
        public void AddNews_ValidModel_Created()
        {
            // Arrange
            var service = new MockNewsService { _AddNews = (newNews) => 1 };
            var controller = new NewsController(service);

            // Act
            var result = controller.AddNews(MockAddNewsViewModel.Get(0)) as CreatedAtRouteResult;
            
            // Assert
            Assert.Equal(201, result.StatusCode);
            Assert.Equal("GetNewsById", result.RouteName as string);
            Assert.Single(result.RouteValues.Keys);
            Assert.True(result.RouteValues.ContainsKey("newsId"));
            Assert.Equal(1, result.RouteValues.GetValueOrDefault("newsId"));
        }

        [Fact]
        public void AddNews_NullModel_BadRequest()
        {
            // Arrange
            var service = new MockNewsService { _AddNews = (newNews) => 1 };
            var controller = new NewsController(service);

            // Act
            var result = controller.AddNews(null) as BadRequestResult;
            
            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void AddNews_InvalidState_BadRequest()
        {
            // Arrange
            var service = new MockNewsService { _AddNews = (newNews) => 1 };
            var controller = new NewsController(service);
            controller.ModelState.AddModelError("fake_field", "fake_error_message");

            // Act
            var result = controller.AddNews(MockAddNewsViewModel.Get(0)) as BadRequestObjectResult;
            var error = result.Value as SerializableError;
            
            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Single(error.Keys);
            Assert.True(error.ContainsKey("fake_field"));
            Assert.Equal(new string[]{"fake_error_message"}, error.GetValueOrDefault("fake_field"));
        }

        [Fact]
        public void RemoveNews_NonExisting_NotFound()
        {
            // Arrange
            var service = new MockNewsService{_RemoveNewsById = (id) => throw new NewsNotFoundException()};
            var controller = new NewsController(service);

            // Act
            var result = controller.RemoveNews(5) as NotFoundResult;
            
            // Assert
            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public void RemoveNews_Existing_NoContent()
        {
            // Arrange
            var service = new MockNewsService{_RemoveNewsById = (id) => {}};
            var controller = new NewsController(service);

            // Act
            var result = controller.RemoveNews(5) as NoContentResult;
            
            // Assert
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void EditNews_ValidModel_Ok()
        {
            // Arrange
            var service = new MockNewsService { _EditNewsById = (id, newNews) => {}};
            var controller = new NewsController(service);

            // Act
            var result = controller.EditNews(0, MockEditNewsViewModel.Get(0)) as OkResult;
            
            // Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public void EditNews_NullModel_BadRequest()
        {
            // Arrange
            var service = new MockNewsService { _EditNewsById = (id, newNews) => {}};
            var controller = new NewsController(service);

            // Act
            var result = controller.EditNews(0, null) as BadRequestResult;
            
            // Assert
            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public void EditNews_InvalidModel_BadRequest()
        {
            // Arrange
            var service = new MockNewsService { _EditNewsById = (id, newNews) => {}};
            var controller = new NewsController(service);    
            controller.ModelState.AddModelError("fake_field", "fake_error_message");

            // Act
            var result = controller.EditNews(3, MockEditNewsViewModel.Get(3)) as BadRequestObjectResult;
            var error = result.Value as SerializableError;
            
            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Single(error.Keys);
            Assert.True(error.ContainsKey("fake_field"));
            Assert.Equal(new string[]{"fake_error_message"}, error.GetValueOrDefault("fake_field"));
        }

        [Fact]
        public void EditNews_NonExisting_NotFound()
        {
            // Arrange
            var service = new MockNewsService { _EditNewsById = (id, newNews) => throw new NewsNotFoundException()};
            var controller = new NewsController(service);

            // Act
            var result = controller.EditNews(124, MockEditNewsViewModel.Get(0)) as NotFoundResult;
            
            // Assert
            Assert.Equal(404, result.StatusCode);
        }
    }
}
