namespace Blog.Services.Data.Articles
{
    using Common;
    using Models;
    using ViewModels.InputViewModels;

    public interface IArticleService : IService
    {
        Task<IEnumerable<ArticleListingServiceModel>> GetAll(int page);
        
        Task<int> Create(ArticleInputModel input);

        Task<bool> Edit(int id, string title, string description);

        Task<ArticleDetailServiceModel> Details(int id);

        Task<bool> Delete(int id);

        Task<bool> Exists(int id, string userId);
    }
}
