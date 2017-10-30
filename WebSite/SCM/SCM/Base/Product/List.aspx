<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="List.aspx.cs" Inherits="SCM.Web.Product.List" Title="" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<%@ Register Src="~/UploadFileControl.ascx" TagName="UploadFile" TagPrefix="UploadFileControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
      <style type="text/css">
        .import_div1
        {
            position: absolute;
            top: 505px;
            left:2px;
            width: 248px;
            padding: 2px;
            z-index: 10;
        }
        .import_div
        {
            left:5px;
            width: 280px;
            padding: 2px;
            z-index: 10;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;商品
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table class="inputTable" cellpadding="0px;" cellspacing="0px;">
            <tr>
                <td class="tdTitle">
                    商品编号：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtCode" runat="server" Width="200"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    商品名称：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductName" runat="server" Width="200"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品种类:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductGroupCode" runat="server" Width="60" OnTextChanged="ProductGroupCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                    <asp:Label ID="lblProductGroupName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    尺码:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSizeCode" runat="server" Width="60" OnTextChanged="SizeCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品尺码查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SIZE','<%=this.txtSizeCode.ClientID%>','<%=this.lblSizeName.ClientID%>');" />
                    <asp:Label ID="lblSizeName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品款式:
                </td>
                <td class="tdText" style="width: 250px;">
                    <asp:TextBox ID="txtStyleCode" runat="server" Width="60" OnTextChanged="SysleCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                    <asp:Label ID="lblStyleName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    单位：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtUnitCode" runat="server" Width="60" OnTextChanged="UnitCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品单位查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('UNIT','<%=this.txtUnitCode.ClientID%>','<%=this.lblUnitName.ClientID%>');" />
                    <asp:Label ID="lblUnitName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    颜色:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtColorCode" runat="server" Width="60" OnTextChanged="ClolorCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品颜色查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('COLOR','<%=this.txtColorCode.ClientID%>','<%=this.lblColorName.ClientID%>');" />
                    <asp:Label ID="lblColorName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td style="text-align: right;" colspan="2">
                    <a href="#" id="btnCancel" class="LinkButton3" onclick="window.open('../../ExplainHtml/productExplain.htm')">导入明细</a>
                    <asp:LinkButton ID="btnImport" runat="server" Text="导入" OnClick="processClick"
                        CssClass="LinkButton3"></asp:LinkButton>
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
                        <asp:TemplateField HeaderText="名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("NAME") %>'
                                    CommandArgument='<%# Eval("CODE") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="PRODUCT_SPEC" HeaderText="规格"></asp:BoundField>
                        <asp:BoundField DataField="STYLE_NAME" HeaderText="款式"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_GROUP_NAME" HeaderText="种类"></asp:BoundField>
                        <asp:BoundField DataField="COLOR_NAME" HeaderText="颜色"></asp:BoundField>
                        <asp:BoundField DataField="SIZE_NAME" HeaderText="尺码"></asp:BoundField>
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="单位"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnPhoto" runat="server" CausesValidation="false" CommandArgument='<%# Eval("CODE") %>'
                                    Text="照片" CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("CODE") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="130px"></ItemStyle>
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
      <asp:Panel ID="import_div" runat="server" CssClass="import_div" Visible="false" 
            Width="414px">
            <uploadfilecontrol:uploadfile id="uploadFile" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
