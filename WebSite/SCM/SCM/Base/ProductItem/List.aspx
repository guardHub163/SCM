<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.ProductItem.List" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;成品原料
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            商品编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtProductCode" runat="server" Width="160" OnTextChanged="Product_Changed"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <asp:Image title="商品种类查询" ID="btnProduct" runat="server" OnClick="processClick" ImageUrl="../../Images/search.jpg"
                                CssClass="inputImg" />
                        </td>
                        <td class="tdTitle">
                            商品名称：
                        </td>
                        <td class="tdText">
                            <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label> &nbsp;
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
                <asp:GridView ID="gridView" runat="server" CellPadding="0" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="商品编号"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品名称"></asp:BoundField>
                        <asp:BoundField DataField="ITEM_CODE" HeaderText="原料编号"></asp:BoundField>
                        <asp:BoundField DataField="ITEM_NAME" HeaderText="原料名称"></asp:BoundField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="数量"></asp:BoundField>
                        <asp:BoundField DataField="SUPPLIER_NAME" HeaderText="供货商"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE1" HeaderText="备注1"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE2" HeaderText="备注2"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE3" HeaderText="备注3"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("PRODUCT_CODE")+"|"+Eval("ITEM_CODE") %>'
                                    CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("PRODUCT_CODE")+"|"+Eval("ITEM_CODE") %>'
                                    CssClass="GridViewLinkButton" />
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
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
