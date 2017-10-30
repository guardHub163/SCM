<%@ Page Language="C#" AutoEventWireup="true" CodeFile="TimeOut.aspx.cs" Inherits="SCM.Web.TimeOut" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登录超时</title>
    <link href="Css/CommonStyle.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" runat="server">
    <table style="margin-left: auto; margin-right: auto; margin-top: 50px;">
        <tr>
            <td>
                <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/timeOut.gif" Height="86px" />
            </td>
            <td style="font-family: 华文仿宋; font-size: 50px; height: 83px;">
                登录超时!
            </td>
        </tr>
        <tr>
            <td colspan="2" style="text-align: center;">
                <asp:LinkButton ID="btnReLogin" runat="server" Text="重新登录" OnClick="process_login"
                    CssClass="LinkButton3" Visible="false" Enabled="false" ></asp:LinkButton>
                <asp:LinkButton PostBackUrl="#" ID="btnClose" runat="server" Text="关闭页面" CssClass="LinkButton3"
                    Width="92px"></asp:LinkButton>
            </td>
        </tr>
        <tr>
         <td  colspan="2">
           <hr />
           </td>
        </tr>
        <tr>
          
            <td style="font-size: 16px; text-align: center; margin-top: 40px;" colspan="2">
                 你登录时间过于长久</br> 你尚未登录
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
