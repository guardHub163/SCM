<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="SCM.Web.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>正道供应链管理系统</title>
</head>
<body style="text-align: center; margin: 0px; background-image: url(Images/back.png);
    background-repeat: repeat-x;">
    <form id="Form1" method="post" runat="server">
    <table style="margin-left: auto; margin-right: auto; margin-top: 10%; width: 473px;
        height: 284px; background-image: url(Images/loginBack.png); background-repeat: no-repeat;
        z-index: 1; table-layout: fixed;">
        <tr style="height: 150px;">
            <td style="width: 200px;">
                &nbsp;
            </td>
            <td style="width: 80px;">
                &nbsp;
            </td>
            <td style="width: 160px;">
                &nbsp;
            </td>
            <td style="width: 33px;">
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px;">
            <td>
            </td>
            <td style="text-align:left">
                用户名：
            </td>
            <td>
                <input tabindex="1" maxlength="18" name="user" id="txtUsername" runat="server" style="width: 140px;" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 30px;">
            <td>
            </td>
            <td style="text-align:left">
                密&nbsp;&nbsp; 码：
            </td>
            <td>
                <input name="user" type="password" tabindex="2" maxlength="18" id="txtPass" runat="server"
                    enableviewstate="True" style="width: 140px;" />
            </td>
        </tr>
        <tr style="height: 30px;">
            <td>
            </td>
            <td style="text-align:left">
                <asp:CheckBox ID="chkPos" Text="POS" runat="server" TabIndex="3"/>
            </td>
            <td style="text-align: right; padding-right: 10px;">
                <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="Images/login.png" 
                    TabIndex="4" />
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr style="height: 44px;">
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
            <td>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
