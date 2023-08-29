﻿namespace Blog.Controllers
{
    using Extensions;
    using Services.Data.Articles;
    using Models.InputViewModels;
    using Microsoft.AspNetCore.Authorization;
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

        [HttpGet]
        [Authorize]
        public IActionResult Create()
            => this.View();

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ArticleInputModel input)
        {
            if (ModelState.IsValid)
            {
                var userId = this.User.GetUserId();
                var id = await this.articleService.Create(input.Title, input.Description, userId);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            return this.View(input);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var userId = this.User.GetUserId();
            var articleExists = await this.articleService.Exists(id, userId);

            if (!articleExists)
            {
                return this.NotFound();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ArticleInputModel input)
        {
            var userId = this.User.GetUserId();
            var articleExists = await this.articleService.Exists(id, userId);

            if (!articleExists)
            {
                return this.NotFound();
            }

            if (ModelState.IsValid)
            {
                var isEdited = this.articleService.Edit(id, input.Title, input.Description);
                return this.RedirectToAction(nameof(Details), new { id });
            }

            return this.View(input);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var articleDetails = await this.articleService.Details(id);

            if (articleDetails == null)
            {
                return this.NotFound();
            }

            return this.View(articleDetails);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Delete(int id) 
        {
            var userId = this.User.GetUserId();
            var articleExist = await this.articleService.Exists(id, userId);

            if (!articleExist)
            {
                return this.NotFound();
            }

            return this.View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> ConfirmDelete(int id) 
        {
            var userId = this.User.GetUserId();
            var articleExist = await this.articleService.Exists(id, userId);

            if (!articleExist)
            {
                return this.NotFound();
            }

            await this.articleService.Delete(id);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
