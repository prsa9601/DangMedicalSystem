using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Query.SiteEntity.DTOs;
using Query.SiteEntity.GetForMainPage;

namespace Facade.SitEntities
{
    public interface ISiteEntityFacade
    {
        Task<MainPageDto?> GetMainPage();
    }
    internal sealed class SiteEntityFacade : ISiteEntityFacade
    {
        private readonly IMediator _mediator;
        private readonly IMemoryCache _cache;

        public SiteEntityFacade(IMediator mediator, IMemoryCache cache)
        {
            _mediator = mediator;
            _cache = cache;
        }

        public async Task<MainPageDto?> GetMainPage()
        {
            var cacheOptions = new MemoryCacheEntryOptions()
                .SetAbsoluteExpiration(TimeSpan.FromMinutes(10))
                .SetSlidingExpiration(TimeSpan.FromMinutes(2));

            if (_cache.TryGetValue("MainPageContent", out MainPageDto? cachedResult))
            {
                if (cachedResult == null)
                {
                    var res = await _mediator.Send(new GetForMainPageQuery());
                    return res;
                }
                return cachedResult;
            }

            var result = await _mediator.Send(new GetForMainPageQuery());
            if (result != null)
            {
                _cache.Set("MainPageContent", result, cacheOptions);
            }
            return result;
        }
    }
}
