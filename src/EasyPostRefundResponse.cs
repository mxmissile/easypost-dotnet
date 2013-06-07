using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easypost
{
    public class EasyPostRefundResponse
    {
        public string id { get; set; }
        public string Object { get; set; }
        public string tracking_code { get; set; }
        public string confirmation_number { get; set; }
        public string carrier { get; set; }
        public string status { get; set; }
        public string shipment_id { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }

    }
}
