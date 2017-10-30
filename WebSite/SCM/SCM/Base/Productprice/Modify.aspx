<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="Modify.aspx.cs" Inherits="SCM.Web.Productprice.Modify" Title="编辑" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />
    <style type="text/css">
        #txtEndTime
        {
            width: 121px;
        }
        #txtStartTime
        {
            width: 121px;
        }
    </style>
     <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>
     
           <script language="javascript" type="text/javascript" src="../../Script/My97DatePicker/WdatePicker.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigationChild">
        <tr>
            <td>
                基本资料 &nbsp;>>&nbsp;单价&nbsp;>>&nbsp;编辑
            </td>
        </tr>
    </table>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <table cellpadding="0" cellspacing="0" class="inputTable" >
                <tr>
                    <td class="tdTitle_3">
                        编号：
                    </td>
                    <td class="tdText_3">
                        <asp:Label ID="lblId" runat="server" Width="200px"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <td class="tdTitle_3">
                        原价：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtOriPrice" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        价格：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtPrice" runat="server" Width="121px" 
                            ontextchanged="txtPrice_TextChanged" AutoPostBack="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        折扣：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtDiscount" runat="server" Width="120px" Enabled="false"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3" style="width: 100px;">
                        种类:
                    </td>
                    <td class="tdText_3">
                        <asp:DropDownList ID="ddlPrice" Width="121px" runat="server" Enabled="true" Height="20px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        部门：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtDepartment_Code" runat="server" Width="120px" OnTextChanged="Department_change"  CssClass="inputText"
                            AutoPostBack="true"></asp:TextBox>
                        <img title="部门查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('DEPARTMENT','<%=this.txtDepartment_Code.ClientID%>','<%=this.lblWarehouseName.ClientID%>');" />
                        <asp:Label ID="lblWarehouseName" runat="server" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        款式:
                    </td>
                    <td class="tdText_3" style="width: 250px;">
                        <asp:TextBox ID="txtStyleCode" runat="server" Width="120px" OnTextChanged="SysleCode_Chanage"  CssClass="inputText"
                            AutoPostBack="true"></asp:TextBox>
                        <img title="款式查询" alt="" src="../../Images/search.jpg" class="inputImg" onclick="processMasterClick('STYLE','<%=this.txtStyleCode.ClientID%>','<%=this.lblStyleName.ClientID%>');" />
                        <asp:Label ID="lblStyleName" runat="server" Text="" CssClass="label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        起始日期:
                    </td>
                    <td class="tdText_3">
                          <asp:TextBox ID="txtStartTime" runat="server" Width="120" OnTextChanged="StartTime_Changed"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        <img onclick="WdatePicker({el:'<%=this.txtEndTime.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        结束日期:
                    </td>
                     <td class="tdText_3">
                          <asp:TextBox ID="txtEndTime" runat="server" Width="120" OnTextChanged="EndTime_Changed"
                            AutoPostBack="true" MaxLength="10"></asp:TextBox>
                        <img onclick="WdatePicker({el:'<%=this.txtEndTime.ClientID%>'})" src="../../Script/My97DatePicker/skin/datePicker.gif"
                            style="width: 16px; height: 22px; vertical-align: middle" alt="" class="img"></img>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        默认单价 :
                    </td>
                    <td class="tdText_3">
                        <asp:RadioButton ID="rdo1" runat="server" GroupName="rdo" Text="是" AutoPostBack="true"
                            Enabled="true" />
                        <asp:RadioButton ID="rdo2" runat="server" AutoPostBack="true" Enabled="true" GroupName="rdo"
                            Text="否" />
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        备注1：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAttribute1" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        备注2：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAttribute2" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="tdTitle_3">
                        备注3：
                    </td>
                    <td class="tdText_3">
                        <asp:TextBox ID="txtAttribute3" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="operateTable">
                <tr>
                    <td>
                        <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2">
                        </asp:LinkButton>
                        <a href="#" id="btnCancel" class="LinkButton2" onclick="processClose();">取消</a>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
