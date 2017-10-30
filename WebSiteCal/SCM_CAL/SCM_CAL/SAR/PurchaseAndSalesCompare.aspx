<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PurchaseAndSalesCompare.aspx.cs" Inherits="SCM.Web.SAR._PurchaseAndSalesCompare" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>进销比曲线图</title>
    <link href="../Css/SarStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div style="margin-top: 0px; margin-left: auto; border:solid 1px Gray;">
        <asp:Chart ID="Chart1" runat="server" Height="540px" Width="1019px">
        </asp:Chart>
    </div>
    </form>
</body>
</html>
