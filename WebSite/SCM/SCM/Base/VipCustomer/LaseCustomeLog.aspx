<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="LaseCustomeLog.aspx.cs" Inherits="SCM.Web.VipCustomer.LaseCustomeLog"
    Title="消费记录" %>
<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料&nbsp;>>&nbsp;客户&nbsp;>>&nbsp;消费记录
                <asp:TextBox ID="txtCode" runat="server" Width="200" Visible="false"></asp:TextBox>
                <asp:TextBox ID="txtName" runat="server" Width="200" Visible="false"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                AlternatingRowStyle-BackColor="#F0F1F2">
                <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="CREATE_DATE_TIME" HeaderText="消费时间"></asp:BoundField>
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="客户"></asp:BoundField>
                    <asp:BoundField DataField="POINTS" HeaderText="积分"></asp:BoundField>
                    <asp:BoundField DataField="USED_POINTS" HeaderText="消费积分"></asp:BoundField>
                    <asp:BoundField DataField="SLIP_NUMBER" HeaderText="流水号"></asp:BoundField>
                    <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品"></asp:BoundField>
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="单位"></asp:BoundField>
                    <asp:BoundField DataField="SALES_EMPLOYEE" HeaderText="销售人员"></asp:BoundField>
                    <asp:BoundField DataField="ORI_PRICE" HeaderText="原价"></asp:BoundField>
                    <asp:BoundField DataField="DISCOUNT_RATE" HeaderText="折扣"></asp:BoundField>
                    <asp:BoundField DataField="PRICE" HeaderText="单价"></asp:BoundField>
                    <asp:BoundField DataField="QUANTITY" HeaderText="数量"></asp:BoundField>
                    <asp:BoundField DataField="AMOUNT" HeaderText="总金额"></asp:BoundField>
                    <asp:BoundField DataField="MEMO" HeaderText="备注"></asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F0F1F2"></AlternatingRowStyle>
            </asp:GridView>
            <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                <PageControl:Paging ID="paging" runat="server" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
