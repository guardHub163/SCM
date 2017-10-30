<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="TransferOutPlanDetail.aspx.cs" Inherits="SCM.Web.TransferOut.TransferOutPlanDetail"
    Title="出库确认" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self">

    <script language="javascript" type="text/javascript" src="../Js/TransferOutPlanDetail.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                出库&nbsp;>>&nbsp;成品出库预定&nbsp;>>&nbsp;出库确认
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    出库仓库:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtWarehouseCode" runat="server" Width="100" CssClass="inputText"
                        Enabled="false"></asp:TextBox>
                    <img title="仓库查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                </td>
                <td class="tdTitle">
                    配送时间:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtDepartureDate" runat="server" Width="150" Enabled="false" CssClass="inputText"></asp:TextBox>
                    <img src="../../Script/My97DatePicker/skin/datePicker.gif" style="width: 16px; height: 22px;
                        vertical-align: middle" alt="" class="img" />
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    门店:
                </td>
                <td class="tdText">
                    <asp:TextBox ID="txtShopCode" runat="server" Width="100" CssClass="inputText" Enabled="false"></asp:TextBox>
                    <img title="门店查询" alt="" src="../../Images/search_disabled.jpg" class="inputImg" />
                    <asp:Label ID="lblShopName" runat="server" CssClass="label"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="gridHaderTable" cellpadding="0" cellspacing="0">
            <tr class="GridViewHeader">
                <td style="width: 150px; border-right: 1px solid green; text-align: center;">
                    商品编码
                </td>
                <td style="width: 200px; border-right: 1px solid green; text-align: center;">
                    商品名称
                </td>
                <td style="width: 150px; border-right: 1px solid green; text-align: center;">
                    款号&nbsp;
                </td>
                <td style="width: 130px; border-right: 1px solid green; text-align: center;">
                    颜色&nbsp;
                </td>
                <td style="width: 100px; border-right: 1px solid green; text-align: center;">
                    尺码&nbsp;
                </td>
                <td style="width: 100px; border-right: 1px solid green; text-align: right">
                    预定数量&nbsp;
                </td>
                <td style="width: 120px; text-align: center;">
                    实际数量&nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                    Width="996px" Height="340px">
                    <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                        DataKeyNames="SLIP_NUMBER" Width="784px" ShowHeader="false" OnRowDataBound="gridView_RowDataBound"
                        AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#F0F1F2">
                        <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                        <Columns>
                            <asp:BoundField ItemStyle-Width="0" DataField="SLIP_NUMBER"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="0" DataField="UNIT_CODE"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150" DataField="PRODUCT_CODE"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="200" DataField="PRODUCT_NAME"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="150" DataField="STYLE_NAME"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="130" DataField="COLOR_NAME"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="100" DataField="SIZE_NAME"></asp:BoundField>
                            <asp:BoundField ItemStyle-Width="100" DataField="QUANTITY" ItemStyle-CssClass="cellRight">
                            </asp:BoundField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Text='<%# String.Format("{0:F0}",Eval("QUANTITY"))%>'
                                        CssClass="gridTextRight" OnTextChanged="Quantity_Change" AutoPostBack="true"></asp:TextBox>
                                </ItemTemplate>
                                <ControlStyle Height="12px" Width="90px"></ControlStyle>
                                <ItemStyle Width="100px" HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <table class="searchTable" cellpadding="0px;" cellspacing="0px;">
            <tr>
                <td>
                    <asp:CheckBox ID="ckd" runat="server" Text="打印出库单" Font-Size="Larger" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:LinkButton ID="btnSave" runat="server" Text="出库" OnClick="processClick" CssClass="LinkButton2">
                    </asp:LinkButton>
                    <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose('你确定要退出出库确认吗?');">
                        取消</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
