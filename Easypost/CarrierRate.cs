using System.Net.Http;
using Newtonsoft.Json;

namespace Easypost
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