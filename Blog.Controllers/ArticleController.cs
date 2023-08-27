namespace Blog.Controllers
{
    using Blog.Controllers.Models.InputViewModels;
    using Blog.Services.Data.Articles;
    using Microsoft.AspNetCore.Mvc;

    public class ArticleController : Controller
    {
        private readonly IArticleService articleService;

        public ArticleController(IArticleService articleService)
            => this.articleService = articleService;

        public async Task<IActionResult> Index(int page = 1) 
        {
            var articles = await this.articleService.GetAll(page);
            return this.Ok(articles);
            //return View(articles);
        }

        public async Task<IActionResult> Create(CreateArticleInputModel input) 
        {
            await this.articleService.Create(input.Title, input.Description, input.AuthorId);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
