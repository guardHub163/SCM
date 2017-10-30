<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Modify.aspx.cs" Inherits="SCM.Web.ProductItem.Modify" Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;成品原料&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable">
                <tr>
                    <td class="tdTitle_3">
                        商品:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtProductCode" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <input type="image" src="../../Images/search.jpg" class="inputImg" disabled="disabled"
                            onclick="processMasterClick(this,'<%=this.txtProductCode.ClientID%>','<%=this.lblProductName.ClientID%>');" />
                        <asp:Label ID="lblProductName" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        原料：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtItemCode" runat="server" Width="60" Enabled="false"></asp:TextBox>
                        <input type="image" src="../../Images/search.jpg" class="inputImg" disabled="disabled"
                            onclick="processMasterClick('ITEM','<%=this.txtItemCode.ClientID%>','<%=this.lblItemName.ClientID%>');" />
                        <asp:Label ID="lblItemName" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        供应商:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtSupplierCode" runat="server" Width="60" OnTextChanged="Supplier_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="供应商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                        <asp:Label ID="lblSupplierName" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        数量：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtQuantity" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        备注1：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAttribute1" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        备注2：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAttribute2" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        备注3：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAttribute3" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="operateTable">
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2">
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
