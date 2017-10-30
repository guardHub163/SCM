<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ShopReceiptPlanDetail.aspx.cs" Inherits="SCM.Web.TransferIn.ShopReceiptPlanDetail"
    Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                入库&nbsp;>>&nbsp;门店入库预定&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:TextBox ID="txtSlipNumber" runat="server" Visible="false"></asp:TextBox>
            <table cellpadding="0px;" cellspacing="0px;" class="inputTable" 
                <tr>
                    <td class="tdTitle_3">
                        订单编号:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblPurchaseSlipNumber" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        出库仓库:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblFromWarehouseName" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        入库仓库:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblToWarehouseName" runat="server"></asp:Label> &nbsp;
                        <asp:Label ID="lblToWarehousecode" runat="server" Visible="false"></asp:Label> &nbsp;
                        <asp:Label ID="lblFromWarehousecode" runat="server" Visible="false"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        采购日期:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblDepartureDate" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        交货预定日:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblArrivalDate" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        商品编号:
                    </td>
                    <td class="tdText_3"> 
                        <asp:Label ID="lblProductCode" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        商品名称:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblProductName" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        单位:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblUnitName" runat="server"></asp:Label> &nbsp;
                        <asp:Label ID="lblUnitcode" runat="server" Visible="false"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        预定数量:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtOldQuantity" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        入库数量:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtQuantity" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="operateTable">
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" Text="入库" OnClick="processClick" CssClass="LinkButton2">
                        </asp:LinkButton>
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose('你确定要取消入库吗?');">
                            取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
