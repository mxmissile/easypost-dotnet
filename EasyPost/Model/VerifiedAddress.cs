namespace EasyPost.Model
{
    /// <summary>
    /// TODO
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