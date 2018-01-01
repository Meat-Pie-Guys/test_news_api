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
        };

        public static IEnumerable<AddNewsViewModel> GetAll()
        {
            return data;
        }

        public static AddNewsViewModel Get(int index)
        {
            if (index < 0 || index >= data.Length) 
            {
                throw new ArgumentException("You are accessing test data that does not exist");
            }
            return data[index];
        }
    }
}