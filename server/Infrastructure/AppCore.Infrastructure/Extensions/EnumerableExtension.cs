using System.Collections;

namespace AppCore.Infrastructure.Extensions
{
    public static class EnumerableExtension
    {
        public static bool IsEmpty<T>(this IEnumerable<T> items)
        {
            return !items.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> items)
        {
            return items.Any();
        }

        public static bool IsNotNullNorEmpty<T>(this IEnumerable<T> items)
        {
            return items != null && items.Any();
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            var array = items.ToArray();
            foreach (T t in array)
            {
                action?.Invoke(t);
            }
            return array;
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> items, Action<T, int> action)
        {
            var array = items.ToArray();

            for (int i = 0; i < array.Length; i++)
            {
                action?.Invoke(array[i], i);
            }

            return array;
        }

        public static bool ContainsAny<T>(this IEnumerable<T> items, params T[] values)
        {
            var list = items.ToArray();
            foreach (T value in values)
            {
                if (list.Contains(value))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool ContainsAll<T>(this IEnumerable<T> items, params T[] values)
        {
            var list = items.ToArray();
            foreach (T value in values)
            {
                if (!list.Contains(value))
                {
                    return false;
                }
            }

            return true;
        }

        public static Hashtable ToHashtable(this IDictionary dictionary)
        {
            return new Hashtable(dictionary);
        }

        public static IList<T> ToNotNullList<T>(this IEnumerable<T> items)
        {
            return items?.ToList() ?? new List<T>();
        }
    }
}
