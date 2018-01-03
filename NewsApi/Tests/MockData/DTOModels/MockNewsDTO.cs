using NewsApi.Models.DTOModels;
using System;
using System.Collections.Generic;

namespace Tests.MockData.DTOModels
{
    public static class MockNewsDTO
    {
        private static readonly NewsDTO[] data = 
        {
            new NewsDTO
            {
                Id = 1,
                Title = "A",
                ReleaseDate = new DateTime(2015, 4, 3, 12, 44, 39),
                Content = "_A_"
            },
            new NewsDTO
            {
                Id = 2,
                Title = "B",
                ReleaseDate = new DateTime(2015, 4, 3, 12, 44, 11),
                Content = "_B_"
            },
            new NewsDTO
            {
                Id = 3,
                Title = "C",
                ReleaseDate = new DateTime(2014, 4, 3, 12, 44, 11),
                Content = "_C_"
            },
            new NewsDTO
            {
                Id = 4,
                Title = "D",
                ReleaseDate = new DateTime(2015, 4, 3, 10, 11, 0),
                Content = "_D_"
            },
            new NewsDTO
            {
                Id = 5,
                Title = "E",
                ReleaseDate = new DateTime(2017, 11, 30, 23, 49, 59),
                Content = "_E_"
            },
            new NewsDTO
            {
                Id = 6,
                Title = "F",
                ReleaseDate = new DateTime(2017, 12, 24, 18, 0, 0),
                Content = "_F_"
            },
        };

        public static IEnumerable<NewsDTO> GetAll()
        {
            var copyList = new List<NewsDTO>();
            for (int i = 0; i < data.Length; i++)
            {
                copyList.Add(Get(i));
            }
            return copyList;
        }

        public static NewsDTO Get(int index)
        {
            if (index < 0 || index >= data.Length) 
            {
                throw new ArgumentException("You are accessing test data that does not exist");
            }
            return new NewsDTO
            {
                Id = data[index].Id,
                Title = data[index].Title,
                ReleaseDate = data[index].ReleaseDate,
                Content = data[index].Content
            };
        }
    }
}