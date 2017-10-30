<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.News.List" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;新闻
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            发布时间：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtPublishFromDate" runat="server" Width="80" OnTextChanged="PublishFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtPublishFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtPublishToDate" runat="server" Width="80" OnTextChanged="PublishToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtPublishToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td class="tdTitle">
                            发布人员：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtUserCode" runat="server" Width="60" OnTextChanged="User_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="人员查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('USER','<%=this.txtUserCode.ClientID%>','<%=this.lblUserName.ClientID%>');" />
                            <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            关键字：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtTitle" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            类型
                        </td>
                        <td class="tdText">
                            <asp:DropDownList ID="selNewsType" runat="server" Width="100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class=" searchTable" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="ID" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="ID" ItemStyle-Width="0"></asp:BoundField>
                        <asp:TemplateField HeaderText="新闻标题">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("NEWS_TITLE") %>'
                                    CommandArgument='<%# Eval("ID") %>' />
                                <itemstyle width="200px"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="TYPE_NAME" HeaderText="新闻类型"></asp:BoundField>
                        <asp:BoundField DataField="CREATE_USER" HeaderText="创建人员" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="CREATE_NAME" HeaderText="发布人员"></asp:BoundField>
                        <asp:BoundField DataField="PUBLISH_DATE" HeaderText="发布时间"></asp:BoundField>
                        <asp:BoundField DataField="NEWS_CONTENT" HeaderText="新闻内容"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("ID")+"|"+Eval("CREATE_USER") %>'
                                    CssClass="GridViewLinkButton"></asp:LinkButton>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("ID")%>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="95px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
