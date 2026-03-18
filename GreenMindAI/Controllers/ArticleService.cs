using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GreenMindAI.Controllers
{
    [ApiController]
    [Route("articles/api")]
    public class ArticlesController : ControllerBase
    {
        private readonly IArticleService _articleService;

        public ArticlesController(IArticleService articleService)
        {
            _articleService = articleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetArticlesPage()
        {
            var result = await _articleService.GetArticlesPageAsync();
            return Ok(result);
        }
    }
}