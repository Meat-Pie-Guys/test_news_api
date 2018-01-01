using NewsApi.Models.EntityModels;
using System;
using System.Collections.Generic;

namespace Tests.MockData.EntityModels
{
    public static class MockNews
    {
        private static readonly News[] data = 
        {
            new News
            {
                Id = 1,
                Title = "A",
                ReleaseDate = new DateTime(2015, 4, 3, 12, 44, 39),
                Content = "_A_"
            },
            new News
            {
                Id = 2,
                Title = "B",
                ReleaseDate = new DateTime(2015, 4, 3, 12, 44, 11),
                Content = "_B_"
            },
            new News
            {
                Id = 3,
                Title = "C",
                ReleaseDate = new DateTime(2014, 4, 3, 12, 44, 11),
                Content = "_C_"
            },
            new News
            {
                Id = 4,
                Title = "D",
                ReleaseDate = new DateTime(2015, 4, 3, 10, 11, 0),
                Content = "_D_"
            },
            new News
            {
                Id = 5,
                Title = "E",
                ReleaseDate = new DateTime(2017, 11, 30, 23, 49, 59),
                Content = "_E_"
            },
            new News
            {
                Id = 6,
                Title = "F",
                ReleaseDate = new DateTime(2017, 12, 24, 18, 0, 0),
                Content = "_F_"
            },
        };

        public static IEnumerable<News> GetAll()
        {
            return data;
        }

        public static News Get(int index)
        {
            if (index < 0 || index >= data.Length) 
            {
                throw new ArgumentException("You are accessing test data that does not exist");
            }
            return data[index];
        }
    }
}