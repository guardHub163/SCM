<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.Supplier.List" Title="无标题页" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;供应商
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    供应商名称：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSupplierName" runat="server" Width="200"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    供应商地址：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSupplierAddress" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    供应商类型:
                </td>
                <td class="tdText">
                    <select id="selInputType" style="width: 100px;" runat="server">
                        <option value="0" selected="selected">全部</option>
                        <option value="1">成品</option>
                        <option value="2">原料</option>
                    </select>
                </td>
                <td style="text-align: right;" colspan="2">
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="CODE" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="CODE" HeaderText="编号"></asp:BoundField>
                        <asp:TemplateField HeaderText="供应商名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("NAME") %>'
                                    CommandArgument='<%# Eval("CODE") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="NAME_SHORT" HeaderText="供应商简称" SortExpression="NAME_SHORT">
                        </asp:BoundField>
                        <asp:BoundField DataField="ADDRESS" HeaderText="供应商地址"></asp:BoundField>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="供应商仓库"></asp:BoundField>
                        <asp:BoundField DataField="POST_CODE" HeaderText="邮政编码"></asp:BoundField>
                        <asp:BoundField DataField="TEL" HeaderText="联系方式"></asp:BoundField>
                        <asp:BoundField DataField="CONTACT" HeaderText="联系人"></asp:BoundField>
                        <asp:BoundField DataField="TYPE_NAME" HeaderText="商家类型"></asp:BoundField>
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
