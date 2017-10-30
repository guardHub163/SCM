<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="DeliveryAnswerModify.aspx.cs" Inherits="SCM.Web.TransferIn.DeliveryAnswerModify"
    Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self">
        <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                采购&nbsp;>>&nbsp;成品交期确认&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:TextBox ID="txtSlipNumber" runat="server" Visible="false"></asp:TextBox>
            <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
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
                        订单类型:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblInputType" runat="server"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        入库仓库:
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblWarehouseName" runat="server"></asp:Label> &nbsp;
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
                        <asp:TextBox ID="txtStockFromDate" runat="server" Width="120" OnTextChanged="StockFromDate_Changed"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox> 
                        <img onclick="WdatePicker({el:'<%=this.txtStockFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        商品编号:
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
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        采购数量:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtOldQuantity" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        交货数量:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtQuantity" runat="server" OnTextChanged="Quantity_Changed" AutoPostBack="true"
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        分期:
                    </td>
                    <td class="tdText_3">
                        <asp:RadioButton ID="rdo1" runat="server" GroupName="rdo" Text="否" Checked="true"
                            OnCheckedChanged="Rdo_CheckedChanged" AutoPostBack="true" />
                        <asp:RadioButton ID="rdo2" runat="server" GroupName="rdo" Text="是" OnCheckedChanged="Rdo_CheckedChanged"
                            AutoPostBack="true" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        二期交货预定日:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtNewArrivalDate" runat="server" Width="120" OnTextChanged="NewArrivalDate_Changed"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        <img onclick="WdatePicker({el:'<%=this.txtNewArrivalDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        二期交货数量:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtNewQuantity" runat="server" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table  class="operateTable" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" Text="修正" OnClick="processClick" CssClass="LinkButton2">
                        </asp:LinkButton>
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose('你确定要取消编辑吗?');">
                            取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
