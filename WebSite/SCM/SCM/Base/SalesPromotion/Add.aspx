<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="SCM.Web.SalesPromotion.Add" Title="新建" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;促销&nbsp;>>&nbsp;添加
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
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        部门：
                    </td>
                    <td class="tdText">
                        <asp:TextBox ID="txtDepartmentCode" runat="server" Width="80px" OnTextChanged="Department_Change"
                            CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                        <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                        <asp:Label ID="lblDepartmentName" runat="server" CssClass="label"></asp:Label>
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
                        开始时间：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtFromDate" runat="server" Width="190px" OnTextChanged="FromDate_Changed"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        结束时间：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtToDate" runat="server" Width="190px" OnTextChanged="ToDate_Changed"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        满额：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtProperty1" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        减免：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtProperty2" runat="server" Width="200px" OnTextChanged="txtProperty2_TextChanged"
                            AutoPostBack="true"></asp:TextBox>
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
