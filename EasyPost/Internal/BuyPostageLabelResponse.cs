using EasyPost;
using Newtonsoft.Json;

namespace Easypost.Internal
{
    internal class BuyPostageLabelResponse : ReponseBase
    {
        [JsonProperty("postage_label")]
        public PostageLabel PostageLabel { get; set; }
    }
}