<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGroupCompare.aspx.cs"
    Inherits="SCM.Web.SAR._ProductGroupCompare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>分类商品</title>
    <link href="../Css/SarStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Chart ID="Chart1" runat="server" Height="340px" Width="1022px">
    </asp:Chart>
    <asp:Chart ID="Chart2" runat="server" Height="340px" Width="1022px">
    </asp:Chart>
    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#EEF9FD"
        RowStyle-HorizontalAlign="Left" RowStyle-VerticalAlign="Middle" CellPadding="0"
        CssClass="GridView" Width="1022px">
        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle"></RowStyle>
        <Columns>
            <asp:BoundField DataField="NUMBER" HeaderText="序号" />
            <asp:BoundField DataField="NAME" HeaderText="种类" />
            <asp:BoundField DataField="AMOUNT" HeaderText="销售额(元)" DataFormatString="{0:N2}"
                HtmlEncode="False" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="SORT" HeaderText="占比（%）" ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="QUANTITY" HeaderText="数量（件）" ItemStyle-HorizontalAlign="Right" />
        </Columns>
        <AlternatingRowStyle BackColor="#EEF9FD"></AlternatingRowStyle>
    </asp:GridView>
    </form>
</body>
</html>
