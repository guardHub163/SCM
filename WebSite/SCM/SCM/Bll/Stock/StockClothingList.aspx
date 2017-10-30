<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="StockClothingList.aspx.cs" Inherits="SCM.Web.Stock.StockClothingList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <style>
        .gridHaderTable
        {
            /*margin-left: 3px;*/
            border: solid 1px green;
            border-bottom: solid 0px green;
            table-layout: fixed;
        }
        .gridHaderTable tr
        {
            background-image: url(../../Images/GridViewHeader.png);
            background-repeat: inherit;
            line-height: 25px;
            height: 25px;
            border: 0px;
        }
        .gridHaderTable tr td
        {
            border-right: 1px solid green;
        }
        .ScrollGridView
        {
            margin: 0px;
            table-layout: fixed;
        }
        .ScrollGridView tr
        {
            line-height: 20px;
            height: 20px;
            font-size: 12px;
        }
        .ScrollPanel
        {
            margin-left: 3px;
            border-top: 1px solid green;
            border-left: 1px solid green;
            border-bottom: solid 1px green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                库存&nbsp;>>&nbsp;库存查询
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    仓库:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80px" OnTextChanged="Warehouse_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    商品种类*:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80px" OnTextChanged="ProductGroupCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                    <asp:Label ID="lblProductGroupName" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品款式:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtStyleCode" runat="server" Width="80px" OnTextChanged="StyleCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                    <asp:Label ID="lblStyleName" runat="server" Text="" CssClass=" label"></asp:Label>
                </td>
                <td class="tdTitle">
                    颜色:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtColorCode" runat="server" Width="80px" OnTextChanged="ClolorCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="颜色查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('COLOR','<%=this.txtColorCode.ClientID%>','<%=this.lblColorName.ClientID%>');" />
                    <asp:Label ID="lblColorName" runat="server" Text="" CssClass=" label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    库存数量:
                </td>
                <td class="tdText">
                    <asp:RadioButton ID="rdoAll" runat="server" GroupName="QTY" Text="全部" Checked="true" />
                    <asp:RadioButton ID="rdoThanZero" runat="server" GroupName="QTY" Text="大于0" />
                    <asp:RadioButton ID="rdoEqualZero" runat="server" GroupName="QTY" Text="等于0" />
                    <asp:RadioButton ID="rdoBelowZero" runat="server" GroupName="QTY" Text="小于0" />
                </td>
                <td class="tdText" colspan="2" style="text-align: right;">
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    <asp:LinkButton ID="btnExport" runat="server" Text="导出" OnClick="processClick" CssClass="LinkButton2"
                        Enabled="false"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable" style="margin-top: 4px;
            width: 350px;">
            <tr>
                <td class="tdTitle">
                    库存合计：
                </td>
                <td class="tdText" style="width: 200px; text-align: right;">
                    <asp:Label ID="txtStockTote" runat="server" CssClass="cellRight"></asp:Label>
                    &nbsp;
                </td>
            </tr>
        </table>
        <!--表头-->
        <div style="table-layout: fixed; margin-top: 4px;">
            <div class="Scroll" style="width: 1000px; overflow: hidden;" id="headerScroll">
                <asp:Table runat="server" ID="VHeader" CssClass="gridHaderTable" CellPadding="0"
                    CellSpacing="0">
                </asp:Table>
            </div>
        </div>
        <!--表数据-->
        <div class="ScrollPanel Scroll" style="width: 995px; overflow-y: scroll; overflow-x: auto;
            height: 500px;" onscroll="javascript:document.getElementById('headerScroll').scrollLeft = this.scrollLeft;">
            <asp:GridView ID="gridView" runat="server" CellPadding="0" ShowHeader="false" CssClass="ScrollGridView "
                AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#F0F1F2" OnRowDataBound="gridView_RowDataBound">
            </asp:GridView>
        </div>
    </div>
</asp:Content>
