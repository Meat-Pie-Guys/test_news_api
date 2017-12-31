using NewsApi.Models.EntityModels;
using System;
using System.Collections.Generic;

namespace Tests.MockData
{
    public static class MockNews
    {
        private static News[] data = 
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
        };

        public static List<News> GetAll()
        {
            return new List<News>(data);
        }

        public static News GetNews(int index)
        {
            if (index < 0 || index >= data.Length) throw new ArgumentException("You are accessing test data that does not exist");
            return data[index];
        }
    }
}