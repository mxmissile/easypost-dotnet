using System.Net.Http;
using Easypost.Internal;

namespace EasyPost.Model
{
    /// <summary>
    /// Used to insure a shipment, simply the dollar amount.
    /// </summary>
    public class Insurance : IEncodable
    {
        public double Amount { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddRequired("amount".ToKvp(Amount));
            return collection.AsFormUrlEncodedContent();
        }
    }
}
