<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LeadingErrorInfo.aspx.cs"
    Inherits="SCM.Web.LeadingErrorInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>错误信息</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-size: 22px; text-align: center;">
        <asp:Label ID="Label2" runat="server" Text="错误信息"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <a href="#" id="btnCancel" onclick="window.close();">关闭</a>
    </div>
    <br />
    <div style="font-size: 18px;">
        <asp:Label ID="Label1" runat="server" Text="这些数据有误，有可能是数据库没有相对的数据，亦或者数据格式错误等等。"></asp:Label>
    </div>
    <hr />
    <asp:Label ID="Label3" runat="server" Text="请根据如下错误明细，仔细核对你的Excel:"></asp:Label>
    <br />
    <br />
    <div id="error" runat="server">
    </div>
    <hr />
    </form>
</body>
</html>
