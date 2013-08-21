using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost
{
    public class CarrierRate : EasyPostBase, IEncodable
    {
        public string Carrier { get; set; }
        public string Service { get; set; }
        public decimal Rate { get; set; }

        [JsonProperty("shipment_id")]
        public string ShipmentId { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            return new CollectionBuilder()
                .AddRequired("rate[id]".ToKvp(Id))
                .AsFormUrlEncodedContent();
        }
    }
}