using System;
using System.Linq;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Model
{
    public class ShippingInfo
    {
        public string ShippingToCountry { get; set; }
        public string ShippingToZip { get; set; }
        public double TotalCartWeight { get; set; }
    }
}
