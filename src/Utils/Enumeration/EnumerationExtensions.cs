using System.Collections.Generic;
using System.Linq;

namespace Utils.Enumeration
{
    public static class EnumerationExtensions
    {
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
            => self?.Select((item, index)
            => (item, index))
            ?? Enumerable.Empty<(T, int)>();
    }
}
