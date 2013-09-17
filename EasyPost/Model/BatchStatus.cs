using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// Contains counts for the shipment statuses.
    /// </summary>
    public class BatchStatus
    {
        [JsonProperty("created")]
        public int CreatedCount { get; set; }

        [JsonProperty("postage_purchased")]
        public int PostagePurchasedCount { get; set; }
        
        [JsonProperty("postage_purchase_failed")]
        public int PostagePurchaseFailedCount { get; set; }
    }
}