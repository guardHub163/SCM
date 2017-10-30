<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.VipCustomer.List" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料&nbsp;>>&nbsp;客户
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtCode" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            真实姓名:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtName" runat="server" Width="200"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            门店:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtDepartmentCode" runat="server" Width="60px" OnTextChanged="Department_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="门店查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                            <asp:Label ID="lblDepartmentName" runat="server" CssClass=" label"></asp:Label>
                        </td>
                        <td style="text-align: right;" colspan="2">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <!--Search end-->
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="CODE" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="CODE" HeaderText="编号"></asp:BoundField>
                        <asp:TemplateField HeaderText="姓名">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("NAME") %>'
                                    CommandArgument='<%# Eval("CODE") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="150px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPARTEMENT_NAME" HeaderText="部门"></asp:BoundField>
                        <asp:BoundField DataField="ADDRESS" HeaderText="地址"></asp:BoundField>
                        <asp:BoundField DataField="QQ" HeaderText="QQ" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="EMAIL" HeaderText="邮箱" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="WW" HeaderText="旺旺" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="BIRTH_DATE" HeaderText="生日" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="DISCOUNT_RATE" HeaderText="折扣" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="POINTS" HeaderText="积分" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="LAST_SALES_DATE" HeaderText="最后消费时间"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnSales" runat="server" CausesValidation="false" Text="详细" OnClick="processClick"
                                    CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="135px"></ItemStyle>
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
