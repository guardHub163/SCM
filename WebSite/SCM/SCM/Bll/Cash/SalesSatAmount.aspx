<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="SalesSatAmount.aspx.cs" Inherits="SCM.Web.Cash.SalesSatAmount" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                统计&nbsp;>>&nbsp;销售统计
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    部门:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtDepartmentCode" runat="server" Width="80px" OnTextChanged="Department_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                    <asp:Label ID="lblDepartmentName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    商品种类:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80" OnTextChanged="ProductGroupCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                    <asp:Label ID="lblProductGroupName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    销售人员：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtUserCode" runat="server" Width="80" OnTextChanged="User_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="人员查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('USER','<%=this.txtUserCode.ClientID%>','<%=this.lblUserName.ClientID%>');" />
                    <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    商品款式:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtStyleCode" runat="server" Width="80" OnTextChanged="SysleCode_Chanage"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="商品款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                    <asp:Label ID="lblStyleName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    销售日期：
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                    <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                        alt="" class="img"></img>～
                    <asp:TextBox ID="txtToDate" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                    <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                        alt="" class="img"></img>
                </td>
                <td class="tdTitle">
                    分组条件：
                </td>
                <td class="tdText">
                    <asp:DropDownList ID="GroupCondition" runat="server" Width="150">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    图形选项:
                </td>
                <td class="tdText">
                    <asp:DropDownList ID="CharType" runat="server" Width="100" OnSelectedIndexChanged="CharType_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td style="text-align: right" colspan="2">
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                    <asp:LinkButton ID="btnExcel" runat="server" Text="导出" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Chart ID="Chart1" runat="server" Height="320px" Width="998px">
                </asp:Chart>
                <asp:Chart ID="Chart2" runat="server" Height="320px" Width="998px">
                </asp:Chart>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
