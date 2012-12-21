using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using Telerik.Sitefinity.Configuration;
using Telerik.Sitefinity.Ecommerce.Orders.Model;
using Telerik.Sitefinity.Localization;
using Telerik.Sitefinity.Modules.Ecommerce.Configuration;
using Telerik.Sitefinity.Modules.Ecommerce.Orders;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Business;
using Telerik.Sitefinity.Modules.Ecommerce.Orders.Web.UI.CheckoutViews;
using Telerik.Sitefinity.Modules.Pages;
using Telerik.Sitefinity.Pages.Model;
using Telerik.Sitefinity.Services;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout.Helpers
{
    internal class EmailHelper
    {
        internal static void SendOrderPlacedEmailToClientAndMerchant(CartOrder cartOrder, CheckoutState checkoutState,int orderNumber)
        {
            var messageBody = GetEmailMessageBody(cartOrder, checkoutState);
            if (string.IsNullOrEmpty(messageBody))
            {
                //JMABase.WriteLogFile("Cannot send an email because there is no message body", "/ecommercelog.txt");
                return;
            }

            //JMABase.WriteLogFile("Message Body for email: " + messageBody, "/ecommercelog.txt");

            string fromAddress = Config.Get<EcommerceConfig>().MerchantEmail;
            string subject = String.Format(Res.Get<OrdersResources>("OrderEmailSubject"), orderNumber);
            SendEmail(fromAddress, checkoutState.BillingEmail, subject, messageBody, true); 

        }
        private static string GetEmailMessageBody(CartOrder cartOrder, CheckoutState checkoutSate)
        {
            Guid templateId = new Guid("f949cccb-c337-4d0e-ad1e-f35a466b01e8");
            ControlPresentation emailTemplate;

            var pageManager = PageManager.GetManager();
            //using (var pageManager = PageManager.GetManager())
            //{
                IQueryable<ControlPresentation> controlPresentations = pageManager.GetPresentationItems<ControlPresentation>();

                //emailTemplate = controlPresentations.Where(tmpl => tmpl.Id == templateId).SingleOrDefault();
                emailTemplate = controlPresentations.Where(a => a.Name == "Order Placed Client email").SingleOrDefault();

            //}

            return emailTemplate != null ? ReplaceValuesInTemplate(emailTemplate.Data, checkoutSate, cartOrder) : "";
        }

        private static string ReplaceValuesInTemplate(string template, CheckoutState checkoutSate, CartOrder cartOrder)
        {
            var order = OrdersManager.GetManager().GetOrder(cartOrder.Id);
            if (order != null)
            {
                var orderConfirmationEmailTemplateFormatter = new OrderConfirmationEmailTemplateFormatter();
                return orderConfirmationEmailTemplateFormatter.ReplaceValuesInTemplate(template, checkoutSate, order);
            }
            return string.Empty;
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
