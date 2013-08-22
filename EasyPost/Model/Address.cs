using System.Net.Http;
using Easypost.Internal;

namespace EasyPost.Model
{
    /// <summary>
    /// Address objects represent the origin and destination of any shipment, and are required in order to get rates and purchase postage.
    // The phone attribute, and either a name or company are required for addresses used as a "from" address in a shipment.
    /// </summary>
    public class Address : EasyPostBase, IEncodable
    {
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Country { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder().AddAddress("address", this);
            return collection.AsFormUrlEncodedContent();
        }
    }
}
