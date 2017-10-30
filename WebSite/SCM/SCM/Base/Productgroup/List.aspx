<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.Productgroup.List" Title="无标题页" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;商品种类
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table class="inputTable" cellpadding="0px;" cellspacing="0px;" >
            <tr>
                <td class="tdTitle">
                    种类名称：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductGroupName" runat="server" Width="200"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    商品种类:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80" OnTextChanged="ProductGroupCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                    <asp:Label ID="lblProductGroupName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="CODE" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="CODE" HeaderText="编号"></asp:BoundField>
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("NAME") %>'
                                    CommandArgument='<%# Eval("CODE") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PARENT_NAME" HeaderText="种类"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE1" HeaderText="备注1"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE2" HeaderText="备注2"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE3" HeaderText="备注3"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="90px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F0F1F2"></AlternatingRowStyle>
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
