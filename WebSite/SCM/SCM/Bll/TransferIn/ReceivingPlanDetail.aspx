<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ReceivingPlanDetail.aspx.cs" Inherits="SCM.Web.TransferIn.ReceivingPlanDetail"
    Title="入库确认" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                入库&nbsp;>>&nbsp;成品入库预定&nbsp;>>&nbsp;入库确认
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
                        <asp:TextBox ID="txtQuantity" runat="server" OnTextChanged="Quantity_Changed" AutoPostBack="true"
                            MaxLength="10"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        返品数量:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtReQuantity" runat="server" AutoPostBack="true" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        返品原因:
                    </td>
                    <td class="tdText_3">
                        <table cellpadding="0px;" cellspacing="0px;" style="height: 140px;">
                            <tr>
                                <td style="width: 50px;">
                                    原因:
                                </td>
                                <td style="width: 150px;">
                                    <asp:DropDownList ID="ddlReason" Width="135" runat="server" Enabled="false">
                                    </asp:DropDownList>
                                </td>
                                <td style="width: 50px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    数量:
                                </td>
                                <td>
                                    <asp:TextBox ID="txtQ" runat="server" Width="130" Enabled="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnNew" runat="server" Text="增加" OnClick="processClick" UseSubmitBehavior="false"
                                        Enabled="false" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:ListBox ID="lbReason" runat="server" Width="180" Height="80"></asp:ListBox>
                                </td>
                                <td valign="bottom">
                                    <asp:Button ID="btnDelete" runat="server" Text="移除" OnClick="processClick" UseSubmitBehavior="false"
                                        Enabled="false" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        返品处理:
                    </td>
                    <td class="tdText_3">
                        <asp:RadioButton ID="rdo1" runat="server" GroupName="rdo" Text="余数取消" Checked="true"
                            OnCheckedChanged="Rdo_CheckedChanged" AutoPostBack="true" Enabled="false" />
                        <asp:RadioButton ID="rdo2" runat="server" GroupName="rdo" Text="余数分期" OnCheckedChanged="Rdo_CheckedChanged"
                            AutoPostBack="true" Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        二期交货预定日:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtNewArrivalDate" runat="server" Width="80" OnTextChanged="NewArrivalDate_Changed"
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
