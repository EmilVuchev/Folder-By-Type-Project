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

        public async Task<bool> Edit(int id, string title, string description)
        {
            var article = await this.dbContext
                .Articles
                .FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
            {
                return false;
            }

            article.Title = title;
            article.Description = description;

            await this.dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Exists(int id, string userId)
            => await this.dbContext.Articles.AnyAsync(a => a.Id == id && a.AuthorId == userId);

        public async Task<ArticleDetailServiceModel> Details(int id)
            => await this.dbContext
                .Articles
                .Where(a => a.Id == id)
                .Select(a => new ArticleDetailServiceModel
                {
                    Id = a.Id,
                    Title = a.Title,
                    Description = a.Description,
                    Author = a.Author.UserName,
                })
                .FirstOrDefaultAsync();

        public async Task<bool> Delete(int id)
        {
            var article = await this.dbContext.Articles.FindAsync(id);

            if (article == null)
            {
                return false;
            }

            this.dbContext.Remove(article);
            await this.dbContext.SaveChangesAsync();
            
            return true;
        }
    }
}
