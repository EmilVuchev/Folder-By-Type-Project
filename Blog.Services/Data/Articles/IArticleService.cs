namespace Blog.Services.Data.Articles
{
    using Common;
    using Models;

    public interface IArticleService : IService
    {
        Task<IEnumerable<ArticleListingServiceModel>> GetAll(int page);

        Task<int> Create(string title, string description, string authorId);

        Task<bool> Edit(int id, string title, string description);

        Task<ArticleDetailServiceModel> Details(int id);

        Task<bool> Delete(int id);

        Task<bool> Exists(int id, string userId);
    }
}
