<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ReceiptReturn.aspx.cs" Inherits="SCM.Web.TransferIn.ReceiptReturn" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation" id="h">
        <tr>
            <td>
                入库&nbsp;>>&nbsp;成品返品查询
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    入库单号:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSlipNumber" runat="server" Width="200px"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    商品:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductCode" runat="server" Width="80px" OnTextChanged="Product_Changed"
                        AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                    <img title="商品查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT','<%=this.txtProductCode.ClientID%>','<%=this.lblProductName.ClientID%>');" />
                    <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    供应商编号:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSupplierCode" runat="server" Width="80px" OnTextChanged="Supplier_Change"
                        AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                    <img title="供应商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                    <asp:Label ID="lblSupplierName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    返品仓库:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80px" OnTextChanged="Warehouse_Change"
                        AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
            <tr>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass=" LinkButton2">
                    </asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" HeaderText="编号"></asp:BoundField>
                        <asp:BoundField DataField="SUPPLIER_NAME" HeaderText="供应商"></asp:BoundField>
                        <asp:BoundField DataField="RECIEPT_SLIP_NUMBER" HeaderText="入库单号"></asp:BoundField>
                        <asp:BoundField DataField="REASON_NAME" HeaderText="返品理由"></asp:BoundField>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="返品仓库"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品名称"></asp:BoundField>
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="单位"></asp:BoundField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="数量" ReadOnly="True" DataFormatString="{0:F0}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE1" HeaderText="备注1"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE2" HeaderText="备注2"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE3" HeaderText="备注3"></asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F0F1F2"></AlternatingRowStyle>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
