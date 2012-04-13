<%@ Control Language="C#" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sitefinity" Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Register Assembly="Telerik.Sitefinity" TagPrefix="sfFields" Namespace="Telerik.Sitefinity.Web.UI.Fields" %>
<%@ Register TagPrefix="designers" Namespace="Telerik.Sitefinity.Web.UI.ControlDesign" Assembly="Telerik.Sitefinity" %>

<sitefinity:ResourceLinks id="resourcesLinks" runat="server">
   <sitefinity:ResourceFile Name="Styles/Window.css" />
</sitefinity:ResourceLinks>

<telerik:RadWindowManager ID="windowManager" runat="server"
    Height="100%"
    Width="100%"
    Behaviors="None"
    Skin="Sitefinity"
    ShowContentDuringLoad="false"
    VisibleStatusBar="false">
    <Windows>
        <telerik:RadWindow ID="widgetEditorDialog" runat="server" ReloadOnShow="true" Modal="true" />
    </Windows>
</telerik:RadWindowManager>

<div id="receiptSelectorTag" style="display: none;" class="sfDesignerSelector">
    <sitefinity:PagesSelector runat="server" ID="receiptPageSelector" AllowExternalPagesSelection="false"
        AllowMultipleSelection="false" />
</div>


<div class="sfContentViews sfSingleContentView">
    <h2>
        <asp:Literal ID="Literal1" runat="server" Text='Link to Receipt page' />
        <span class="sfRequiredItalics sfNote">
            <asp:Literal ID="Literal2" runat="server" Text='<%$Resources:OrdersResources, RequiredField %>' />
        </span>
    </h2>
    <div>
        <span id="receiptPageText" class="sfSelectedItem" style='display: none;'></span>
        <asp:LinkButton NavigateUrl="javascript:void(0)" runat="server" OnClientClick="return false;"
            CssClass="sfLinkBtn sfChange" ID="selectReceiptPageButton">
            <strong class="sfLinkBtnIn"><asp:Literal ID="Literal4" runat="server" Text="<%$Resources:Labels, SelectPage %>" /></strong>
        </asp:LinkButton>
    </div>
</div>
