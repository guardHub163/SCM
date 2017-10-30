<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ProductSearchThree.aspx.cs"
    Inherits="SCM.Web.Common.ProductSearchThree" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>商品检索</title>
    <base target="_self" />
    <link href="../Css/CommonStyle.css" type="text/css" rel="stylesheet" />

    <script language="javascript" type="text/javascript" src="../Script/ComScript.js"></script>

</head>
<body onunload="processUnload(this);">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
                <tr>
                    <!--款式 -->
                    <td class="tdTitle_2">
                        <asp:Label ID="Label2" runat="server" Text="款式:"></asp:Label>
                    </td>
                    <td class="tdText_2">
                        <asp:TextBox ID="txtStyleCode" runat="server" Width="80" MaxLength="10" CssClass="inputText"
                            OnTextChanged="StyleCode_Chanage" AutoPostBack="true"></asp:TextBox>
                        <img title="款式查询" alt="" src="../Images/search.jpg" class="inputImg" onclick="processMasterClickTwo('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                        <asp:Label ID="lblStyleName" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                    <!--查询 -->
                    <td colspan="2" style="text-align: right;">
                    </td>
                </tr>
            </table>
            <asp:GridView ID="gridView" runat="server" CellPadding="0" AutoGenerateColumns="False"
                RowStyle-HorizontalAlign="Center" CssClass="GridView" AlternatingRowStyle-BackColor="#F0F1F2">
                <HeaderStyle CssClass="GridViewHeader" />
                <RowStyle HorizontalAlign="Center" CssClass="RowStyle"></RowStyle>
                <Columns>
                    <asp:BoundField DataField="COLOR_CODE" HeaderText=""></asp:BoundField>
                    <asp:BoundField DataField="COLOR_NAME" HeaderText="颜色|尺码"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </ContentTemplate>
        <Triggers>
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
    
    function processKeyPress() 
    { 
        if(!(event.keyCode==46)&&!(event.keyCode==8)&&!(event.keyCode==37)&&!(event.keyCode==39)) 
        if(!((event.keyCode>=48&&event.keyCode<=57)||(event.keyCode>=96&&event.keyCode<=105))) 
        event.returnValue=false; 
    } 
    -->
</script>

