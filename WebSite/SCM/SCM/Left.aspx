<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Left.aspx.cs" Inherits="SCM.Web.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <link href="Css/CommonStyle.css" type="text/css" rel="stylesheet" />
    <link href="Css/Left.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:Image ID="Image1" runat="server" ImageUrl="Images/left.png" />
    <div style="position: absolute; left: 10px; top: 30px; width: 175px; height: 80px;
        border: solid 1px #345889; z-index: 1">
        <img id="photo1" style="margin: 2px; margin-top: 4px; width: 70px; height: 70px;
            border: 1px solid Green; cursor: pointer;" runat="server" src="" alt="照片显示错误！"
            title="点击照片区域添加或更改照片" />
        <table style="position: absolute; left: 75px; top: 5px; width: 100px; height: 60px;">
            <tr>
                <td>
                    你好:
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblName" runat="server" Text=""></asp:Label>
                </td>
            </tr>
            <tr>
                <%--<td style="font-size:14px;"><a href="Base/User/UpdatePassWord.aspx">修改密码</a></td>--%>
                <td>
                    <input id="updatepassword" type="button" runat="server" value="修改密码" style="background-color: Transparent; color:Green;
                        cursor: pointer;" />
                </td>
            </tr>
        </table>
    </div>
    <div id="upload">
        <img id="photo2" style="width: 170px; height: 170px; border: 1px solid Green;" runat="server"
            src="" alt="" />
        <input type="file" runat="server" id="txtFile" style="margin-top: 2px; width: 170px;
            height: 20px; border: 1px solid Green;" /><br />
        <asp:LinkButton ID="btnUpload" runat="server" Text="上传" CssClass="LinkButton2" Style="margin-top: 2px;"
            OnClick="Upload_Click"></asp:LinkButton>
        <a href="#" id="btnCancel" class="LinkButton2" style="margin-top: 2px;" onclick="document.getElementById('upload').style.visibility = 'hidden';">
            取消</a>
    </div>
    <div id="update" style="text-align: left;">
        <span style="width:175px; height:30px; background: #9FBEE1; text-align: center; border-left:solid 1px Green; border-right:solid 1px Green; border-top:solid 1px Green">修改密码:</span><br />
        <span style="width:175px; height:25px; background: #9FBEE1; border-left:solid 1px Green; border-right:solid 1px Green; border-top:solid 1px Green">原密码：</span><br />
        <asp:TextBox ID="txtOldPassWord" runat="server" TextMode="Password" CssClass="textupdate"></asp:TextBox><br />
        <span style="width:175px; height:25px; background: #9FBEE1; border-left:solid 1px Green; border-right:solid 1px Green; border-top:solid 1px Green">新密码 ：</span><br />
        <asp:TextBox ID="txtNewPassWord" runat="server" TextMode="Password" CssClass="textupdate"></asp:TextBox><br />
        <span style="width:175px; height:25px; background: #9FBEE1; border-left:solid 1px Green; border-right:solid 1px Green; border-top:solid 1px Green;">确认密码 ：</span><br />
        <asp:TextBox ID="txtRePassWord" runat="server" TextMode="Password" CssClass="textupdate1"></asp:TextBox><br />
        <span style="text-align:center;"> <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="btnSave_Click" CssClass="LinkButton2">
        </asp:LinkButton>
        <a href="#" id="btnCancel1" class="LinkButton2" style="margin-top: 2px;" onclick="document.getElementById('update').style.visibility = 'hidden';">
            取消</a></span>
    </div>
    <div id="nav">
    </div>
    </form>
</body>
</html>

<script src="Script/left.js" type="text/javascript"></script>

