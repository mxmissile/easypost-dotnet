using EasyPost;
using EasyPost.Model;
using Newtonsoft.Json;

namespace Easypost.Internal
{
    internal class BuyPostageLabelResponse
    {
        [JsonProperty("postage_label")]
        public PostageLabel PostageLabel { get; set; }
    }
}