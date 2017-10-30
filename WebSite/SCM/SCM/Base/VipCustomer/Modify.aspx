<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Modify.aspx.cs" Inherits="SCM.Web.VipCustomer.Modify" Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料&nbsp;>>&nbsp;客户&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable">
                <tr>
                    <td class="tdTitle_3">
                        编号：
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblCode" runat="server" Width="200px"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        等级：
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblLevel" runat="server" Width="200px"></asp:Label> &nbsp;
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
                        门店:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtDepartmentCode" runat="server" Width="60px" OnTextChanged="Department_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="门店查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                        <asp:Label ID="lblDepartmentName" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        地址：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAdress" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        QQ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtQQ" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        Email：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtEmail" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        旺旺：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtWW" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        生日：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtbirth" runat="server" Width="80" OnTextChanged="txtbirth_TextChanged"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        <img onclick="WdatePicker({el:'<%=this.txtbirth.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt=""  class="img"></img>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        消费日期：
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblSalesTime" runat="server" Width="200px"></asp:Label> &nbsp;
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        折扣：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtDiscount" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        积分：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtPoints" runat="server" Width="200px" Enabled="false"></asp:TextBox> &nbsp;
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
