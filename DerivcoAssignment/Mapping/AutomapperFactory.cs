using AutoMapper;

namespace DerivcoAssignment.Web.Mapping
{
    public static class AutomapperFactory
    {
        public static IMapper CreateAndConfigure()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<GeneratorProfile>();
            });

            return new Mapper(config);
        }
    }
}
