using System;
using System.Linq;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class Util
    {
        public static string GetSafeString(object o)
        {
            if (o == null)
            {
                return null;
            }
            return o.ToString();
        }
    }
}
