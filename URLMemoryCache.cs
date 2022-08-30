using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

public class URLMemoryCache<TItem>
{
    private MemoryCache _cache = new MemoryCache(new MemoryCacheOptions());
    private readonly IConfiguration _configuration;
    private readonly int _slidingExpiration;
    private readonly int _absoluteExpiration;

    public URLMemoryCache(IConfiguration configuration)
    {
        _configuration = configuration;
        _slidingExpiration = _configuration.GetValue<int>("CacheSlidingExpiration");
        _absoluteExpiration = _configuration.GetValue<int>("CacheAbsoluteExpiration");

        Console.WriteLine("URLMemoryCache::[sliding : absolue] policy " + _slidingExpiration + " : " + _absoluteExpiration + " mins");
    }

    public TItem? GetCache(object key, Func<TItem?> getFromDB)
    {
        TItem? cacheEntry;
        if (!_cache.TryGetValue(key, out cacheEntry))
        {
            // Key not in cache, so get data from DB and then add to cache
            cacheEntry = getFromDB();
            if (cacheEntry != null) 
            {
                Console.WriteLine("URLMemoryCache::Found in DB, adding to cache");
                Cache(key, cacheEntry);    
            }                
        } 
        else 
        {
            Console.WriteLine("URLMemoryCache::Cache hit");
        }        

        return cacheEntry;
    }

    public TItem? Cache(object key, TItem? cacheEntry)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(_slidingExpiration))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(_absoluteExpiration));

        _cache.Set(key, cacheEntry, cacheOptions);
        return cacheEntry;
    }
}
