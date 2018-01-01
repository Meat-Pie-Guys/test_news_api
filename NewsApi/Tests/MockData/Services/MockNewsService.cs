using System;
using System.Collections.Generic;
using NewsApi.Exceptions;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;
using NewsApi.Services.NewsServices;

namespace Tests.MockData.Services
{
    public class MockNewsService : INewsService
    {
        public Func<AddNewsViewModel, int> _AddNews { get; set; }
        public Action<EditNewsViewModel, int> _EditNewsById { get; set; }
        public Func<string, string, string, IEnumerable<NewsDTO>> _GetAllNews { get; set; }
        public Func<DateTime, IEnumerable<NewsDTO>> _GetAllNewsForDayInTimeOrder { get; set; }
        public Func<IEnumerable<NewsDTO>> _GetAllNewsInTimeOrder { get; set; }
        public Func<int, NewsDTO> _GetNewsById { get; set; }
        public Action<int> _RemoveNewsById { get; set; }

        public int AddNews(AddNewsViewModel newNews)
        {
            return _AddNews(newNews);
        }

        public void EditNewsById(EditNewsViewModel changedNews, int id)
        {
            _EditNewsById(changedNews, id);
        }

        public IEnumerable<NewsDTO> GetAllNews(string year, string month, string day)
        {
            return _GetAllNews(year, month, day);
        }

        public IEnumerable<NewsDTO> GetAllNewsForDayInTimeOrder(DateTime date)
        {
            return _GetAllNewsForDayInTimeOrder(date);
        }

        public IEnumerable<NewsDTO> GetAllNewsInTimeOrder()
        {
            return _GetAllNewsInTimeOrder();
        }

        public NewsDTO GetNewsById(int id)
        {
            return _GetNewsById(id);
        }

        public void RemoveNewsById(int id)
        {
            _RemoveNewsById(id);
        }
    }
}