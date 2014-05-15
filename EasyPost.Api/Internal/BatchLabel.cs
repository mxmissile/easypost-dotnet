using System.Net.Http;
using Easypost.Internal;
using EasyPost.Model;

namespace EasyPost.Internal
{
    internal class BatchLabel : IEncodable
    {
        public BatchLabelFormat FileFormat { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddRequired("file_format".ToKvp(FileFormat.ToString().ToLower()));
            return collection.AsFormUrlEncodedContent();
        }
    }
}
