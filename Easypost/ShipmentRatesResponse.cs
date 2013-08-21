using System.Collections.Generic;

namespace Easypost
{
    public class ShipmentRatesResponse : ReponseBase
    {
        public List<CarrierRate> Rates { get; set; }
    }
}