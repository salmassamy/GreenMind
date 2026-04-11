using GreenMind.Presistance.Data.DbContexts;
using GreenMind.ServiceAbstraction.DTOs;
using GreenMind.ServiceAbstraction.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GreenMind.Service.Services
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext _context;

        public ArticleService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ArticlesPageDto> GetArticlesPageAsync()
        {
            var articles = await _context.Articles
                .OrderByDescending(x => x.IsFeatured)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();

            // ✔ بدل Exception → رجع فاضي
            if (!articles.Any())
            {
                return new ArticlesPageDto
                {
                    FeaturedArticle = null,
                    AllArticles = new List<ArticleCardDto>()
                };
            }

            string Limit(string text) =>
                text.Length <= 150 ? text : text[..150] + "...";

            var featured = articles.FirstOrDefault(x => x.IsFeatured)
                           ?? articles.First();

            return new ArticlesPageDto
            {
                FeaturedArticle = new ArticleCardDto
                {
                    Id = featured.Id,
                    Title = featured.Title,
                    Description = Limit(featured.Description),
                    ImageUrl = featured.ImageUrl
                },
                AllArticles = articles
                    .Where(x => x.Id != featured.Id)
                    .Select(x => new ArticleCardDto
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = Limit(x.Description),
                        ImageUrl = x.ImageUrl
                    })
                    .ToList()
            };
        }
    }
}