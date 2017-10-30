<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="TransferOutPlan.aspx.cs" Inherits="SCM.Web.TransferOut.TransferOutPlan" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                出库&nbsp;>>&nbsp;成品出库预定
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0" cellspacing="0" class="inputTable">
            <tr>
                <td class="tdTitle">
                    出库仓库:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtWarehouseCode" runat="server" Width="100" OnTextChanged="Warehouse_Change"
                        CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtWarehouseCode.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    出库预定日:
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
            </tr>
        </table>
        <table class=" searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton3"></asp:LinkButton>
                    <asp:LinkButton ID="btnPrintWarehouse" runat="server" Text="拣货单打印" OnClick="processClick"
                        CssClass="LinkButton3"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" OnRowDataBound="gridView_RowDataBound"
                    AutoGenerateColumns="False" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="FROM_WAREHOUSE_CODE" HeaderText="仓库编号"></asp:BoundField>
                        <asp:BoundField DataField="FROM_WAREHOUSE_NAME" HeaderText="仓库名称"></asp:BoundField>
                        <asp:BoundField DataField="TO_WAREHOUSE_CODE" HeaderText="门店编号"></asp:BoundField>
                        <asp:BoundField DataField="TO_WAREHOUSE_NAME" HeaderText="门店名称"></asp:BoundField>
                        <asp:BoundField DataField="DEPARTUAL_DATE" HeaderText="出库日期" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="配送总量" HeaderStyle-CssClass="headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false" ItemStyle-CssClass="cellRight">
                        </asp:BoundField>
                        <asp:BoundField DataField="STATUS_NAME" HeaderText="状态"></asp:BoundField>
                        <asp:TemplateField HeaderText="操作">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="出货"
                                    Enabled='<%# Eval("STATUS_FLAG").ToString()=="0"?true:false %>' OnClick="processClick"
                                    CssClass="GridViewLinkButton" />&nbsp;
                            </ItemTemplate>
                            <ControlStyle Height="15px" Width="35px"></ControlStyle>
                            <ItemStyle Width="45px"></ItemStyle>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <asp:Panel ID="panelPage" runat="server" CssClass="panelPage" Visible="false">
                    <PageControl:Paging ID="paging" runat="server" />
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="btnPrintWarehouse" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
