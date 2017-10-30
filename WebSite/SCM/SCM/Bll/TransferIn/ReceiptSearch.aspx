<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ReceiptSearch.aspx.cs" Inherits="SCM.Web.TransferIn.ReceiptSearch" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                入库&nbsp;>>&nbsp;成品入库查询
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            入库单号:
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
                                AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                            <img title="供应商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                            <asp:Label ID="lblSupplierName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            入库仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtWarehouseCode" runat="server" Width="80" OnTextChanged="Warehouse_Change"
                                AutoPostBack="true" CssClass="inputText"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                            <asp:Label ID="lblWarehouseName" runat="server" CssClass="img"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            入库日期:
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
                        <td style="text-align: right;" colspan="2">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <!--Search end-->
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <%--                    <asp:BoundField DataField="SLIP_NUMBER" HeaderText="入库单号"></asp:BoundField>--%>
                        <asp:TemplateField HeaderText="入库单号">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("SLIP_NUMBER")%>'
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:TemplateField>
                        <asp:BoundField DataField="INPUT_TYPE_NAME" HeaderText="订单类型"></asp:BoundField>
                        <asp:BoundField DataField="SUPPLIER_NAME" HeaderText="供应商"></asp:BoundField>
                        <asp:BoundField DataField="WAREHOUSE_NAME" HeaderText="入库仓库"></asp:BoundField>
                        <asp:BoundField DataField="ARRIVAL_DATE" HeaderText="入库日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="编辑"
                                    Enabled='<%# Eval("RECEIPT_TYPE").ToString()=="2"?true:false %>' OnClick="processClick"
                                    CommandArgument='<%# Eval("SLIP_NUMBER").ToString()+"|"+Eval("RECEIPT_TYPE").ToString() %>'
                                    CssClass="GridViewLinkButton" />
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    Enabled='<%# Eval("RECEIPT_TYPE").ToString()=="2"?true:false %>' OnClick="processClick"
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
        </asp:UpdatePanel>
    </div>
</asp:Content>
