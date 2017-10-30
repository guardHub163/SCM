<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="TransferRelationSearch.aspx.cs" Inherits="SCM.Web.TransferIn.TransferRelationSearch"
     %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table class="navigation">
                <tr>
                    <td>
                        调拨&nbsp;>>&nbsp; 仓库调拨查询 
                    </td>
                </tr>
            </table>
            <div class="border_div">
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            调拨单号:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtSlipNumber" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            出库仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtFromWarehouseCode" runat="server" Width="80px" OnTextChanged="From_Warehouse_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtFromWarehouseCode.ClientID%>','<%=this.lblFromWarehouseName.ClientID%>');" />
                            <asp:Label ID="lblFromWarehouseName" runat="server" CssClass="img"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            出库日期:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtOutFromDate" runat="server" Width="80" OnTextChanged="OutFromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtOutFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>～
                            <asp:TextBox ID="txtOutToDate" runat="server" Width="80" OnTextChanged="OutToDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtOutToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td colspan="2" style="text-align: right; vertical-align: bottom;">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <%-- <asp:LinkButton ID="btnNew" runat="server" Text="新建" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>--%>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="GridView" DataKeyNames="SLIP_NUMBER"
                    AutoGenerateColumns="false" AlternatingRowStyle-BackColor="#F0F1F2" OnRowDataBound="gridView_RowDataBound">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="调拨单号">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("SLIP_NUMBER")%>'
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' />
                            </ItemTemplate>
                            <ItemStyle Width="110px"></ItemStyle>
                        </asp:TemplateField>
                        <%--     <asp:BoundField DataField="SLIP_NUMBER" HeaderText="调拨单号"></asp:BoundField>--%>
                        <asp:BoundField DataField="FROM_WAREHOUSE_NAME" HeaderText="出库仓库"></asp:BoundField>
                        <asp:BoundField DataField="TO_WAREHOUSE_NAME" HeaderText="入库仓库"></asp:BoundField>
                        <asp:BoundField DataField="SHIPMENT_SLIP_NUMBER" HeaderText="出库单号"></asp:BoundField>
                        <asp:BoundField DataField="TRANSFER_IN_SLIP_NUMBER" HeaderText="入库单号"></asp:BoundField>
                        <asp:BoundField DataField="CREATE_DATE_TIME" HeaderText="调拨时间" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="CREATE_NAME" HeaderText="负责人员"></asp:BoundField>
                        <%--<asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                    OnClick="processClick" CommandArgument='<%# Eval("SLIP_NUMBER") %>' CssClass="GridViewLinkButton" />
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
    </asp:UpdatePanel>
    </div>
</asp:Content>
