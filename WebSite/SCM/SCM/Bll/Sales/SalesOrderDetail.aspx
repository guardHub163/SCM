<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="SalesOrderDetail.aspx.cs" Inherits="SCM.Web.Sales.SalesOrderDetail"
    Title="销售明细" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                AlternatingRowStyle-BackColor="#F0F1F2">
                <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="部门"></asp:BoundField>
                    <asp:BoundField DataField="PRODUCT_CODE" HeaderText="商品编号"></asp:BoundField>
                    <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品名称"></asp:BoundField>
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="单位"></asp:BoundField>
                    <asp:BoundField DataField="SALES_EMPLOYEE" HeaderText="销售人员"></asp:BoundField>
                    <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="客户"></asp:BoundField>
                    <asp:BoundField DataField="ORI_PRICE" HeaderText="原价" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="DISCOUNT_RATE" HeaderText="折扣" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="PRICE" HeaderText="单价" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="QUANTITY" HeaderText="数量" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="AMOUNT" HeaderText="总金额" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="POINTS" HeaderText="积分" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                         <asp:BoundField DataField="PROMOTION_AMOUNT" HeaderText="促销金额"></asp:BoundField>
                          <asp:BoundField DataField="PROMOTION_DISCOUNTS" HeaderText="促销折扣"></asp:BoundField>
                    <asp:BoundField DataField="USED_POINTS" HeaderText="消费积分" HeaderStyle-CssClass=" headerRight"
                        ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="CREATE_DATE_TIME" HeaderText="消费时间"></asp:BoundField>
                    <asp:BoundField DataField="MEMO" HeaderText="备注"></asp:BoundField>
                </Columns>
                <AlternatingRowStyle BackColor="#F0F1F2"></AlternatingRowStyle>
            </asp:GridView>
            <table style="text-align: right; width: 998px;">
                <td colspan="2">
                    合计： 总积分：
                    <asp:Label ID="lblpoints" runat="server"></asp:Label>&nbsp;&nbsp; 总金额：
                    <asp:Label ID="lblAmount" runat="server"></asp:Label>&nbsp;&nbsp; 总数量：
                    <asp:Label ID="lblQuantity" runat="server"></asp:Label>
                </td>
            </table>
            <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                <PageControl:Paging ID="paging" runat="server" Visible="false" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
