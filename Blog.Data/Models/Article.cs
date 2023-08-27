namespace Blog.Data.Models
{
    using BaseModels;
    using System.ComponentModel.DataAnnotations;

    using static Blog.Data.DataValidations.Article;

    public class Article : Entity<int>
    {
        [Required]
        [MaxLength(MaxTitleLength)]
        public string Title { get; set; }

        [MaxLength(MaxDescriptionLength)]
        public string Description { get; set; }

        [Required]
        public string AuthorId { get; set; }

        public User Author { get; set; }
    }
}
