<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="SCM.Web.Department.Add" Title="新建" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server"> 
     <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;部门&nbsp;>>&nbsp;新建
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable" >
                <tr>
                    <td class="tdTitle_3">
                        编号：
                    </td>
                    <td class="tdText_3" >
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        部门名称：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtName" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        上级部门：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtDepartment_Code" runat="server" Width="71px" OnTextChanged="Department_change" CssClass="inputText"
                            AutoPostBack="true"></asp:TextBox>
                        <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg"  onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartment_Code.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                        <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
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
