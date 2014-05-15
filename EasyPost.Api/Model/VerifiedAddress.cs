namespace EasyPost.Model
{
    /// <summary>
    /// Address representing closest verified address.
    /// In some cases the address provided will be valid but will be missing some data, such as the apartment number. 
    /// For these cases we return an address as well as a "message" field.
    /// </summary>
    public class VerifiedAddress
    {
        public Address Address { get; set; }
        public string Message { get; set; }

        public bool IsMissingInfo
        {
            get { return Address != null && !string.IsNullOrEmpty(Message); }
        }
    }
}