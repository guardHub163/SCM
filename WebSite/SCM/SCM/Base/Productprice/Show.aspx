<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="SCM.Web.Productprice.Show" Title="详细信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;单价&nbsp;>>&nbsp;详细信息
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="inputTable" >
        <tr>
            <td class="tdTitle_3">
                编号 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblId" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                原价：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblOriPrice" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                价格：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblPrice" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                折扣：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblDricount" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                种类：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblType" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                部门 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblDepartment" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                样式：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblStyle" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                备注1：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblAttribute1" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                备注2：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblAttribute2" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                备注3：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblAttribute3" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                起始时间：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblStartTime" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                结束时间：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblEndTime" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                创建人员：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblCreate_user" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                创建时间：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblCreate_date_time" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                更新人员：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblLast_update_user" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                更新时间：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblLast_update_time" runat="server"></asp:Label> &nbsp;
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
