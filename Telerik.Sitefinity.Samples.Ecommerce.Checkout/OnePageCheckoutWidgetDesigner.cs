using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Telerik.Sitefinity.Web.UI;
using Telerik.Sitefinity.Web.UI.ControlDesign;
using Telerik.Web.UI;

namespace Telerik.Sitefinity.Samples.Ecommerce.Checkout
{
    [assembly: WebResource("Telerik.Sitefinity.Samples.Ecommerce.Checkout.Resources.OnePageCheckoutWidgetDesigner.js", "application/x-javascript")]
    public class OnePageCheckoutWidgetDesigner : ControlDesignerBase
    {

        protected override string LayoutTemplateName
        {
            get
            {
                return OnePageCheckoutWidgetDesigner.layoutTemplateName;
            }
        }

        protected override Type ResourcesAssemblyInfo
        {
            get
            {
                return typeof(OnePageCheckoutWidgetDesigner);
            }
        }

        protected virtual RadWindowManager RadWindowManager
        {
            get
            {
                return this.Container.GetControl<RadWindowManager>("windowManager", true);
            }
        }

        protected LinkButton SelectReceiptPageButton
        {
            get 
            { 
                return this.Container.GetControl<LinkButton>("selectReceiptPageButton", false); 
            }
        }


        protected PagesSelector ReceiptPageSelector
        {
            get 
            {
                return this.Container.GetControl<PagesSelector>("receiptPageSelector", false); 
            }
        }


        protected override void InitializeControls(GenericContainer container)
        {
            if (this.PropertyEditor != null)
            {
                string uiCulture = this.PropertyEditor.PropertyValuesCulture;
                
                this.ReceiptPageSelector.UICulture = uiCulture;
            }
        }

        public override IEnumerable<ScriptReference> GetScriptReferences()
        {
            var scripts = new List<ScriptReference>(base.GetScriptReferences());
            scripts.Add(new ScriptReference(OnePageCheckoutWidgetDesigner.scriptReference, typeof(OnePageCheckoutWidgetDesigner).Assembly.FullName));
            return scripts;
        }

        public override IEnumerable<ScriptDescriptor> GetScriptDescriptors()
        {
            IEnumerable<ScriptDescriptor> descriptors = new List<ScriptDescriptor>(base.GetScriptDescriptors());
            var descriptor = (ScriptControlDescriptor)descriptors.Last();

            descriptor.AddElementProperty("selectReceiptPageButton", this.SelectReceiptPageButton.ClientID);

            descriptor.AddComponentProperty("receiptPageSelector", this.ReceiptPageSelector.ClientID);

            descriptor.AddComponentProperty("radWindowManager", this.RadWindowManager.ClientID);
            return descriptors;
        }

        private const string layoutTemplateName = "Telerik.Sitefinity.Samples.Ecommerce.Checkout.Resources.OnePageCheckoutWidgetDesigner.ascx";
        private const string scriptReference = "Telerik.Sitefinity.Samples.Ecommerce.Checkout.Resources.OnePageCheckoutWidgetDesigner.js";
    }

}
