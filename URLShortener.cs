public interface IKeyGenerator 
{
    string GenerateKey();
}

public class KeyGenerator: IKeyGenerator
{
    public string GenerateKey()
    {
        var byteArray = Guid.NewGuid().ToByteArray();
        var stringRepresentation = Convert.ToBase64String(byteArray);
        return stringRepresentation.Replace("/", "_").Replace("=","").Replace("+","-");
    }  
}