<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.SalesPromotion.List" Title="促销" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;促销
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    部门编号：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtDepartmentCode" runat="server" Width="80px" OnTextChanged="Department_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                </td>
                <td class="tdTitle">
                部门名称：
                </td>
                <td class="tdText ">
                    <asp:Label ID="lblDepartmentName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
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
                        <asp:BoundField DataField="DEPARTMENT_CODE" HeaderText="部门编号"></asp:BoundField>
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="部门名称"></asp:BoundField>
                        <asp:BoundField DataField="NAME" HeaderText="名称"></asp:BoundField>
                        <asp:BoundField DataField="START_TIME" HeaderText="开始时间"  DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="END_TIME" HeaderText="结束时间"  DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="PROPERTY1" HeaderText="满额"></asp:BoundField>
                        <asp:BoundField DataField="PROPERTY2" HeaderText="减免"></asp:BoundField>
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
