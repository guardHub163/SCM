<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Modify.aspx.cs" Inherits="SCM.Web.Product.Modify" Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
 <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
    <base target="_self" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;商品&nbsp;>>&nbsp;编辑
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
               规格：
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtProduct_spec" runat="server" Width="200px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                <asp:Label ID="Label1" runat="server" Text="种类:"></asp:Label> &nbsp;
            </td>
            <td class="tdText_3" style="width: 250px;">
                <asp:TextBox ID="txtProductGroupCode" runat="server" OnTextChanged="ProductGroupCode_Chanage" AutoPostBack="true" CssClass="inputText"
                    Width="80"></asp:TextBox>
                <img title="商品种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');"/>
                <asp:Label ID="lblProductGroupName" runat="server" CssClass="label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                <asp:Label ID="Label2" runat="server" Text="款式:"></asp:Label> &nbsp;
            </td>
            <td class="tdText_3" style="width: 250px;">
                <asp:TextBox ID="txtStyleCode" runat="server" OnTextChanged="StyleCode_Chanage" Width="80" AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                <img title="商品款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');"/>
                <asp:Label ID="lblStyleName" runat="server" CssClass="label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                <asp:Label ID="Label3" runat="server" Text="颜色:"></asp:Label> &nbsp;
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtColorCode" runat="server" Width="80" OnTextChanged="ClolorCode_Chanage" AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                <img title="商品颜色查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('COLOR','<%=this.txtColorCode.ClientID%>','<%=this.lblColorName.ClientID%>');" />
                <asp:Label ID="lblColorName" runat="server" CssClass="label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                <asp:Label ID="Label4" runat="server" Text="尺码:"></asp:Label> &nbsp;
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtSizeCode" runat="server" Width="80" OnTextChanged="SizeCode_Chanage" AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                <img title="商品尺码查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SIZE','<%=this.txtSizeCode.ClientID%>','<%=this.lblSizeName.ClientID%>');" />
                <asp:Label ID="lblSizeName" runat="server" CssClass="label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="tdTitle_3">
                <asp:Label ID="Label5" runat="server" Text="单位:"></asp:Label> &nbsp;
            </td>
            <td class="tdText_3">
                <asp:TextBox ID="txtUnitCode" runat="server" Width="80" OnTextChanged="UnitCode_Chanage" AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                <img title="商品单位查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('UNIT','<%=this.txtUnitCode.ClientID%>','<%=this.lblUnitName.ClientID%>');" />
                <asp:Label ID="lblUnitName" runat="server" CssClass="label"></asp:Label>
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
            <td style="text-align: right;">
                 <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass ="LinkButton2" >
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
