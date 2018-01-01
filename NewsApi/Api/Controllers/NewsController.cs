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
    [Route("api/news")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET api/news/{newsId}
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

        // GET api/news
        [HttpGet("")]
        public IActionResult GetAllNews([FromQuery] string year = null, [FromQuery] string month = null, [FromQuery] string day = null)
        {
            return Ok(_newsService.GetAllNews(year, month, day));
        }

        // POST api/news
        [HttpPost("")]
        public IActionResult AddNews([FromBody] AddNewsViewModel newNews)
        {
            if(!ModelState.IsValid) 
            {
                return BadRequest(ModelState);
            }
            int newId = _newsService.AddNews(newNews);
            return CreatedAtRoute("GetNewsById", new {newsId = newId}, null);
        }

        // DELETE api/news/{newsId}
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

        // PUT api/news/{newsId}
        [HttpPut("{newsId:int}")]
        public IActionResult EditNews(int newsId, [FromBody] EditNewsViewModel changedNews)
        {
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
