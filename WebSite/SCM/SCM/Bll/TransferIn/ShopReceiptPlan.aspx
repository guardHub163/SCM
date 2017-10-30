<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ShopReceiptPlan.aspx.cs" Inherits="SCM.Web.TransferIn.ShopReceiptPlan" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                入库&nbsp;>>&nbsp;门店入库预定
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            入库单编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtSlipNumber" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            入库仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80px" CssClass="inputText"
                                Enabled="false" AutoPostBack="true"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" />
                            <asp:Label ID="lblWarehouseName" runat="server" CssClass=" label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            入库预定日:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtReFromDate" runat="server" Width="80px" OnTextChanged="ReFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtReFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtReToDate" runat="server" Width="80px" OnTextChanged="ReToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtReToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td colspan="2" style="text-align: right;">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" />
                        <asp:BoundField DataField="SHIPMENT_SLIP_NUMBER" HeaderText="入库订单" />
                        <asp:BoundField DataField="SHOP_NAME" HeaderText="出库仓库" />
                        
                        <asp:BoundField DataField="DEPARTUAL_DATE" HeaderText="采购日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" />
                        <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="预定入库日" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" />
                             <asp:BoundField DataField="PRODUCT_CODE" HeaderText="商品编号" />
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="名称" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="数量" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="单位" />
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="状态" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnAllEnter" runat="server" CausesValidation="false" Text="入库"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnHaifEnter" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="95px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
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
