Type.registerNamespace("Telerik.Sitefinity.Samples.Ecommerce.Checkout");



Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner = function (element) {

    Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner.initializeBase(this, [element]);
    this._propertyEditor = null;
    this._controlData = null;
    this._selectReceiptPageButton = null;
    this._receiptPageSelector = null;
    this._radWindowManager = null;
    this._receiptPageId = null; 
    this._selectedPageId = null;


    this._showReceiptPageSelectorDelegate = null;
    this._receiptPageSelectedDelegate = null;
    this._receiptPageSelectionAppliedDelegate = null;
    
    this._emptyGuid = Telerik.Sitefinity.getEmptyGuid();
}

Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner.prototype = {

    /* --------------------------------- set up and tear down --------------------------------- */

    initialize: function () {

        Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner.callBaseMethod(this, 'initialize');

       this._showReceiptPageSelectorDelegate = this._showReceiptPageSelectorDelegate || Function.createDelegate(this, this._showReceiptPageSelector);
        $addHandler(this.get_selectReceiptPageButton(), "click", this._showReceiptPageSelectorDelegate);

        this._receiptPageSelectedDelegate = this._receiptPageSelectedDelegate || Function.createDelegate(this, this._receiptPageSelected);
        this._receiptPageSelector.add_doneClientSelection(this._receiptPageSelectedDelegate);

        this._receiptPageSelectionAppliedDelegate = this._receiptPageSelectionAppliedDelegate || Function.createDelegate(this, this._receiptPageSelectionApplied);
        this._receiptPageSelector.add_selectionApplied(this._receiptPageSelectionAppliedDelegate);
    },

    dispose: function () {

        Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner.callBaseMethod(this, 'dispose');
    },

    /* --------------------------------- public methods --------------------------------- */

    // refereshed the user interface. Call this method in case underlying control object
    // has been changed somewhere else then through this desinger.
    refreshUI: function () {
         var controlData = this.get_controlData();

          if (controlData.ReceiptPageId !== this._emptyGuid) {
            this._receiptPageId = controlData.ReceiptPageId;
            this._receiptPageSelector.setSelectedItems([{ Id: this._receiptPageId, Title: ''}]);
        }
    },

    // once the data has been modified, call this method to apply all the changes made
    // by this designer on the underlying control object.
    applyChanges: function () {
        var controlData = this.get_controlData();

        controlData.ReceiptPageId = this._receiptPageId;
    },

    /* --------------------------------- event handlers --------------------------------- */
    _showReceiptPageSelector: function () {
        if (this._receiptPageId !== this._emptyGuid) {
            this._receiptPageSelector.setSelectedItems([{ Id: this._receiptPageId, Title: '' }]);
        }
        jQuery(this.get_element()).find('#receiptSelectorTag').show();
        dialogBase.resizeToContent();
    },

    _receiptPageSelected: function (items) {
        jQuery(this.get_element()).find('#receiptSelectorTag').hide();
        dialogBase.resizeToContent();

        if (items && items.length) {
            this._updateReceiptPageTitle(items[0].Title);
            this._receiptPageId = items[0].Id;
        }
    },

    
    _receiptPageSelectionApplied: function () {
        var selectedItem = this._receiptPageSelector.getSelectedItems()[0];
        if (selectedItem) {
            this._selectedPageId = selectedItem.Id;
            this._updateReceiptPageTitle(selectedItem.Title);
        }
    },
    /* --------------------------------- private methods --------------------------------- */
    
    _updateReceiptPageTitle: function (text) {
        if (text && text.length) {
            jQuery("#receiptPageText").text(text).show();
        }
    },

    /* --------------------------------- properties --------------------------------- */
    get_controlData: function () {
        return this.get_propertyEditor().get_control();
    },

    // gets the reference to the propertyEditor control
    get_propertyEditor: function () {
        return this._propertyEditor;
    },
    // sets the reference fo the propertyEditor control
    set_propertyEditor: function (value) {
        this._propertyEditor = value;
    },

    get_selectReceiptPageButton: function () {
        return this._selectReceiptPageButton;
    },

    set_selectReceiptPageButton: function (value) {
        this._selectReceiptPageButton = value;
    },

    get_receiptPageSelector: function () {
        return this._receiptPageSelector;
    },

    set_receiptPageSelector: function (value) {
        this._receiptPageSelector = value;
    },

    get_radWindowManager: function () {
        return this._radWindowManager
    },

    set_radWindowManager: function (value) {
        this._radWindowManager = value;
    }

}

Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner.registerClass('Telerik.Sitefinity.Samples.Ecommerce.Checkout.OnePageCheckoutWidgetDesigner', Telerik.Sitefinity.Web.UI.ControlDesign.ControlDesignerBase);
if (typeof (Sys) !== 'undefined') Sys.Application.notifyScriptLoaded();