using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Locations.Configuration;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class CountryHelper
    {
        internal static List<CountryElement> GetCountriesList()
        {
            return Config.Get<LocationsConfig>().Countries.Values.Where(x => x.CountryIsActive).OrderBy(x => x.Name).ToList();
        }
    }
}
