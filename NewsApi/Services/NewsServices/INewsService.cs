using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;

namespace NewsApi.Services.NewsServices
{
    public interface INewsService
    {
        IEnumerable<NewsDTO> GetAllNews(String year, String month, String day);
        IEnumerable<NewsDTO> GetAllNewsInTimeOrder();
        IEnumerable<NewsDTO> GetAllNewsForDayInTimeOrder(DateTime date);
        NewsDTO GetNewsById(int id);
        int AddNews(AddNewsViewModel newNews);
        void EditNewsById(EditNewsViewModel changedNews, int id);
        void RemoveNewsById(int id);
    }
}