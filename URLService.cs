
using System.Collections;

public interface IURLService {
    string AddURL(string longURL);
    string? GetURL(string shortURL);
}

public class URLService: IURLService
{
    private readonly IKeyGenerator _keyGenerator;
    private readonly URLMemoryCache<URLPair> _memoryCache;
    private readonly ShortURLContext _context;

    public URLService(IKeyGenerator urlGenerator, URLMemoryCache<URLPair> memoryCache, ShortURLContext context)
    {
        _keyGenerator = urlGenerator;
        _memoryCache = memoryCache;
        _context = context;
    }

    public string AddURL(string longURL)
    {
        var shortURL = _keyGenerator.GenerateKey();
        Console.WriteLine("URLService::AddURL " + shortURL + " : " + longURL);

        var entry = new URLPair(longURL, shortURL);
        _context.Add(entry);
        _context.SaveChanges();
        _memoryCache.Cache(shortURL, entry);

        return shortURL;
    }

    public string? GetURL(string shortURL)
    {
        string? longURL = _memoryCache.GetCache(shortURL, () => _context.URLPair.FirstOrDefault(u => u.ShortURL == shortURL))?.LongURL;
        Console.WriteLine("URLService::GetURL short URL " + shortURL + " = " + longURL);
        return longURL;
    }
}
