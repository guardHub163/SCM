<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductCompare.aspx.cs"
    Inherits="SCM.Web.SAR._ProductCompare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品明细</title>
    <link href="../Css/SarStyle.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .CGridView
        {
            margin-top: 0px; /*margin-left: 3px; 	margin-right: 3px;border: solid 1px #87CEEB;*/
            width: 1000px;
            table-layout: fixed;
            border: 0px;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="ProductDiv" runat="server" style="border: solid 1px Gray; margin-top: 0px;
        margin-left: auto; margin-right: auto;">
        <table cellpadding="0px" cellspacing="0px" style="width: 1022px; height: 30px; background-image: url(../Images/tableTop.png);
            background-repeat: repeat-x; font-size: 15px;">
            <tr>
                <td style="width: 200px; text-align: left;">
                    序号
                </td>
                <td style="width: 200px; text-align: left;">
                    货品名
                </td>
                <td style="width: 200px; text-align: left;">
                    销售额（元）
                </td>
                <td style="width: 200px; text-align: left;">
                    占比（%）
                </td>
                <td style="width: 195px; text-align: left;">
                    数量（件）
                </td>
            </tr>
        </table>
        <div style="height: 513px; overflow: auto;">
            <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#EEF9FD"
                RowStyle-HorizontalAlign="Left" RowStyle-VerticalAlign="Middle" CellPadding="0"
                CssClass="CGridView" ShowHeader="false">
                <Columns>
                    <asp:BoundField DataField="NUMBER" ItemStyle-Width="200px"></asp:BoundField>
                    <asp:BoundField DataField="NAME" ItemStyle-Width="200px"></asp:BoundField>
                    <asp:BoundField DataField="AMOUNT" ItemStyle-Width="210px"></asp:BoundField>
                    <asp:BoundField DataField="SORT" ItemStyle-Width="210px"></asp:BoundField>
                    <asp:BoundField DataField="QUANTITY"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    </form>
</body>
</html>
