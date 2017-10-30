<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductGroupCompares.aspx.cs"
    Inherits="ProductGroupCompares" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>款式分类</title>
    <link href="../Css/SarStyle.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .style1
        {
            width: 1176px;
            height: 25px;
            padding-left: 5px;
            vertical-align: middle;
            text-align: left;
            border-bottom: solid 1px Gray;
            border-right: solid 1px Gray;
            background-color: #EFFBFE;
        }
        .style2
        {
            width: 155px;
            height: 35px;
            padding-left: 5px;
            vertical-align: middle;
            font-size: 14px;
            text-align: left;
            border-bottom: solid 1px Gray;
            border-right: solid 1px Gray;
            background-color: #D9EEF9;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top: 0px; margin-left: auto; margin-right: auto; border: solid 1px Gray;
        height: 40px;">
        <table>
            <tr>
                <td class="style2">
                    商 品 分 类
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlGroup" runat="server" AutoPostBack="True" DataTextField="NAME"
                        DataValueField="CODE" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"
                        Height="25px" Width="150px">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
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
            <asp:BoundField DataField="STYLE_NAME" HeaderText="款式" />
            <asp:BoundField DataField="PRICE" HeaderText="销售额(元)" DataFormatString="{0:N2}" HtmlEncode="False"
                ItemStyle-HorizontalAlign="Right" />
            <asp:BoundField DataField="QUANTITY" HeaderText="数量（件）" ItemStyle-HorizontalAlign="Right" />
        </Columns>
        <AlternatingRowStyle BackColor="#EEF9FD"></AlternatingRowStyle>
    </asp:GridView>
    </form>
</body>
</html>
