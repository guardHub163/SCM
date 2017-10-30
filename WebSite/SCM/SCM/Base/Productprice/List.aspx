<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.Productprice.List" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<%@ Register Src="~/UploadFileControl.ascx" TagName="UploadFile" TagPrefix="UploadFileControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />
    <style type="text/css">
        .style1
        {
            height: 25px;
            padding-left: 5px;
            background-color: #F4F8FC;
            border-bottom: solid 1px Green;
            border-right: solid 1px Green;
            width: 250px;
        }
        .import_div
        {
            left: 5px;
            width: 430px;
            padding: 2px;
            z-index: 10;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;商品单价
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    款式:
                </td>
                <td class="style1">
                    <asp:TextBox ID="txtStyleCode" runat="server" Width="100" OnTextChanged="SysleCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                    <asp:Label ID="lblStyleName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    部门：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtDepartment_Code" runat="server" Width="100px" OnTextChanged="Department_change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartment_Code.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    种类:
                </td>
                <td class="style1">
                    <asp:DropDownList ID="ddlPrice" Width="125" runat="server" Enabled="true">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right;" colspan="2" align="justify">
                    <a href="#" id="btnCancel" class="LinkButton3" onclick="window.open('../../ExplainHtml/ProductPriceExplain.htm')">
                        导入明细</a>
                    <asp:LinkButton ID="btnImport" runat="server" Text="导入" OnClick="processClick" CssClass="LinkButton3"></asp:LinkButton>
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="ID" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="ID" HeaderText="编号"></asp:BoundField>
                        <asp:TemplateField HeaderText="价格">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("SALES_PRICE") %>'
                                    CommandArgument='<%# Eval("ID") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ORI_PRICE" HeaderText="原价"></asp:BoundField>
                        <asp:BoundField DataField="DISCOUNT_RATE" HeaderText="折扣"></asp:BoundField>
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="部门"></asp:BoundField>
                        <asp:BoundField DataField="STYLE_NAME" HeaderText="样式"></asp:BoundField>
                        <asp:BoundField DataField="PARENT_NAME" HeaderText="种类"></asp:BoundField>
                        <asp:BoundField DataField="START_DATE" HeaderText="起始时间" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="END_DATE" HeaderText="结束时间" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
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
        <asp:Panel ID="import_div" runat="server" CssClass="import_div" Visible="false">
            <UploadFileControl:UploadFile ID="uploadFile" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
