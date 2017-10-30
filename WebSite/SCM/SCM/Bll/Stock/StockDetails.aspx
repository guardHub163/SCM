<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="StockDetails.aspx.cs" Inherits="SCM.Web.Stock.StockDetails" Title="详细" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                库存&nbsp;>>&nbsp;库存查询&nbsp;>>&nbsp;详细
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0" cellspacing="0" class="inputTable">
            <tr>
                <td class="tdTitle">
                    仓库:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtWarehouseCode" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label> &nbsp;
                </td>
                <td class="tdTitle">
                    颜色:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtColorCode" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblColorName" runat="server" Text="" CssClass="label"></asp:Label> &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductCode" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label> &nbsp;
                </td>
                <td class="tdTitle">
                    尺码:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSizeCode" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblSizeName" runat="server" Text="" CssClass="label"></asp:Label> &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品款式:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtStyleCode" runat="server" Width="60" Enabled="false" CssClass="inputText"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblStyleName" runat="server" Text="" CssClass="label"></asp:Label> &nbsp;
                </td>
            </tr>
        </table>
        <table class="gridHaderTable" cellpadding="0" cellspacing="0">
            <tr class="GridViewHeader">
                <td style="width: 120px; border-right: 1px solid green; text-align: center;">
                    日期
                </td>
                <td style="width: 120px; border-right: 1px solid green; text-align: center;">
                    区分
                </td>
                <td style="width: 250px; border-right: 1px solid green; text-align: center;">
                    门店/供应商
                </td>
                <td style="width: 120px; border-right: 1px solid green; text-align: right;">
                    库存数量&nbsp;
                </td>
                <td style="width: 120px; border-right: 1px solid green; text-align: right">
                    出库预定数量&nbsp;
                </td>
                <td style="width: 120px; text-align: right">
                    入库预定数量&nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
            Width="996px" Height="340px">
            <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                ShowHeader="false" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#F0F1F2">
                <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                <Columns>
                    <asp:BoundField ItemStyle-Width="120" DataField="OPT_DATE" DataFormatString="{0:yyyy-MM-dd}"
                        HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField ItemStyle-Width="120" DataField="TYPE"></asp:BoundField>
                    <asp:BoundField ItemStyle-Width="250" DataField="NAME"></asp:BoundField>
                    <asp:BoundField ItemStyle-Width="120" DataField="QUANTITY" ItemStyle-CssClass=" cellRight"
                        HeaderStyle-CssClass=" headerRight" ReadOnly="True" DataFormatString="{0:F0}"
                        HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField ItemStyle-Width="120" DataField="OUTNUMBER" ItemStyle-CssClass=" cellRight"
                        HeaderStyle-CssClass=" headerRight" ReadOnly="True" DataFormatString="{0:F0}"
                        HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField ItemStyle-Width="220" DataField="ENTERNUMBER" ItemStyle-CssClass=" cellRight"
                        HeaderStyle-CssClass=" headerRight" ReadOnly="True" DataFormatString="{0:F0}"
                        HtmlEncode="false"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </asp:Panel>
        <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
            <tr>
                <td>
                    <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
