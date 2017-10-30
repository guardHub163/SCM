<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="SCM.Web.ProductItem.Add" Title="新建" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                 基本资料 &nbsp;>>&nbsp;成品原料&nbsp;>>&nbsp;新建
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable" >
                <tr>
                    <td class="tdTitle_3">
                        商品编号：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtProductCode" runat="server" Width="100" OnTextChanged="Product_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <asp:ImageButton ID="btnProduct" runat="server" OnClick="processClick" ImageUrl="../../Images/search.jpg"
                            CssClass="inputImg" />
                        <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        原料编号：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtItemCode" runat="server" Width="100" OnTextChanged="Item_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="原料查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('ITEM','<%=this.txtItemCode.ClientID%>','<%=this.lblItemName.ClientID%>');" />
                        <asp:Label ID="lblItemName" runat="server" CssClass="label"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        供应商编号：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtSupplierCode" runat="server" Width="100" OnTextChanged="Supplier_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="供应商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                        <asp:Label ID="lblSupplierName" runat="server" CssClass="label"></asp:Label> &nbsp;
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
