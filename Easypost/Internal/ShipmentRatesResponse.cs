using System.Collections.Generic;
using EasyPost;

namespace Easypost.Internal
{
    internal class ShipmentRatesResponse : ReponseBase
    {
        public List<CarrierRate> Rates { get; set; }
    }
}