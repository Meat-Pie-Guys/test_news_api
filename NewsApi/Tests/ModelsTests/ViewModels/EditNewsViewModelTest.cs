using NewsApi.Models.ViewModels;
using Tests.TestUtils;
using System.Linq;
using Xunit;

namespace Tests.ModelTests.ViewModels
{
    public class EditNewsViewModelTest
    {
        
        private EditNewsViewModel _model;
        
        public EditNewsViewModelTest()
        {
            _model = new EditNewsViewModel{ Title = "MyTitle", Content = "MyContent"};
        }

        [Fact]
        public void EditNewsViewModelGettersTest()
        {
            Assert.Equal("MyTitle", _model.Title);
            Assert.Equal("MyContent", _model.Content);
        }

        [Fact]
        public void EditNewsViewModelSettersTest()
        {
            _model.Title = "AnotherTitle";
            _model.Content = "AnotherTitle";
            Assert.Equal("AnotherTitle", _model.Title);
            Assert.Equal("AnotherTitle", _model.Content);
        }

        [Fact]
        public void EditNewsValidator_BothNull_NotValid()
        {
            // Arrange
            var model = new EditNewsViewModel{};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_REQUIRED, errorMessagess);
            Assert.Contains(ErrorMessages.CONTENT_REQUIRED, errorMessagess);
        }

        [Fact]
        public void EditNewsValidator_BothEmpty_NotValid()
        {
            // Arrange
            var model = new EditNewsViewModel{Title = "", Content = ""};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_REQUIRED, errorMessagess);
            Assert.Contains(ErrorMessages.CONTENT_REQUIRED, errorMessagess);
        }

        [Fact]
        public void EditNewsValidator_ContentNull_NotValid()
        {
            // Arrange
            var model = new EditNewsViewModel{Title = "X"};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.CONTENT_REQUIRED, errorMessagess);
        }

        [Fact]
        public void EditNewsValidator_TitleNull_NotValid()
        {
            // Arrange
            var model = new EditNewsViewModel{ Content = "X"};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_REQUIRED, errorMessagess);
        }

        [Fact]
        public void EditNewsValidator_TitleTooLong_NotValid()
        {
            // Arrange
            var model = new EditNewsViewModel{Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", Content = "B"};

            // Act
            var validation = ModelValidator.ValidateModel(model);
            var errorMessagess = validation.Select(x => x.ErrorMessage).ToHashSet();

            // Assert
            Assert.True(validation.Count > 0);
            Assert.Contains(ErrorMessages.TITLE_TOO_LONG, errorMessagess);
        }

        [Fact]
        public void EditNewsValidator_BothValid_Valid()
        {
            // Arrange
            var model = new EditNewsViewModel{Title = "A", Content = "B"};

            // Act
            var validation = ModelValidator.ValidateModel(model);

            // Assert
            Assert.Equal(0, validation.Count);
        }
    }
}