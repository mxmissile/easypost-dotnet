using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// CustomsInfo objects contain CustomsItem objects and all necessary information 
    /// for the generation of customs forms required for international shipping.
    /// </summary>
    public class CustomsInfo : EasyPostBase, IEncodable
    {
        [JsonProperty("customs_items")]
        public List<CustomsItem> CustomsItems { get; set; }

        [JsonProperty("eel_pfc")]
        public string EelPfc { get; set; }

        [JsonProperty("contents_type")]
        public string ContentsType { get; set; }

        [JsonProperty("contents_explanation")]
        public string ContentsExplanation { get; set; }

        [JsonProperty("customs_certify")]
        public bool CustomsCertify { get; set; }

        [JsonProperty("customs_signer")]
        public string CustomsSigner { get; set; }

        [JsonProperty("non_delivery_option")]
        public string NonDeliveryOption { get; set; }

        [JsonProperty("restriction_type")]
        public string RestrictionType { get; set; }

        [JsonProperty("restriction_comments")]
        public string RestrictionComments { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddCustomsInfo("customs_info", this);
            return collection.AsFormUrlEncodedContent();
        }
    }
}