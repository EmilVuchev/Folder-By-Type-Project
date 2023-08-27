namespace Blog.Services.Data.Articles
{
    using Common;
    using Models;

    public interface IArticleService : IService
    {
        Task<IEnumerable<ArticleListingServiceModel>> GetAll(int page);

        Task<int> Create(string title, string description, string authorId);
    }
}
