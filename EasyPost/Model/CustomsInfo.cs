using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// TODO
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
            var collection = new CollectionBuilder()
                .AddRequired("customs_info[customs_certify]".ToKvp(CustomsCertify.ToString()))
                .AddRequired("customs_info[contents_type]".ToKvp(ContentsType))
                .AddRequired("customs_info[contents_explanation]".ToKvp(ContentsExplanation))
                .AddRequired("customs_info[restriction_type]".ToKvp(RestrictionType))
                .AddRequired("customs_info[eel_pfc]".ToKvp(EelPfc))
                .Add("customs_info[customs_signer]".ToKvp(CustomsSigner))
                .Add("customs_info[non_delivery_option]".ToKvp(NonDeliveryOption))
                .Add("customs_info[restriction_comments]".ToKvp(RestrictionComments));

            for (var i = 0; i < CustomsItems.Count; i++)
            {
                collection.AddCustomsItem(string.Format("customs_info[customs_items][{0}]", i), CustomsItems[i]);
            }

            return collection.AsFormUrlEncodedContent();
        }
    }
}