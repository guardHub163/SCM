<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MasterSearch.aspx.cs" Inherits="SCM.Web.Common.MasterSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>检索</title>
    <base target="_self">
    <link href="../Css/CommonStyle.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="Js/MasterSearch.js"></script>
    
</head>
<body onunload="processUnload(this);">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:TextBox ID="txtTableName" runat="server" Visible="false"></asp:TextBox>
    <asp:TextBox ID="txtWarehouseType" runat="server" Visible="false"></asp:TextBox>
    <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
        <tr>
            <td class="tdTitle_2">
                <asp:Label ID="Label1" runat="server" Text="名称:"></asp:Label>
            </td>
            <td class="tdText_2">
                <asp:TextBox ID="txtName" runat="server" Width="190"></asp:TextBox>
            </td>
        </tr>
    </table>
    <div style="position: fixed; top: 7px; left: 370px;">
        <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2" OnClientClick="processSearch();"></asp:LinkButton>
    </div>
    <table id="tabList" style="width: 474px; border: 1px solid green; margin-top: 3px;
        margin-left: 3px;" cellpadding="0" cellspacing="0">
        <tr class="GridViewHeader">
            <td style="width: 100px; border-right: 1px solid green;">
                &nbsp;编号
            </td>
            <td style="width: 370px;">
                &nbsp;名称
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                Width="472px" Height="212px">
                <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                    DataKeyNames="CODE" Width="453px" OnRowDataBound="gridView_RowDataBound" RowStyle-HorizontalAlign="Center"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#F0F1F2" ShowHeader="false">
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="100px" DataField="CODE" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="352px" DataField="NAME" ReadOnly="True" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <table style="width: 480px;">
                <tr>
                    <td style="text-align: right;">
                      <input type="button" id="btnOK" value="确定" runat="server" onclick="processClick(this,'<%=this.gridView.ClientID %>')"
                            disabled="disabled" class="LinkButton2" style="height:22px;" />                        
                       <a href="#" id="btnCancel" class="LinkButton2" onclick="processClick(this);">取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
    </form>
</body>
</html>

<script language="javascript" type="text/javascript">   
<!-- 
     Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
    function EndRequestHandler(sender, args){
        if (args.get_error() != undefined) 
        { 
            alert(args.get_error().message.substring(52));
        }
        args.set_errorHandled(true);
    }
    -->
</script>

