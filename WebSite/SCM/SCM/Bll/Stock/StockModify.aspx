<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="StockModify.aspx.cs" Inherits="SCM.Web.Stock.StockModify" %>

<%@ Register Src="~/UploadFileControl.ascx" TagName="UploadFile" TagPrefix="UploadFileControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <style type="text/css">
        .import_div
        {
            position: absolute;
            top: 324px;
            left: 1px;
            width: 350px;
            padding: 2px;
            z-index: 10;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                库存&nbsp;>>&nbsp;库存修改
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table id="tabSearch" cellpadding="0" cellspacing="0" class="inputTable">
                <tr>
                    <td class="tdTitle">
                        修改时间:
                    </td>
                    <td class="tdText">
                        <asp:Label ID="txtDate" runat="server" Width="200px"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        修改人员:
                    </td>
                    <td class="tdText">
                        <asp:Label ID="txtName" runat="server" Width="200px"></asp:Label>
                        &nbsp;
                        <asp:Label ID="lblUnit" runat="server" Width="200px" Visible="false"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        仓库编号:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtWarehouseCode" runat="server" Width="100" OnTextChanged="Warehouse_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <asp:ImageButton ID="btnWarehouse" runat="server" OnClick="processClick" ImageUrl="../../Images/search.jpg"
                            CssClass="inputImg" />
                        <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        商品编号:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtProductCode" runat="server" Width="100" CssClass="inputText"
                            OnTextChanged="Product_Changed" AutoPostBack="true"></asp:TextBox>
                        <asp:ImageButton ID="btnProduct" runat="server" OnClick="processClick" ImageUrl="../../Images/search.jpg"
                            CssClass="inputImg" />
                        <asp:Label ID="lblProductName" runat="server" CssClass=" label"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        款式:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtStyleCode" runat="server" Width="100" Enabled="false" CssClass="inputText"></asp:TextBox>
                        <img title="款式查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                        <asp:Label ID="lblStyleName" runat="server" search_disabled="" CssClass=" label"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        颜色:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtColorCode" runat="server" Width="100" Enabled="false" CssClass="inputText"></asp:TextBox>
                        <img title="颜色查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                        <asp:Label ID="lblColorName" runat="server" Text="" CssClass=" label"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        尺码:
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtSizeCode" runat="server" Width="100" Enabled="false" CssClass="inputText"></asp:TextBox>
                        <img title="尺码查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                        <asp:Label ID="lblSizeName" runat="server" Text="" CssClass=" label"></asp:Label>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        当前库存：
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="lblNowStock" runat="server" Width="100px" Enabled="false" CssClass=" textRight"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        修改库存：
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtUpdateStock" runat="server" Width="100px" OnTextChanged="UpdateStock_Change"
                            AutoPostBack="true" CssClass=" textRight"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        修改后库存：
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="lblUpdatelastStock" runat="server" Width="100px" Enabled="false"
                            CssClass=" textRight"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        修改理由：
                    </td>
                    <td class="tdText">
                        <asp:DropDownList ID="ddlPrice" Width="121px" runat="server" Enabled="true" Height="20px">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <table style="margin-top: 3px; margin-left: 3px; width: 500px;">
                <tr>
                    <td style="text-align: left; color:Green;">
                        <a href="#" onclick="window.open('../../ExplainHtml/StockExcelExplain.htm')">导入明细方法</a>
                    </td>
                    <td style="text-align: right;">
                        <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2">
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnImport" runat="server" Text="明细导入" OnClick="processClick"
                            CssClass="LinkButton3"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            <asp:PostBackTrigger ControlID="btnImport" />
        </Triggers>
    </asp:UpdatePanel>
    <asp:Panel ID="import_div" runat="server" CssClass="import_div" Visible="false">
        <UploadFileControl:UploadFile ID="uploadFile" runat="server" />
    </asp:Panel>
</asp:Content>
