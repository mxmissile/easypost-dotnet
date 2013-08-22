using System.Collections.Generic;
using System.Net.Http;
using Easypost.Internal;
using Newtonsoft.Json;

namespace EasyPost.Model
{
    /// <summary>
    /// TODO
    /// </summary>
    public class ScanForm : EasyPostBase, IEncodable
    {
        public Address Address { get; set; }

        [JsonProperty("tracking_codes")]
        public List<string> TrackingCodes { get; set; }

        [JsonProperty("form_url")]
        public string FormUrl { get; set; }
        
        [JsonProperty("form_url_type")]
        public string FormUrlType { get; set; }

        public FormUrlEncodedContent AsFormUrlEncodedContent()
        {
            var collection = new CollectionBuilder()
                .AddRequired("scan_form[tracking_codes]".ToKvp(string.Join(",", TrackingCodes)));

            if (!string.IsNullOrEmpty(Address.Id))
            {
                collection.Add("scan_form[from_address][id]".ToKvp(Address.Id));
            }
            else
            {
                collection.AddRequired("scan_form[from_address][street1]".ToKvp(Address.Street1))
                    .AddRequired("scan_form[from_address][street2]".ToKvp(Address.Street2))
                    .AddRequired("scan_form[from_address][city]".ToKvp(Address.City))
                    .AddRequired("scan_form[from_address][state]".ToKvp(Address.State))
                    .AddRequired("scan_form[from_address][zip]".ToKvp(Address.Zip))
                    .Add("scan_form[from_address][country]".ToKvp(Address.Country))
                    .Add("scan_form[from_address][name]".ToKvp(Address.Name))
                    .Add("scan_form[from_address][company]".ToKvp(Address.Company))
                    .Add("scan_form[from_address][email]".ToKvp(Address.Email))
                    .Add("scan_form[from_address][phone]".ToKvp(Address.Phone));
            }

            return collection.AsFormUrlEncodedContent();
        }
    }
}