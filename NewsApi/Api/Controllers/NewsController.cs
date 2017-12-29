using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NewsApi.Services.NewsServices;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    public class NewsController : Controller
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_newsService.getNews());
        }
    }
}
