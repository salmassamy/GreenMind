using GreenMind.Domain.Entities;
using GreenMind.Presistance.Data.DbContexts;

namespace GreenMind.Presistance.Data.Seed
{
    public static class ArticleSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (context.Articles.Any())
                return;

            context.Articles.AddRange(
                new Article
                {
                    Title = "Modern Farming Techniques",
                    Description = "Learn about modern agriculture and smart farming methods.",
                    ImageUrl = "https://images.unsplash.com/photo-1500595046743-cd271d694d30",
                    ExternalUrl = "https://www.fao.org/land-water/water/water-management/en/",
                    IsFeatured = true,
                    CreatedAt = DateTime.Now
                },
         new Article
         {
             Title = "Global Agriculture Development (World Bank)",
             Description = "How agriculture supports global development and economy.",
             ImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c",
             ExternalUrl = "https://www.worldbank.org/en/topic/agriculture/overview",
             IsFeatured = false,
             CreatedAt = DateTime.Now
         },
        new Article
        {
            Title = "Smart Agriculture with AI",
            Description = "How AI is transforming agriculture and crop production.",
            ImageUrl = "https://images.unsplash.com/photo-1586771107445-d3ca888129ff",
            ExternalUrl = "https://www.fao.org/digital-agriculture/en/",
            IsFeatured = false,
            CreatedAt = DateTime.Now
        }
            );

            context.SaveChanges();
        }
    }
}