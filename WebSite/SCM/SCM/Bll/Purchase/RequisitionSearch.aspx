<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="RequisitionSearch.aspx.cs" Inherits="SCM.Web.Purchase.RequisitionSearch" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                采购&nbsp;>>&nbsp;补货申请
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            申请门店 :
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                            <img title="门店查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                            <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            申请时间：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                 alt="" class="img"></img>～
                            <asp:TextBox ID="txtToDate" runat="server" Width="80"  MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                alt="" class="img"></img>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            申请人 :
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtUserId" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                            <img title="申请人查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                            <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            状态:
                        </td>
                        <td class="tdText">
                            <asp:RadioButton ID="rdo1" runat="server" GroupName="rdo" Text="申请中" Checked="true" />
                            <asp:RadioButton ID="rdo2" runat="server" GroupName="rdo" Text="审核完成" />
                        </td>
                    </tr>
                </table>
                <table class=" searchTable" cellpadding="0px;" cellspacing="0px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" HeaderText="申请单号"></asp:BoundField>
                        <asp:BoundField DataField="FROM_WAREHOUSE_NAME" HeaderText="申请门店"></asp:BoundField>
                        <asp:BoundField DataField="TO_WAREHOUSE_NAME" HeaderText="补货仓库"></asp:BoundField>
                        <asp:BoundField DataField="REQUISITION_NAME" HeaderText="补货期间"></asp:BoundField>
                        <asp:BoundField DataField="DEPARTUAL_DATE" HeaderText="申请日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="申请到货日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER")%>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER")%>' CssClass="GridViewLinkButton" />
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
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
