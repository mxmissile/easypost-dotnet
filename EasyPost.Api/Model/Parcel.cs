using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// Parcel objects represent the physical container being shipped. 
    /// Please provide either the length, width, and height dimensions, or a predefined package.
    /// </summary>
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

    /// <summary>
    /// Predefined package sizes for USPS, UPS and FedEx
    /// </summary>
    public enum ParcelType
    {
        // USPS
        Card,
        Letter,
        Flat,
        Parcel,
        LargeParcel,
        IrregularParcel,
        FlatRateEnvelope,
        FlatRateLegalEnvelope,
        FlatRatePaddedEnvelope,
        FlatRateGiftCardEnvelope,
        FlatRateWindowEnvelope,
        FlatRateCardboardEnvelope,
        SmallFlatRateEnvelope,
        SmallFlatRateBox,
        MediumFlatRateBox,
        LargeFlatRateBox,
        RegionalRateBoxA,
        RegionalRateBoxB,
        RegionalRateBoxC,
        LargeFlatRateBoardGameBox,

        // UPS
        UPSLetter,
        UPSExpressBox,
        UPS25kgBox,
        UPS10kgBox,
        Tube,
        Pak,
        Pallet,
        SmallExpressBox,
        MediumExpressBox,
        LargeExpressBox,

        // FedEx
        FedExEnvelope,
        FedExBox,
        FedExPak,
        FedExTube,
        FedEx10kgBox,
        FedEx25kg,
    }
}