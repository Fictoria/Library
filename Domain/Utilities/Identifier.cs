namespace Fictoria.Domain.Utilities;

public class Identifier
{
    // https://stackoverflow.com/a/1344258/636680
    public static string Random(int length = 6)
    {
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var stringChars = new char[length];
        var random = new Random();

        for (int i = 0; i < stringChars.Length; i++)
        {
            stringChars[i] = chars[random.Next(chars.Length)];
        }

        return new String(stringChars);
    }
}