using System;
using System.Linq;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.EntityModels;
using NewsApi.Models.ViewModels;
using NewsApi.Repositories;
using NewsApi.Exceptions;
using Microsoft.EntityFrameworkCore;
using NewsApi.Utils.TimeUtils;

namespace NewsApi.Services.NewsServices
{
    public class NewsService : INewsService
    {
        private readonly AppDataContext _db;

        public NewsService(AppDataContext db)
        {
            _db = db;
        }

        public IEnumerable<NewsDTO> GetAllNews(String year, String month, String day)
        {
            if (year == null || month == null || day == null)
            {
                return GetAllNewsInTimeOrder();
            }
            var date = DateBuilder.CreateDate(year, month, day);
            return date == null ? new List<NewsDTO>() : GetAllNewsForDayInTimeOrder(date.Value);
        }

        public IEnumerable<NewsDTO> GetAllNewsInTimeOrder()
        {
            return 
            (
                from n in _db.News
                orderby n.ReleaseDate
                select new NewsDTO(n)
            ).ToList();
        }

        public IEnumerable<NewsDTO> GetAllNewsForDayInTimeOrder(DateTime date)
        {
            return
            (
                from n in _db.News
                where n.ReleaseDate.Date == date
                orderby n.ReleaseDate
                select new NewsDTO(n)
            ).ToList();
        }

        public NewsDTO GetNewsById(int id)
        {
            var news = _db.News.SingleOrDefault(x => x.Id == id);
            if (news == null)
            {
                throw new NewsNotFoundException();
            }
            return new NewsDTO(news);
        }

        public int AddNews(AddNewsViewModel newNews)
        {
            var news = new News(newNews);
            _db.Add(news);
            _db.SaveChanges();
            return news.Id;
        }

        public void EditNewsById(EditNewsViewModel changedNews, int id)
        {
            var news = _db.News.SingleOrDefault(x => x.Id == id);
            if (news == null)
            {
                throw new NewsNotFoundException();
            }
            news.Edit(changedNews);
            _db.SaveChanges();
        }

        public void RemoveNewsById(int id)
        {
            var news = _db.News.SingleOrDefault(x => x.Id == id);
            if (news == null)
            {
                throw new NewsNotFoundException();
            }
            _db.Remove(news);
            _db.SaveChanges();
        }
    }
}