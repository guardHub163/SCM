<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ReceivingPlan.aspx.cs" Inherits="SCM.Web.Item.ReceivingPlan" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                入库&nbsp;>>&nbsp;原料入库预定
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table id="tabSearch" cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            采购单编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtSlipNumber" runat="server" Width="200px"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            入库仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80px" OnTextChanged="Warehouse_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                            <asp:Label ID="lblWarehouseName" runat="server" CssClass=" label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            订单类型:
                        </td>
                        <td class="tdText">
                            <select id="selInputType" style="width: 100px;" runat="server">
                                <option value="2">原料</option>
                            </select>
                        </td>
                        <td class="tdTitle">
                            供应商编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtSupplierCode" runat="server" Width="80px" OnTextChanged="Supplier_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="供货商" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                            <asp:Label ID="lblSupplierName" runat="server" CssClass=" label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            采购日期:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtStockFromDate" runat="server" Width="80px" OnTextChanged="StockFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtStockFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtStockToDate" runat="server" Width="80px" OnTextChanged="StockToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtStockToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td class="tdTitle">
                            商品编号:
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
                            入库预定日:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtReFromDate" runat="server" Width="80px" OnTextChanged="ReFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtReFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtReToDate" runat="server" Width="80px" OnTextChanged="ReToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtReToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td colspan="2" style="text-align: right;">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" />
                        <asp:BoundField DataField="PURCHASE_SLIP_NUMBER" HeaderText="采购订单编号" />
                        <asp:BoundField DataField="PURCHASE_LINE_NUMBER" HeaderText="明细NO" />
                        <%--                    <asp:BoundField DataField="INPUT_TYPE_NAME" HeaderText="订单类型" />--%>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="入库仓库" />
                        <asp:BoundField DataField="SUPPLIER_NAME" HeaderText="供应商" />
                        <asp:BoundField DataField="DEPARTUAL_DATE" HeaderText="采购日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" />
                        <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="预定入库日" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" />
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="商品编号" />
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品名称" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="数量" ReadOnly="True" DataFormatString="{0:F0}"
                            HtmlEncode="false" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="单位" />
                        <%--<asp:BoundField DataField="STATUS_NAME" HeaderText="状态" />--%>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnEnter" runat="server" CausesValidation="false" Text="入库" OnClick="processClick"
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="90px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
