using System.ComponentModel.DataAnnotations;

public class URLPair
{
    [Key]
    public string ShortURL {get; set;}
    public string LongURL {get; set;}

    public URLPair(string longURL, string shortURL)
    {
        LongURL = longURL;
        ShortURL = shortURL;
    }        
}