using System;
using System.Collections.Generic;
using NewsApi.Models.DTOModels;
using NewsApi.Models.ViewModels;

namespace NewsApi.Services.NewsServices
{
    /// <summary>
    /// A interface for a news service that defines what methods such a service must implement.
    /// </summary>
    public interface INewsService
    {
        /// <summary>
        /// Returns all news, either for entire history or filtered by day.
        /// </summary>
        /// <param name="year">a year as a string, possibly null</param>
        /// <param name="month">a month as a string, possibly null</param>
        /// <param name="day">a day as a string, possibly null</param>
        /// <returns>A list of news, ordered by release date</returns>
        IEnumerable<NewsDTO> GetAllNews(String year, String month, String day);

        /// <summary>
        /// All news.
        /// </summary>
        /// <returns>All news ordered by time</returns>
        IEnumerable<NewsDTO> GetAllNewsInTimeOrder();

        /// <summary>
        /// All news on a given day.
        /// </summary>
        /// <param name="date">A date to compare all news against</param>
        /// <returns>All news released on a specific date</returns>
        IEnumerable<NewsDTO> GetAllNewsForDayInTimeOrder(DateTime date);

        /// <summary>
        /// A single news article.
        /// </summary>
        /// <param name="id">an id for a news article</param>
        /// <returns>A single news article with given id</returns>
        NewsDTO GetNewsById(int id);

        /// <summary>
        /// Create a news article.
        /// </summary>
        /// <param name="newNews">A model with attributes needed to create new news article</param>
        /// <returns>the id of the newly created news article</returns>
        int AddNews(AddNewsViewModel newNews);

        /// <summary>
        /// Edit a news article.
        /// </summary>
        /// <param name="changedNews">A model with attributes needed to edit an existing news article</param>
        /// <param name="id">the id of the news article to be altered</param>
        void EditNewsById(EditNewsViewModel changedNews, int id);

        /// <summary>
        /// Remove a news article.
        /// </summary>
        /// <param name="id">the id of the news article to be removed</param>
        void RemoveNewsById(int id);
    }
}