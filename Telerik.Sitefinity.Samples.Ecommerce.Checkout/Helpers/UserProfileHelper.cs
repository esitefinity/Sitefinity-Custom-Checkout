using System;
using System.Linq;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.SitefinityExceptions;
using Telerik.Sitefinity.Security.Data;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class UserProfileHelper
    {
        internal static SitefinityProfile GetSitefinityProfileOfUser(User user)
        {
            UserProfileManager userProfileManager = UserProfileManager.GetManager();
            foreach (var manager in userProfileManager.Providers.Select(provider => UserProfileManager.GetManager(provider.Name)))
            {
                var sitefinityProfile = manager.GetUserProfile<SitefinityProfile>(user);
                if (sitefinityProfile != null)
                {
                    return sitefinityProfile;
                }
            }
            throw new ArgumentException("User information cannot be found in any of the providers");
        }

        internal static SitefinityProfile GetSitefinityProfileOfCurrentlyLoggedInUser()
        {
            return GetSitefinityProfileOfUser(SecurityManager.GetUser(SecurityManager.GetCurrentUserId()));
        }

        internal static Guid GetCurrentUserId()
        {
            return SecurityManager.GetCurrentUserId();
        }

        internal static CustomerProfile GetCustomerProfileOfUser(UserProfileManager profileManager, User user)
        {
            return profileManager.GetUserProfile<CustomerProfile>(user);
        }

        internal static CustomerProfile GetCustomerProfileOfCurrentUser(UserProfileManager profileManager)
        {
            return GetCustomerProfileOfUser(profileManager, SecurityManager.GetUser(SecurityManager.GetCurrentUserId()));
        }

        internal static void AssignCustomerToRoles(UserManager userManager, RoleManager roleManager, CatalogManager catalogManager, Guid userId, Order order)
        {
            using (new ElevatedModeRegion(roleManager))
            {
                bool associationsFound = false;
                foreach (OrderDetail detail in order.Details)
                {
                    var product = catalogManager.GetProduct(detail.ProductId);
                    if (product.AssociateBuyerWithRole != Guid.Empty)
                    {
                        var user = userManager.GetUser(userId);
                        try
                        {
                            var role = roleManager.GetRole(product.AssociateBuyerWithRole);
                            roleManager.AddUserToRole(user, role);
                            associationsFound = true;
                        }
                        catch (ItemNotFoundException)
                        {
                            // skip over the role if it no longer exists
                        }
                    }
                }

                if (associationsFound)
                {
                    roleManager.SaveChanges();
                }
            }
        }

        internal static Customer GetCustomerInfoOrCreateOneIfDoesntExsist(UserProfileManager userProfileManager, OrdersManager ordersManager, CheckoutState checkoutState)
        {
            User customerUser = null;
            CustomerProfile customerProfile = null;
            Customer customer = null;

            Guid userId = SecurityManager.CurrentUserId;
            if (userId != Guid.Empty)
            {
                customerUser = SecurityManager.GetUser(userId);
                if (customerUser != null)
                {

                    foreach (UserProfileDataProvider dataProvider in userProfileManager.Providers)
                    {
                        UserProfileManager subUserManager = UserProfileManager.GetManager(dataProvider.Name);
                        customerProfile = subUserManager.GetUserProfile<CustomerProfile>(customerUser);
                        if (customerProfile != null)
                        {
                            Guid guid = customerProfile.Id;
                            customer = ordersManager.GetCustomers().Where(c => c.ProfileId == guid).SingleOrDefault();
                        }
                    }

                }
            }

            if (customer == null)
            {
                // Customer does not exist in database, so create a new one
                customer = ordersManager.CreateCustomer();
                if (customerProfile != null)
                {
                    customer.ProfileId = customerProfile.Id;
                }
                else
                {
                    customer.CustomerFirstName = checkoutState.BillingFirstName;
                    customer.CustomerLastName = checkoutState.BillingLastName;
                    customer.CustomerEmail = checkoutState.BillingEmail;
                }
            }

            return customer;
        }
    }
}
