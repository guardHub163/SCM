<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ReceiptModify.aspx.cs" Inherits="SCM.Web.Item.ReceiptModify" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                <label id="lblTitle" runat="server">
                </label>
            </td>
        </tr>
    </table>
    <div class="border_div">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <!--表头入力-->
                <table cellpadding="0" cellspacing="0" class="inputTable">
                    <tr>
                        <td class="tdTitle_2">
                            订单编号:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtSlipNumber" runat="server" Width="180px" Enabled="false"></asp:TextBox>
                        </td>
                        <td class="tdTitle_2">
                            订单类型:
                        </td>
                        <td class="tdText_2">
                            <select id="selInputType" style="width: 100px;" runat="server">
                                <option value="2">原料</option>
                            </select>
                        </td>
                        <td class="tdTitle_2">
                            备注2:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtAttribute2" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle_2">
                            入库仓库:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtToWarehouseCode" runat="server" Width="80px" OnTextChanged="TO_Warehouse_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="仓库查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('WAREHOUSE','<%=this.txtToWarehouseCode.ClientID%>','<%=this.lblToWarehouseName.ClientID%>');" />
                            <asp:Label ID="lblToWarehouseName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle_2">
                            供应商编号:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtSupplierCode" runat="server" Width="80px" OnTextChanged="Supplier_Change"
                                CssClass="inputText" AutoPostBack="true"></asp:TextBox>
                            <img title="供货商查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('SUPPLIER','<%=this.txtSupplierCode.ClientID%>','<%=this.lblSupplierName.ClientID%>');" />
                            <asp:Label ID="lblSupplierName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle_2">
                            备注3:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtAttribute3" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle_2">
                            入库日期:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtFromDate" runat="server" Width="90px" OnTextChanged="FromDate_Changed"
                                AutoPostBack="true" MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtFromDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                        </td>
                        <td class="tdTitle_2">
                            备注1:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtAttribute1" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <!-- 明细入力-->
                <table class="lineTitleTable">
                    <tr>
                        <td>
                            <img alt="" src="../../Images/title.png" style="vertical-align: middle;" />
                            <b>订单明细</b>
                        </td>
                    </tr>
                </table>
                <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                    <tr>
                        <td class="tdTitle_2">
                            商品编号:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtLineNumber" runat="server" Visible="false"></asp:TextBox>
                            <asp:TextBox ID="txtProductCode" runat="server" Width="160" OnTextChanged="Product_Changed"
                                AutoPostBack="true"></asp:TextBox>
                            <asp:ImageButton ID="btnProduct" runat="server" OnClick="processClick" ImageUrl="../../Images/search.jpg"
                                CssClass="inputImg" />
                        </td>
                        <td class="tdTitle_2">
                            尺码:
                        </td>
                        <td class="tdText_2">
                            <asp:Label ID="lblSize" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="tdTitle_2">
                            备注2:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtLineAttribute2" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle_2">
                            名称:
                        </td>
                        <td class="tdText_2">
                            <asp:Label ID="lblProductName" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="tdTitle_2">
                            单位:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtUnitCode" runat="server" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblUnit" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="tdTitle_2">
                            备注3:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtLineAttribute3" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle_2">
                            款式:
                        </td>
                        <td class="tdText_2">
                            <asp:Label ID="lblStyle" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="tdTitle_2">
                            数量:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtQuantity" runat="server" Width="180px" MaxLength="8"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle_2">
                            颜色:
                        </td>
                        <td class="tdText_2">
                            <asp:Label ID="lblColor" runat="server" Text=""></asp:Label>&nbsp;
                        </td>
                        <td class="tdTitle_2">
                            备注1:
                        </td>
                        <td class="tdText_2">
                            <asp:TextBox ID="txtLineAttribute1" runat="server" Width="180px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <table class=" searchTable" cellpadding="0px;" cellspacing="0px;">
                    <tr>
                        <td style="width: 825px; text-align: left">
                            <asp:Label ID="lblMessage" runat="server" Text="" ForeColor="Red"></asp:Label> &nbsp;
                        </td>
                        <td style="text-align: right;">
                            <asp:LinkButton ID="btnAdd" runat="server" Text="添加" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnClear" runat="server" Text="清空" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <table class="gridHaderTable" cellpadding="0" cellspacing="0">
                    <tr class="GridViewHeader">
                        <td style="width: 68px; border-right: 1px solid green; text-align: center;">
                            &nbsp;明细NO.
                        </td>
                        <td style="width: 100px; border-right: 1px solid green; text-align: center;">
                            &nbsp;商品编号
                        </td>
                        <td style="width: 200px; border-right: 1px solid green; text-align: center;">
                            &nbsp;商品名称
                        </td>
                        <td style="width: 100px; border-right: 1px solid green; text-align: center;">
                            &nbsp;款式
                        </td>
                        <td style="width: 100px; border-right: 1px solid green; text-align: center;">
                            &nbsp;颜色
                        </td>
                        <td style="width: 100px; border-right: 1px solid green; text-align: center;">
                            &nbsp;尺码
                        </td>
                        <td style="width: 100px; border-right: 1px solid green; text-align: center;">
                            &nbsp;单位
                        </td>
                        <td style="width: 100px; border-right: 1px solid green; text-align: right">
                            数量&nbsp;
                        </td>
                        <td style="width: 100px; text-align: center">
                            &nbsp;操作
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                    Width="996px" Height="256px">
                    <asp:GridView ID="gridView" runat="server" CellPadding="0" CellSpacing="0" DataKeyNames="LINE_NUMBER"
                        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" RowStyle-HorizontalAlign="Center"
                        CssClass="ScrollGridView" AlternatingRowStyle-BackColor="#F0F1F2" ShowHeader="false">
                        <Columns>
                            <asp:BoundField ItemStyle-Width="68px" DataField="LINE_NUMBER" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="PRODUCT_CODE" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="200px" DataField="PRODUCT_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="STYLE_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="COLOR_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="SIZE_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="UNIT_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="QUANTITY" ReadOnly="True" ItemStyle-HorizontalAlign="Right" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnModify" runat="server" CausesValidation="false" Text="修正"
                                        OnClick="processClick" CommandArgument='<%# Eval("LINE_NUMBER") %>' CssClass="GridViewLinkButton" />
                                    <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="false" Text="删除"
                                        OnClick="processClick" CommandArgument='<%# Eval("LINE_NUMBER") %>' CssClass="GridViewLinkButton" />
                                </ItemTemplate>
                                <ControlStyle Height="15px" Width="35px"></ControlStyle>
                                <ItemStyle Width="90px"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                        <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                    </asp:GridView>
                </asp:Panel>
                <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" runat="server" Text="取消" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnAdd" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnClear" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
