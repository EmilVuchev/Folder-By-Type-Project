namespace Blog.Services.Models
{
    using AutoMapper;
    using Blog.Data.Models;
    using Common.Mapping;

    public class ArticleListingServiceModel : IMapFrom<Article>, IMapExplicitly
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public void RegisterMappings(IProfileExpression profileExpression)
        {
            profileExpression
                .CreateMap<Article, ArticleListingServiceModel>()
                .ForMember(x => x.Author, cfg => cfg.MapFrom(y => y.Author.UserName));
        }
    }
}
