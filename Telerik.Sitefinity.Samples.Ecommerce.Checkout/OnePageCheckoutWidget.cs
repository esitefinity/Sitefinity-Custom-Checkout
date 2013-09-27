using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Ecommerce.Payment.Model;
using Telerik.Sitefinity.Ecommerce.Shipping.Model;
using Telerik.Sitefinity.Locations;
using Telerik.Sitefinity.Modules.Ecommerce;
using Telerik.Sitefinity.Modules.Ecommerce.BusinessServices;
using Telerik.Sitefinity.Modules.Ecommerce.Catalog;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration.Contracts;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;
using Telerik.Sitefinity.Modules.Ecommerce.Shipping;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers;
using Telerik.Sitefinity.Samples.Ecommerce.Checkout.Model;
using Telerik.Sitefinity.Security;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Sitefinity.Web.UI.Fields;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout
{
    [ControlDesigner(typeof(OnePageCheckoutWidgetDesigner))]
    public class OnePageCheckoutWidget : SimpleScriptView, IOrdersControl, IEcommerceControl
    {

        protected override string LayoutTemplateName
        {
            get
            {
                return OnePageCheckoutWidget.layoutTemplateName;
            }
        }

        protected override Type ResourcesAssemblyInfo
        {
            get
            {
                return typeof(OnePageCheckoutWidget);
            }
        }

        public Guid ReceiptPageId
        {
            get { return this.receiptPageId; }
            set { this.receiptPageId = value; }
        }

        protected virtual HtmlGenericControl WidgetStatus
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("widgetStatus", false);
            }
        }

        protected virtual Label WidgetStatusMessage
        {
            get
            {
                return this.Container.GetControl<Label>("widgetStatusMessage", false);
            }
        }

        protected virtual HtmlGenericControl Widget
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("widget", false);
            }
        }

        protected virtual TextField FirstNameShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("firstNameShipping", false);
            }
        }

        protected virtual TextField LastNameShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("lastNameShipping", false);
            }
        }

        protected virtual TextField CompanyShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("companyShipping", false);
            }
        }

        protected virtual TextField EmailShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("emailShipping", false);
            }
        }

        protected virtual TextField Address1Shipping
        {
            get
            {
                return this.Container.GetControl<TextField>("address1Shipping", false);
            }
        }

        protected virtual TextField Address2Shipping
        {
            get
            {
                return this.Container.GetControl<TextField>("address2Shipping", false);
            }
        }

        protected virtual TextField CityShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("cityShipping", false);
            }
        }

        protected virtual TextField PhoneNumberShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("phoneNumberShipping", false);
            }
        }

        protected virtual TextField ZipShipping
        {
            get
            {
                return this.Container.GetControl<TextField>("zipShipping", false);
            }
        }

        protected virtual TextField FirstNameBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("firstNameBilling", false);
            }
        }

        protected virtual TextField LastNameBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("lastNameBilling", false);
            }
        }

        protected virtual TextField CompanyBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("companyBilling", false);
            }
        }

        protected virtual TextField EmailBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("emailBilling", false);
            }
        }

        protected virtual TextField Address1Billing
        {
            get
            {
                return this.Container.GetControl<TextField>("address1Billing", false);
            }
        }

        protected virtual TextField Address2Billing
        {
            get
            {
                return this.Container.GetControl<TextField>("address2Billing", false);
            }
        }

        protected virtual TextField CityBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("cityBilling", false);
            }
        }

        protected virtual TextField PhoneNumberBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("phoneNumberBilling", false);
            }
        }

        protected virtual TextField ZipBilling
        {
            get
            {
                return this.Container.GetControl<TextField>("zipBilling", false);
            }
        }

        protected virtual CheckBox BillingAddressSameAsShipping
        {
            get
            {
                return this.Container.GetControl<CheckBox>("useShippingAddressAsBillingAddress", false);
            }
        }

        protected virtual RadComboBox CountryShipping
        {
            get
            {
                return this.Container.GetControl<RadComboBox>("countryShipping", false);
            }
        }


        protected virtual RadComboBox StateShipping
        {
            get
            {
                return this.Container.GetControl<RadComboBox>("stateShipping", false);
            }
        }

        protected virtual RadComboBox CountryBilling
        {
            get
            {
                return this.Container.GetControl<RadComboBox>("countryBilling", false);
            }
        }


        protected virtual RadComboBox StateBilling
        {
            get
            {
                return this.Container.GetControl<RadComboBox>("stateBilling", false);
            }
        }

        protected virtual RadioButtonList ShippingMethodsList
        {
            get
            {
                return this.Container.GetControl<RadioButtonList>("shippingMethodsList", false);
            }
        }

        protected virtual RadGrid ShoppingCartGrid
        {
            get
            {
                return this.Container.GetControl<RadGrid>("shoppingCartGrid", false);
            }
        }

        protected virtual IButtonControl PlaceOrderButton
        {
            get
            {
                return this.Container.GetControl<IButtonControl>("placeOrderButton", false);
            }
        }

        protected virtual ChoiceField CreditCards
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("creditCards", false);
            }
        }

        protected virtual TextField CreditCardNumber
        {
            get
            {
                return this.Container.GetControl<TextField>("creditCardNumber", false);
            }
        }

        protected virtual TextField CardHolderName
        {
            get
            {
                return this.Container.GetControl<TextField>("cardHolderName", false);
            }
        }

        protected virtual ChoiceField CardExpirationMonth
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("cardExpirationMonth", false);
            }
        }

        protected virtual ChoiceField CardExpirationYear
        {
            get
            {
                return this.Container.GetControl<ChoiceField>("cardExpirationYear", false);
            }
        }

        protected virtual TextField SecurityCode
        {
            get
            {
                return this.Container.GetControl<TextField>("securityCode", false);
            }
        }

        protected virtual CheckBox UseBillingAddressAsShippingAddress
        {
            get
            {
                return this.Container.GetControl<CheckBox>("useBillingAddressAsShippingAddress", false);
            }
        }

        public virtual HtmlGenericControl ShippingForm
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("shippingForm", false);
            }
        }

        public virtual HtmlGenericControl BillingForm
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("billingForm", false);
            }
        }

        public virtual HtmlGenericControl UseBillingAddressAsShippingAddressPanel
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("useBillingAddressAsShippingAddressPanel", false);
            }
        }

        public virtual HtmlGenericControl ShippingOptionsForm
        {
            get
            {
                return this.Container.GetControl<HtmlGenericControl>("shippingOptionsForm", false);
            }
        }

        protected virtual Panel PaymentProblemPanel
        {
            get
            {
                return this.Container.GetControl<Panel>("paymentProblemPanel", true);
            }
        }

        protected virtual Message MessageControl
        {
            get
            {
                return this.Container.GetControl<Message>("message", true);
            }
        }

        protected OrdersManager OrdersManager
        {
            get
            {
                if (this.ordersManager == null)
                    this.ordersManager = OrdersManager.GetManager();
                return this.ordersManager;
            }
        }

        protected CatalogManager CatalogManager
        {
            get
            {
                if (this.catalogManager == null)
                    this.catalogManager = CatalogManager.GetManager();
                return this.catalogManager;
            }
        }

        protected ShippingManager ShippingManager
        {
            get
            {
                if (this.shippingManager == null)
                    this.shippingManager = ShippingManager.GetManager();
                return this.shippingManager;
            }
        }

        protected UserProfileManager UserProfileManager
        {
            get
            {
                if (this.userProfileManager == null)
                    this.userProfileManager = UserProfileManager.GetManager();
                return this.userProfileManager;
            }
        }

        protected RoleManager RoleManager
        {
            get
            {
                if (this.roleManager == null)
                    this.roleManager = RoleManager.GetManager();
                return this.roleManager;
            }
        }

        protected UserManager UserManager
        {
            get
            {
                if (this.userManager == null)
                    this.userManager = UserManager.GetManager();
                return this.userManager;
            }
        }

        protected ShippingInfo ShippingInfo
        {
            get;
            set;
        }

        protected override void InitializeControls(GenericContainer container)
        {
            if (CheckIfConfiguredShowErrorIfNot())
            {
                HideShippingRelatedInfoIfCartDoesntHaveShippableItems();
                List<CountryElement> countries = CountryHelper.GetCountriesList();
                BindCountryDropDown(countries, CountryShipping);
                BindCountryDropDown(countries, CountryBilling);
                BindStateDropDown(StateShipping);
                BindStateDropDown(StateBilling);
                BindUserProfileInformation();
                BindShippingMethods(ShippingMethodsList);
                BindPreviewGrid(ShoppingCartGrid);
                AttachPlacerOrderButtonEvent();
            }
        }

        private void HideShippingRelatedInfoIfCartDoesntHaveShippableItems()
        {
            bool cartHasShippableItems = CartHelper.DoesCartHaveAnyShippableItems(this.OrdersManager, this.GetShoppingCartId());
            if (!cartHasShippableItems)
            {
                ShippingForm.Visible = false;
                UseBillingAddressAsShippingAddressPanel.Visible = false;
                ShippingOptionsForm.Visible = false;
                BillingForm.Visible = true;
            }
        }

        private bool CheckIfConfiguredShowErrorIfNot()
        {
            Guid shoppingCartId = this.GetShoppingCartId();
            if (shoppingCartId == Guid.Empty)
            {
                return ShowErrorMessageAndHidePanels("You cannot checkout without having anything in the cart");
            }
            else if (CartHelper.GetCartOrder(this.OrdersManager, shoppingCartId).Details.Count == 0)
            {
                return ShowErrorMessageAndHidePanels("You cannot checkout with no items in the cart");
            }
            else if (UserProfileHelper.GetCurrentUserId() == Guid.Empty)
            {
                return ShowErrorMessageAndHidePanels("You have to login to use checkout");
            }
            return true;
        }

        private bool ShowErrorMessageAndHidePanels(string message)
        {
            WidgetStatus.Visible = true;
            Widget.Visible = false;
            WidgetStatusMessage.Text = message;
            return false;
        }
        protected virtual void PlaceOrderButton_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                CheckoutState checkoutState = GetCheckoutState();
                Guid cartId = this.GetShoppingCartId();
                IPaymentResponse response = OrderHelper.PlaceOrder(this.OrdersManager, this.CatalogManager, this.UserManager, this.RoleManager, this.UserProfileManager, checkoutState, cartId);

                if (!response.IsOffsitePayment)
                {
                    if (!response.IsSuccess) // in case of unsuccessful direct payment
                    {
                        // Order was declined
                        this.PaymentProblemPanel.Visible = true;
                        this.MessageControl.ShowNegativeMessage(response.GatewayResponse);
                        return;
                    }
                    CleanUp();
                }
                //leave the cart for offsite payments ones a notification came it will become paid.

                PageNode pageNode = App.WorkWith().Page(this.ReceiptPageId).Get();

                string fullUrl = this.GetPageNodeUrlForCurrentPageCulture(pageNode);

                fullUrl += "/order/" + cartId;
                this.Page.Response.Redirect(fullUrl);
            }
        }

        private bool ValidateInput()
        {
            var isValid = true;

            isValid = this.FirstNameBilling.IsValid() && isValid;
            isValid = this.LastNameBilling.IsValid() && isValid;
            isValid = this.EmailBilling.IsValid() && isValid;
            isValid = this.Address1Billing.IsValid() && isValid;
            isValid = this.CityBilling.IsValid() && isValid;
            isValid = this.ZipBilling.IsValid() && isValid;

            if (UseBillingAddressAsShippingAddress != null && !UseBillingAddressAsShippingAddress.Checked)
            {
                isValid = this.FirstNameShipping.IsValid() && isValid;
                isValid = this.LastNameShipping.IsValid() && isValid;
                isValid = this.EmailShipping.IsValid() && isValid;
                isValid = this.Address1Shipping.IsValid() && isValid;
                isValid = this.CityShipping.IsValid() && isValid;
                isValid = this.ZipShipping.IsValid() && isValid;
            }

            isValid = this.CreditCardNumber.IsValid() && isValid;
            isValid = this.CreditCardNumber.IsValid() && isValid;
            isValid = this.CardHolderName.IsValid() && isValid;
            isValid = this.SecurityCode.IsValid() && isValid;

            return isValid;
        }

        private CheckoutState GetCheckoutState()
        {
            CheckoutState checkoutState = new CheckoutState();

            checkoutState.BillingFirstName = FirstNameBilling.Value.ToString();
            checkoutState.BillingLastName = LastNameBilling.Value.ToString();
            checkoutState.BillingAddress1 = Address1Billing.Value.ToString();
            checkoutState.BillingAddress2 = Address2Billing.Value.ToString();
            checkoutState.BillingCity = CityBilling.Value.ToString();
            checkoutState.BillingCountry = CountryBilling.SelectedValue.ToString();
            checkoutState.BillingState = StateBilling.SelectedValue.ToString();
            checkoutState.BillingZip = ZipBilling.Value.ToString();
            checkoutState.BillingPhoneNumber = PhoneNumberBilling.Value.ToString();
            checkoutState.BillingCompany = CompanyBilling.Value.ToString();
            checkoutState.BillingEmail = EmailBilling.Value.ToString();



            if (UseBillingAddressAsShippingAddress != null && !UseBillingAddressAsShippingAddress.Checked)
            {
                checkoutState.ShippingFirstName = FirstNameShipping.Value.ToString();
                checkoutState.ShippingLastName = LastNameShipping.Value.ToString();
                checkoutState.ShippingAddress1 = Address1Shipping.Value.ToString();
                checkoutState.ShippingAddress2 = Util.GetSafeString(Address2Shipping.Value);
                checkoutState.ShippingCity = CityShipping.Value.ToString();
                checkoutState.ShippingCountry = CountryShipping.SelectedValue.ToString();
                checkoutState.ShippingState = StateShipping.SelectedValue.ToString();
                checkoutState.ShippingZip = ZipShipping.Value.ToString();
                checkoutState.ShippingPhoneNumber = Util.GetSafeString(PhoneNumberShipping.Value);
                checkoutState.ShippingCompany = Util.GetSafeString(CompanyShipping.Value);
                checkoutState.ShippingEmail = EmailShipping.Value.ToString();
            }
            else
            {
                checkoutState.ShippingFirstName = FirstNameBilling.Value.ToString();
                checkoutState.ShippingLastName = LastNameBilling.Value.ToString();
                checkoutState.ShippingAddress1 = Address1Billing.Value.ToString();
                checkoutState.ShippingAddress2 = Util.GetSafeString(Address2Billing.Value);
                checkoutState.ShippingCity = CityBilling.Value.ToString();
                checkoutState.ShippingCountry = CountryBilling.SelectedValue.ToString();
                checkoutState.ShippingState = StateBilling.SelectedValue.ToString();
                checkoutState.ShippingZip = ZipBilling.Value.ToString();
                checkoutState.ShippingPhoneNumber = Util.GetSafeString(PhoneNumberBilling.Value);
                checkoutState.ShippingCompany = Util.GetSafeString(CompanyBilling.Value);
                checkoutState.ShippingEmail = EmailBilling.Value.ToString();
            }


            checkoutState.CreditCardInfo = new CreditCardInfo();
            checkoutState.CreditCardInfo.CreditCardNumber = CreditCardNumber.Value.ToString();
            checkoutState.CreditCardInfo.CreditCardCardholderName = CardHolderName.Value.ToString();
            checkoutState.CreditCardInfo.CreditCardExpirationMonth = Convert.ToInt32(CardExpirationMonth.Value.ToString());
            checkoutState.CreditCardInfo.CreditCardExpirationYear = Convert.ToInt32(CardExpirationYear.Value.ToString());
            checkoutState.CreditCardInfo.CreditCardSecurityCode = SecurityCode.Value.ToString();
            checkoutState.CreditCardInfo.CreditCardType = CreditCardType.Visa; //for now

            //get the first payment. The example doesn't support the case with multiple payment methods.
            PaymentMethod paymentMethod = this.OrdersManager.GetPaymentMethods().Where(pm => pm.IsActive && pm.PaymentMethodType == PaymentMethodType.PaymentProcessor).First();
            if (paymentMethod == null)
            {
                throw new ArgumentException("Please configure a payment method");
            }

            checkoutState.PaymentMethodId = paymentMethod.Id;

            return checkoutState;
        }

        private void CleanUp()
        {
            var cartOrder = this.OrdersManager.GetCartOrder(this.GetShoppingCartId());
            this.OrdersManager.DeleteCartOrder(cartOrder);
            this.OrdersManager.SaveChanges();
            this.OrdersManager.DeleteOrphanedCartAddresses();
            this.OrdersManager.SaveChanges();
            this.RemoveShoppingCartCookie();
        }

        private void AttachPlacerOrderButtonEvent()
        {
            this.PlaceOrderButton.Click += this.PlaceOrderButton_Click;
        }

        private void BindUserProfileInformation()
        {
            var userBillingAndShippingInformation = CustomerAddressHelper.GetCustomerBillingAndShippingInfo(UserProfileHelper.GetCurrentlyLoggedInUser(), this.OrdersManager, this.UserProfileManager);
            if (userBillingAndShippingInformation != null)
            {
                FirstNameBilling.Value = userBillingAndShippingInformation.ShippingFirstName;
                LastNameBilling.Value = userBillingAndShippingInformation.ShippingFirstName;
                EmailBilling.Value = userBillingAndShippingInformation.ShippingEmail;
                CompanyBilling.Value = userBillingAndShippingInformation.ShippingCompany;
                Address1Billing.Value = userBillingAndShippingInformation.ShippingAddress1;
                Address2Billing.Value = userBillingAndShippingInformation.ShippingAddress2;
                CityBilling.Value = userBillingAndShippingInformation.ShippingCity;
                CountryBilling.SelectedValue = userBillingAndShippingInformation.ShippingCountry;
                StateBilling.SelectedValue = userBillingAndShippingInformation.ShippingState;
                ZipBilling.Value = userBillingAndShippingInformation.ShippingZip;
                PhoneNumberBilling.Value = userBillingAndShippingInformation.ShippingPhoneNumber;

                double totalWeight = CartHelper.GetTotalWeightOfCart(this.OrdersManager, this.GetShoppingCartId());
                this.ShippingInfo = new ShippingInfo { ShippingToCountry = userBillingAndShippingInformation.ShippingCountry, ShippingToZip = userBillingAndShippingInformation.ShippingZip, TotalCartWeight = totalWeight };
            }
        }

        private void BindPreviewGrid(RadGrid shoppingCartGrid)
        {
            shoppingCartGrid.NeedDataSource += new Telerik.Web.UI.GridNeedDataSourceEventHandler(ShoppingCartGrid_NeedDataSource);
        }

        protected void ShoppingCartGrid_NeedDataSource(object sender, Telerik.Web.UI.GridNeedDataSourceEventArgs e)
        {
            ShoppingCartGrid.DataSource = CartHelper.GetCartOrder(this.OrdersManager, this.GetShoppingCartId()).Details;
        }

        private void BindShippingMethods(RadioButtonList shippingMethodsList)
        {
            this.ShippingMethodsList.Items.Clear();
            foreach (var shippingMethod in this.AvailableShippingMethods)
            {
                var servicePrice = shippingMethod.ServicePrice;
                
                var selectedCurrency = EcommerceSettings.Currencies.DefaultCurrency;
                CurrencyContract selectedCurrencyElement = EcommerceSettings.Currencies.SupportedCurrencies.Values.Where(ca => ca.Name == selectedCurrency).FirstOrDefault();
                CultureInfo cultureInfo = CultureInfo.InvariantCulture;
                if (selectedCurrencyElement != null)
                {
                    cultureInfo = new CultureInfo(selectedCurrencyElement.Culture);
                }
                
                var servicePriceFormatted = String.Format(cultureInfo,"{0:C}", servicePrice);
                this.ShippingMethodsList.Items.Add(new ListItem(shippingMethod.ServiceName + "<span class='servicePrice'> " + servicePriceFormatted + "</span></b>", shippingMethod.ServiceCode));
            }
            this.ShippingMethodsList.SelectedIndex = 0;

            shippingMethodsList.SelectedIndexChanged +=  ShippingMethodsListSelectedIndexChanged;
        }

        /// <summary>
        /// Gets the collection of available shipping methods.
        /// </summary>
        private IList<IShippingResponse> AvailableShippingMethods
        {
            get
            {
                if (this.availableShippingMethods == null)
                {
                    CheckoutState checkoutState = new CheckoutState();
                    checkoutState.ShippingCountry = this.ShippingInfo.ShippingToCountry;
                    checkoutState.ShippingZip = this.ShippingInfo.ShippingToZip;
                    checkoutState.Total = (decimal)this.ShippingInfo.TotalCartWeight;

                    this.availableShippingMethods = EcommerceBusinessServicesFactory.GetShippingMethodService().GetApplicableShippingMethods(checkoutState, ordersManager.GetCartOrder(this.GetShoppingCartId())).ToList();
                }
                return this.availableShippingMethods;
            }
        }

        /// <summary>
        /// each time when the shipping method is changed populated the cart with the shipping information
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ShippingMethodsListSelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectShippingMethod(this.ShippingMethodsList.SelectedValue);
        }

        /// <summary>
        /// populate the cart with the selected shipping method id and service code. Otherwise the validation for shipping method will fail.
        /// </summary>
        /// <param name="serviceCode"></param>
        private void SelectShippingMethod(string serviceCode)
        {
            var shippingMethod = this.AvailableShippingMethods.Where(sm => sm.ServiceCode == serviceCode).Single();
            var cart = CartHelper.GetCartOrder(this.OrdersManager, this.GetShoppingCartId());
            cart.ShippingMethodId = shippingMethod.ShippingMethodId;
            cart.ShippingServiceCode = shippingMethod.ServiceCode;
            cart.ShippingServiceName = shippingMethod.ServiceName;
            this.OrdersManager.SaveChanges();
        }

        private void BindStateDropDown(RadComboBox stateRadComboBox)
        {
            stateRadComboBox.LoadStatesAndProvinces();
        }

        private void BindCountryDropDown(List<CountryElement> countries, RadComboBox countryRadComboBox)
        {
            countryRadComboBox.DataSource = countries;
            countryRadComboBox.DataTextField = "Name";
            countryRadComboBox.DataValueField = "IsoCode";
            countryRadComboBox.DataBind();
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            var descriptors = new List<ScriptDescriptor>();
            var descriptor = new ScriptControlDescriptor(typeof(OnePageCheckoutWidget).FullName, this.ClientID);
            descriptor.AddProperty("_showBillingForm", true);
            descriptors.Add(descriptor);

            return new[] { descriptor };
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>()
            {
                new ScriptReference(OnePageCheckoutWidget.onePageCheckoutWidgetScripts, typeof(OnePageCheckoutWidget).Assembly.FullName),
            };
            return scripts;
        }

        private static readonly string layoutTemplateName = "Telerik.Sitefinity.Samples.Ecommerce.Checkout.Resources.OnePageCheckoutWidget.ascx";
        private static readonly string onePageCheckoutWidgetScripts = "Telerik.Sitefinity.Samples.Ecommerce.Checkout.Resources.OnePageCheckoutWidget.js";

        private CatalogManager catalogManager;
        private OrdersManager ordersManager;
        private ShippingManager shippingManager;
        private UserProfileManager userProfileManager;
        private UserManager userManager;
        private RoleManager roleManager;

        private IList<IShippingResponse> availableShippingMethods;

        private Guid receiptPageId;
    }
}
