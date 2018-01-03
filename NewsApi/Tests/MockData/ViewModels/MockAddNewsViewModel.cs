using System;
using System.Collections.Generic;
using NewsApi.Models.ViewModels;

namespace Tests.MockData.ViewModels
{
    public static class MockAddNewsViewModel
    {
        private static readonly AddNewsViewModel[] data = 
        {
            new AddNewsViewModel
            {
                Title = "A",
                Content = "_A_"
            },
            new AddNewsViewModel
            {
                Title = "B",
                Content = "_B_"
            },
            new AddNewsViewModel
            {
                Title = "C",
                Content = "_C_"
            },
            new AddNewsViewModel
            {
                Title = "D",
                Content = "_D_"
            },
            new AddNewsViewModel
            {
                Title = "E",
                Content = "_E_"
            },
            new AddNewsViewModel
            {
                Title = "F",
                Content = "_F_"
            },
            new AddNewsViewModel
            {
                Title = "G",
                Content = "_G_"
            },
        };

        public static IEnumerable<AddNewsViewModel> GetAll()
        {
            var copyList = new List<AddNewsViewModel>();
            for (int i = 0; i < data.Length; i++)
            {
                copyList.Add(Get(i));
            }
            return copyList;
        }

        public static AddNewsViewModel Get(int index)
        {
            if (index < 0 || index >= data.Length) 
            {
                throw new ArgumentException("You are accessing test data that does not exist");
            }
            return new AddNewsViewModel
            {
                Title = data[index].Title,
                Content = data[index].Content
            };
        }
    }
}