﻿<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Show.aspx.cs" Inherits="SCM.Web.Productgroup.Show" Title="详细信息" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;商品种类&nbsp;>>&nbsp;详细信息
            </td>
        </tr>
    </table>
              <table class="inputTable" cellpadding="0" cellspacing="0" >
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
                种类：
            </td>
            <td class="tdText_3">
                <asp:Label ID="lblProductGroupCode" runat="server"></asp:Label> &nbsp;
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
            <td style="text-align: right">
                 <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
            </td>
        </tr>
    </table>
</asp:Content>
