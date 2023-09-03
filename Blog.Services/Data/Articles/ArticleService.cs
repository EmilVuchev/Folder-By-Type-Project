namespace Blog.Services.Data.Articles
{
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using Blog.Data;
    using Blog.Data.Models;
    using Blog.Services.Models;
    using Microsoft.EntityFrameworkCore;

    public class ArticleService : IArticleService
    {
        private const int ItemsPerPage = 20;

        private readonly BlogDbContext dbContext;
        private readonly IMapper mapper;

        public ArticleService(BlogDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        public async Task<IEnumerable<ArticleListingServiceModel>> GetAll(int page)
            => await this.dbContext
                .Articles
                .Skip((page - 1) * ItemsPerPage)
                .Take(ItemsPerPage)
                .ProjectTo<ArticleListingServiceModel>(this.mapper.ConfigurationProvider)
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
                .ProjectTo<ArticleDetailServiceModel>(this.mapper.ConfigurationProvider)
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
