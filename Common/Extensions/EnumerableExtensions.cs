using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    /// <summary>
    /// Extensions for <see cref="IEnumerable"/>
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Determine whether the enumerable is null or contains no elements
        /// </summary>
        /// <typeparam name="T">The type of enumerable</typeparam>
        /// <param name="enumerable">The enumerable</param>
        /// <returns>True when null or empty</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            if (enumerable is null)
            {
                return true;
            }

            // If this is a list, use the Count property for efficiency.
            // The Count property is O(1) while IEnumerable.Count() is O(N)
            return enumerable is ICollection<T> collection ? collection.Count < 1 : !enumerable.Any();
        }
    }
}
