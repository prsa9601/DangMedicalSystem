using AutoMapper;

namespace Infrastructure.Mapping.Service
{
    public interface IMapperService
    {
        TDestination Map<TDestination>(object source);
        TDestination Map<TSource, TDestination>(TSource source);
        TDestination Map<TSource, TDestination>(TSource source, TDestination destination);
    }
    public class AutoMapperService : IMapperService
    {
        private readonly IMapper _mapper;

        public AutoMapperService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public TDestination Map<TDestination>(object source)
            => _mapper.Map<TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source)
            => _mapper.Map<TSource, TDestination>(source);

        public TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
            => _mapper.Map(source, destination);
    }
}
