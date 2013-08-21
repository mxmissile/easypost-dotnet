using System.Collections.Generic;
using System.Net.Http;

namespace Easypost
{
    public class RefundRequest : IEncodable
    {
        public string Carrier { get; set; }
        public IEnumerable<string> TrackingCodes { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder()
                .AddRequired("refund[carrier]".ToKvp(Carrier))
                .AddRequired("refund[tracking_codes]".ToKvp(string.Join(",", TrackingCodes)));

            return collection.AsFormUrlEncodedContent();
        }
    }
}