using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// Batches allow you to create a large number of shipments at one time,
    /// and retrieve details and shipping labels for them all at once.
    /// </summary>
    public class Batch : EasyPostBase, IEncodable
    {
        [JsonProperty("label_url")]
        public string LabelUrl { get; set; }
        public BatchStatus Status { get; set; }
        public List<BatchShipment> Shipments { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            if (!string.IsNullOrEmpty(Id))
            {
                return new CollectionBuilder().Add("batch[id]".ToKvp(Id)).AsFormUrlEncodedContent();
            }

            var collection = new CollectionBuilder();

            if (Shipments != null)
            {
                for (var i = 0; i < Shipments.Count; i++)
                {
                    var keyBase = string.Format("batch[shipment][{0}]", i);
                    collection.AddShipment(keyBase, Shipments[i]);
                    collection.Add("[carrier]".ToKvp(keyBase, Shipments[i].Carrier));
                    collection.Add("[service]".ToKvp(keyBase, Shipments[i].Service));
                }
            }

            return collection.AsFormUrlEncodedContent();
        }
    }

    /// <summary>
    /// File format for a BatchLabel
    /// </summary>
    public enum BatchLabelFormat
    {
        Pdf,
        Epl2,
    }
}
