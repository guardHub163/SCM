<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="UserType.aspx.cs" Inherits="SCM.Web.User.UserType" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <style type="text/css">
        .ListFatherPower
        {
            width: 150px;
            height: 490px;
            border: solid 1px green;
            background-color: #FDFFFF;
            font-size: 15px;
        }
        .FinelyType
        {
            width: 338px;
            height: 490px;
            border: solid 1px green;
            background-color: #FBFFFD;
            font-size: 15px;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                用户权限
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    用户类型:
                </td>
                <td class="tdText">
                    <asp:DropDownList ID="selUserType" runat="server" Width="200" OnSelectedIndexChanged="selUserType_SelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <table style="margin-left: 3px; margin-top: 3px; width: 500px; height: 500px; border: solid 1px green;">
                    <tr>
                        <td>
                            <asp:ListBox ID="ListFatherPower" runat="server" OnSelectedIndexChanged="ListFatherPower_SelectedIndexChanged"
                                AutoPostBack="true" CssClass="ListFatherPower"></asp:ListBox>
                        </td>
                        <td>
                            <asp:CheckBoxList ID="FinelyType" runat="server" OnSelectedIndexChanged="FinelyType_SelectedIndexChanged"
                                AutoPostBack="true" CssClass="FinelyType">
                            </asp:CheckBoxList>
                        </td>
                    </tr>
                </table>
                <table style="margin-top: 3px; margin-left: 3px; text-align: right; width: 500px;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" Text="保存" OnClick="processClick" CssClass="LinkButton2"></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="selUserType" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
