<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.Size.List" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;尺码
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    编号：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtCode" runat="server" Width="200"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    名称：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSizeName" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    种类:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductGroupCode" runat="server" Width="60" OnTextChanged="ProductGroupCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                    <asp:Label ID="lblProductGroupName" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: right" colspan="2">
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
                                    CommandArgument='<%# Eval("CODE")+"|"+Eval("PRODUCT_GROUP_CODE") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRODUCT_GROUP_NAME" HeaderText="种类"></asp:BoundField>
                        <asp:BoundField DataField="REFERENCE_PERCENTAGE" HeaderText="参考比例"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE1" HeaderText="备注1"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE2" HeaderText="备注2"></asp:BoundField>
                        <asp:BoundField DataField="ATTRIBUTE3" HeaderText="备注3"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE")+"|"+Eval("PRODUCT_GROUP_CODE") %>'
                                    CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE")+"|"+Eval("PRODUCT_GROUP_CODE") %>'
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
                <asp:AsyncPostBackTrigger ControlID="btnNew" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
