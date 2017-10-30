<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PageControl.ascx.cs" Inherits="SCM.Web.PageControl" %>
<table>
    <tr>
        <td style="width: 50px;">
            <asp:LinkButton ID="btnFirst" runat="server" CausesValidation="False" 
                CssClass="LinkButton" CommandName="First" OnCommand="LinkButton_Click">首页</asp:LinkButton>
        </td>
        <td style="width: 50px;">
            <asp:LinkButton ID="btnPrev" runat="server" CausesValidation="False" 
                CssClass="LinkButton" CommandName="Previous" OnCommand="LinkButton_Click">上一页</asp:LinkButton>
        </td>
        <td style="width: 50px;">
            <asp:LinkButton ID="btnNext" runat="server" CausesValidation="False" 
                CssClass="LinkButton" CommandName="Next" OnCommand="LinkButton_Click">下一页</asp:LinkButton>
        </td>
        <td style="width: 50px;">
            <asp:LinkButton ID="btnLast" runat="server" CausesValidation="False" 
                CssClass="LinkButton" CommandName="Last" OnCommand="LinkButton_Click">尾页</asp:LinkButton>
        </td>
        <td style="width: 50px;">
            第<asp:Label ID="lblCurrentPage" runat="server" Text="0"></asp:Label>页
        </td>
        <td style="width: 50px;">
            共<asp:Label ID="lblTotalPage" runat="server" Text="0"></asp:Label>页
        </td>
        <td style="width: 80px;">
            跳到<asp:TextBox ID="txtCurrentPage" runat="server" Text="" Width="27px"></asp:TextBox>
        </td>
        <td style="width: 50px;">
            <asp:LinkButton ID="btnGoto" runat="server" CausesValidation="False" 
                CssClass="LinkButton" CommandName="Goto" Text="Goto" OnCommand="LinkButton_Click"></asp:LinkButton>
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>