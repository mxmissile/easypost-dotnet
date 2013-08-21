using System.Net.Http;
using Newtonsoft.Json;

namespace Easypost
{
    public class CustomsItem : EasyPostBase, IEncodable
    {
        public string Description { get; set; }
        public int Quantity { get; set; }
        public int Value { get; set; }

        [JsonProperty("weight")]
        public int WeightOunces { get; set; }

        [JsonProperty("hs_tariff_number")]
        public string HsTariffNumber { get; set; }

        [JsonProperty("origin_country")]
        public string OriginCountry { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder()
                .AddRequired("customs_item[description]".ToKvp(Description))
                .AddRequired("customs_item[quantity]".ToKvp(Quantity))
                .AddRequired("customs_item[weight]".ToKvp(WeightOunces))
                .AddRequired("customs_item[value]".ToKvp(Value))
                .AddRequired("customs_item[hs_tariff_number]".ToKvp(HsTariffNumber))
                .AddRequired("customs_item[origin_country]".ToKvp(OriginCountry));

            return collection.AsFormUrlEncodedContent();
        }
    }
}
