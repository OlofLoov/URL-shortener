using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("")]
public class URLShortenerController : ControllerBase
{
    private readonly ILogger<URLShortenerController> _logger;
    private readonly IURLService _urlService;

    public URLShortenerController(ILogger<URLShortenerController> logger, IURLService cacheService)
    {
        Console.WriteLine("-- URLShortenerController Request");
        _logger = logger;
        _urlService = cacheService;
    }

    [HttpGet]
    [Route("url/{shortURL}")]
    public IActionResult GetURL(string shortURL)
    {   
        Console.WriteLine("URLShortenerController::GetURL " + shortURL); 
        string? longURL = _urlService.GetURL(shortURL);

        if (longURL == null)
            return NotFound();

        return Redirect(longURL);
    }

    [HttpPost]
    [Route("CreateShortURL")]    
    public IActionResult CreateShortURL([FromForm]string longURL)
    {
        Console.WriteLine("URLShortenerController::CreateShortURL " + longURL);
        var shortURL = _urlService.AddURL(longURL);
        return Redirect("/#" + shortURL); // redirect to base + add hash + return short url
    }
}


