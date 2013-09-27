<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI"
    TagPrefix="sf" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Web.UI.FieldControls" TagPrefix="sitefinity" %>
<%@ Register TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI.Fields"
    Assembly="Telerik.Sitefinity" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Ecommerce.Catalog.Web.UI.Fields"
    TagPrefix="sfCatalog" %>
<sf:ResourceLinks id="resourcesLinks" runat="server">
    <sf:ResourceFile JavaScriptLibrary="JQuery" />
    <sf:ResourceFile Name="CheckoutStyles.css" />
</sf:ResourceLinks>
<sitefinity:FormManager ID="formManager" runat="server" />
<div id="widgetStatus" runat="server" visible="false">
    <asp:Label ID="widgetStatusMessage" runat="server" />
</div>
<asp:Panel ID="paymentProblemPanel" runat="server" Visible="false">
    <sf:Message runat="server" ID="message" ElementTag="div" FadeDuration="10" />
</asp:Panel>
<div id="widget" runat="server">
    <fieldset id="billingForm" class="sfcheckoutBillingFormWrp sfcheckoutFormWrp" runat="server"
        >
        <h2 class="sfcheckoutStepTitle">
            <asp:Literal ID="Literal1" runat="server" Text='<%$Resources:OrdersResources, BillingAddress %>' />
        </h2>
        <ul class="sfcheckoutFormList">
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="firstNameBilling" runat="server" CssClass="sfRequired"
                            DisplayMode="Write" Title='<%$Resources:OrdersResources, FirstName %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, FirstNameBillingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="lastNameBilling" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, LastName %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, LastNameBillingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="companyBilling" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, Company %>'>
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="emailBilling" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, Email %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, EmailBillingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="address1Billing" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, AddressLine1 %>'>
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="address2Billing" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, AddressLine2 %>'>
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="cityBilling" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, City %>'>
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <asp:Label ID="Label2" runat="server" Text='<%$Resources:OrdersResources, Country %>'
                            AssociatedControlID="countryBilling" CssClass="sfTxtLbl" />
                        <telerik:RadComboBox id="countryBilling" runat="server" CssClass="sfRequired sfCountryBilling"
                            Skin="Sitefinity" CollapseAnimation-Type="None" ExpandAnimation-Type="None" Height="250"
                            Width="205" />
                    </div>
                </div>
            </li>
            <li id="stateBillingContainer" class="sfcheckoutFormItm sfcheckoutFormItmDdl">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <asp:Label ID="stateLabelBilling" runat="server" Text='<%$Resources:OrdersResources, StateOrProvince %>'
                            AssociatedControlID="stateBilling" CssClass="sfTxtLbl" />
                        <telerik:RadComboBox id="stateBilling" runat="server" Skin="Sitefinity" Width="205"
                            Height="250" CollapseAnimation-Type="None" ExpandAnimation-Type="None" CssClass="sfStateBilling" />
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="zipBilling" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, Zip %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, ZipBillingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li id="Li2" class="sfcheckoutFormItm sfcheckoutFormItmDdl">
                <div class="twocolumnwrapper">
                    <sitefinity:TextField ID="phoneNumberBilling" runat="server" CssClass="sfRequired"
                        DisplayMode="Write" Title='<%$Resources:OrdersResources, PhoneNumber %>'>
                    </sitefinity:TextField>
                </div>
            </li>
        </ul>
    </fieldset>
    <fieldset id="shippingForm" class="sfcheckoutShippingFormWrp sfcheckoutFormWrp" runat="server" style="display: none">
        <h2 class="sfcheckoutStepTitle">
            <asp:Literal ID="shippingAddressLiteral" runat="server" Text='<%$Resources:OrdersResources, ShippingAddress %>' />
        </h2>
        <ul class="sfcheckoutFormList">
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="firstNameShipping" runat="server" CssClass="sfRequired"
                            DisplayMode="Write" Title='<%$Resources:OrdersResources, FirstName %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, FirstNameShippingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="lastNameShipping" runat="server" CssClass="sfRequired"
                            DisplayMode="Write" Title='<%$Resources:OrdersResources, LastName %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, LastNameShippingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="companyShipping" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, Company %>'>
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="emailShipping" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, Email %>'>
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="address1Shipping" runat="server" CssClass="sfRequired"
                            DisplayMode="Write" Title='<%$Resources:OrdersResources, AddressLine1 %>'>
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="address2Shipping" runat="server" CssClass="sfRequired"
                            DisplayMode="Write" Title='<%$Resources:OrdersResources, AddressLine2 %>'>
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:TextField ID="cityShipping" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, City %>'>
                        </sitefinity:TextField>
                    </div>
                    <div class="rightcolumn">
                        <asp:Label ID="Label1" runat="server" Text='<%$Resources:OrdersResources, Country %>'
                            AssociatedControlID="countryShipping" CssClass="sfTxtLbl" />
                        <telerik:RadComboBox id="countryShipping" runat="server" CssClass="sfRequired sfCountryShipping"
                            Skin="Sitefinity" CollapseAnimation-Type="None" ExpandAnimation-Type="None" Height="250"
                            Width="205" />
                    </div>
                </div>
            </li>
            <li id="stateShippingContainer" class="sfcheckoutFormItm sfcheckoutFormItmDdl">
                <div class="twocolumnwrapper">
                    <div class="leftcolumn">
                        <asp:Label ID="stateLabelShipping" runat="server" Text='<%$Resources:OrdersResources, StateOrProvince %>'
                            AssociatedControlID="stateShipping" CssClass="sfTxtLbl" />
                        <telerik:RadComboBox id="stateShipping" runat="server" Skin="Sitefinity" Width="205"
                            Height="250" CollapseAnimation-Type="None" ExpandAnimation-Type="None" CssClass="sfStateShipping" />
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:TextField ID="zipShipping" runat="server" CssClass="sfRequired" DisplayMode="Write"
                            Title='<%$Resources:OrdersResources, Zip %>'>
                            <ValidatorDefinition required="True" requiredviolationmessage="<%$Resources:OrdersResources, ZipShippingIsRequired %>"
                                messagecssclass="sfError" />
                        </sitefinity:TextField>
                    </div>
                </div>
            </li>
            <li id="Li1" class="sfcheckoutFormItm sfcheckoutFormItmDdl">
                <div class="twocolumnwrapper">
                    <sitefinity:TextField ID="phoneNumberShipping" runat="server" CssClass="sfRequired"
                        DisplayMode="Write" Title='<%$Resources:OrdersResources, PhoneNumber %>'>
                    </sitefinity:TextField>
                </div>
            </li>
        </ul>
    </fieldset>

    <div id="useBillingAddressAsShippingAddressPanel" class="sfcheckoutFormItm sfcheckoutFormItmCheckbox"
        runat="server">
        <asp:CheckBox ID="useBillingAddressAsBillingAddress" runat="server" CssClass="sfUseBillingAddressAsShippingAddress"
            Text="Shipping address is the same as the billing address" Checked="true" />
    </div>
    <fieldset id="shippingOptionsForm" class="sfcheckoutShippingFormWrp sfcheckoutFormWrp"
        runat="server">
        <h2 class="sfcheckoutStepTitle">
            <asp:Literal ID="Literal2" runat="server" Text='<%$Resources:OrdersResources, ShippingOptions %>' />
        </h2>
        <asp:RadioButtonList ID="shippingMethodsList" runat="server" CssClass="sfcheckoutFormItmCheckboxList" RepeatLayout="OrderedList"/>
    </fieldset>
    <fieldset id="paymentOptionsForm" class="sfcheckoutShippingFormWrp sfcheckoutFormWrp"
        runat="server">
        <h2 class="sfcheckoutStepTitle">
            <asp:Literal ID="Literal3" runat="server" Text='<%$Resources:OrdersResources, PaymentOptions %>' />
        </h2>
        <ul class="sfcheckoutFormList">
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <sitefinity:ChoiceField ID="creditCards" DisplayMode="Write" RenderChoicesAs="DropDown"
                    runat="server" CssClass="sfFormIn sfCheckListBox" Title='<%$Resources:OrdersResources, PaymentMethodCreditCards %>'>
                    <Choices>
                        <sitefinity:ChoiceItem Text="American Express" Value="americanexpress" />
                        <sitefinity:ChoiceItem Text="Discover" Value="discover" />
                        <sitefinity:ChoiceItem Text="Mastercard" Value="mastercard" />
                        <sitefinity:ChoiceItem Text="Visa" Value="visa" />
                        <sitefinity:ChoiceItem Text="Delta" Value="delta" />
                        <sitefinity:ChoiceItem Text="Solo" Value="solo" />
                        <sitefinity:ChoiceItem Text="Maestro" Value="maestro" />
                        <sitefinity:ChoiceItem Text="Uke" Value="uke" />
                        <sitefinity:ChoiceItem Text="DinersClub" Value="dinersclub" />
                        <sitefinity:ChoiceItem Text="JCB" Value="jcb" />
                        <sitefinity:ChoiceItem Text="Laser" Value="laser" />
                  </Choices>
                </sitefinity:ChoiceField>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt sfcheckoutFormItmSep">
                <sitefinity:TextField ID="creditCardNumber" runat="server" CssClass="sfRequired"
                    DisplayMode="Write" Title='<%$Resources:OrdersResources, CardNumber %>'>
                    <ValidatorDefinition required="True" requiredviolationmessage="Credit card is required"
                        messagecssclass="sfError" />
                </sitefinity:TextField>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt">
                <sitefinity:TextField ID="cardHolderName" runat="server" CssClass="sfRequired" DisplayMode="Write"
                    Title='<%$Resources:OrdersResources, CardHolderName %>'>
                    <ValidatorDefinition required="True" requiredviolationmessage="Card holder name is required"
                        messagecssclass="sfError" />
                </sitefinity:TextField>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmDdl">
                <asp:Label ID="expirationDateLabel" runat="server" Text='<%$Resources:OrdersResources, ExpirationDate %>'
                    CssClass="sfTxtLbl" />
                <div class="smalltwocolumnwrapper">
                    <div class="leftcolumn">
                        <sitefinity:ChoiceField ID="cardExpirationMonth" DisplayMode="Write" RenderChoicesAs="DropDown"
                            runat="server" CssClass="" Title=''>
                            <Choices>
                                <sitefinity:ChoiceItem Text="1" Value="1" />
                                <sitefinity:ChoiceItem Text="2" Value="2" />
                                <sitefinity:ChoiceItem Text="3" Value="3" />
                                <sitefinity:ChoiceItem Text="4" Value="4" />
                                <sitefinity:ChoiceItem Text="5" Value="5" />
                                <sitefinity:ChoiceItem Text="6" Value="6" />
                                <sitefinity:ChoiceItem Text="7" Value="7" />
                                <sitefinity:ChoiceItem Text="8" Value="8" />
                                <sitefinity:ChoiceItem Text="9" Value="9" />
                                <sitefinity:ChoiceItem Text="10" Value="10" />
                                <sitefinity:ChoiceItem Text="11" Value="11" />
                                <sitefinity:ChoiceItem Text="12" Value="12" />
                            </Choices>
                        </sitefinity:ChoiceField>
                    </div>
                    <div class="rightcolumn">
                        <sitefinity:ChoiceField ID="cardExpirationYear" DisplayMode="Write" RenderChoicesAs="DropDown"
                            runat="server" CssClass="" Title=''>
                            <Choices>
                                <sitefinity:ChoiceItem Text="2012" Value="2012" />
                                <sitefinity:ChoiceItem Text="2013" Value="2013" />
                                <sitefinity:ChoiceItem Text="2014" Value="2014" />
                                <sitefinity:ChoiceItem Text="2015" Value="2015" />
                                <sitefinity:ChoiceItem Text="2016" Value="2016" />
                            </Choices>
                        </sitefinity:ChoiceField>
                    </div>
                </div>
            </li>
            <li class="sfcheckoutFormItm sfcheckoutFormItmTxt sfcheckoutFormItmCodeTxt">
                <sitefinity:TextField ID="securityCode" runat="server" CssClass="sfRequired" DisplayMode="Write"
                    Title='<%$Resources:OrdersResources, SecurityCode %>'>
                    <ValidatorDefinition required="True" requiredviolationmessage="Security code is required"
                        messagecssclass="sfError" />
                </sitefinity:TextField>
            </li>
        </ul>
    </fieldset>
    <fieldset id="previewForm" class="sfcheckoutPaymentFormWrp sfcheckoutFormWrp" runat="server">
        <h2 class="sfcheckoutStepTitle">
            <asp:Literal ID="Literal4" runat="server" Text='Preview' />
        </h2>
        <telerik:RadGrid id="shoppingCartGrid" runat="server" Skin="Basic" EnableEmbeddedBaseStylesheet="false"
            EnableEmbeddedSkins="false">
            <MasterTableView autogeneratecolumns="false">
            <Columns>
                    <telerik:GridTemplateColumn HeaderText='<%$Resources:OrdersResources, ProductDescription %>' 
                        UniqueName="ProductTitle" ItemStyle-CssClass="sfItmTitleCol" HeaderStyle-CssClass="sfItmTitleCol">
                        <ItemTemplate>
                            <div>
                                <%# Eval("Title") %>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn HeaderText='<%$Resources:OrdersResources, ProductOptions %>' UniqueName="Options" ItemStyle-CssClass="sfItmOptionsCol" HeaderStyle-CssClass="sfItmOptionsCol">
                        <ItemTemplate>
                            <div>
                                <%# Eval("Options")%>
                            </div>
                        </ItemTemplate>
                    </telerik:GridTemplateColumn> 
                    <telerik:GridTemplateColumn HeaderText='<%$Resources:OrdersResources, Price %>'
                        HeaderStyle-CssClass="sfSingleItmPriceCol"
                        ItemStyle-CssClass="sfSingleItmPriceCol" 
                        UniqueName="BasePrice"
                        SortExpression="Total"
                        >
                    <ItemTemplate>
                        <sfCatalog:DisplayPriceField id="displayPriceField" ObjectType="Cart" ObjectId='<%# Eval("Id") %>' runat="server" />
                    </ItemTemplate>
                    </telerik:GridTemplateColumn>
                 
                    <telerik:GridBoundColumn DataField="Quantity"
                                            HeaderText='<%$Resources:OrdersResources, Quantity %>'
                                            SortExpression="Quantity" 
                                            ItemStyle-CssClass="sfItmQuantityCol" 
                                            HeaderStyle-CssClass="sfItmQuantityCol"
                                            UniqueName="ProductQuantity">
                    </telerik:GridBoundColumn>

                    <telerik:GridBoundColumn DataField="DisplayTotalFormatted" 
                                            HeaderText='<%$Resources:OrdersResources, Subtotal %>'
                                            SortExpression="DisplayTotalFormatted" 
                                            ItemStyle-CssClass="sfItmPriceCol" 
                                            HeaderStyle-CssClass="sfItmPriceCol"
                                            UniqueName="ProductPriceTotal">
                    </telerik:GridBoundColumn>
                </Columns>
        </MasterTableView>
        </telerik:RadGrid>
    </fieldset>
    <div id="placeOrder" class="sfcheckoutFormItm" runat="server">
        <asp:Button ID="placeOrderButton" runat="server" Text='<%$Resources:OrdersResources, PlaceThisOrder %>'
            CssClass="sfcheckoutContinueBtn" />
    </div>
</div>
