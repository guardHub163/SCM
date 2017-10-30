<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="SCM.Web.Main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
        .GridView
        {
            margin: 0px;
            border: 0px;
            width: 100%;
            height: 200px;
        }
        .GridView td
        {
            border-collapse: collapse;
            border: 0px;
            border-bottom: dashed 1px Green;
        }
        .GridView th
        {
            border-collapse: collapse;
            border: 0px;
            border-bottom: solid 1px Green;
        }
        .RowStyle
        {
            line-height: 20px;
            height: 20px;
        }
        .GridViewLinkButton
        {
            line-height: 16px;
            height: 16px;
            margin-top: 1px;
            color: Black;
            text-decoration: none;
        }
        .GridView a:link
        {
            text-decoration: none;
            color: Black;
        }
        .GridView a:visited
        {
            text-decoration: none;
            color: Black;
        }
        .GridView a:hover
        {
            text-decoration: none;
            color: red;
        }
        .GridView a:actived
        {
            text-decoration: none;
            color: Black;
        }
        .GridViewHeader
        {
            background-image: url(Images/GridViewHeader.png);
            background-repeat: inherit;
            line-height: 20px;
            height: 20px;
        }
        .navigation
        {
            width: 100%;
            height: 30px;
            background-image: url(Images/navigation.png);
            background-repeat: no-repeat;
            font-weight: bold;
            color: White;
            padding-left: 5px;
        }
        .table
        {
            width: 100%;
            margin: 0px;
            margin-top: 5px;
            margin-left: 5px;
            table-layout: fixed;
            border: solid 1px green;
        }
        .td
        {
            background-image: url(Images/back.png);
            font-size: 14px;
            padding-left: 5px;
            border-bottom: solid 1px green;
            height: 20px;
        }
    </style>
</head>
<body style="margin: 0; font-family: Verdana, Arial, Helv, Helvetica, sans-serif;
    font-size: 12px;">
    <form id="Form1" runat="server">
    <table class="navigation">
        <tr>
            <td>
                新闻/消息
            </td>
        </tr>
    </table>
    <!--最新消息-->
    <table class="table" style="height: 270px; width: 500px; float: left;" cellpadding="0"
        cellspacing="0">
        <tr>
            <td class="td">
                <img alt="" src="Images/title.png" style="vertical-align: middle;" />&nbsp;最新消息
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="CODE" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                            HeaderText="No"></asp:BoundField>
                        <asp:TemplateField HeaderText="标题" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("NEWS_TITLE") %>'
                                    CommandArgument='<%# Eval("ID") %>' ForeColor="Lime" />
                                <itemstyle width="200px"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PUBLISH_DATE" HeaderText="发布时间" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="CREATE_NAME" ItemStyle-HorizontalAlign="right" HeaderText="发布人"
                            HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; color: #3366ff;">
                <a href="Base/News/List.aspx" style="text-decoration: none;">更多...</a>
            </td>
        </tr>
    </table>
    <!--系统维护信息-->
    <table class="table" style="height: 270px; width: 500px;" cellpadding="0" cellspacing="0">
        <tr>
            <td class="td">
                <img alt="" src="Images/title.png" style="vertical-align: middle;" />&nbsp;系统维护信息
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridView3" runat="server" CellPadding="0" OnRowDataBound="gridView3_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="CODE" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                            HeaderText="No"></asp:BoundField>
                        <asp:TemplateField HeaderText="标题" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShowSystem" runat="server" CausesValidation="false" Text='<%# Eval("NEWS_TITLE") %>'
                                    CommandArgument='<%# Eval("ID") %>' ForeColor="Lime" />
                                <itemstyle width="200px"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PUBLISH_DATE" HeaderText="发布时间" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="CREATE_NAME" ItemStyle-HorizontalAlign="right" HeaderText="发布人"
                            HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
        <tr>
            <td style="text-align: right; color: #3366ff">
                <a href="Base/News/List.aspx" style="text-decoration: none;">更多...</a>
            </td>
        </tr>
    </table>
    <!--入库通知-->
    <table class="table" style="height: 255px; width: 500px; float: left;" cellpadding="0"
        cellspacing="0">
        <tr>
            <td class="td">
                <img alt="" src="Images/title.png" style="vertical-align: middle;" />&nbsp;入库通知
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridView1" runat="server" CellPadding="0" AutoGenerateColumns="False"
                    CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="CODE" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                            HeaderText="No"></asp:BoundField>
                        <asp:BoundField DataField="TO_WAREHOUSE_NAME" HeaderText="入库仓库" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="ARRIVAL_DATE" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false"
                            HeaderText="入库时间" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField DataField="SUM_QUANTITY" ItemStyle-HorizontalAlign="right" DataFormatString="{0:F0}"
                            HeaderText="入库数量" HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <!--出库通知-->
    <table class="table" style="height: 255px; width: 500px;" cellpadding="0" cellspacing="0">
        <tr>
            <td class="td">
                <img alt="" src="Images/title.png" style="vertical-align: middle;" />&nbsp;出库通知
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridView4" runat="server" CellPadding="0" AutoGenerateColumns="False"
                    CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="CODE" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center"
                            HeaderText="No"></asp:BoundField>
                        <asp:BoundField DataField="FROM_WAREHOUSE_NAME" HeaderText="出库仓库" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="DEPARTUAL_DATE" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="false"
                            HeaderText="出库时间" HeaderStyle-HorizontalAlign="Left"></asp:BoundField>
                        <asp:BoundField DataField="SUM_QUANTITY" ItemStyle-HorizontalAlign="right" DataFormatString="{0:F0}"
                            HeaderText="出库数量" HeaderStyle-HorizontalAlign="Right"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <!--安全库存提醒消息-->
    <table class="table" style="height: 255px; width: 1005px; display: none" cellpadding="0"
        cellspacing="0">
        <tr>
            <td class="td">
                <img alt="" src="Images/title.png" style="vertical-align: middle;" />&nbsp;安全库存提醒消息
            </td>
        </tr>
        <tr>
            <td style="padding: 0px;">
                <asp:GridView ID="gridView2" runat="server" CellPadding="0" OnRowDataBound="gridView2_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="20px" DataField="ID" ItemStyle-HorizontalAlign="Center"
                            HeaderText="No"></asp:BoundField>
                        <asp:TemplateField ItemStyle-Width="290px" HeaderText="仓库" HeaderStyle-HorizontalAlign="Left">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("WAREHOUSE_NAME") %>'
                                    CommandArgument='<%# Eval("ID") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="库存" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="SECURE_QUANTITY" HeaderText="安全库存" HeaderStyle-HorizontalAlign="Left">
                        </asp:BoundField>
                        <asp:BoundField DataField="DESC"></asp:BoundField>
                        <asp:BoundField DataField="USER_NAME"></asp:BoundField>
                        <asp:BoundField DataField="DATE_TIME" ItemStyle-HorizontalAlign="right"></asp:BoundField>
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
