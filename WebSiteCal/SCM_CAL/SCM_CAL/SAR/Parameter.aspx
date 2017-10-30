<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Parameter.aspx.cs" Inherits="SCM.Web.SAR._Parameter" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>参数设定</title>

    <script language="javascript" type="text/javascript" src="../Script/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="../Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="Js/Parameter.js"></script>

    <script language="javascript" type="text/javascript" src="../Script/jquery.js"></script>

    <link href="../Css/SarStyle.css" type="text/css" rel="stylesheet" />
</head>
<body onload="processPageOnload()">
    <form id="form1" runat="server">
    <div style="margin-top: 0px; margin-left: auto; margin-right: auto; border: solid 1px Gray;
        height: 545px;">
        <table>
            <tr>
                <td class="tdSarTiTle1">
                    年业绩指标
                </td>
                <td class="tdSarText1">
                    <input id="txtIndicator" style="width: 150px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    ASP(件单价)
                </td>
                <td class="tdSarText1">
                    <input id="txtAsp" style="width: 150px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblAsp" runat="server" Width="50" Text="(元/件)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    ATV(客单价)
                </td>
                <td class="tdSarText1">
                    <input id="txtAtv" style="width: 150px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblAtv" runat="server" Width="50" Text="(元/单)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    月坪效
                </td>
                <td class="tdSarText1">
                    <input id="txtPing" style="width: 150px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblPing" runat="server" Width="50" Text="(元/坪)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    月人效
                </td>
                <td class="tdSarText1">
                    <input id="txtHumaneffect" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblHumaneffect" runat="server" Width="50" Text="(元/人)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    丢失率
                </td>
                <td class="tdSarText1">
                    <input id="txtMiss1" style="width: 150px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblMiss1" runat="server" Width="50px" Text="(%)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    达标率
                </td>
                <td class="tdSarText1">
                    <input id="txtPerformance" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblPerformance" runat="server" Width="50px" Text="(%)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    VIP占比
                </td>
                <td class="tdSarText1">
                    <input id="txtVip1" style="width: 65px; text-align: right height: 25px; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblVip1" runat="server" Width="40px" Text="(%)~"></asp:Label>
                    <input id="txtVip2" style="width: 65px; text-align: right height: 25px; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblVip2" runat="server" Width="30px" Text="(%)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    主商品(配比率)
                </td>
                <td class="tdSarText1">
                    <input id="txtLordProductRatio1" style="width: 65px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblLordProductRatio1" runat="server" Width="40px" Text="(%)~"></asp:Label>
                    <input id="txtLordProductRatio2" style="width: 65px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblLordPriductRatio2" runat="server" Width="30px" Text="(%)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    进销比
                </td>
                <td class="tdSarText1">
                    <input id="txtSalesratio1" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblSalesratio1" runat="server" Width="40px" Text="(%)~"></asp:Label>
                    <input id="txtSalesratio2" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblSalesratio2" runat="server" Width="30px" Text="(%)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    折扣率
                </td>
                <td class="tdSarText1">
                    <input id="txtDiscount1" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblDiscount1" runat="server" Width="40px" Text="(%)~"></asp:Label>
                    <input id="txtDiscount2" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblDiscount2" runat="server" Width="30px" Text="(%)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    报损率
                </td>
                <td class="tdSarText1">
                    <input id="txtLossratel1" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblLossratel1" runat="server" Width="40px" Text="(%)~"></asp:Label>
                    <input id="txtLossratel2" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblLossratel2" runat="server" Width="30px" Text="(%)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    同比
                </td>
                <td class="tdSarText1">
                    <input id="txtCompart1" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblconmpart1" runat="server" Width="40px" Text="(%)~"></asp:Label>
                    <input id="txtCompart2" style="width: 65px; text-align: right; height: 20px; font-size: larger;" />
                    <asp:Label ID="lblconmpart2" runat="server" Width="30px" Text="(%)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    1月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtOneIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="Label1" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    2月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtTwoIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblTwoIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    3月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtThreeIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblThreeIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    4月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtFourIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblFourIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    5月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtFiveIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblFiveIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    6月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtSixIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblSixIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    7月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtSevenIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="Label3" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    8月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtEightIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblEightIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    9月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtNineIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblNineIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    10月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtTenIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblTenIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
                <td class="tdSarTiTle1">
                    11月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtElevenIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblElevenIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="tdSarTiTle1">
                    12月业绩
                </td>
                <td class="tdSarText1">
                    <input id="txtTwelveIndicator" style="width: 150px; text-align: right; height: 20px;
                        font-size: larger;" />
                    <asp:Label ID="lblTwelveIndicator" runat="server" Width="50" Text="(元)"></asp:Label>
                </td>
            </tr>
        </table>
        <table class="searchTable" cellpadding="0" cellspacing="0" style="width: 1000px">
            <tr>
                <td>
                    <a href="#" id="btnSave" class="LinkButton2" onclick="processClick(this)">保存</a>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
