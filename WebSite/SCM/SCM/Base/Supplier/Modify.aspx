<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Modify.aspx.cs" Inherits="SCM.Web.Supplier.Modify" Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                 基本资料 &nbsp;>>&nbsp;供应商&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="inputTable" >
        <tr>
            <td class="tdTitle_3">
                编号：
            </td>
            <td class="tdText_3">
                <asp:Label ID="txtCode" runat="server" Width="200px"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                名称：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                简称：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtName_short" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                类型：
            </td>
            <td class="tdText_3">
                <select id="selInputType" style="width: 100px;" runat="server">
                    <option value="1">成品</option>
                    <option value="2">原料</option>
                </select>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                地址：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtAddress" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                仓库:
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtWarehouseCode" runat="server" Width="60" OnTextChanged="Warehouse_Change" CssClass="inputText"
                    AutoPostBack="true"></asp:TextBox>
                <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                <asp:Label ID="lblWarehouseName" runat="server"  CssClass=" label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                邮编：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtPost_code" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                电话：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtTel" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                传真：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtFax" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                联系人：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtContact" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                电子邮件：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
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
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
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
