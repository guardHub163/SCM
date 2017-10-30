<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="SCM.Web.User.Show" Title="详细信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;用户&nbsp;>>&nbsp;详细信息
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="inputTable" >
        <tr>
            <td class="tdTitle_3">
                用户名 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblUSER_ID" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                真实姓名 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblTRUE_NAME" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                性别 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblSEX" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                电话 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblPHONE" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                邮箱 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblEMAIL" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                所属部门 ：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtDepartmentCode" runat="server" Width="80px"  CssClass="inputText" Enabled="false"></asp:TextBox>
                <img title="供应商查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg"/>
                <asp:Label ID="lblDepartmentName" runat="server" Enabled="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                所属供应商 ：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtSupplierCode" runat="server" Width="80px"  CssClass="inputText" Enabled="false"></asp:TextBox>
             <img title="供应商查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg"/>
                <asp:Label ID="lblSupplierName" runat="server"  Enabled="false"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                用户类型 ：
            </td>
            <td class="tdText_3">
                <asp:DropDownList ID="selUserType" runat="server" Width="100" Enabled="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                权限等级 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblROLES_ID" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                页面风格 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblSTYLE" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                照片:
            </td>
            <td class="tdText_3">
                <img id="imgPhoto" style="margin: 2px; height: 100px; width: 100px;" alt="" src=""
                    runat="server" /><br />
             </td>
        </tr>
    </table>
    <table class="operateTable">
        <tr>
            <td>
                <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
            </td>
        </tr>
    </table>
</asp:Content>
