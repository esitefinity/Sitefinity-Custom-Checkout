using System;
using System.Linq;
using System.Web;
using Telerik.Sitefinity.Data;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Security.Model;
using Telerik.Sitefinity.SitefinityExceptions;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class UserProfileHelper
    {
        internal static SitefinityProfile GetSitefinityProfileOfUser(User user)
        {
            if (user == null)
            {
                return null;
            }
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
            UserManager um = UserManager.GetManager();
            User u = um.GetUser(HttpContext.Current.User.Identity.Name);
            if (u == null)
            {
                return Guid.Empty;
            }

            return u.Id;
            //return SecurityManager.GetCurrentUserId();
        }

        internal static User GetCurrentlyLoggedInUser()
        {
            return SecurityManager.GetUser(SecurityManager.GetCurrentUserId());
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
            var customerRetriever = new CustomerRetriever(ordersManager, userProfileManager);

            User customerUser = null;
            Customer customer = null;

            Guid userId = SecurityManager.CurrentUserId;
            if (userId != Guid.Empty)
            {
                customerUser = SecurityManager.GetUser(userId);
                if (customerUser != null)
                {

                    customer = customerRetriever.GetCustomer(customerUser, checkoutState);
                }
            }

            return customer;
        }
    }
}
