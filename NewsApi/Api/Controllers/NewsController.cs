using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Exceptions;
using NewsApi.Models.ViewModels;
using NewsApi.Services.NewsServices;

namespace Api.Controllers
{
    /// <summary>
    /// A controller for all news related requests.
    /// </summary>
    [Route("api/news")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        /// <summary>
        /// Constructor that injects a service.
        /// </summary>
        /// <param name="newsService">A service implementing an interface</param>
        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        /// <summary>
        /// Get a specific news article given its id. 
        /// GET api/news/{newsId}
        /// </summary>
        /// <param name="newsId">the id of the news article</param>
        /// <returns>200 and news object if found, otherwise 404</returns>
        [HttpGet("{newsId:int}", Name = "GetNewsById")]
        public IActionResult GetNews(int newsId)
        {
            try
            {
                return Ok(_newsService.GetNewsById(newsId));
            }
            catch (NewsNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get a list of news, either all or for a specific day, ordered by time.
        /// To filter by day, all parameters must be provided.
        /// GET api/news
        /// </summary>
        /// <param name="year">Optional year when the news was released</param>
        /// <param name="month">Optional month when the news was released</param>
        /// <param name="day">Optional day when the news was released</param>
        /// <returns>200 and a list of news</returns>
        [HttpGet("")]
        public IActionResult GetAllNews([FromQuery] string year = null, [FromQuery] string month = null, [FromQuery] string day = null)
        {
            return Ok(_newsService.GetAllNews(year, month, day));
        }

        [HttpPost("")]
        /// <summary>
        /// Create a new news article.
        /// Post api/news
        /// </summary>
        /// <param name="newNews">A model with news title and content</param>
        /// <returns>201 and get route in header if successful, 400 otherwise</returns>
        public IActionResult AddNews([FromBody] AddNewsViewModel newNews)
        {
            if (newNews == null)
            {
                return BadRequest();
            }
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            int newId = _newsService.AddNews(newNews);
            return CreatedAtRoute("GetNewsById", new {newsId = newId}, null);
        }

        /// <summary>
        /// Delete a specific news article.
        /// DELETE api/news/{newsId}
        /// </summary>
        /// <param name="newsId">the id of the news article</param>
        /// <returns>204 if successful, 404 otherwise</returns>
        [HttpDelete("{newsId:int}")]
        public IActionResult RemoveNews(int newsId)
        {
            try
            {
                _newsService.RemoveNewsById(newsId);
                return NoContent();
            }
            catch (NewsNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Update title and/or content of a given news article.
        /// PUT api/news/{newsId}
        /// </summary>
        /// <param name="newsId">the id of the news article</param>
        /// <param name="changedNews">A model with updated news title and content</param>
        /// <returns>200 if successful, 404 if news article is not found, 400 otherwise</returns>
        [HttpPut("{newsId:int}")]
        public IActionResult EditNews(int newsId, [FromBody] EditNewsViewModel changedNews)
        {
            if (changedNews == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _newsService.EditNewsById(changedNews, newsId);
                return Ok();
            }
            catch (NewsNotFoundException)
            {
                return NotFound();
            }
        }        
    }
}
