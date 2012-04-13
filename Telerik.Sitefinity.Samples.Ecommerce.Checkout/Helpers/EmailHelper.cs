using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Services;
using System.Net.Mail;
using System.Net;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class EmailHelper
    {
        internal static void SendOrderPlacedEmailToClientAndMerchant(CartOrder cartOrder, CheckoutState checkoutState,int orderNumber)
        {
            var messageBody = GetEmailMessageBody(cartOrder, checkoutState);
            if (string.IsNullOrEmpty(messageBody))
            {
                return;
            }

            string fromAddress = Config.Get<EcommerceConfig>().MerchantEmail;
            string subject = String.Format(Res.Get<OrdersResources>("OrderEmailSubject"), orderNumber);
            SendEmail(fromAddress, checkoutState.BillingEmail, subject, messageBody, true); 

        }
        private static string GetEmailMessageBody(CartOrder cartOrder, CheckoutState checkoutSate)
        {
            Guid templateId = new Guid("f949cccb-c337-4d0e-ad1e-f35a466b01e8");
            ControlPresentation emailTemplate;

            using (var pageManager = PageManager.GetManager())
            {
                IQueryable<ControlPresentation> controlPresentations = pageManager.GetPresentationItems<ControlPresentation>();

                emailTemplate = controlPresentations.Where(tmpl => tmpl.Id == templateId).SingleOrDefault();
            }

            return emailTemplate != null ? ReplaceValuesInTemplate(emailTemplate.Data, checkoutSate, cartOrder) : "";
        }

        private static string ReplaceValuesInTemplate(string template, CheckoutState checkoutSate, CartOrder cartOrder)
        {
            CheckoutState checkoutState = checkoutSate;

            var orderConfirmationEmailTemplateFormatter = new OrderConfirmationEmailTemplateFormatter();
            return orderConfirmationEmailTemplateFormatter.ReplaceValuesInTemplate(template,
                cartOrder.Details, cartOrder.Discounts, checkoutState, cartOrder);
        }

        private static void SendEmail(string from, string to, string subject, string body, bool isHtml)
        {
            // Get the SMTP settings.
            var smtpSettings = Config.Get<SystemConfig>().SmtpSettings;
            string smtpServer = smtpSettings.Host;
            int smtpPort = smtpSettings.Port;
            string smtpUserName = smtpSettings.UserName;
            string smtpPassword = smtpSettings.Password;

            using (var message = new MailMessage(new MailAddress(from), new MailAddress(to)))
            {
                message.Subject = subject;
                message.Body = body;
                message.IsBodyHtml = isHtml;

                using (var emailClient = new SmtpClient(smtpServer, smtpPort))
                {
                    emailClient.Credentials = new NetworkCredential(smtpUserName, smtpPassword);

                    emailClient.Send(message);
                }
            }
        }

    }
}
