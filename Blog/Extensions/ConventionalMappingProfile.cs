namespace Blog.Extensions
{
    using AutoMapper;
    using Blog.Common.Mapping;

    public class ConventionalMappingProfile : Profile
    {
        public ConventionalMappingProfile()
        {
            var mapFromType = typeof(IMapFrom<>);
            var mapToType = typeof(IMapTo<>);
            var mapExplicitlyType = typeof(IMapExplicitly);

            var modelRegistrations = AppDomain
                .CurrentDomain
                .GetAssemblies()
                .Where(x => x.GetName().Name.StartsWith("Blog."))
                .SelectMany(a => a.GetExportedTypes())
                .Where(t => t.IsClass && !t.IsAbstract)
                .Select(t => new
                {
                    Type = t,
                    MapFrom = this.GetMappingType(t, mapFromType),
                    MapTo = this.GetMappingType(t, mapToType),
                    ExplicitMap = t
                        .GetInterfaces()
                        .Where(i => i == mapExplicitlyType)
                        .Select(i => (IMapExplicitly)Activator.CreateInstance(t))
                        .FirstOrDefault()
                })
                .ToList();

            foreach (var modelRegistration in modelRegistrations)
            {
                if (modelRegistration.MapFrom != null)
                {
                    this.CreateMap(modelRegistration.MapFrom, modelRegistration.Type);
                }

                if (modelRegistration.MapTo != null)
                {
                    this.CreateMap(modelRegistration.Type, modelRegistration.MapTo);
                }

                modelRegistration.ExplicitMap?.RegisterMappings(this);
            };
        }

        private Type GetMappingType(Type type, Type mappingInterface)
            => type
                .GetInterfaces()
                .FirstOrDefault(i =>
                    i.IsGenericType &&
                    i.IsGenericTypeDefinition.Equals(mappingInterface))
                ?.GetGenericArguments()
                .First();
    }
}
