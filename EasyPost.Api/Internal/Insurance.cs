using System.Net.Http;
using Easypost.Internal;

namespace EasyPost.Internal
{
    internal class Insurance : IEncodable
    {
        public double Amount { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddRequired("amount".ToKvp(Amount));
            return collection.AsFormUrlEncodedContent();
        }
    }
}
