<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.User.List" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;用户
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            用户名:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtUserID" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            所属部门:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtDepartmentCode" runat="server" Width="80px" OnTextChanged="Department_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                            <asp:Label ID="lblDepartmentName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            真实姓名:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtTrueName" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            用户类型:
                        </td>
                        <td class="tdText">
                            <asp:DropDownList ID="selUserType" runat="server" Width="100">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table class="searchTable" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                             <asp:LinkButton ID="btnUpdate" runat="server" Text="修改密码" OnClick="processClick" CssClass="LinkButton3" Visible="false"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <!--Search end-->
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="ID" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="USER_ID" HeaderText="用户名"></asp:BoundField>
                        <asp:TemplateField HeaderText="真实姓名">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("TRUE_NAME") %>'
                                    CommandArgument='<%# Eval("ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="SEX" HeaderText="性别"></asp:BoundField>
                        <asp:BoundField DataField="PHONE" HeaderText="电话"></asp:BoundField>
                        <asp:BoundField DataField="EMAIL" HeaderText="邮箱"></asp:BoundField>
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="所属部门"></asp:BoundField>
                        <asp:BoundField DataField="USER_TYPE_NAME" HeaderText="用户类型"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("ID") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("ID") %>' CssClass="GridViewLinkButton" />
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
