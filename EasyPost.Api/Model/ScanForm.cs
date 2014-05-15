using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// SCAN forms are USPS specific documents that can be created to speed up and simplify your carrier parcel pickup process. 
    /// The SCAN form is one document that can be scanned to mark all included tracking codes as "Accepted for Shipment" by the USPS.
    /// </summary>
    public class ScanForm : EasyPostBase, IEncodable
    {
        public Address Address { get; set; }

        [JsonProperty("tracking_codes")]
        public List<string> TrackingCodes { get; set; }

        [JsonProperty("form_url")]
        public string FormUrl { get; set; }
        
        [JsonProperty("form_url_type")]
        public string FormUrlType { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddScanForm("scan_form", this);
            return collection.AsFormUrlEncodedContent();
        }
    }
}