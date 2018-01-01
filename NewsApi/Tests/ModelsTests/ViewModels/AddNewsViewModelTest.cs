using NewsApi.Models.ViewModels;
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
    }
}