<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ProductSearchTwo.aspx.cs" Inherits="SCM.Web.Common.ProductSearchTwo"
    Title="商品检索" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />
        <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
        <tr>
            <!--种类-->
            <td class="tdTitle" style="width: 100px;">
                <asp:Label ID="Label1" runat="server" Text="种类:"></asp:Label>
            </td>
            <td class="tdText" style="width: 250px;">
                <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                <input type="button" id="btnProductGroup" value=".." onclick="processMasterClick(this,'<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                <asp:Label ID="lblProductGroupName" runat="server" Text=""></asp:Label>
            </td>
            <!--款式 -->
            <td class="tdTitle" style="width: 100px;">
                <asp:Label ID="Label2" runat="server" Text="款式:"></asp:Label>
            </td>
            <td class="tdText" style="width: 250px;">
                <asp:TextBox ID="txtStyleCode" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                <input type="button" id="btnStyle" value=".." onclick="processMasterClick(this,'<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                <asp:Label ID="lblStyleName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <!--颜色-->
            <td class="tdTitle">
                <asp:Label ID="Label3" runat="server" Text="颜色:"></asp:Label>
            </td>
            <td class="tdText">
                <asp:TextBox ID="txtColorCode" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                <input type="button" id="btnColor" value=".." onclick="processMasterClick(this,'<%=this.txtColorCode.ClientID%>','<%=this.lblColorName.ClientID%>');" />
                <asp:Label ID="lblColorName" runat="server" Text=""></asp:Label>
            </td>
            <!--尺码 -->
            <td class="tdTitle">
                <asp:Label ID="Label4" runat="server" Text="尺码:"></asp:Label>
            </td>
            <td class="tdText">
                <asp:TextBox ID="txtSizeCode" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                <input type="button" id="btnSize" value=".." onclick="processMasterClick(this,'<%=this.txtSizeCode.ClientID%>','<%=this.lblSizeName.ClientID%>');" />
                <asp:Label ID="lblSizeName" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <!--名称 -->
            <td class="tdTitle">
                <asp:Label ID="Label5" runat="server" Text="名称:"></asp:Label>
            </td>
            <td class="tdText">
                <asp:TextBox ID="txtProductName" runat="server" Width="200" MaxLength="20"></asp:TextBox>
            </td>
            <!--查询 -->
            <td colspan="2" style="text-align: right;">
                <asp:Button ID="btnSearch" runat="server" Text="查询" OnClick="processClick" />
            </td>
        </tr>
    </table>
    <table id="tabList" style="width: 722px; border: 1px solid green; margin-top: 5px;
        margin-left: 5px;" cellpadding="0" cellspacing="0">
        <tr class="GridViewHeader">
            <td style="width: 20px; border-right: 1px solid green;">
                &nbsp;
            </td>
            <td style="width: 100px; border-right: 1px solid green;">
                &nbsp;编号
            </td>
            <td style="width: 200px; border-right: 1px solid green;">
                &nbsp;名称
            </td>
            <td style="width: 100px; border-right: 1px solid green;">
                &nbsp;款式
            </td>
            <td style="width: 100px; border-right: 1px solid green;">
                &nbsp;颜色
            </td>
            <td style="width: 100px; border-right: 1px solid green;">
                &nbsp;尺码
            </td>
            <td style="width: 100px; text-align: center;">
                &nbsp;数量
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                Width="720px" Height="212px">
                <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                    DataKeyNames="CODE" Width="702px" OnRowDataBound="gridView_RowDataBound" RowStyle-HorizontalAlign="Center"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#F0F1F2" ShowHeader="false">
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:TemplateField ItemStyle-Width="20px">
                            <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField ItemStyle-Width="100px" DataField="CODE" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="200px" DataField="NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="0px" DataField="STYLE" ReadOnly="True" ItemStyle-BorderWidth="0px" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="STYLE_NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="0px" DataField="COLOR" ReadOnly="True" ItemStyle-BorderWidth="0px" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="COLOR_NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="0px" DataField="SIZE" ReadOnly="True" ItemStyle-BorderWidth="0px" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="SIZE_NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="0px" DataField="UNIT_CODE" ReadOnly="True" ItemStyle-BorderWidth="0px" />
                        <asp:BoundField ItemStyle-Width="0px" DataField="UNIT_NAME" ReadOnly="True" ItemStyle-BorderWidth="0px" />
                        <asp:TemplateField ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:TextBox ID="txtQuantity" runat="server" Width="75px" Height="18px" BorderWidth="0"
                                    Text='<%# Eval("QUANTITY")%>' MaxLength="10" BackColor="#FF99FF" CssClass="textRight"
                                    OnTextChanged="Quantity_Changed" AutoPostBack="true"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <table style="width: 720px;">
                <tr>
                    <td style="text-align: right;">
                        <asp:Button ID="btnOK" runat="server" Text="确定" OnClick="processClick" />
                        <input type="button" id="btnCancel" value="取消" onclick="window.returnValue = new Array();window.close();" />
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
