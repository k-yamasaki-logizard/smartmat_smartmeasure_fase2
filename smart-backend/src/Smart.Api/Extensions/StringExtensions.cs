namespace Smart.Api.Extensions;

public static class StringExtensions
{
    public static string EscapeCsvField(this string value) => string.IsNullOrEmpty(value) ? value : value.Replace("\"", "\"\"");
}