using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunSet.Extensions;

public static class LinqExtension
{
    public static string JoinToString<T>(this IEnumerable<T> source, Func<T, string> func, string separator = "")
    {
        if (source == null || !source.Any())
        {
            return string.Empty;
        }
        return string.Join(separator, source.Select(item => func(item)));
    }
}
