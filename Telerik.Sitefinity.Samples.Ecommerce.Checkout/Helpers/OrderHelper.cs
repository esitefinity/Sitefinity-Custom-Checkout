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

        internal static Tuple<bool, IPaymentResponse> PlaceOrder(OrdersManager ordersManager, CatalogManager catalogManager, UserManager userManager, RoleManager roleManager, UserProfileManager userProfileManager, CheckoutState checkoutState, Guid cartOrderId)
        {
            CartOrder cartOrder = ordersManager.GetCartOrder(cartOrderId);
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

            if (order.OrderStatus == OrderStatus.Paid)
            {
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
