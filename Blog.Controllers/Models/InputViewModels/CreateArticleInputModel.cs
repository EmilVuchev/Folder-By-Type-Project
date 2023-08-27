namespace Blog.Controllers.Models.InputViewModels
{
    using System.ComponentModel.DataAnnotations;

    using static Blog.Data.DataValidations.Article;

    public class CreateArticleInputModel
    {
        [Required]
        [StringLength(MaxTitleLength, MinimumLength = MinTitleLength)]
        public string Title { get; set; }

        [StringLength(MaxDescriptionLength, MinimumLength = MinDescriptionLength)]
        public string Description { get; set; }

        public string AuthorId { get; set; }
    }
}
