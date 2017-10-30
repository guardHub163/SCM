<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="SCM.Web.VipCustomer.Show" Title="详细信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料&nbsp;>>&nbsp;客户&nbsp;>>&nbsp;详细信息
            </td>
        </tr>
    </table>
    <table cellpadding="0" cellspacing="0" class="inputTable">
        <tr>
            <td class="tdTitle_3">
                编号 ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblCode" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                名称：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblName" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                等级：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblLevel" runat="server" Width="200px"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                门店：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblDepartment" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                地址：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblAdress" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                QQ：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblQQ" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                Email：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblEmail" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                旺旺：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblWw" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                生日：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblBirth" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                消费日期：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblSalesTime" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                折扣：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblDiscount" runat="server"></asp:Label> &nbsp;
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                积分：
            </td>
            <td class="tdText_3">
                <asp:Label ID="LblPoints" runat="server"></asp:Label> &nbsp;
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
