using System;
using System.Collections.Generic;
using NewsApi.Models.ViewModels;

namespace Tests.MockData.ViewModels
{
    public static class MockEditNewsViewModel
    {
        private static readonly EditNewsViewModel[] data = 
        {
            new EditNewsViewModel
            {
                Title = "A",
                Content = "_A_"
            },
            new EditNewsViewModel
            {
                Title = "B",
                Content = "_B_"
            },
            new EditNewsViewModel
            {
                Title = "C",
                Content = "_C_"
            },
            new EditNewsViewModel
            {
                Title = "D",
                Content = "_D_"
            },
            new EditNewsViewModel
            {
                Title = "E",
                Content = "_E_"
            },
            new EditNewsViewModel
            {
                Title = "F",
                Content = "_F_"
            },
        };

        public static IEnumerable<EditNewsViewModel> GetAll()
        {
            return data;
        }

        public static EditNewsViewModel Get(int index)
        {
            if (index < 0 || index >= data.Length) 
            {
                throw new ArgumentException("You are accessing test data that does not exist");
            }
            return data[index];
        }
    }
}