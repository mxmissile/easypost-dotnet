using System.Net.Http;

namespace Easypost
{
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
            var collection = new CollectionBuilder()
                    .AddRequired("address[street1]".ToKvp(Street1))
                    .AddRequired("address[street2]".ToKvp(Street2))
                    .AddRequired("address[city]".ToKvp(City))
                    .AddRequired("address[state]".ToKvp(State))
                    .AddRequired("address[zip]".ToKvp(Zip))
                    .Add("address[country]".ToKvp(Country))
                    .Add("address[name]".ToKvp(Name))
                    .Add("address[company]".ToKvp(Company))
                    .Add("address[email]".ToKvp(Email))
                    .Add("address[phone]".ToKvp(Phone));

            return collection.AsFormUrlEncodedContent();
        }
    }
}
