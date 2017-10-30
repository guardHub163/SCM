<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="SalesOrderSearch.aspx.cs" Inherits="SCM.Web.Sales.SalesOrderSearch" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                销售&nbsp;>>&nbsp;销售查询
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle">
                            流水号：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtCode" runat="server" Width="200"></asp:TextBox>
                        </td>
                        <td class="tdTitle">
                            部门:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtDepartmentCode" runat="server" Width="80px" OnTextChanged="Department_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartmentCode.ClientID%>','<%=this.lblDepartmentName.ClientID%>');" />
                            <asp:Label ID="lblDepartmentName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            商品:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtProductCode" runat="server" Width="80px" OnTextChanged="Product_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="商品查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('PRODUCT','<%=this.txtProductCode.ClientID%>','<%=this.lblProductName.ClientID%>');" />
                            <asp:Label ID="lblProductName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            销售人员：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtUserCode" runat="server" Width="80px" OnTextChanged="User_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="人员查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('USER','<%=this.txtUserCode.ClientID%>','<%=this.lblUserName.ClientID%>');" />
                            <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            销售日期：
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtFromDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                alt="" class="img"></img>～
                            <asp:TextBox ID="txtToDate" runat="server" Width="80px" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtToDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                alt="" class="img"></img>
                        </td>
                        <td style="text-align: right;" colspan="2">
                            <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnDerive" runat="server" Text="导出" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <asp:GridView ID="gridView" runat="server" CellPadding="0" DataKeyNames="SLIP_NUMBER"
                    OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
                    CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                    <HeaderStyle CssClass="GridViewHeader" />
                    <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:TemplateField HeaderText="流水号">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnShow" runat="server" CausesValidation="false" Text='<%# Eval("SLIP_NUMBER") %>'
                                    CommandArgument='<%# Eval("SLIP_NUMBER") %>' />
                                <itemstyle width="200px"></itemstyle>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="DEPARTMENT_NAME" HeaderText="部门"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_CODE" HeaderText="商品编号"></asp:BoundField>
                        <asp:BoundField DataField="PRODUCT_NAME" HeaderText="商品名称"></asp:BoundField>
                        <%--<asp:BoundField DataField="UNIT_NAME" HeaderText="单位"></asp:BoundField>--%>
                        <asp:BoundField DataField="SALES_EMPLOYEE" HeaderText="销售人员"></asp:BoundField>
                        <asp:BoundField DataField="CUSTOMER_NAME" HeaderText="客户"></asp:BoundField>
                        <asp:BoundField DataField="PRICE" HeaderText="单价" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="QUANTITY" HeaderText="数量" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="AMOUNT" HeaderText="总金额" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                        <%--<asp:BoundField DataField="POINTS" HeaderText="积分" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>
                        <asp:BoundField DataField="USED_POINTS" HeaderText="消费积分" HeaderStyle-CssClass=" headerRight"
                            ReadOnly="True" DataFormatString="{0:F0}" HtmlEncode="false"></asp:BoundField>--%>
                        <asp:BoundField DataField="CREATE_DATE_TIME" HeaderText="消费时间"></asp:BoundField>
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
                <asp:PostBackTrigger ControlID="btnDerive" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
