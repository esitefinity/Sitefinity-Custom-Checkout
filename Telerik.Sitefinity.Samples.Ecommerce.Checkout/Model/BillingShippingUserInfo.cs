using System;
using System.Linq;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Model
{
    public class BillingShippingUserInfo
    {
        #region Shipping address

        /// <summary>
        /// Gets or sets the first name of the destination address.
        /// </summary>
        public string ShippingFirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name of the destination address.
        /// </summary>
        public string ShippingLastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first line of the destination address.
        /// </summary>
        public string ShippingAddress1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the second line of the destination address.
        /// </summary>
        public string ShippingAddress2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the shipping destination city.
        /// </summary>
        public string ShippingCity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the shipping destination country code.
        /// </summary>
        public string ShippingCountry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the shipping destination country full name.
        /// </summary>
        public string ShippingCountryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the shipping destination state.
        /// </summary>
        public string ShippingState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the shipping destination zip code.
        /// </summary>
        public string ShippingZip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number of the shipping destination.
        /// </summary>
        public string ShippingPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the company of the shipping destination.
        /// </summary>
        public string ShippingCompany
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email of the shipping destination.
        /// </summary>
        public string ShippingEmail
        {
            get;
            set;
        }
        #endregion

        #region Billing address

        /// <summary>
        /// Gets or sets the first name of the billing address.
        /// </summary>
        public string BillingFirstName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the last name of the billing address.
        /// </summary>
        public string BillingLastName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the first line of the billing address.
        /// </summary>
        public string BillingAddress1
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the second line of the billing address.
        /// </summary>
        public string BillingAddress2
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the city of the billing destination.
        /// </summary>
        public string BillingCity
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country code of the billing destination.
        /// </summary>
        public string BillingCountry
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the country name of the billing destination.
        /// </summary>
        public string BillingCountryName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the state of the billing destination.
        /// </summary>
        public string BillingState
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the zip code of the billing destination.
        /// </summary>
        public string BillingZip
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the phone number of the billing destination.
        /// </summary>
        public string BillingPhoneNumber
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the company of the billing destination.
        /// </summary>
        public string BillingCompany
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the email of the billing destination.
        /// </summary>
        public string BillingEmail
        {
            get;
            set;
        }
        #endregion

    }
}
