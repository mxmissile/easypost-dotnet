using System;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// Base class for all EasyPost objects.
    /// </summary>
    public abstract class EasyPostBase
    {
        public string Id { get; set; }
        public string Object { get; set; }
        
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}