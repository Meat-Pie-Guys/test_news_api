using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using NewsApi.Models.ViewModels;
using Tests.TestUtils;
using Xunit;

namespace Tests.ModelTests.ViewModels
{
    public class AddNewsViewModelTest
    {
        private AddNewsViewModel _model;
        
        public AddNewsViewModelTest()
        {
            _model = new AddNewsViewModel{ Title = "MyTitle", Content = "MyContent"};
        }

        [Fact]
        public void AddNewsViewModelGettersTest()
        {
            Assert.Equal("MyTitle", _model.Title);
            Assert.Equal("MyContent", _model.Content);
        }

        [Fact]
        public void AddNewsViewModelSettersTest()
        {
            _model.Title = "AnotherTitle";
            _model.Content = "AnotherTitle";
            Assert.Equal("AnotherTitle", _model.Title);
            Assert.Equal("AnotherTitle", _model.Content);
        }

        [Fact]
        public void AddNewsValidator_BothNull_NotValid()
        {
            // Arrange
            var model = new AddNewsViewModel{};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_REQUIRED, errorMessagess);
            Assert.Contains(ErrorMessages.CONTENT_REQUIRED, errorMessagess);
        }

        [Fact]
        public void AddNewsValidator_BothEmpty_NotValid()
        {
            // Arrange
            var model = new AddNewsViewModel{Title = "", Content = ""};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_REQUIRED, errorMessagess);
            Assert.Contains(ErrorMessages.CONTENT_REQUIRED, errorMessagess);
        }

        [Fact]
        public void AddNewsValidator_ContentNull_NotValid()
        {
            // Arrange
            var model = new AddNewsViewModel{Title = "X"};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.CONTENT_REQUIRED, errorMessagess);
        }

        [Fact]
        public void AddNewsValidator_TitleNull_NotValid()
        {
            // Arrange
            var model = new AddNewsViewModel{ Content = "X"};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_REQUIRED, errorMessagess);
        }

        [Fact]
        public void AddNewsValidator_TitleTooLong_NotValid()
        {
            // Arrange
            var model = new AddNewsViewModel{Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", Content = "B"};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_TOO_LONG, errorMessagess);
        }

        [Fact]
        public void AddNewsValidator_BothValid_Valid()
        {
            // Arrange
            var model = new AddNewsViewModel{Title = "A", Content = "B"};

            // Act
            var validation = ModelValidator.ValidateModel(model);

            // Assert
            Assert.Equal(0, validation.Count);
        }
    }
}