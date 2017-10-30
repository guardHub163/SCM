<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="StockList.aspx.cs" Inherits="SCM.Web.Stock.StockList" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                库存&nbsp;>>&nbsp;库存查询
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80px" OnTextChanged="Warehouse_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                            <asp:Label ID="lblWarehouseName" runat="server" CssClass=" label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            商品:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtProductCode" runat="server" Width="80px" OnTextChanged="Product_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="商品查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT','<%=this.txtProductCode.ClientID%>','<%=this.lblProductName.ClientID%>');" />
                            <asp:Label ID="lblProductName" runat="server" CssClass=" label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            商品种类:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80px" OnTextChanged="ProductGroupCode_Chanage"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="种类查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                            <asp:Label ID="lblProductGroupName" runat="server" Text="" CssClass=" label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            颜色:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtColorCode" runat="server" Width="80px" OnTextChanged="ClolorCode_Chanage"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="颜色查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('COLOR','<%=this.txtColorCode.ClientID%>','<%=this.lblColorName.ClientID%>');" />
                            <asp:Label ID="lblColorName" runat="server" Text="" CssClass=" label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            商品款式:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtStyleCode" runat="server" Width="80px" OnTextChanged="SysleCode_Chanage"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                            <asp:Label ID="lblStyleName" runat="server" Text="" CssClass=" label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            尺码:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtSizeCode" runat="server" Width="80px" OnTextChanged="SizeCode_Chanage"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="尺码查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SIZE','<%=this.txtSizeCode.ClientID%>','<%=this.lblSizeName.ClientID%>');" />
                            <asp:Label ID="lblSizeName" runat="server" Text="" CssClass=" label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            库存数量:
                        </td>
                        <td class="tdText">
                            <asp:RadioButton ID="RadioButton1" GroupName="a" runat="server" Text="全部" />
                            <asp:RadioButton ID="RadioButton2" GroupName="a" runat="server" Text="大于0" />
                            <asp:RadioButton ID="RadioButton3" GroupName="a" runat="server" Text="等于0" />
                            <asp:RadioButton ID="RadioButton4" GroupName="a" runat="server" Text="小于0" />
                        </td>
                        <td colspan="2" style="text-align: right;">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnExcel" runat="server" Text="导出" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable" style="margin-top: 4px;
                    width: 350px;">
                    <tr>
                        <td class="tdTitle">
                            库存合计：
                        </td>
                        <td class="tdText" style="width: 200px; text-align: right;">
                            <asp:Label ID="txtStockTote" runat="server" CssClass="cellRight"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            入库预定合计：
                        </td>
                        <td class="tdText" style="width: 200px; text-align: right;">
                            <asp:Label ID="txtStouckEnter" runat="server" CssClass="cellRight"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            出库预定合计：
                        </td>
                        <td class="tdText" style="width: 200px; text-align: right;">
                            <asp:Label ID="txtStockOut" runat="server" CssClass="cellRight"></asp:Label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="WAREHOUSE_CODE" HeaderText="仓库编号"></asp:BoundField>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="仓库名称"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="商品编号"></asp:BoundField>
                        <asp:TemplateField HeaderText="商品名称">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("PRODUCT_NAME") %>'
                                    CommandArgument='<%# Eval("WAREHOUSE_CODE")+"|"+Eval("PRODUCT_CODE") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="200px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="STYLE_NAME" HeaderText="款式"></asp:BoundField>
                        <asp:BoundField DataField="COLOR_NAME" HeaderText="颜色"></asp:BoundField>
                        <asp:BoundField DataField="SIZE_NAME" HeaderText="尺码"></asp:BoundField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="库存数" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false" ItemStyle-CssClass=" cellRight">
                        </asp:BoundField>
                        <asp:BoundField DataField="SP_QUANTITY" HeaderText="出库预定数" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false" ItemStyle-CssClass=" cellRight">
                        </asp:BoundField>
                        <asp:BoundField DataField="RP_QUANTITY" HeaderText="入库预定数" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false" ItemStyle-CssClass=" cellRight">
                        </asp:BoundField>
                        <%--<asp:TemplateField HeaderText="操作">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                OnClick="processClick" CommandArgument='<%# Eval("WAREHOUSE_CODE")+"|"+Eval("PRODUCT_CODE") %>'
                                CssClass="GridViewLinkButton" />&nbsp;
                        </ItemTemplate>
                        <ControlStyle Height="15px" Width="35px"></ControlStyle>
                        <ItemStyle Width="45px"></ItemStyle>
                    </asp:TemplateField>--%>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:PostBackTrigger ControlID="btnExcel" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
