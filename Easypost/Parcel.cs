using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost
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
            if (!string.IsNullOrEmpty(Id))
            {
                return new CollectionBuilder().Add("parcel[id]".ToKvp(Id)).AsFormUrlEncodedContent();
            }

            var collection = new CollectionBuilder()
                .Add("parcel[weight]".ToKvp(WeightOunces));

            if (PredefinedPackage == null)
            {
                collection.Add("parcel[length]".ToKvp(LengthInches.ToString()))
                    .Add("parcel[width]".ToKvp(WidthInches.ToString()))
                    .Add("parcel[height]".ToKvp(HeightInches.ToString()));
            }
            else
            {
                collection.Add("parcel[predefined_package]".ToKvp(PredefinedPackage.Value.ToString()));
            }

            return collection.AsFormUrlEncodedContent();
        }
    }
}