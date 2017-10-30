<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ShowInfo.aspx.cs" Inherits="SCM.Web.News.ShowInfo" Title="详细信息" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

    <style type="text/css">
        .header
        {
            height: 25px;
            padding: 2px;
            vertical-align: middle;
            background-color: #DAE6F4;
            border-bottom: solid 1px Green;
            border-right: solid 1px Green;
        }
        .title
        {
            width: 110px;
            height: 110px;
            padding: 2px;
            vertical-align: middle;
            background-color: #DAE6F4;
            border-bottom: solid 1px Green;
            border-right: solid 1px Green;
        }
        .text
        {
            width: 878px;
            height: 110px;
            padding: 2px;
            vertical-align: middle;
            background-color: #F4F8FC;
            border-bottom: solid 1px Green;
            border-right: solid 1px Green;
        }
        img
        {
        	width:60px;
        	height:60px;
            border: double 1px green;
            padding: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;新闻&nbsp;>>&nbsp;详细信息
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0" cellspacing="0" class="inputTable">
            <tr>
                <td class="header" colspan="2">
                    <b><asp:Label ID="lblTitle" runat="server"></asp:Label></b> &nbsp;
                    <asp:Label ID="Labelid" runat="server" Visible="false"></asp:Label> &nbsp;
                    <asp:Label ID="lblType" runat="server" Visible="false"></asp:Label> &nbsp;
                </td>
            </tr>
            <tr class="RowStyle">
                <td class="title" style="text-align: center;">
                    <img id="PhotoCreate" runat="server" src="" alt="" /><br /> &nbsp;
                    <asp:Label ID="lblName" runat="server"></asp:Label><br /> &nbsp;
                    <asp:Label ID="lblTime" runat="server"></asp:Label> &nbsp;
                </td>
                <td class="text">
                    <asp:Label ID="lblContent" runat="server"></asp:Label> &nbsp;
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="ID" AutoGenerateColumns="False"
                    CssClass="GridView" ShowHeader="False" Width="998">
                    <RowStyle CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-CssClass="title" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <img id="PhotoRestore" runat="server" src='<%# "../../Image.aspx?TYPE=USER&FILE_NAME="+Eval("NAME_PHOTO") %>'
                                    alt="" /><br />
                                <asp:Label ID="returnName" runat="server" Text='<%# Eval("CREATE_NAME") %>'></asp:Label><br />
                                <asp:Label ID="returntime" runat="server" Text='<%# Eval("CREATE_DATE_TIME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ItemStyle-CssClass="text">
                            <ItemTemplate>
                                <asp:Label ID="returnContent" runat="server" Text='<%# Eval("NEWS_CONTENT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
                <table class="searchTable" style="text-align: left;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <b>内容回复：</b>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="txtNewsContent" runat="server" style="width: 995px; height: 200px;
                                border: solid 1px Green;" rows="10"></textarea>
                        </td>
                    </tr>
                </table>
                <table class=" searchTable">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnReturn" runat="server" Text="回复" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnReturn" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
