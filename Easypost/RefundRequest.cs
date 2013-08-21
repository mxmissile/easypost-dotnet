using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;

namespace EasyPost
{
    public class RefundRequest : IEncodable
    {
        public string Carrier { get; set; }
        public List<string> TrackingCodes { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder()
                .AddRequired("refund[carrier]".ToKvp(Carrier))
                .AddRequired("refund[tracking_codes]".ToKvp(string.Join(",", TrackingCodes)));

            return collection.AsFormUrlEncodedContent();
        }
    }
}