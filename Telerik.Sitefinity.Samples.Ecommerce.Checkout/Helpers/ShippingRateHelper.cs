using System;
using System.Collections.Generic;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Shipping.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Shipping;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Shipping.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Samples.Ecommerce.Checkout.Model;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class ShippingRateHelper
    {
        internal static object GetShippingRates(ShippingManager shippingManager, OrdersManager ordersManager, Guid shoppingCartId,ShippingInfo shippingInfo)
        {
            var availableShippingMethods = new List<IShippingResponse>();

            foreach (var shippingCarrier in Config.Get<ShippingConfig>().ShippingCarrierProviders.Values.Where(x => x.IsActive))
            {
                var carrierProvider = shippingManager.GetShippingCarrierProvider(shippingCarrier.Name);

                IShippingRequest genericShippingRequest = GenerateShippingRequest(shippingInfo);

                genericShippingRequest.CartOrder = CartHelper.GetCartOrder(ordersManager, shoppingCartId);

                ShippingResponseContext shippingContext = carrierProvider.GetServiceRates(genericShippingRequest);

                if (shippingContext.ShippingResponses != null)
                {
                    availableShippingMethods.AddRange(shippingContext.ShippingResponses);
                }
            }

            return availableShippingMethods.OrderBy(x => x.SortOrder).ToList();
        }

        private static IShippingRequest GenerateShippingRequest(ShippingInfo shippingInfo)
        {
            EcommerceConfig config = Config.Get<EcommerceConfig>();
            var request = new GenericShippingRequest(config.ShippingFromCountry,
                config.ShippingFromZip,
                shippingInfo.ShippingToCountry,
                shippingInfo.ShippingToZip,
                config.DefaultBoxLength,
                config.DefaultBoxWidth,
                config.DefaultBoxHeight,
                shippingInfo.TotalCartWeight
            );
            
            return request;
        }
    }
}
