<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ItemSearch.aspx.cs" Inherits="SCM.Web.Common.ItemSearch" Title="原料查询" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />

    <script language="javascript" type="text/javascript" src="Js/MasterSearch.js"></script>

    <script language="javascript" type="text/javascript">
    window.onunload=processUnload;
    </script>

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="inputTable" cellpadding="0px;" cellspacing="0px;" >
        <tr>
            <!--名称 -->
            <td class="tdTitle_2">
                <asp:Label ID="Label5" runat="server" Text="名称:"></asp:Label>
            </td>
            <td class="tdText_2">
                <asp:TextBox ID="txtItemName" runat="server" Width="190" MaxLength="20"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="position: fixed; top: 7px; left: 445px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
    </div>
    <table id="tabList" style="width: 492px; border: 1px solid green; margin-top: 3px;
        table-layout: fixed; margin-left: 3px;" cellpadding="0" cellspacing="0">
        <tr class="GridViewHeader">
            <td style="width: 100px; border-right: 1px solid green;">
                &nbsp;编号
            </td>
            <td style="width: 190px; border-right: 1px solid green;">
                &nbsp;名称
            </td>
            <td style="width: 200px;">
                &nbsp;规格
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                Width="492px" Height="212px">
                <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                    DataKeyNames="CODE" Width="473px" OnRowDataBound="gridView_RowDataBound" AutoGenerateColumns="False"
                    AlternatingRowStyle-BackColor="#F0F1F2" ShowHeader="false">
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="100px" DataField="CODE" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="190px" DataField="NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="180px" DataField="SPEC" ReadOnly="True" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <table style="width: 500px;">
                <tr>
                    <td style="text-align: right;">
                        <input type="button" id="btnOK" value="确定" onclick="processClick(this,'<%=this.gridView.ClientID %>')"
                            disabled="disabled" class="LinkButton2" style="height: 22px;" />
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClick(this);">取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
