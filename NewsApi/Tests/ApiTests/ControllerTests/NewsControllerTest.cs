using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using NewsApi.Exceptions;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;
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
            Assert.Equal(MockNewsDTO.Get(5), newsResult);
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
            var newsListResult = result.Value as List<NewsDTO>;

            // Assert
            Assert.Equal(2, newsListResult.Count);
            Assert.Contains(MockNewsDTO.Get(0), newsListResult);
            Assert.Contains(MockNewsDTO.Get(3), newsListResult);
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
            controller.ModelState.AddModelError("id", "required");

            // Act
            var result = controller.AddNews(MockAddNewsViewModel.Get(0)) as BadRequestObjectResult;
            var error = result.Value as SerializableError;
            
            // Assert
            Assert.Equal(400, result.StatusCode);
            Assert.Single(error.Keys);
            Assert.True(error.ContainsKey("id"));
            Assert.Equal("required", error.GetValueOrDefault("id"));
        }
    }
}

/*

            // Arrange
            var service = new MockNewsService{_RemoveNewsById = (id) => throw new NewsNotFoundException()};
            var controller = new NewsController(service);

            // Act
            var result = controller.RemoveNews(5) as NotFoundResult;
            
            // Assert
            Assert.Equal(404, result.StatusCode);

 */