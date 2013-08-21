using Newtonsoft.Json;

namespace Easypost
{
    public class BuyPostageLabelResponse : ReponseBase
    {
        [JsonProperty("postage_label")]
        public PostageLabel PostageLabel { get; set; }
    }
}