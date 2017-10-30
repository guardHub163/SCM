<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="RequiditionAdd.aspx.cs" Inherits="SCM.Web.Purchase.RequiditionAdd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                <label id="lblTitle" runat="server">
                </label>
            </td>
        </tr>
    </table>
    <div class="border_div">
        <!--申请信息-->
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:TextBox ID="txtSlipNumber" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                <table cellpadding="0" cellspacing="0" class="inputTable">
                    <tr>
                        <td class="tdTitle" colspan="4" style="text-align: center;">
                            申请信息
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            申请日期:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtDepartualDate" runat="server" Width="80" CssClass="inputText"
                                Enabled="false"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtDepartualDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                class="img" alt="" />
                        </td>
                        <td class="tdTitle">
                            申请门店:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtToWarehouseCode" runat="server" Width="80" CssClass="inputText"
                                Enabled="false"></asp:TextBox>
                            <img alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                            <asp:Label ID="lblToWarehouseName" runat="server" CssClass="label"></asp:Label> &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            申请到货日期:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtArrivalDate" runat="server" Width="80" CssClass="inputText" 
                                MaxLength="10"></asp:TextBox>
                            <img onclick="WdatePicker({el:'<%=this.txtArrivalDate.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                                class="img" alt="" />
                        </td>
                        <td class="tdTitle">
                            申请人:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtUserId" runat="server" Width="80" CssClass="inputText" Enabled="false"></asp:TextBox>
                            <img alt="" src="../../Images/search_disabled.jpg" class=" inputImg" />
                            <asp:Label ID="lblUserName" runat="server" CssClass="label"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            商品种类:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80" CssClass="inputText"
                                OnTextChanged="Product_Group_Changed" AutoPostBack="true"></asp:TextBox>
                            <asp:ImageButton ID="btnProductGroup" ImageUrl="../../Images/search.jpg" CssClass="inputImg"
                                OnClick="processClick" runat="server" BorderWidth="0px" />
                            <asp:Label ID="lblProductGroupName" runat="server" CssClass="label"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            补货期间:
                        </td>
                        <td class="tdText">
                            <asp:DropDownList ID="ddlPeriod" runat="server" Width="110px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <!--参考信息-->
                <table cellpadding="0" cellspacing="0" class="inputTable">
                    <tr>
                        <td class="tdTitle" colspan="4" style="text-align: center;">
                            参考信息
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            补货仓库:
                        </td>
                        <td class="tdText">
                            <asp:TextBox ID="txtFromWarehouseCode" runat="server" Enabled="false" Visible="false"></asp:TextBox>
                            <asp:Label ID="lblFromWarehouse" runat="server"></asp:Label>
                        </td>
                        <td class="tdTitle">
                            商品种类库存:
                        </td>
                        <td class="tdText">
                            <asp:Label ID="lblGroupStock" runat="server"></asp:Label> &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            区域销量占比:
                        </td>
                        <td class="tdText">
                            <asp:Label ID="lblAreaPercentage" runat="server"></asp:Label> &nbsp;
                        </td>
                        <td class="tdTitle">
                            区域补货上限:
                        </td>
                        <td class="tdText">
                            <asp:Label ID="lblAreaMaxQuantity" runat="server"></asp:Label> &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="tdTitle">
                            门店销量占比:
                        </td>
                        <td class="tdText">
                            <asp:Label ID="lblShopPercentage" runat="server"></asp:Label> &nbsp;
                        </td>
                        <td class="tdTitle">
                            门店补货上限:
                        </td>
                        <td class="tdText">
                            <asp:Label ID="lblShopMaxQuantity" runat="server"></asp:Label> &nbsp;
                        </td>
                    </tr>
                </table>
                <table class="searchTable" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSearch" runat="server" Text="商品载入" OnClick="processClick"
                                CssClass="LinkButton3">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnClear" runat="server" Text="清空商品" OnClick="processClick" CssClass="LinkButton3">
                            </asp:LinkButton>
                        </td>
                    </tr>
                </table>
                <!--商品-->                
                <table class="gridHaderTable" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="10" style="text-align: center; height: 25px; background-color: #DAE6F4;border-bottom: 1px solid green;">
                         申请商品信息
                        </td>
                    </tr>
                    <tr class="ScrollGridViewHeader" style="text-align: center; font-weight: bold;">
                        <td style="width: 150px; border-right: 1px solid green;">
                            编号
                        </td>
                        <td style="width: 150px; border-right: 1px solid green;">
                            款号
                        </td>
                        <td style="width: 100px; border-right: 1px solid green;">
                            颜色
                        </td>
                        <td style="width: 100px; border-right: 1px solid green;">
                            尺码
                        </td>
                        <td style="width: 90px; border-right: 1px solid green;">
                            仓库库存
                        </td>
                        <td style="width: 90px; border-right: 1px solid green;">
                            门店库存
                        </td>
                        <td style="width: 100px; border-right: 1px solid green;">
                            到货前销售
                        </td>
                        <td style="width: 100px; border-right: 1px solid green;">
                            申请数量
                        </td>
                        <td style="width: 92px;">
                            箱号
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                    Width="996px" Height="256px">
                    <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                        OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#F0F1F2"
                        ShowHeader="false">
                        <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                        <Columns>
                            <asp:BoundField ItemStyle-Width="150px" DataField="PRODUCT_CODE" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="150px" DataField="STYLE_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="COLOR_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="100px" DataField="SIZE_NAME" ReadOnly="True" />
                            <asp:BoundField ItemStyle-Width="90px" DataField="WAREHOUSE_STOCK" ReadOnly="True"
                                DataFormatString="{0:F0}" HtmlEncode="false" />
                            <asp:BoundField ItemStyle-Width="90px" DataField="SHOP_STOCK" ReadOnly="True" DataFormatString="{0:F0}"
                                HtmlEncode="false" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBeforeSalesQuantity" runat="server" Text='<%# String.Format("{0:F0}",Eval("BEFORE_SALES_QUANTITY"))%>'
                                        CssClass="gridTextRight" OnTextChanged="Before_Sales_Quantity_Changed" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Height="12px" Width="90px"></ControlStyle>
                                <ItemStyle Width="100px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtRequistionQuantity" runat="server" Text='<%# String.Format("{0:F0}",Eval("REQUISTION_QUANTITY"))%>'
                                        CssClass="gridTextRight" OnTextChanged="Requisition_Quantity_Changed" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Height="12px" Width="90px"></ControlStyle>
                                <ItemStyle Width="100px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtBoxNumber" runat="server" Text='<%# Eval("BOX_NUMBER")%>' CssClass="gridTextRight"
                                        OnTextChanged="Box_Number_Changed" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Height="12px" Width="64px"></ControlStyle>
                                <ItemStyle Width="74px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
                            <asp:BoundField ItemStyle-Width="0px" DataField="UNIT_CODE" ReadOnly="True" ItemStyle-BorderWidth="0px" />
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <!--操作 -->
                <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnMonitor" runat="server" Text="监测" OnClick="processClick" CssClass="LinkButton2">
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2">
                            </asp:LinkButton>
                            <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose('你确定要取消吗?');">取消</a>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
