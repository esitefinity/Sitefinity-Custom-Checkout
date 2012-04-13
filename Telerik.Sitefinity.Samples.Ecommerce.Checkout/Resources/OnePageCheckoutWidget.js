
Type.registerNamespace("Telerik.Sitefinity.Samples.Ecommerce.Checkout");

Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget = function (element) {
    Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget.initializeBase(this, [element]);

    this._onLoadDelegate = null;
}

Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget.prototype =
{
    initialize: function () {
        Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget.callBaseMethod(this, "initialize");

        if (this._onLoadDelegate === null) {
            this._onLoadDelegate = Function.createDelegate(this, this._onLoad);
        }

        var useBillingAddressAsShippingAddressCheckBox = jQuery(".sfUseBillingAddressAsShippingAddress input[type=checkbox]");
        useBillingAddressAsShippingAddressCheckBox.change(function () {
            var shippingForm = jQuery(".sfcheckoutShippingFormWrp");
            shippingForm.toggle();
        });

        Sys.Application.add_load(this._onLoadDelegate);
    },

    dispose: function () {
        Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget.callBaseMethod(this, "dispose");

        Sys.Application.remove_load(this._onLoadDelegate);
        if (this._onLoadDelegate) {
            delete this._onLoadDelegate;
        }
    },

    /* -------------------- public methods ------------ */

    /* -------------------- events -------------------- */

    /* -------------------- event handlers ------------ */

    _onLoad: function (sender, args) {
        
    }


    /* -------------------- private methods ----------- */

    /* -------------------- properties ---------------- */

};

Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget.registerClass("Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidget", Sys.UI.Control);
