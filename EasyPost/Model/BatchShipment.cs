namespace EasyPost.Model
{
    /// <summary>
    /// Special Shipment object for batch shipments, which allows
    /// specifying a carrier and service for a batch.
    /// </summary>
    public class BatchShipment : Shipment
    {
        public string Carrier { get; set; }
        public string Service { get; set; }
    }
}