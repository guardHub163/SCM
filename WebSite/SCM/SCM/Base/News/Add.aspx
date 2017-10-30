<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="SCM.Web.News.Add" Title="新建" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;新闻&nbsp;>>&nbsp;新建
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
                       <%-- <asp:DropDownList ID="selNewsType" runat="server" Width="100">
                        </asp:DropDownList>--%>
                          <select id="txtType" runat="server" style="width: 200px;">
                    <option value="1">系统消息</option>
                    <option value="2">普通消息</option>
                </select>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        新闻标题：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtTitle" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        新闻内容：
                    </td>
                    <td class="tdText_3">
                        <%--<asp:TextBox ID="txtNewsContent" runat="server" Width="290px" Height="200px" Rows="5"></asp:TextBox>--%>
                         <textarea id="txtNewsContent" cols="20"  runat="server" style="width:302px; height:200px;"  rows="10"></textarea>
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
