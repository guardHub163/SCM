<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="StockHistorySearch.aspx.cs" Inherits="SCM.Web.Stock.StockHistory"%>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                库存&nbsp;>>&nbsp;修改记录查询
            </td>
        </tr>
    </table>
    <div class="border_div">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                <tr>
                    <td class="tdTitle">
                        仓库:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtWarehouseCode" runat="server" Width="60" OnTextChanged="Warehouse_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                        <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                    </td>
                    <td class="tdTitle">
                        商品:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtProductCode" runat="server" Width="60" OnTextChanged="Product_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="商品查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT','<%=this.txtProductCode.ClientID%>','<%=this.lblProductName.ClientID%>');" />
                        <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
            </table>
            <table class=" searchTable" cellpadding="0px;" cellspacing="0px;">
                <tr>
                    <td >
                        <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="WAREHOUSE_NAME"
                AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                AlternatingRowStyle-BackColor="#F0F1F2">
                <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="仓库"></asp:BoundField>
                    <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品"></asp:BoundField>
                    <asp:BoundField DataField="REASON_NAME" HeaderText="修改理由"></asp:BoundField>
                    <asp:BoundField DataField="UNIT_NAME" HeaderText="单位"></asp:BoundField>
                    <asp:BoundField DataField="FROM_QUANTITY" HeaderText="修改前数量" ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="TO_QUANTITY" HeaderText="修改后数量" ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="DIFF_QUANTITY" HeaderText="修改数量" ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="CREATE_NAME" HeaderText="修改人员"></asp:BoundField>
                    <asp:BoundField DataField="CREATE_DATE_TIME" HeaderText="修改时间"></asp:BoundField>
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
