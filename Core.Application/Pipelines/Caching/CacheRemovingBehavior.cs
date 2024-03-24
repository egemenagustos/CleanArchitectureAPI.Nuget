﻿using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text;
using System.Text.Json;

namespace Core.Application.Pipelines.Caching
{
    public class CacheRemovingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>, ICacheRemoverRequest
    {
        private readonly IDistributedCache _cache;

        public CacheRemovingBehavior(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request.BypassCache)
            {
                return await next();
            }

            TResponse response = await next();

            if(request.CacheKey is not null)
            {
                await _cache.RemoveAsync(request.CacheKey,cancellationToken);
            }

            if(request.CacheGroupKey is not null)
            {
                byte[]? cachedGroup = await _cache.GetAsync(request.CacheGroupKey, cancellationToken);
                if(cachedGroup is not null)
                {
                    HashSet<string> keysInGroup = JsonSerializer.Deserialize<HashSet<string>>(Encoding.UTF8.GetString(cachedGroup));
                    foreach(string key in keysInGroup)
                    {
                        await _cache.RemoveAsync(key, cancellationToken);
                    }
                    await _cache.RemoveAsync(request.CacheGroupKey, cancellationToken);
                    await _cache.RemoveAsync(key: $"{request.CacheGroupKey}SlidingExpiration", cancellationToken);
                }
            }
            return response;
        }
    }
}
