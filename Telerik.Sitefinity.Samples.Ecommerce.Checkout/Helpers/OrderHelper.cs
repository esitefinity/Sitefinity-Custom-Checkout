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

        internal static void PlaceOrder(OrdersManager ordersManager, CatalogManager catalogManager, UserManager userManager, RoleManager roleManager, UserProfileManager userProfileManager, CheckoutState checkoutState, Guid cartOrderId)
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
            if (Config.Get<EcommerceConfig>().BypassPaymentProcessing)
            {
                order.OrderStatus = OrderStatus.Pending;
            }
            else
            {
                order.OrderStatus = OrderStatus.Declined;
                if (paymentResponse.IsSuccess)
                {
                    if (paymentResponse.IsAuthorizeOnlyTransaction)
                    {
                        //If it the transaction is successful and the request is only for authorize then mark the order as authorized
                        order.OrderStatus = OrderStatus.Authorized;
                    }
                    else
                    {
                        order.OrderStatus = checkoutState.PaymentMethodType == PaymentMethodType.Offline ? OrderStatus.Pending : OrderStatus.Paid;
                    }
                }
            }
            ordersManager.SaveChanges();



            if (order.OrderStatus == OrderStatus.Paid)
            {
                UserProfileHelper.AssignCustomerToRoles(userManager, roleManager, catalogManager, SecurityManager.GetCurrentUserId(), order);
                EmailHelper.SendOrderPlacedEmailToClientAndMerchant(cartOrder, checkoutState, order.OrderNumber);
            }

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
