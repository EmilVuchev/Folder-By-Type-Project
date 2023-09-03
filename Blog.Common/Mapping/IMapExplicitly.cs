using AutoMapper;

namespace Blog.Common.Mapping
{
    public interface IMapExplicitly
    {
        void RegisterMappings(IProfileExpression profileExpression);
    }
}
