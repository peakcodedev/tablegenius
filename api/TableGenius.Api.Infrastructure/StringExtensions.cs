namespace TableGenius.Api.Infrastructure;

public static class StringExtensions
{
    public static string GetStringBetweenIndexes(this string str, int startIndex, int endIndex)
    {
        return str.Substring(startIndex, endIndex - startIndex);
    }
}