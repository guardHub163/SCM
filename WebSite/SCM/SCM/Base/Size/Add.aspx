<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Add.aspx.cs" Inherits="SCM.Web.Size.Add" Title="新建" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />
     <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                 基本资料 &nbsp;>>&nbsp;尺码&nbsp;>>&nbsp;新建
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
                    <td class="tdText_3">
                        <asp:TextBox ID="txtCode" runat="server" Width="200px"></asp:TextBox>
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
                        商品种类:
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtProductGroupCode" runat="server" Width="60" OnTextChanged="ProductGroupCode_Chanage" CssClass="inputText"
                            AutoPostBack="true"></asp:TextBox>
                        <img title="种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                        <asp:Label ID="lblProductGroupName" runat="server" Text=""></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="tdTitle_3">
                        参考比例：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtReference" runat="server" Width="200px"></asp:TextBox>
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
