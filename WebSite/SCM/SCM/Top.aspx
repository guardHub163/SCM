<%@ Page Language="C#" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <style type="text/css">
        body
        {
            font-family:Verdana, Arial, Helv, Helvetica, sans-serif;
            font-size: 12px;
            margin: 0px;
            height: 120px;
        }
        #back
        {
            z-index: -1;
            position: relative;
            top: 0px;
            left: 0px;
            height: 120px;
            background-image: url(Images/top.png);
            background-repeat: no-repeat;
        }
        #info
        {
            z-index: 1;
            position: absolute;
            top: 25px;
            left: 150px;
            font-family: 华文行楷;
            font-size: 50px;
            font-weight: bolder;
            color: #000000;
        }
        .table
        {
            position: relative;
            top: 93px;
            left: 70%;
        }
        .LinkButton
        {
            font-weight: bold;
            text-decoration: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="back">
    </div>
    <div style="position: absolute; top: 50px; left: 860px; width:230px;">
        <a href='main.aspx?' target='mainFrame'><img src="Images/home.gif" border="0" alt="我的桌面" /></a>
        &nbsp;<a href='javascript:'onclick='window.parent.mainFrame.location.reload();'><img src="Images/refresh.gif" border="0" alt="刷新主区域" /></a>
        &nbsp;<a href='javascript:history.go(-1);'><img src="Images/gopre.gif" border="0" alt="后退" /></a>
        &nbsp;<a href='javascript:history.go(1);'><img src="Images/gonext.gif" border="0" alt="前进" /></a>
        <a href="Logout.aspx" target='_top' onclick="return window.confirm('确定要退出系统吗');"><img src="Images/exit.gif" border="0" alt="退出系统"/></a>
    </div>
    <div id="info">
        正道供应链管理系统</div>
    </form>
</body>
</html>
