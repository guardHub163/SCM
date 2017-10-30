<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="DeliveryAnswer.aspx.cs" Inherits="SCM.Web.TransferIn.DeliveryAnswer" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                采购&nbsp;>>&nbsp;成品交期确认
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
                            <asp:TextBox ID="txtSlipNumber" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            入库仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80" OnTextChanged="Warehouse_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                            <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            订单类型:
                        </td>
                        <td class="tdText">
                            <select id="selInputType" style="width: 100px;" runat="server">
                                <option value="1">成品</option>
                            </select>
                        </td>
                        <td class="tdTitle">
                            供应商编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtSupplierCode" runat="server" Width="80" OnTextChanged="Supplier_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="供货商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                            <asp:Label ID="lblSupplierName" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            采购日期:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtStockFromDate" runat="server" Width="80" OnTextChanged="StockFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtStockFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtStockToDate" runat="server" Width="80" OnTextChanged="StockToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtStockToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td class="tdTitle">
                            商品编号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtProductCode" runat="server" Width="80" OnTextChanged="Product_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="商品查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT','<%=this.txtProductCode.ClientID%>','<%=this.lblProductName.ClientID%>');" />
                            <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            交货预定日:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtArrivalFromDate" runat="server" Width="80" OnTextChanged="ArrivalFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtArrivalFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtArrivalToDate" runat="server" Width="80" OnTextChanged="ArrivalToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtArrivalToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td colspan="2" style="text-align: right;">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" CssClass="GridView"
                    RowStyle-HorizontalAlign="Center" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" />
                        <asp:BoundField DataField="PURCHASE_SLIP_NUMBER" HeaderText="采购订单" />
                        <asp:BoundField DataField="PURCHASE_LINE_NUMBER" HeaderText="订单明细" />
                        <asp:BoundField DataField="INPUT_TYPE_NAME" HeaderText="订单类型" />
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="入库仓库" />
                        <asp:BoundField DataField="DEPARTUAL_DATE" HeaderText="采购日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" />
                        <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="交货预定日" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false" />
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品" />
                        <asp:BoundField DataField="QUANTITY" HeaderText="数量" />
                        <asp:BoundField DataField="UNIT_NAME" HeaderText="单位" />
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="状态" />
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />&nbsp;
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="50px"></ItemStyle>
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
