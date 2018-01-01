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
    /// <summary>
    /// The news service that our production API uses.
    /// </summary>
    public class NewsService : INewsService
    {
        private readonly AppDataContext _db;

        /// <summary>
        /// A constructor that injects AppDataContext.
        /// </summary>
        /// <param name="db">A DbContext to access our database</param>
        public NewsService(AppDataContext db)
        {
            _db = db;
        }

        /// <summary>
        /// Returns all news, either for entire history or filtered by day.
        /// </summary>
        /// <param name="year">a year as a string, possibly null</param>
        /// <param name="month">a month as a string, possibly null</param>
        /// <param name="day">a day as a string, possibly null</param>
        /// <returns>A list of news, ordered by release date</returns>
        public IEnumerable<NewsDTO> GetAllNews(String year, String month, String day)
        {
            // If any of the parameters is null, they are ignored.
            if (year == null || month == null || day == null)
            {
                return GetAllNewsInTimeOrder();
            }
            // if date creation was unsuccesful, we return an empty list
            var date = DateBuilder.CreateDate(year, month, day);
            return date == null ? new List<NewsDTO>() : GetAllNewsForDayInTimeOrder(date.Value);
        }

        /// <summary>
        /// All news.
        /// </summary>
        /// <returns>All news ordered by time</returns>
        public IEnumerable<NewsDTO> GetAllNewsInTimeOrder()
        {
            return 
            (
                from n in _db.News
                orderby n.ReleaseDate
                select new NewsDTO(n)
            ).ToList();
        }

        /// <summary>
        /// All news on a given day.
        /// </summary>
        /// <param name="date">A date to compare all news against</param>
        /// <returns>All news released on a specific date</returns>
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

        /// <summary>
        /// A single news article.
        /// </summary>
        /// <param name="id">an id for a news article</param>
        /// <returns>A single news article with given id</returns>
        public NewsDTO GetNewsById(int id)
        {
            var news = _db.News.SingleOrDefault(x => x.Id == id);
            if (news == null)
            {
                throw new NewsNotFoundException();
            }
            return new NewsDTO(news);
        }

        /// <summary>
        /// Create a news article.
        /// </summary>
        /// <param name="newNews">A model with attributes needed to create new news article</param>
        /// <returns>the id of the newly created news article</returns>
        public int AddNews(AddNewsViewModel newNews)
        {
            var news = new News(newNews);
            _db.Add(news);
            _db.SaveChanges();
            return news.Id;
        }

        /// <summary>
        /// Edit a news article.
        /// </summary>
        /// <param name="changedNews">A model with attributes needed to edit an existing news article</param>
        /// <param name="id">the id of the news article to be altered</param>
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

        /// <summary>
        /// Remove a news article.
        /// </summary>
        /// <param name="id">the id of the news article to be removed</param>
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