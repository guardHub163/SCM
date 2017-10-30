<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Modify.aspx.cs" Inherits="SCM.Web.News.Modify" Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;新闻&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable" >
                <tr>
                    <td class="tdTitle_3">
                        类型
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblType" runat="server"></asp:Label> &nbsp;
                        <asp:Label ID="lblId" runat="server" Visible="false"></asp:Label> &nbsp;
                        <asp:Label ID="lblTypeCode" runat="server" Visible="false"></asp:Label> &nbsp;
                        <asp:Label ID="lblTime" runat="server" Visible="false"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        新闻标题：
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblTitle" runat="server" Width="200px"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        新闻内容：
                    </td>
                    <td class="tdText_3">
                        <textarea id="txtNewsContent" cols="20" runat="server" style="width: 302px; height: 200px;"
                            rows="10"></textarea>
                    </td>
                </tr>
            </table>
            <table class="operateTable">
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2">
                        </asp:LinkButton>
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
