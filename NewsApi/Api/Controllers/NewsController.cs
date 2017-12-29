using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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

        // GET api/news
        [HttpGet("")]
        public IActionResult GetAllNews()
        {
            return Ok(_newsService.getNews());
        }

        [HttpPost("")]
        public IActionResult AddNews([FromBody] NewsViewModel newNews)
        {
            if(!ModelState.IsValid) 
            { 
                return StatusCode(412);
            }
            _newsService.AddNews(newNews);
            return StatusCode(201);
        }
    }
}
