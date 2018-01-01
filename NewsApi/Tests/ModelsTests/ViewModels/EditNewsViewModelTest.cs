using NewsApi.Models.ViewModels;
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
    }
}