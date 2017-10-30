<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="InventoryList.aspx.cs" Inherits="SCM.Web.Stock.InventoryList" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                盘点&nbsp;>>&nbsp;盘点查询
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    盘点单号：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSlipNumber" runat="server" Width="150"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    仓库：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtWarehouseCode" runat="server" Width="60" OnTextChanged="Warehouse_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    开始时间：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80" OnTextChanged="FromDate_Changed"
                        AutoPostBack="true" MaxLength="10"></asp:TextBox>
                    <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                        style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img" />～
                    <asp:TextBox ID="txtToDate" runat="server" Width="80" OnTextChanged="ToDate_Changed"
                        AutoPostBack="true" MaxLength="10"></asp:TextBox>
                    <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                        style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img" />
                </td>
                <td class="tdTitle">
                    状态：
                </td>
                <td class="tdText">
                    <asp:RadioButton ID="rdo1" runat="server" Text="全部" GroupName="status" Checked="true" /><asp:RadioButton
                        ID="rdo2" runat="server" Text="开始" GroupName="status" /><asp:RadioButton ID="rdo3"
                            runat="server" Text="结束" GroupName="status" />
                </td>
            </tr>
        </table>
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                     <asp:LinkButton ID="btnExcel" runat="server" Text="导出" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" HeaderText="盘点单号"></asp:BoundField>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="仓库"></asp:BoundField>
                         <asp:BoundField DataField="GROUP_NAME" HeaderText="商品种类"></asp:BoundField>
                        <asp:BoundField DataField="INVENTORY_START_DATE" HeaderText="开始时间" DataFormatString="{0:yyyy-MM-dd}">
                        </asp:BoundField>
                        <asp:BoundField DataField="INVENTORY_END_DATE" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}">
                        </asp:BoundField>
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="状态"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="盘点"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    Enabled='<%# Eval("STATUS_FLAG").ToString()=="0"?true:false %>' OnClick="processClick"
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="90px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
