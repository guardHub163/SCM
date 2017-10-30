<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoadImage.aspx.cs" Inherits="SCM.Web.Common.LoadImage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../Css/CommonStyle.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
        function processChange(item)
        {
            var filePath = item.value;
            window.parent.document.getElementById('ctl00_bodyPlace_maxImg').src = filePath;
        } 
        function processClose(item)
        {
            window.parent.document.getElementById("divImgList").style.visibility = "visible";
            window.parent.document.getElementById("divOperate").style.visibility = "visible";
            window.parent.document.getElementById("divUpLoadImg").style.visibility = "hidden";
        }   
    </script>

</head>
<body style="margin: 0px; text-align:right;">
    <form id="form1" runat="server">
    <asp:TextBox ID="txtProductCode" runat="server" Enabled="false" Visible="false"></asp:TextBox>
    <input type="file" runat="server" onchange="processChange(this);" id="txtFile" style="width:690px; height:22px; border: 1px solid  Green;"/>
    <asp:LinkButton ID="btnUpload" runat="server" Text="上传" CssClass="LinkButton2" OnClick="Upload"></asp:LinkButton>
    <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose(this);">取消</a>
    </form>
</body>
</html>
