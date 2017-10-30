<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="PurchaseSearch.aspx.cs" Inherits="SCM.Web.TransferIn.PurchaseSearch" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                采购&nbsp;>>&nbsp;成品采购
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    订单编号:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSlipNumber" runat="server" Width="200"></asp:TextBox>
                </td>
                <td class="tdTitle">
                    订单类型:
                </td>
                <td class="tdText">
                    <select id="selInputType" style="width: 100px;" runat="server">
                        <option value="1">成品</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    供应商编号:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtSupplierCode" runat="server" Width="80" OnTextChanged="Supplier_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="供应商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                    <asp:Label ID="lblSupplierName" runat="server" CssClass="label"></asp:Label>
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
                    采购日期:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="80" OnTextChanged="FromDate_Changed"
                        AutoPostBack="true" MaxLength="10"></asp:TextBox>
                    <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                        style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                    <asp:TextBox ID="txtToDate" runat="server" Width="80" OnTextChanged="ToDate_Changed"
                        AutoPostBack="true" MaxLength="10"></asp:TextBox>
                    <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                        style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                </td>
                <td class="tdTitle">
                    入库状态:
                </td>
                <td class="tdText">
                    <asp:RadioButton ID="rdoAll" runat="server" GroupName="rdoStatus" Text="全部" Checked="true" />
                    <asp:RadioButton ID="rdo1" runat="server" GroupName="rdoStatus" Text="未入库" />
                    <asp:RadioButton ID="rdo2" runat="server" GroupName="rdoStatus" Text="入库" />
                </td>
            </tr>
        </table>
        <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
            <tr>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <!--Search end-->
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <%--<asp:BoundField DataField="SLIP_NUMBER" HeaderText="订单编号"></asp:BoundField>--%>
                        <asp:TemplateField HeaderText="订单编号">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("SLIP_NUMBER")%>'
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="INPUT_TYPE_NAME" HeaderText="订单类型"></asp:BoundField>
                        <asp:BoundField DataField="SUPPLIER_NAME" HeaderText="供应商"></asp:BoundField>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="入库仓库"></asp:BoundField>
                        <asp:BoundField DataField="PURCHASE_DATE" HeaderText="采购日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="RECEIPT_STATUS_NAME" HeaderText="入库状态"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER").ToString()+"|"+Eval("RECEIPT_STATUS_FLAG").ToString() %>'
                                    Enabled='<%# Eval("RECEIPT_STATUS_FLAG").ToString()=="0"?true:false %>' CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    Enabled='<%# Eval("RECEIPT_STATUS_FLAG").ToString()=="0"?true:false %>' OnClick="processClick"
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
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
