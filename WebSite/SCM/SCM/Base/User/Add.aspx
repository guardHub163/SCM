<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="SCM.Web.User.Add" Title="新建" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />
     <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;用户&nbsp;>>&nbsp;新建
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable" >
                <tr>
                    <td class="tdTitle_3">
                        用户名 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtUSER_ID" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        密码 ：
                    </td>
                    <td class="tdText_3">
                        <input type="password" id="txtPASSWORD" runat="server" width="200px" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        确认密码 ：
                    </td>
                    <td class="tdText_3">
                        <input type="password" id="txtRePASSWORD" runat="server" width="200px" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        真实姓名 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtTRUE_NAME" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        性别 ：
                    </td>
                    <td class="tdText_3">
                        <asp:RadioButton ID="sex1" GroupName="sex" runat="server" Checked="true" Text="男" />
                        <asp:RadioButton ID="sex2" GroupName="sex" runat="server" Text="女" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        电话 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtPHONE" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        邮箱 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtEMAIL" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        所属部门 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtDepartmentCode" runat="server" Width="60px" OnTextChanged="Department_Change"
                            AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                        <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                        <asp:Label ID="lblDepartmentName" runat="server" CssClass=" label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        所属供应商：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtSupplierCode" runat="server" Width="60px" OnTextChanged="Supplier_Change"
                            AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                        <img title="供应商查询" alt="" src="../../Images/search.jpg" class="inputImg"  onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                        <asp:Label ID="lblSupplierName" runat="server"  CssClass=" label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        用户类型 ：
                    </td>
                    <td class="tdText_3">
                        <asp:DropDownList ID="selUserType" runat="server" Width="100">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        权限 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtROLES_ID" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr style="display:none ">
                    <td class="tdTitle_3">
                        页面风格 ：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtSTYLE" runat="server" Width="200px" ></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="operateTable" cellpadding="0" cellspacing="0">
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
