namespace SunSet.Extensions;

public static class StringExtension
{
    public static string SFormat(this string str, params object[] args)
    {
        if (args == null || args.Length == 0)
        {
            return str;
        }
        return string.Format(str, args);
    }
}
