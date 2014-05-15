using System.Collections.Generic;

namespace Easypost.Internal
{
    internal static class CollectionExtensions
    {
        public static KeyValuePair<string, string> ToKvp(this string key, object value)
        {
            return new KeyValuePair<string, string>(key, value == null ? null : value.ToString());
        }

        public static KeyValuePair<string, string> ToKvp(this string key, string baseKey, object value)
        {
            return new KeyValuePair<string, string>(baseKey + key, value == null ? null : value.ToString());
        }
    }
}