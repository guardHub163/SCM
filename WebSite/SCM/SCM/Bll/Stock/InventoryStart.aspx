<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="InventoryStart.aspx.cs" Inherits="SCM.Web.Stock.InventoryStart" Title="创建盘点单" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                库存盘点&nbsp;>>&nbsp;创建盘点单
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable">
                <tr>
                    <td class="tdTitle">
                        盘点开始日期：
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtFromDate" runat="server" Width="80" Enabled="false"></asp:TextBox>
                        <img src="../../Script/My97DatePicker/skin/datePicker.gif" style="width: 16px; height: 22px;
                            vertical-align: middle" alt="" class="img" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle">
                        盘点仓库：
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtWarehouseCode" runat="server" Width="60" OnTextChanged="Warehouse_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                        <asp:Label ID="lblWarehouseName" runat="server" CssClass=" label"></asp:Label>
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
                </tr>
            </table>
            <table style="margin-top: 5px; margin-left: 5px; width: 390px;">
                <tr>
                    <td style="text-align: right;">
                        <asp:LinkButton ID="btnSave" runat="server" Text="创建" OnClick="processClick" CssClass="LinkButton2">
                        </asp:LinkButton>
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
