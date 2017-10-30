<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeCompare.aspx.cs"
    Inherits="SCM.Web.SAR._EmployeeCompare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>员工信息</title>
    <link href="../Css/SarStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Chart ID="Chart1" runat="server" Height="340px" Width="1022px">
    </asp:Chart>
    <asp:Chart ID="Chart2" runat="server" Height="340px" Width="1022px">
    </asp:Chart>
    <%--<div style="margin-top: 0px; margin-left: auto; margin-right: auto; border: solid 1px Gray;">
        <table cellpadding="0px;" cellspacing="0px;" style="width: 1022px; height: 30px;
            background-image: url(../Images/tableTop.png); background-repeat: repeat-x; font-size: 15px;">
            <tr>
                <td style="width: 105px; text-align: left;">
                    店员
                </td>
                <td style="width: 125px; text-align: left;">
                    销售金额
                </td>
                <td style="width: 95px; text-align: left;">
                    金额排名
                </td>
                <td style="width: 145px; text-align: left;">
                    销售金额占比（%）
                </td>
                <td style="width: 125px; text-align: left;">
                    销售单数
                </td>
                <td style="width: 125px; text-align: left;">
                    单数排名
                </td>
                <td style="width: 145px; text-align: left;">
                    销售单数占比（%）
                </td>
                <td style="text-align: left;">
                    连带销售率（%）
                </td>
            </tr>
        </table>
        <div>--%>
    <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#EEF9FD"
        RowStyle-HorizontalAlign="Left" RowStyle-VerticalAlign="Middle" CellPadding="0"
        CssClass="GridView" Width="1022px">
        <RowStyle HorizontalAlign="Left" VerticalAlign="Middle"></RowStyle>
        <Columns>
            <asp:BoundField DataField="USERNAME" HeaderText="店员" />
            <asp:BoundField DataField="AMOUNT" HeaderText="销售金额" DataFormatString="{0:N2}" HtmlEncode="False"
                ItemStyle-HorizontalAlign="Right">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AMOUNT_SORT" HeaderText="金额排名" ItemStyle-HorizontalAlign="Right">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="AMOUNT_COMPARE" HeaderText="销售金额占比(%)" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="QUANTITY" HeaderText="销售单数" ItemStyle-HorizontalAlign="Right">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="QUANTITY_SORT" HeaderText="单数排名" ItemStyle-HorizontalAlign="Right">
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="QUANTITY_COMPARE" HeaderText="销售单数占比(%)" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
            <asp:BoundField DataField="JOINTSALESRATE" HeaderText="连带销售率(%)" ItemStyle-HorizontalAlign="Right">
                <HeaderStyle Wrap="False" />
                <ItemStyle HorizontalAlign="Right"></ItemStyle>
            </asp:BoundField>
        </Columns>
        <AlternatingRowStyle BackColor="#EEF9FD"></AlternatingRowStyle>
    </asp:GridView>
    <table cellpadding="0px;" cellspacing="0px;" style="margin-left: auto; margin-right: auto;
        height: 30px; width: 1022px;">
        <tr>
            <td class="tdSarText">
                <label id="lblAverAgeaAount" runat="server">
                </label>
            </td>
        </tr>
        <tr>
            <td class="tdSarText">
                <label id="lblAverageQuantity" runat="server">
                </label>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
