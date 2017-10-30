<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="CashSearch.aspx.cs" Inherits="SCM.Web.Cash.CashSearch" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                销售&nbsp;>>&nbsp;钱箱
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
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
                            存取日期：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                alt="" class="img"></img>～
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                alt="" class="img"></img>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            存款银行
                        </td>
                        <td class="tdText">
                            <asp:DropDownList ID="BankType" runat="server" Width="150px">
                            </asp:DropDownList>
                        </td>
                        <td style="text-align: right;" colspan="2">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnExport" runat="server" Text="导入" OnClick="processClick" CssClass="LinkButton2"
                                Visible="false"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="BANK_SLIP_NUMBER"
                    AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center" CssClass="GridView"
                    AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField DataField="SLIP_NUMBER" HeaderText="编号" Visible="false"></asp:BoundField>
                        <asp:BoundField DataField="BANK_NAME" HeaderText="存款银行"></asp:BoundField>
                        <asp:BoundField DataField="LAST_UPDATE_TIME" HeaderText="存取时间" DataFormatString="{0:yyyy-MM-dd}"
                            HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="BANK_SLIP_NUMBER" HeaderText="存取流水号"></asp:BoundField>
                        <asp:BoundField DataField="PROFIT_CASH" HeaderText="本次收益"></asp:BoundField>
                        <asp:BoundField DataField="LAST_CASH" HeaderText="上次留存"></asp:BoundField>
                        <asp:BoundField DataField="TAKE_CASH" HeaderText="存取资金"></asp:BoundField>
                        <asp:BoundField DataField="BALANCE_CASH" HeaderText="剩余资金"></asp:BoundField>
                        <asp:BoundField DataField="SALES_SLIP_NUMBER" HeaderText="截止流水号"></asp:BoundField>
                        <asp:BoundField DataField="MEMO" HeaderText="备注"></asp:BoundField>
                    </Columns>
                    <AlternatingRowStyle BackColor="#F0F1F2"></AlternatingRowStyle>
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
