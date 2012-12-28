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
        internal static object GetShippingRates(ShippingManager shippingManager, OrdersManager ordersManager, Guid shoppingCartId, ShippingInfo shippingInfo)
        {
            //var availableShippingMethods = new List<IShippingResponse>();
            //JMABase.WriteLogFile("SH Count: " + Config.Get<ShippingConfig>().ShippingCarrierProviders.Values.Count().ToString(), "/importErrorLog.txt");
            //foreach (var shippingCarrier in Config.Get<ShippingConfig>().ShippingCarrierProviders.Values.Where(x => x.IsActive))
            //{
            //    var carrierProvider = shippingManager.GetShippingCarrierProvider(shippingCarrier.Name);
            //    JMABase.WriteLogFile("Carrier Name: " + shippingCarrier.Name, "/importErrorLog.txt");
            //    IShippingRequest genericShippingRequest = GenerateShippingRequest(shippingInfo);
            //    JMABase.WriteLogFile("Shipping Country: " + shippingInfo.ShippingToCountry, "/importErrorLog.txt");
            //    JMABase.WriteLogFile("Shipping zip: " + shippingInfo.ShippingToZip, "/importErrorLog.txt");

            //    JMABase.WriteLogFile("Cart weight: " + shippingInfo.TotalCartWeight, "/importErrorLog.txt");

            //    //genericShippingRequest.CartOrder = CartHelper.GetCartOrder(ordersManager, shoppingCartId);
            //    genericShippingRequest.CartOrder = ordersManager.GetCartOrder(shoppingCartId);
            //    ShippingResponseContext shippingContext = carrierProvider.GetServiceRates(genericShippingRequest);
            //    JMABase.WriteLogFile("Cart details: " + genericShippingRequest.CartOrder.Details.Count(), "/importErrorLog.txt");
            //    JMABase.WriteLogFile("Shipping context error: " + shippingContext.ErrorMessage, "/importErrorLog.txt");
            //    JMABase.WriteLogFile("Shipping responses: " + shippingContext.ShippingResponses.Count(), "/importErrorLog.txt");

            //    if (shippingContext.ShippingResponses != null)
            //    {
            //        availableShippingMethods.AddRange(shippingContext.ShippingResponses);
            //    }
            //}

            //                return availableShippingMethods.OrderBy(x => x.SortOrder).ToList();

            List<ShippingMethod> methodsList = new List<ShippingMethod>();
            var order = CartHelper.GetCartOrder(ordersManager, shoppingCartId);
            foreach (var a in shippingManager.GetShippingMethods().OrderBy(a => a.SortOrder))
            {
                string[] sht = a.ShippingLimitToDisplay.Replace("total price:", "").Trim().Split('-');
                decimal hLimit = 0;
                decimal.TryParse(sht[1], out hLimit);
                decimal lLimit = 0;
                decimal.TryParse(sht[0], out lLimit);
                if (hLimit <= order.Total && order.Total >= lLimit)
                {
                    methodsList.Add(a);
                }
            }

            return methodsList;

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
