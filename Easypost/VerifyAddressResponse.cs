namespace Easypost
{
    public class VerifyAddressResponse : ReponseBase
    {
        public Address Address { get; set; }
        public string Message { get; set; }

        public bool IsMissingInfo
        {
            get { return Address != null && !string.IsNullOrEmpty(Message); }
        }
    }
}