using System;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class CartHelper
    {
        internal static CartOrder GetCartOrder(OrdersManager ordersManager, Guid shoppingCartId)
        {
            return ordersManager.GetCart(shoppingCartId);
        }

        internal static CartAddress GetShippingAddressFromCheckoutState(OrdersManager ordersManager, CheckoutState checkoutState)
        {
            CartAddress shippingAddress = ordersManager.CreateCartAddress();
            shippingAddress.FirstName = checkoutState.ShippingFirstName;
            shippingAddress.LastName = checkoutState.ShippingLastName;
            shippingAddress.Email = checkoutState.ShippingEmail;
            shippingAddress.Address = checkoutState.ShippingAddress1;
            shippingAddress.Address2 = checkoutState.ShippingAddress2;
            shippingAddress.AddressType = AddressType.Shipping;
            shippingAddress.City = checkoutState.ShippingCity;
            shippingAddress.PostalCode = checkoutState.ShippingZip;
            shippingAddress.StateRegion = checkoutState.ShippingState;
            shippingAddress.Country = checkoutState.ShippingCountry;
            shippingAddress.Phone = checkoutState.ShippingPhoneNumber;
            shippingAddress.Email = checkoutState.ShippingEmail;
            shippingAddress.Company = checkoutState.ShippingCompany;

            return shippingAddress;
        }

        internal static CartAddress GetBillingAddressFromCheckoutState(OrdersManager ordersManager, CheckoutState checkoutState)
        {
            CartAddress billingAddress = ordersManager.CreateCartAddress();
            billingAddress.FirstName = checkoutState.BillingFirstName;
            billingAddress.LastName = checkoutState.BillingLastName;
            billingAddress.Email = checkoutState.BillingEmail;
            billingAddress.Address = checkoutState.BillingAddress1;
            billingAddress.Address2 = checkoutState.BillingAddress2;
            billingAddress.AddressType = AddressType.Billing;
            billingAddress.City = checkoutState.BillingCity;
            billingAddress.PostalCode = checkoutState.BillingZip;
            billingAddress.StateRegion = checkoutState.BillingState;
            billingAddress.Country = checkoutState.BillingCountry;
            billingAddress.Phone = checkoutState.BillingPhoneNumber;
            billingAddress.Email = checkoutState.BillingEmail;
            billingAddress.Company = checkoutState.BillingCompany;

            return billingAddress;
        }

        internal static CartPayment GetCartPaymentFromCheckoutState(OrdersManager ordersManager, CheckoutState checkoutState)
        {
            CartPayment payment = ordersManager.CreateCartPayment();
            payment.CreditCardCustomerName = checkoutState.CreditCardInfo.CreditCardCardholderName;
            payment.CreditCardExpireMonth = Convert.ToInt32(checkoutState.CreditCardInfo.CreditCardExpirationMonth);
            payment.CreditCardExpireYear = Convert.ToInt32(checkoutState.CreditCardInfo.CreditCardExpirationYear);
            payment.CreditCardNumberLastFour = string.Empty;
            payment.CreditCardNumber = string.Empty;
            payment.CreditCardType = checkoutState.CreditCardInfo.CreditCardType;

            payment.PaymentMethodId = checkoutState.PaymentMethodId;
            payment.PaymentMethodType = checkoutState.PaymentMethodType;

            return payment;
        }

        internal static double GetTotalWeightOfCart(OrdersManager ordersManager, Guid shoppingCartId)
        {
            return GetCartOrder(ordersManager, shoppingCartId).Details.ToList().Sum(od => od.Weight);
        }

        internal static bool DoesCartHaveAnyShippableItems(OrdersManager ordersManager, Guid shoppingCartId)
        {
            return GetCartOrder(ordersManager, shoppingCartId).Details.ToList().Where(od => od.IsShippable).Count() > 0;
        }
    }
}
