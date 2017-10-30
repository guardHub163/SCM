<%@ Page Language="C#" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>正道供应链管理系统</title>
</head>
<frameset rows="120,*" frameborder="no" border="0" framespacing="0" name="topset">
		<frame name="topFrame" scrolling="NO" noresize src="Top.aspx">
		<frameset rows="*" cols="195,*" framespacing="0" frameborder="no" border="0" name="middleset">
			<frame name="leftFrame" noresize src="left.aspx" >
			<frame id = "mainFrame" name="mainFrame" src="Main.aspx">
		</frameset>
</frameset>
<noframes>
    <body bgcolor="#FFFFFF" text="#000000">
    </body>
</noframes>
</html>
