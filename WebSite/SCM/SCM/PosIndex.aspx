<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PosIndex.aspx.cs" Inherits="SCM.Web.PosIndex" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>正道供应链管理系统</title>
    <style type="text/css">
        /******************** parentNav  *********************************/#parentNav
        {
            margin: 0px;
            width: 1024px;
            height: 35px;
            padding-left: 50px;
            padding-right: 50px;
            background-image: url(Images/menu_bar.png);
            background-repeat: no-repeat;
            z-index: 3;
        }
        #parentNav li
        {
            display: inline;
            list-style-type: none;
        }
        #parentNav li a
        {
            width: 80px;
            font-weight: normal;
            line-height: 35px;
            text-align: center;
            text-decoration: none;
            font-size: 15px;
            font-weight: bold; /*background-image: url(Images/menu_a_2.png);
            background-repeat: no-repeat;*/
        }
        #parentNav li a:hover
        {
            color: red;
            text-decoration: none;
        }
        #parentNav a:link
        {
            color: Black;
            text-decoration: none;
        }
        #parentNav a:visited
        {
            color: Black;
            text-decoration: none;
        }
        #parentNav a:hover
        {
            color: Black;
            text-decoration: none;
        }
        /******************** childNav  *********************************/#childNav
        {
            margin: 0px;
            position: absolute;
            top: 85px;
            width: 302px;
            border: solid 1px green;
            background-color: White;
            visibility: hidden;
            z-index: 2;
        }
        #childNav li
        {
            display: inline;
            list-style-type: none;
        }
        #childNav li a
        {
            width: 100px;
            padding-left: 5px;
            padding-right: 5px;
            font-weight: normal;
            font-size: 12px;
            line-height: 25px;
            text-align: left;
        }
        #childNav li a:hover
        {
            background: #8DABD3;
        }
        #childNav a:link
        {
            color: #666;
            text-decoration: none;
        }
        #childNav a:visited
        {
            color: #666;
            text-decoration: none;
        }
        #childNav a:hover
        {
            color: #FFF;
            text-decoration: none;
        }
    </style>
</head>
<body style="text-align: center; margin: 0px;">
    <table style="margin-left: auto; margin-right: auto; margin-top: 5px; width: 1010px;
        background-repeat: no-repeat; border: 0px; table-layout: fixed;" cellpadding="0"
        cellspacing="0">
        <tr style="height: 50px;">
            <td style="font-family: 华文行楷; font-size: 50px; font-weight: bolder; color: #000000;
                text-align: center;">
                正道供应链管理系统
            </td>
        </tr>
        <tr>
            <td>
                <div onmouseout="processMouseOut(this,event);">
                    <div id="parentNav">
                    </div>
                    <div id="childNav">
                    </div>
                </div>
            </td>
        </tr>
    </table>
    <iframe id="ifr" name="ifr" style="margin-left: auto; margin-right: auto; width: 1010px;
        z-index: 1;" src="Main.aspx" scrolling="no" frameborder="0" onload="this.height=ifr.document.body.scrollHeight + 40">
    </iframe>
    <table style="margin-left: auto; margin-right: auto; width: 1010px; text-align: center;
        border: 0px; table-layout: fixed;" cellpadding="0" cellspacing="0">
        <tr>
            <td style="border-top: solid 1px black;">
                copyright@2012 常州正道信息咨询有限公司
            </td>
        </tr>
    </table>
</body>
</html>

<script type="text/javascript" src="Script/PosIndex.js"></script>

