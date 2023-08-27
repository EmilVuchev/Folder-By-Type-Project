namespace Blog.Services.Data.Articles
{
    using Blog.Data;
    using Blog.Data.Models;
    using Blog.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class ArticleService : IArticleService
    {
        private const int ItemsPerPage = 20;

        private readonly BlogDbContext dbContext;

        public ArticleService(BlogDbContext dbContext)
            => this.dbContext = dbContext;

        public async Task<IEnumerable<ArticleListingServiceModel>> GetAll(int page)
            => await this.dbContext
                .Articles
                .Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .Select(a => new ArticleListingServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Author = a.Author.UserName
                })
                .ToListAsync();

        public async Task<int> Create(string title, string description, string authorId)
        {
            var article = new Article
            {
                Title = title,
                Description = description,
                CreatedOn = DateTime.UtcNow,
                AuthorId = authorId,
            };

            await this.dbContext.AddAsync(article);
            await this.dbContext.SaveChangesAsync();

            return article.Id;
        }
    }
}
