<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RequisitionMonitor.aspx.cs"
    Inherits="SCM.Web.Purchase.RequisitionMonitor" Title="监测分析" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <link href="../../Css/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <table class="navigationChild">
        <tr>
            <td>
                采购&nbsp;>>&nbsp;补货&nbsp;>>&nbsp;监测分析
            </td>
        </tr>
    </table>
    <div class="border_div">
        <form id="form1" runat="server">
        <asp:GridView ID="gridView" runat="server" CellPadding="0" AutoGenerateColumns="False"
            RowStyle-HorizontalAlign="Center" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
            <HeaderStyle CssClass="GridViewHeader" />
            <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
            <Columns>
            </Columns>
        </asp:GridView>
        <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
            <tr>
                <td>
                    <a href="#" id="btnCancel" class="LinkButton2" onclick="javascript:window.opener=null;window.close();">关闭</a>
                </td>
            </tr>
        </table>
        </form>
    </div>
</body>
</html>
