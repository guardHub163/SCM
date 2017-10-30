<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="SCM.Web.SAR._Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>门店医生</title>

    <script language="javascript" type="text/javascript" src="Script/My97DatePicker/WdatePicker.js"></script>

    <script language="javascript" type="text/javascript" src="Script/Index.js"></script>

    <script language="javascript" type="text/javascript" src="Script/ComScript.js"></script>

    <script language="javascript" type="text/javascript" src="Script/jquery.js"></script>

    <link href="Css/SarStyle.css" type="text/css" rel="stylesheet" />
    <style type="text/css">
        .title
        {
            border-bottom: solid 1px Gray;
            width: 100px;
            height: 25px;
        }
        .text
        {
            border-bottom: solid 1px Gray;
        }
        .twoTitle
        {
            border-bottom: solid 1px Gray;
        }
        .twoText
        {
            border-bottom: solid 1px Gray;
        }
    </style>
</head>
<body style="margin-top: 0px;" onload="processPageOnload()">
    <table style="margin-top: 0px; margin-left: auto; margin-right: auto; width: 1024px;
        table-layout: fixed;" cellpadding="0" cellspacing="0">
        <tr style="height: 130px; background-image: url('Images/sar_index.jpg'); background-repeat: no-repeat;">
            <!--标题图片 -->
            <td>
                &nbsp;
            </td>
        </tr>
        <tr class="MiddleTable">
            <td>
                <!--菜单栏 -->
                <a href="#" id="menuHome" onclick="processMenuClick(this);">主 页</a> <a href="#" id="menuMain"
                    onclick="processMenuClick(this);">门店诊断</a> <a href="#" id="menuUser" onclick="processMenuClick(this);">
                        员工信息对比</a> <a href="#" id="menuProduct" onclick="processMenuClick(this);" style="display: none;">
                            商品销售占比</a> <a href="#" id="menuProductGroup" onclick="processMenuClick(this);">商品分类占比</a>
                <a href="#" id="menuProductGroupCompares" onclick="processMenuClick(this);">商品款式占比</a>
                <a href="#" id="menuSales" onclick="processMenuClick(this);">进销比曲线图</a> <a href="#"
                    id="menuParam" onclick="processMenuClick(this);">参数设定</a>
            </td>
        </tr>
        <tr style="display: none;">
            <td>
                <!--标题 -->
                <table class="Sarnavigation" cellpadding="0" cellspacing="0" style="margin-top: 0px;">
                    <tr>
                        <td style="margin-top: 0px; background-color: #F2F2F2; color: Black; border-top: solid 0px;
                            width: 1024px; height: 35px;">
                            统计分析》》<asp:Label ID="lblT" runat="server" Text="门店检测"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr id="info" style="display: none;">
            <td>
                <!--查询条件 -->
                <table style="width: 100%; height: 50px; font-size: 16px; border: solid 1px Gray;
                    margin-top: 2px;">
                    <tr>
                        <%-- <td style="width: 150px;">
                            <img src="Images/loginM.png" alt=""/>
                        </td>--%>
                        <td style="width: 70px;">
                            门店选择
                        </td>
                        <td style="width: 250px;">
                            <select id="selDepartment" style="width: 200px; height: 25px;">
                            </select>
                        </td>
                        <td style="width: 70px;">
                            分析期间
                        </td>
                        <td>
                            <input id="txtFromDate" style="width: 80px;" />
                            <img onclick="WdatePicker({el:'txtFromDate',dateFmt:'yyyy-MM-dd',alwaysUseStartDate: true})"
                                src="Script/My97DatePicker/skin/datePicker.gif" style="width: 16px; height: 22px;
                                vertical-align: middle" alt="" class="img" />～
                            <input id="txtToDate" style="width: 80px;" />
                            <img onclick="WdatePicker({el:'txtToDate',dateFmt:'yyyy-MM-dd',alwaysUseStartDate: true})"
                                src="Script/My97DatePicker/skin/datePicker.gif" style="width: 16px; height: 22px;
                                vertical-align: middle" alt="" class="img" />
                        </td>
                        <td style="text-align: center;">
                            <a href="#" id="btnSearch" class="LinkButton2" onclick="processClick(this)">诊断</a>
                        </td>
                    </tr>
                </table>
                <!--基本信息和指标栏 -->
                <table style="width: 100%;" cellpadding="0" cellspacing="0">
                    <tr>
                        <td>
                            <table cellpadding="0px;" cellspacing="0px;" style="width: 409px; border: solid 1px Gray;
                                background-color: #f1fafe; margin-left: 0px; margin-top: 2px;">
                                <tr style="background-image: url(Images/line2.png); height: 30px;">
                                    <td style="width: 5px;">
                                    </td>
                                    <td style="width: 100px; vertical-align: bottom; text-align: left;">
                                        <img src="Images/line4.png" alt="" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        店面：
                                    </td>
                                    <td class="text">
                                        <label id="lblDepartmentName">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        负责人：
                                    </td>
                                    <td class="text">
                                        <label id="lblHead">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        地址：
                                    </td>
                                    <td class="text">
                                        <label id="lblAddress">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        负责人电话：
                                    </td>
                                    <td class="text">
                                        <label id="lblHeadPhoto">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        电话：
                                    </td>
                                    <td class="text">
                                        <label id="lblTelephoto">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        面积(㎡)：
                                    </td>
                                    <td class="text">
                                        <label id="lblArea">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        传真：
                                    </td>
                                    <td class="text">
                                        <label id="lblFaxs">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title" style="border-bottom: solid 0px;">
                                        店员人数：
                                    </td>
                                    <td class="text" style="border-bottom: solid 0px;">
                                        <label id="lblEmployeeQuantity">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 20px;">
                        </td>
                        <td>
                            <table style="border: solid 1px Gray; background-color: #f7fdfd; margin-top: 1px;"
                                cellpadding="0px" cellspacing="0px">
                                <tr style="background-image: url(Images/line5.png); height: 30px;">
                                    <td style="width: 5px;">
                                    </td>
                                    <td style="width: 200px; vertical-align: bottom; text-align: left;">
                                        <img src="Images/lineP.png" alt="" />
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td colspan="2">
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 3px;">
                                    </td>
                                    <td class="title" style="width: 120px; height: 29px;">
                                        营业额(元)：
                                    </td>
                                    <td class="text" style="width: 150px;">
                                        <label id="lblTotalAmount">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title" style="width: 120px;">
                                        进销比：
                                    </td>
                                    <td class="text" style="width: 260px; text-align: center;">
                                        <label id="lblSalesRatio">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        达标率：
                                    </td>
                                    <td class="text">
                                        <label id="lblStandardRate">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title">
                                        折扣率：
                                    </td>
                                    <td class="text" style="text-align: center;">
                                        <label id="lblDiscountRate">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        同比：
                                    </td>
                                    <td class="text">
                                        <label id="lblCompared">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title">
                                        连续比：
                                    </td>
                                    <td class="text" style="text-align: center;">
                                        <label id="lbllianx" style="text-align: center;">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        ATV(元/单)：
                                    </td>
                                    <td class="text">
                                        <label id="lblAtv">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title">
                                        ASP(元/件)：
                                    </td>
                                    <td class="text" style="text-align: center;">
                                        <label id="lblASP">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        报损率：
                                    </td>
                                    <td class="text">
                                        <label id="lblLossRate">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title">
                                        丢失率：
                                    </td>
                                    <td class="text" style="text-align: center;">
                                        <label id="lblMiss">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        VIP：
                                    </td>
                                    <td class="text">
                                        <label id="lblVIP">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title">
                                        连带销售率：
                                    </td>
                                    <td class="text" style="text-align: center;">
                                        <label id="lblJointSalesRate">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td class="title">
                                        坪效(元/坪)：
                                    </td>
                                    <td class="text">
                                        <label id="lblPing">
                                            &nbsp;</label>
                                    </td>
                                    <td class="title">
                                        人效(元/人)：
                                    </td>
                                    <td class="text" style="text-align: center;">
                                        <label id="lblHumanEffect">
                                            &nbsp;</label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!--意见诊断栏 -->
                <table cellpadding="0px;" cellspacing="0px;" style="width: 100%; margin-top: 3px;
                    border: solid 1px Gray; background-color: #fffbf7;">
                    <tr style="background-image: url(Images/line.png); height: 30px;">
                        <td style="width: 5px;">
                        </td>
                        <td style="width: 200px; vertical-align: bottom; text-align: left;">
                            <img src="Images/line1.png" alt="" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td>
                        </td>
                        <td class="twoTitle">
                            门店业绩达成能力评判：
                        </td>
                        <td class="twoText">
                            <label id="lblStorePerformanceJudge">
                            </label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            同期业绩增长率评判：
                        </td>
                        <td class="twoText">
                            <label id="lblSamePerformanceJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            业绩指标综合判断评判：
                        </td>
                        <td class="twoText">
                            <label id="lblindicatorJudge">
                                &nbsp;</label>
                            <label id="lblindicator" style="visibility: hidden;">
                            </label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            货品组合的合理性评判：
                        </td>
                        <td class="twoText">
                            <label id="ProductMixJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            定价合理评判：
                        </td>
                        <td class="twoText">
                            <label id="lblPriceJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            门店VIP状况评判：
                        </td>
                        <td class="twoText">
                            <label id="lblStoreVIPJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            质量状况评判：
                        </td>
                        <td class="twoText">
                            <label id="lblLossRateJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            进销平衡判断：
                        </td>
                        <td class="twoText">
                            <label id="lblbalanceJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle">
                            折扣率状况判断：
                        </td>
                        <td class="twoText">
                            <label id="lblDiscountJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td style="width: 3px;">
                        </td>
                        <td class="twoTitle" style="border-bottom: solid 0px;">
                            防盗状况判断：
                        </td>
                        <td class="twoText" style="border-bottom: solid 0px;">
                            <label id="lblPiratesJudge">
                                &nbsp;</label>
                        </td>
                    </tr>
                </table>
                <!--评分栏 -->
                <table cellpadding="0px;" cellspacing="0px;" style="width: 1024px; border: solid 1px Gray;
                    border-bottom: solid 0px; margin-top: 2px; background-color: #f0f8ff;">
                    <tr style="background-image: url(Images/line3.png); height: 30px;">
                        <td style="width: 5px;">
                        </td>
                        <td style="width: 100px; vertical-align: bottom; text-align: left;">
                            <img src="Images/lineZ.png" alt="" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <!--评分栏内容-->
                <table cellpadding="0" cellspacing="0" style="margin-top: 2px; width: 1024px; border: solid 1px Gray;
                    margin-top: 0px; border-top: solid 0px; background-color: #f0f8ff;">
                    <tr style="height: 25px;">
                        <td style="border-bottom: solid 1px Gray;">
                            <label id="lblScore">
                            </label>
                            &nbsp;
                        </td>
                    </tr>
                    <tr style="height: 25px;">
                        <td>
                            <label id="lblGrade">
                            </label>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <!--综合意见栏 -->
                <table class="inputSarTable" style="background-color: #f4fdff; margin-top: 2px; border-bottom: 0px;"
                    cellpadding="0px;" cellspacing="0px;">
                    <tr style="background-image: url(Images/line6.png); height: 30px;">
                        <td style="width: 5px;">
                        </td>
                        <td style="width: 100px; vertical-align: bottom; text-align: left;">
                            <img src="Images/lineH.png" alt="" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
                <!--综合意见栏内容 -->
                <table id="InfoTable" class="inputSarTable " style="height: 50px; padding-bottom: 2px;
                    border-top: 0px; background-color: #f4fdff;" cellpadding="0px;" cellspacing="0px;">
                </table>
            </td>
        </tr>
        <tr id="_info">
            <td>
                <iframe id="iframe" src="Index.html"                     
                    style="border-style: none; border-color: inherit; border-width: 0px; width: 100%; margin-top: 0px;" scrolling="no" 
                onload="this.height=iframe.document.body.scrollHeight" frameborder="0"></iframe>
            </td>
        </tr>
    </table>
    <table style="margin-top: 3px; margin-left: auto; margin-right: auto; width: 1024px;
        border: solid 1px Gray; height: 100px; background-color: #E0E0E0; text-align: center;"
        cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <a href="#" onclick="javascript:window.open('http://www.czzd.info');" style="background-image: url(Images/gw.gif);"
                    class="Guang"></a>
            </td>
            <td>
                <a href="#" onclick="javascript:window.open('http://www.czzd.info');" style="background-image: url(Images/gy.gif);"
                    class="Guang"></a>
            </td>
            <td>
                <a href="#" onclick="javascript:window.open('http://www.czzd.info');" style="background-image: url(Images/mdys.gif);"
                    class="Guang"></a>
            </td>
            <td>
                <a href="#" onclick="javascript:window.open('http://www.czzd.info');" style="background-image: url(Images/pos.gif);"
                    class="Guang"></a>
            </td>
            <td>
                <a href="#" onclick="javascript:window.open('http://www.czzd.info');" style="background-image: url(Images/scm.gif);"
                    class="Guang"></a>
            </td>
        </tr>
    </table>
</body>
</html>
