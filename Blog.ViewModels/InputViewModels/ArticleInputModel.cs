namespace Blog.ViewModels.InputViewModels
{
    using Common.Mapping;
    using Data.Models;
    using System.ComponentModel.DataAnnotations;

    using static Blog.Data.DataValidations.Article;

    public class ArticleInputModel : IMapTo<Article>
    {
        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
        public string Description { get; set; }
    }
}
