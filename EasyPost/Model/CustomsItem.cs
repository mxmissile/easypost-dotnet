using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// TODO
    /// </summary>
    public class CustomsItem : EasyPostBase, IEncodable
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public double Value { get; set; }

        [JsonProperty("weight")]
        public int WeightOunces { get; set; }

        [JsonProperty("hs_tariff_number")]
        public string HsTariffNumber { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddCustomsItem("customs_item", this);
            return collection.AsFormUrlEncodedContent();
        }
    }
}
