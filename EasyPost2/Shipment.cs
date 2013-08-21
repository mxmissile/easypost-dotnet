using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost
{
    public class Shipment : EasyPostBase, IEncodable
    {
        [JsonProperty("to_address")]
        public Address ToAddress { get; set; }

        [JsonProperty("from_address")]
        public Address FromAddress { get; set; }

        public Parcel Parcel { get; set; }

        [JsonProperty("customs_info")]
        public CustomsInfo CustomsInfo { get; set; }

        [JsonProperty("scan_form")]
        public ScanForm ScanForm { get; set; }

        public List<CarrierRate> Rates { get; set; }

        // set after purchasing
        [JsonProperty("selected_rate")]
        public CarrierRate SelectedRate { get; set; }

        // available after 'buy' has completed
        [JsonProperty("postage_label")]
        public PostageLabel PostageLabel { get; set; }

        public string Mode { get; set; }
        public string Reference { get; set; }
        public double? Insurance { get; set; }

        [JsonProperty("batch_status")]
        public string BatchStatus { get; set; }

        [JsonProperty("batch_message")]
        public string BatchMessage { get; set; }

        [JsonProperty("refund_status")]
        public string RefundStatus { get; set; }

        [JsonProperty("tracking_code")]
        public string TrackingCode { get; set; }
        
        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder()
                .AddRequired("shipment[to_address][id]".ToKvp(ToAddress.Id))
                .AddRequired("shipment[from_address][id]".ToKvp(FromAddress.Id))
                .AddRequired("shipment[parcel][id]".ToKvp(Parcel.Id));

            if (CustomsInfo != null)
            {
                collection.Add("shipment[customs_info][id]".ToKvp(CustomsInfo.Id));
            }

            if (ScanForm != null)
            {
                collection.Add("shipment[scan_form][id]".ToKvp(ScanForm.Id));
            }

            return collection.AsFormUrlEncodedContent();
        }
    }
}