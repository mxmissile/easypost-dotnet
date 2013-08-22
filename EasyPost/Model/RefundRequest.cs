using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;

namespace EasyPost.Model
{
    /// <summary>
    /// USPS labels and postage can be refunded if requested within 10 days for domestic and international mail. 
    /// To qualify, the item must not have been scanned by the USPS and must be requested within 10 days of printing.
    /// UPS and FedEx shipments are paid for when they're processed, not when the label is generated, so refund requests for unused labels optional.
    /// </summary>
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