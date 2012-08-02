using System;
using System.Linq;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Samples.Ecommerce.Checkout.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class CustomerAddressHelper
    {
        internal static void SaveCustomerAddressOfCurrentUser(CheckoutState checkoutState, Customer customer)
        {
            checkoutState.SaveCustomerAddress(UserProfileHelper.GetCurrentlyLoggedInUser(), customer);
        }

        internal static BillingShippingUserInfo LoadDataFromSitefinityProfile()
        {
            User user = SecurityManager.GetUser(SecurityManager.GetCurrentUserId());
            if (user != null)
            {
                var sitefinityProfile = UserProfileHelper.GetSitefinityProfileOfUser(user);
                if (sitefinityProfile != null)
                {
                    var billingShippingUserInfo = new BillingShippingUserInfo();

                    billingShippingUserInfo.BillingFirstName = sitefinityProfile.FirstName;
                    billingShippingUserInfo.BillingLastName = sitefinityProfile.LastName;
                    billingShippingUserInfo.ShippingFirstName = sitefinityProfile.FirstName;
                    billingShippingUserInfo.ShippingLastName = sitefinityProfile.LastName;
                    billingShippingUserInfo.BillingEmail = sitefinityProfile.User.Email;
                    billingShippingUserInfo.ShippingEmail = sitefinityProfile.User.Email;

                    return billingShippingUserInfo;
                }
            }
            return null;
        }

        internal static CustomerAddress GetPrimaryCustomerAddress(IQueryable<CustomerAddress> customerAddresses, AddressType addressType)
        {
            if (!customerAddresses.Any())
            {
                return null;
            }

            CustomerAddress customerBillingAddress = customerAddresses.FirstOrDefault(ca => ca.AddressType == addressType && ca.IsPrimary);
            if (customerBillingAddress == null)
            {
                // There is no primary address. Get the most recent address.
                customerBillingAddress = customerAddresses.Where(ca => ca.AddressType == AddressType.Billing).OrderByDescending(ca => ca.LastModified).First();
            }

            return customerBillingAddress;
        }

        internal static BillingShippingUserInfo GetCustomerBillingAndShippingInfo(User user, OrdersManager ordersManager, UserProfileManager userProfileManager)
        {
            if (user == null)
            {
                return null; //don't do anything
            }


            CustomerRetriever customerRetriever = new CustomerRetriever(ordersManager, userProfileManager);

            Customer customer = customerRetriever.GetCustomerOfUser(user);

            if (customer == null)
            {
                return LoadDataFromSitefinityProfile();
            }

            IQueryable<CustomerAddress> customerAddresses = ordersManager.GetPrimaryCustomerAddressesByCustomerId(customer.Id);

            if (customerAddresses.Count() == 0)
            {
                return LoadDataFromSitefinityProfile();
            }

            var billingShippingInfo = new BillingShippingUserInfo();

            CustomerAddress customerBillingAddress = GetPrimaryCustomerAddress(customerAddresses, AddressType.Billing);

            billingShippingInfo.BillingFirstName = customerBillingAddress.FirstName;
            billingShippingInfo.BillingLastName = customerBillingAddress.LastName;
            billingShippingInfo.BillingCompany = customerBillingAddress.Company;
            billingShippingInfo.BillingEmail = customerBillingAddress.Email;
            billingShippingInfo.BillingAddress1 = customerBillingAddress.Address;
            billingShippingInfo.BillingAddress2 = customerBillingAddress.Address2;
            billingShippingInfo.BillingCity = customerBillingAddress.City;
            billingShippingInfo.BillingCountry = customerBillingAddress.Country;
            billingShippingInfo.BillingState = customerBillingAddress.StateRegion;
            billingShippingInfo.BillingZip = customerBillingAddress.PostalCode;
            billingShippingInfo.BillingPhoneNumber = customerBillingAddress.Phone;

            CustomerAddress customerShippingAddress = GetPrimaryCustomerAddress(customerAddresses, AddressType.Shipping);
            if (customerShippingAddress == null)
            {
                customerShippingAddress = customerBillingAddress;
            }
            billingShippingInfo.ShippingFirstName = customerShippingAddress.FirstName;
            billingShippingInfo.ShippingLastName = customerShippingAddress.LastName;
            billingShippingInfo.ShippingCompany = customerShippingAddress.Company;
            billingShippingInfo.ShippingEmail = customerShippingAddress.Email;
            billingShippingInfo.ShippingAddress1 = customerShippingAddress.Address;
            billingShippingInfo.ShippingAddress2 = customerShippingAddress.Address2;
            billingShippingInfo.ShippingCity = customerShippingAddress.City;
            billingShippingInfo.ShippingCountry = customerShippingAddress.Country;
            billingShippingInfo.ShippingState = customerShippingAddress.StateRegion;
            billingShippingInfo.ShippingZip = customerShippingAddress.PostalCode;
            billingShippingInfo.ShippingPhoneNumber = customerShippingAddress.Phone;

            return billingShippingInfo;

        }
    }
}
