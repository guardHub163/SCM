<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductSearch.aspx.cs" Inherits="SCM.Web.Common.ProductSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品检索</title>
    <base target="_self" />
    <link href="../Css/CommonStyle.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="Js/ProductSearch.js"></script>

</head>
<body onunload="processUnload(this);">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                <tr>
                    <!--种类-->
                    <td class="tdTitle_2">
                        <asp:Label runat="server" Text="种类:"></asp:Label>
                    </td>
                    <td class="tdText_2">
                        <asp:TextBox ID="txtProductGroupCode" runat="server" Width="80" MaxLength="10" CssClass="inputText"
                            OnTextChanged="ProductGroupCode_Chanage" AutoPostBack="true"></asp:TextBox>
                        <img title="种类查询" alt="" src="../Images/search.jpg" class="inputImg" onclick="processMasterClickTwo('PRODUCT_GROUP','<%=this.txtProductGroupCode.ClientID%>','<%=this.lblProductGroupName.ClientID%>');" />
                        <asp:Label ID="lblProductGroupName" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                    <!--款式 -->
                    <td class="tdTitle_2">
                        <asp:Label runat="server" Text="款式:"></asp:Label>
                    </td>
                    <td class="tdText_2">
                        <asp:TextBox ID="txtStyleCode" runat="server" Width="80" MaxLength="10" CssClass="inputText"
                            OnTextChanged="SysleCode_Chanage" AutoPostBack="true"></asp:TextBox>
                        <img title="款式查询" alt="" src="../Images/search.jpg" class="inputImg" onclick="processMasterClickTwo('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                        <asp:Label ID="lblStyleName" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!--颜色-->
                    <td class="tdTitle_2">
                        <asp:Label runat="server" Text="颜色:"></asp:Label>
                    </td>
                    <td class="tdText_2">
                        <asp:TextBox ID="txtColorCode" runat="server" Width="80" MaxLength="10" CssClass="inputText"
                            OnTextChanged="ClolorCode_Chanage" AutoPostBack="true"></asp:TextBox>
                        <img title="颜色查询" alt="" src="../Images/search.jpg" class="inputImg" onclick="processMasterClickTwo('COLOR','<%=this.txtColorCode.ClientID%>','<%=this.lblColorName.ClientID%>');" />
                        <asp:Label ID="lblColorName" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                    <!--尺码 -->
                    <td class="tdTitle_2">
                        <asp:Label runat="server" Text="尺码:"></asp:Label>
                    </td>
                    <td class="tdText_2">
                        <asp:TextBox ID="txtSizeCode" runat="server" Width="80" MaxLength="10" CssClass="inputText"
                            OnTextChanged="SizeCode_Chanage" AutoPostBack="true"></asp:TextBox>
                        <img title="尺码查询" alt="" src="../Images/search.jpg" class="inputImg" onclick="processMasterClickTwo('SIZE','<%=this.txtSizeCode.ClientID%>','<%=this.lblSizeName.ClientID%>');" />
                        <asp:Label ID="lblSizeName" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <!--名称 -->
                    <td class="tdTitle_2">
                        <asp:Label runat="server" Text="名称:"></asp:Label>
                    </td>
                    <td class="tdText_2">
                        <asp:TextBox ID="txtProductName" runat="server" Width="200" MaxLength="10"></asp:TextBox>
                    </td>
                    <!--查询 -->
                    <td colspan="2" style="text-align: right;">
                        <asp:LinkButton ID="btnSearch" runat="server" Text="查询" OnClick="processClick" CssClass="LinkButton2"
                            OnClientClick="processSearch();"></asp:LinkButton>
                    </td>
                </tr>
            </table>
            <table id="tabList" style="width: 805px; border: 1px solid green; margin-top: 3px;
                table-layout: fixed; margin-left: 3px;" cellpadding="0" cellspacing="0">
                <tr class="GridViewHeader">
                    <td style="width: 150px; border-right: 1px solid green;">
                        &nbsp;编号
                    </td>
                    <td style="width: 300px; border-right: 1px solid green;">
                        &nbsp;名称
                    </td>
                    <td style="width: 100px; border-right: 1px solid green;">
                        &nbsp;款式
                    </td>
                    <td style="width: 100px; border-right: 1px solid green;">
                        &nbsp;颜色
                    </td>
                    <td style="width: 150px;">
                        &nbsp;尺码
                    </td>
                </tr>
            </table>
            <asp:Panel ID="Panel1" runat="server" ScrollBars="Vertical" CssClass="ScrollPanel"
                Width="804px" Height="212px">
                <asp:GridView ID="gridView" runat="server" CellPadding="0" CssClass="ScrollGridView"
                    DataKeyNames="CODE" Width="784px" OnRowDataBound="gridView_RowDataBound" RowStyle-HorizontalAlign="Center"
                    AutoGenerateColumns="False" AlternatingRowStyle-BackColor="#F0F1F2" ShowHeader="false">
                    <RowStyle HorizontalAlign="Left" CssClass="RowStyle"></RowStyle>
                    <Columns>
                        <asp:BoundField ItemStyle-Width="150px" DataField="CODE" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="300px" DataField="NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="STYLE_NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="100px" DataField="COLOR_NAME" ReadOnly="True" />
                        <asp:BoundField ItemStyle-Width="130px" DataField="SIZE_NAME" ReadOnly="True" />
                    </Columns>
                </asp:GridView>
            </asp:Panel>
            <table style="width: 813px;">
                <tr>
                    <td style="text-align: right;">
                        <input type="button" id="btnOK" value="确定" runat="server" onclick="processClick(this,'<%=this.gridView.ClientID %>')"
                            disabled="disabled" class="LinkButton2" style="height: 22px;" />
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
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

