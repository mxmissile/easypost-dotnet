using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// Cost in USD that a carrier will charge for a shipment.
    /// </summary>
    public class CarrierRate : EasyPostBase, IEncodable
    {
        public string Carrier { get; set; }
        public string Service { get; set; }
        public double Rate { get; set; }

        [JsonProperty("shipment_id")]
        public string ShipmentId { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddRequired("rate[id]".ToKvp(Id));
            return collection.AsFormUrlEncodedContent();
        }
    }
}