using System.Collections.Generic;
using System.Net.Http;

namespace Easypost.Internal
{
    internal class CollectionBuilder
    {
        private readonly List<KeyValuePair<string, string>> _collection = new List<KeyValuePair<string, string>>();

        public CollectionBuilder AddRequired(KeyValuePair<string, string> kvp)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                _collection.Add(kvp);
            }

            return this;
        }

        public CollectionBuilder Add(KeyValuePair<string, string> kvp)
        {
            if (!string.IsNullOrWhiteSpace(kvp.Value))
            {
                _collection.Add(kvp);
            }

            return this;
        }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            return new FormUrlEncodedContent(_collection);
        }
    }
}