using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    public class Parcel : EasyPostBase, IEncodable
    {
        [JsonProperty("weight")]
        public decimal WeightOunces { get; set; }

        [JsonProperty("height")]
        public decimal? HeightInches { get; set; }

        [JsonProperty("width")]
        public decimal? WidthInches { get; set; }

        [JsonProperty("length")]
        public decimal? LengthInches { get; set; }

        [JsonProperty("predefined_package")]
        public ParcelType? PredefinedPackage { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddParcel("parcel", this);
            return collection.AsFormUrlEncodedContent();
        }
    }
}