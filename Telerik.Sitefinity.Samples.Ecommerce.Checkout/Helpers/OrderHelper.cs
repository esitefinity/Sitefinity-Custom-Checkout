using System;
using System.Linq;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Ecommerce.Payment.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class OrderHelper
    {
        private static readonly object orderNumberLock = new object();

        internal static Tuple<bool, IPaymentResponse> PlaceOrder(OrdersManager ordersManager, CatalogManager catalogManager, UserManager userManager, RoleManager roleManager, UserProfileManager userProfileManager, CheckoutState checkoutState, Guid cartOrderId, string zip, decimal shipPrice)
        {
            CartOrder cartOrder = ordersManager.GetCartOrder(cartOrderId);

            decimal tTotal = 0;
            foreach (var de in cartOrder.Details)
            {
                de.TaxRate = CartHelper.GetTaxList(zip);
                cartOrder.EffectiveTaxRate = de.TaxRate;
                var priceWithTax = de.Price * de.TaxRate;
                tTotal += priceWithTax;
                string s = String.Format("Tax Rate {0} Price {1} Total {2} Zip {3}", de.TaxRate, de.Price, tTotal, zip);
                JMABase.WriteLogFile(s, "/ecommercelog.txt");
            }

            cartOrder.ShippingTaxRate = cartOrder.Details[0].TaxRate;
            cartOrder.ShippingTax = shipPrice * cartOrder.ShippingTaxRate;
            cartOrder.ShippingTotal = shipPrice + cartOrder.ShippingTax;
            cartOrder.Tax = tTotal;
            string aa = String.Format("Ship Tax Rate {0} Items Tax {1} Total of Items {2} Ship Total {3} SubTotal", cartOrder.ShippingTaxRate, cartOrder.Tax, cartOrder.Total, cartOrder.ShippingTotal, cartOrder.SubTotalDisplay);
            JMABase.WriteLogFile(aa, "/ecommercelog.txt");
            cartOrder.Total = cartOrder.ShippingTotal + cartOrder.Tax + cartOrder.Total;
            cartOrder.Addresses.Clear();
            cartOrder.Payments.Clear();

            //set the default currency of the order
            string defaultCurrency = Config.Get<EcommerceConfig>().DefaultCurrency;
            cartOrder.Currency = defaultCurrency;

            // set the shipping address
            CartAddress shippingAddress = CartHelper.GetShippingAddressFromCheckoutState(ordersManager, checkoutState);

            cartOrder.Addresses.Add(shippingAddress);

            // set the billing address
            CartAddress billingAddress = CartHelper.GetBillingAddressFromCheckoutState(ordersManager, checkoutState);

            cartOrder.Addresses.Add(billingAddress);

            //Get the first payment method in the shop

            // set the payment
            CartPayment payment = CartHelper.GetCartPaymentFromCheckoutState(ordersManager, checkoutState);

            cartOrder.Payments.Add(payment);

            ordersManager.SaveChanges();


            // Get current customer or create new one

            Customer customer = UserProfileHelper.GetCustomerInfoOrCreateOneIfDoesntExsist(userProfileManager,ordersManager, checkoutState);

            // Save the customer address
            CustomerAddressHelper.SaveCustomerAddressOfCurrentUser(checkoutState, customer);
            
            //Use the API to checkout
            IPaymentResponse paymentResponse = ordersManager.Checkout(cartOrderId, checkoutState, customer);

            // record the "success" state of the checkout
            checkoutState.IsPaymentSuccessful = paymentResponse.IsSuccess;

            Order order = ordersManager.GetOrder(cartOrderId);
            
            //Increment the order
            IncrementOrderNumber(ordersManager, order);

            // add the order to customer
            customer.Orders.Add(order);

            // Update the order
            order.Customer = customer;
           
            ordersManager.SaveChanges();

            if (!paymentResponse.IsSuccess)
            {
                return new Tuple<bool, IPaymentResponse>(false, paymentResponse);
            }

            //JMABase.WriteLogFile("Order Status: " + order.OrderStatus.ToString(), "/ecommercelog.txt");

            if (order.OrderStatus == OrderStatus.Paid)
            {
                //JMABase.WriteLogFile("Sending email...", "/ecommercelog.txt");
                UserProfileHelper.AssignCustomerToRoles(userManager, roleManager, catalogManager, SecurityManager.GetCurrentUserId(), order);
                EmailHelper.SendOrderPlacedEmailToClientAndMerchant(cartOrder, checkoutState, order.OrderNumber);
            }

            return new Tuple<bool, IPaymentResponse>(true, paymentResponse);
        }

        private static void IncrementOrderNumber(OrdersManager ordersManager, Order order)
        {
            if (order.OrderNumber == 0)
            {
                lock (orderNumberLock)
                {
                    NextOrderNumber orderNumber = ordersManager.GetNextOrderNumber();
                    order.OrderNumber = orderNumber.Next;
                    ordersManager.IncrementNextOrderNumber();
                }
            }
        }
    }
}
