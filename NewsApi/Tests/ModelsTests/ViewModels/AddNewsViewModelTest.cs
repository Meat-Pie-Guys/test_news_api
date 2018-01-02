using System.Collections.Generic;
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
        public void foo()
        {
            var m1 = new AddNewsViewModel{};
            var v1 = ModelValidator.ValidateModel(m1);

            
            var m2 = new AddNewsViewModel{Title = "", Content = ""};
            var v2= ModelValidator.ValidateModel(m2);

            
            var m3 = new AddNewsViewModel{Title = "X"};
            var v3= ModelValidator.ValidateModel(m3);

            
            var m4 = new AddNewsViewModel{ Content = "X"};
            var v4= ModelValidator.ValidateModel(m4);

            
            var m5 = new AddNewsViewModel{Title = "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA", Content = "B"};
            var v5= ModelValidator.ValidateModel(m5);

            
            var m6 = new AddNewsViewModel{Title = "A", Content = "B"};
            var v6= ModelValidator.ValidateModel(m6);
        }
    }
}