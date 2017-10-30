<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="UpdatePassWord.aspx.cs" Inherits="SCM.Web.User.UpdatePassWord" Title="修改密码" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <div id="nav" style="background-color: #9DBEE1; height: 782px; width:195px;">
        <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/left1.png" />
        <table class="navigationChild" style="width: 190px; margin-top: -80px; margin-left:3px; background-image: url(../../Images/GridViewHeader.png)">
            <tr>
                <td>
                    修改密码
                </td>
            </tr>
        </table>
        <table cellpadding="0" cellspacing="0" class="inputTable" style="width: 170px; margin-left: 10px;">
            <tr>
                <td class="tdTitle_3" style="width: 130px;">
                    原密码：
                </td>
            </tr>
            <tr>
                <td class="tdText_3">
                    <asp:TextBox ID="txtOldPassWord" runat="server" TextMode="Password"></asp:TextBox>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdTitle_3">
                    新密码 ：
                </td>
            </tr>
            <tr>
                <td class="tdText_3">
                    <asp:TextBox ID="txtNewPassWord" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle_3">
                    确认密码 ：
                </td>
            </tr>
            <tr>
                <td class="tdText_3">
                    <asp:TextBox ID="txtRePassWord" runat="server" TextMode="Password"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table class="operateTable" style="width: 170px">
            <tr>
                <td>
                    <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2">
                    </asp:LinkButton>
                    <%--<a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>--%>
                    <a href="../../Left.aspx" id="btnCancel" class="LinkButton2">取消</a>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
