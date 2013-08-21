using System;
using Newtonsoft.Json;

namespace Easypost
{
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