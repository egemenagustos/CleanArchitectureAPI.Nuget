using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICachableRequest
    {
        private readonly CacheSettings _cacheSettings;
        private readonly IDistributedCache _cache;

        public CachingBehavior(IDistributedCache cache, IConfiguration configuration)
        {
            _cacheSettings = configuration.GetSection("CacheSettings").Get<CacheSettings>() ?? throw new InvalidOperationException();
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(request.BypassCache)
            {
                return await next();
            }

            TResponse response;

            byte[]? cachedResponse = await _cache.GetAsync(request.CacheKey, cancellationToken);
            
            if(cachedResponse is not null)
            {
                response = JsonSerializer.Deserialize<TResponse>(Encoding.UTF8.GetString(cachedResponse));
            }
            else
            {
                response = await getResponseAndAddToCache(request,next,cancellationToken);
            }

            return response;
        }

        private async Task<TResponse?> getResponseAndAddToCache(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse response = await next();
            TimeSpan slidingExpiration = request.SlidingExpiration ?? TimeSpan.FromDays(1);

            DistributedCacheEntryOptions cacheOptions = new() { SlidingExpiration = slidingExpiration };
            byte[] serializeData = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response));

            await _cache.SetAsync(request.CacheKey, serializeData, cacheOptions, cancellationToken);

            return response;
        }
    }
}
