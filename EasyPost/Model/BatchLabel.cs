using System.Net.Http;
using Easypost.Internal;

namespace EasyPost.Model
{
    /// <summary>
    /// Retrieve the postage labels for the entire batch in one file, in 'pdf' or 'epl2' format.
    /// This can only be done once all shipments in the batch are in 'postage_purchased' status. 
    /// Batch label generation is asyncronous, so polling the batch object for the presense of a non-empty label_url is recommended.
    /// </summary>
    public class BatchLabel : IEncodable
    {
        public BatchLabelFormat FileFormat { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddRequired("file_format".ToKvp(FileFormat.ToString().ToLower()));
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
