<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="InventoryModify.aspx.cs" Inherits="SCM.Web.Stock.InventoryModify" Title="编辑" %>

<%@ Register Src="~/PageControl.ascx" TagName="Paging" TagPrefix="PageControl" %>
<%@ Register Src="~/UploadFileControl.ascx" TagName="UploadFile" TagPrefix="UploadFileControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">

    <script language="javascript" type="text/javascript" src="../../Script/ComScript.js"></script>

    <script src="../../Script/jquery.js" type="text/javascript"></script>

    <script type="text/javascript" language="javascript">
     var dataObj="";//用来存储josn数据初始
     var dataNew="";//用来存储josn查询之后的值
     var e=0; //用来判断是查询后的还是未查询的
        $(document).ready(function(){
            $.ajax({
            url: "InventoryModify.aspx?TYPE=1&SN="+document.getElementById("ctl00_bodyPlace_lblSlipNumber").innerHTML,
            type: "post",
            data: null,
            async: true,
            ContentType: "application/json; charset=utf-8",
            success: function (result) {
               if(result != "")
            {
                dataObj=eval("("+result+")");//转换为json对象 
                var dgv = document.getElementById("dataGridView");
                var html="<table id='tab' class='ScrollGridView' style='width:980px; ' cellpadding=\"0px;\" cellspacing=\"0px;\">";
                var i = 1;
                
                $.each(dataObj.root,function(idx,pItem)
                {
                    var color = "#F0F1F2";
                    var color1="";
                    if(i % 2 > 0)
                    {
                        color="White";
                    }
                     if(Math.floor(pItem.REAL_INVENTORY)>Math.floor(pItem.INVENTORY))
                      {
                        color1="blue";
                      }
                      else if(Math.floor(pItem.REAL_INVENTORY)<Math.floor(pItem.INVENTORY))
                      {
                        color1="red";
                      }
                      else
                      {
                         color1="black";
                      }
                    i = i + 1;
                   html= html+ "<tr style=\"background-color:"+color+";height: 25px;\"><td style='width: 180px; text-align:center'>"+pItem.PRODUCT_CODE+
                   "</td><td style='width: 200px; text-align:center'>"+pItem.PRODUCT_NAME+
                   "</td><td style='width: 120px ;text-align:center'>"+pItem.STYLE_NAME+
                   "</td><td style='width: 80px;  text-align:center'>"+pItem.COLOR_NAME+
                   "</td><td style='width: 100px; text-align:center'>"+pItem.SIZE_NAME+
                   "</td><td style='width: 100px; text-align:right'>"+Math.floor(pItem.INVENTORY)+
                   "&nbsp;</td><td style='width: 100px; text-align:right'>"+Math.floor(pItem.REAL_INVENTORY)+
                   "&nbsp;</td><td  style=\"color:"+color1+";text-align:right\">"+Math.floor(pItem.DIFF_QUANTITY)+           
                   "&nbsp;</td></tr>";
                });
                 html= html+"</table>" 
                 dgv.innerHTML=html; 
                 if(document.getElementById("ctl00_bodyPlace_lblStatus").innerHTML!="开始")
                 {
                   $('#btnSave').hide();
                   $('#btnOK').hide();
                   $('#txtProductCode').hide();
                   $('#addProductCode').hide();
                   $('#lblAddProduct').hide();
                 }else{
                  document.getElementById("txtProductCode").focus();
                   }            
            }
            else
                {
                   alert("没有可盘点的商品");
                   window.close();
                   
                }
                         
                },
                failure:function(){  
                    alert("Error!")
                }
                
                 }); 
            })
            
            function Product_change()
            {  
               if(event.keyCode==13){
                   var productcode=$('#txtProductCode').val();
                   $('#txtProductCode').val("");
                   //alert(productcode);
                    var i = 1;
                    var e=0;
                    $.each(dataObj.root,function(idx,pItem)
                    {
                     
                        if(productcode==pItem.PRODUCT_CODE)
                        {
                          e=e+1;
                          if(document.getElementById("addProductCode").checked){
                          
                                pItem.REAL_INVENTORY=Math.floor( pItem.REAL_INVENTORY)-1;
                          
                          }
                          else
                          {
                            pItem.REAL_INVENTORY=Math.floor( pItem.REAL_INVENTORY)+1;
                          }
                          pItem.DIFF_QUANTITY=pItem.REAL_INVENTORY-Math.floor( pItem.INVENTORY);
                          return false;
                          
                        } 
                    });
                    if(e==0)
                    {
                        alert("此商品不在盘点范围内！");
                        $('#txtProductCode').val("");
                       return;
                    }
                    dataObj=dataObj;
                    var tab=document.getElementById("tab");
                    for(var i=0;i<tab.rows.length;i++)
                    {
                      if(tab.rows[i].cells[0].innerHTML==productcode)
                      {
                       if(document.getElementById("addProductCode").checked){
                       
                        tab.rows[i].cells[6].innerHTML=parseInt( tab.rows[i].cells[6].innerHTML)-1+"&nbsp;";
                       
                        }
                        else
                        {
                          tab.rows[i].cells[6].innerHTML=parseInt( tab.rows[i].cells[6].innerHTML)+1+"&nbsp;";
                        }
                          tab.rows[i].cells[7].innerHTML=parseInt( tab.rows[i].cells[6].innerHTML)-parseInt(tab.rows[i].cells[5].innerHTML)+"&nbsp;";
                          if(parseInt( tab.rows[i].cells[6].innerHTML)>parseInt( tab.rows[i].cells[5].innerHTML))
                          {
                             tab.rows[i].cells[7].style.color="blue";
                          }
                          else if(parseInt( tab.rows[i].cells[6].innerHTML)<parseInt( tab.rows[i].cells[5].innerHTML))
                          {
                            tab.rows[i].cells[7].style.color="red";
                          }
                          else
                          {
                              tab.rows[i].cells[7].style.color="black";
                          }
                          
                        return;
                      }
                    }
                }
            }
            
          function btnSearch_click()
          {  
                var dgv = document.getElementById("dataGridView");
                var html="<table id='tab' class='ScrollGridView' style='width:980px; ' cellpadding=\"0px;\" cellspacing=\"0px;\">";
                var i = 1;
               $.each(dataObj.root,function(idx,pItem)
                {
                
                    if( $('input[name=city]').get(1).checked == true)
                    {
                     if(Math.floor( pItem.INVENTORY)==0)
                     { 
                       return true;
                     }
                    }
                   else if($('input[name=city]').get(2).checked == true)
                    {
                      if(Math.floor( pItem.INVENTORY)!=0)
                     { 
                       return true;
                     }
                    }
                    if($('input[name=b]').get(1).checked == true)
                    {
                       if(Math.floor(pItem.REAL_INVENTORY)!=0)
                       {
                         return true;
                       }
                    }
                    else if($('input[name=b]').get(2).checked == true)
                    {
                       if(Math.floor( pItem.REAL_INVENTORY)!=Math.floor( pItem.INVENTORY))
                       {
                         return true;
                       }
                    }
                   else if($('input[name=b]').get(3).checked == true)
                    {
                       if(Math.floor( pItem.REAL_INVENTORY)==Math.floor( pItem.INVENTORY))
                       {
                         return true;
                       }
                    }
                    var color1="";
                    var color = "#F0F1F2";
                    if(i % 2 > 0)
                    {
                        color="White";
                    }
                    if(Math.floor(pItem.REAL_INVENTORY)>Math.floor(pItem.INVENTORY))
                      {
                        color1="blue";
                      }
                      else if(Math.floor(pItem.REAL_INVENTORY)<Math.floor(pItem.INVENTORY))
                      {
                        color1="red";
                      }
                      else
                      {
                         color1="black";
                      }
                    i = i + 1;
                   html= html+ "<tr style=\"background-color:"+color+";height: 25px;\"><td style='width: 180px; text-align:center'>"+pItem.PRODUCT_CODE+
                   "</td><td style='width: 200px; text-align:center'>"+pItem.PRODUCT_NAME+
                   "</td><td style='width: 120px ;text-align:center'>"+pItem.STYLE_NAME+
                   "</td><td style='width: 80px;  text-align:center'>"+pItem.COLOR_NAME+
                   "</td><td style='width: 100px; text-align:center'>"+pItem.SIZE_NAME+
                   "</td><td style='width: 100px; text-align:right'>"+Math.floor(pItem.INVENTORY)+
                   "&nbsp;</td><td style='width: 100px; text-align:right'>"+Math.floor(pItem.REAL_INVENTORY)+
                   "&nbsp;</td><td  style=\"color:"+color1+";text-align:right\">"+Math.floor(pItem.DIFF_QUANTITY)+           
                   "&nbsp;</td></tr>";

                });
                 html= html+"</table>" 
                 dgv.innerHTML=html;      
          }
            
            function btnSave_click(item)
            {
               var e=3;
               if(item.id=="btnSave")
               {
                 e=2;
               }
              var str="";
              $.each(dataObj.root,function(idx,pItem)
              {
                str=str+pItem.LINE_NUMBER+"-"+Math.floor( pItem.REAL_INVENTORY)+"/";
              });
                $.ajax({
                url: "InventoryModify.aspx?TYPE="+e+"&SN="+document.getElementById("ctl00_bodyPlace_lblSlipNumber").innerHTML+"&ST="+str,
                type: "post",
                data: null,
                async: true,
                ContentType: "application/json; charset=utf-8",
                success: function (result) {
                      alert(result);
                      window.close();
                },
                failure:function(){  
                alert("Error!")
                }

                });  
             
            }
            
    </script>

    <base target="_self" />
    <style type="text/css">
        .import_div
        {
            position: absolute;
            top: 460px;
            left: 591px;
            width: 410px;
            padding: 2px;
            z-index: 10;
        }
        .GridView
        {
            margin-top: 0px;
            margin-left: 3px;
            margin-right: 3px;
            border: solid 1px Green;
        }
        .GridView td, th
        {
            border-collapse: collapse;
            border: solid 1px Green;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <table class="navigation">
        <tr>
            <td>
                盘点&nbsp;>>&nbsp;盘点编辑
            </td>
        </tr>
    </table>
    <div class="border_div">
        <table cellpadding="0px;" cellspacing="0px;" class="inputTable">
            <tr>
                <td class="tdTitle">
                    盘点单号：
                </td>
                <td class="tdText">
                    <asp:Label ID="lblSlipNumber" runat="server" Text="Label"></asp:Label>
                    &nbsp;
                </td>
                <td class="tdTitle">
                    仓库：
                </td>
                <td class="tdText">
                    <asp:Label ID="lblWarehouse" runat="server"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    开始时间：
                </td>
                <td class="tdText">
                    <asp:Label ID="lblStartDate" runat="server" Text="Label"></asp:Label>
                    &nbsp;
                </td>
                <td class="tdTitle">
                    状态：
                </td>
                <td class="tdText">
                    <asp:Label ID="lblStatus" runat="server" Text="Label"></asp:Label>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    种类：
                </td>
                <td class="tdText">
                    <asp:Label ID="lblGroupcode" runat="server" Text="Label"></asp:Label>
                    &nbsp;
                </td>
                <td class="tdTitle">
                    库存：
                </td>
                <td class="tdText">
                    <input id="rdostock1" type="radio" name="city" checked="checked" />全部
                    <input id="rdostock2" type="radio" name="city" />不等于0
                    <input id="rdostock3" type="radio" name="city" />等于0
                </td>
            </tr>
            <tr>
                <td class="tdTitle">
                    商品编号：
                </td>
                <td class="tdText">
                    <%--<asp:TextBox ID="txtProductCode" runat="server" onkeydown="Product_change();"></asp:TextBox>--%>
                    <input id="txtProductCode" type="text" onkeypress="Product_change();" />
                    <input id="addProductCode" type="checkbox" /><label id="lblAddProduct">(选择则减少库存)</label>
                    <input id="txt" type="text" style="display: none;" />
                    &nbsp;
                </td>
                <td class="tdTitle">
                    盘点条件：
                </td>
                <td class="tdText">
                    <input id="Radio1" type="radio" name="b" checked="checked" />全部
                    <input id="Radio2" type="radio" name="b" />未盘点
                    <input id="Radio3" type="radio" name="b" />相符商品
                    <input id="Radio4" type="radio" name="b" />盘存差异
                </td>
            </tr>
            <tr>
                <td class="tdText" colspan="4" style="text-align: right;">
                    <%--    <input id="btnSearch" type="button" value="查询" class="LinkButton3" style="cursor:pointer;"  />--%>
                    <a id="btnSearch" class="LinkButton3" onclick="btnSearch_click();" style="cursor: pointer;">
                        查询</a>
                </td>
            </tr>
        </table>
        <table id="tabList" style="width: 1000px; border: 1px solid green; margin-top: 3px;
            text-align: center; table-layout: fixed; margin-left: 3px; border-bottom: 0px;
            border-left: 0px; border-right: 0px;" cellpadding="0" cellspacing="0">
            <tr class="GridViewHeader">
                <td style="width: 180px; border-right: 1px solid green; border-bottom: 1px solid green;
                    border-left: 1px solid green;">
                    &nbsp;编号
                </td>
                <td style="width: 200px; border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;商品名称
                </td>
                <td style="width: 120px; border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;款式
                </td>
                <td style="width: 80px; border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;颜色
                </td>
                <td style="width: 100px; border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;尺码
                </td>
                <td style="width: 100px; border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;理论在库
                </td>
                <td style="width: 100px; border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;实际在库
                </td>
                <td style="border-right: 1px solid green; border-bottom: 1px solid green;">
                    &nbsp;差异数
                </td>
            </tr>
        </table>
        <div id="dataGridView" style="table-layout: fixed; margin-left: 3px; padding: 0px;
            overflow: auto; border: solid 1px Green; border-top: solid 0px Green; width: 998px;
            height: 362px;">
        </div>
        <table class="searchTable" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: right;">
                    <%--<asp:LinkButton ID="btnSave" runat="server" Text="盘点暂存" CssClass="LinkButton3" Visible="false"
                        Enabled="false"></asp:LinkButton>--%>
                    <a id="btnSave" class="LinkButton3" onclick="btnSave_click(this);" style="cursor: pointer;">
                        盘点暂存</a>
                </td>
                <td style="width: 90px;">
                    <%--     <asp:LinkButton ID="btnOK" runat="server" Text="盘点确认" CssClass="LinkButton3" Visible="false"
                        Enabled="false"></asp:LinkButton>--%>
                    <a id="btnOK" class="LinkButton3" onclick="btnSave_click(this);" style="cursor: pointer;">
                        盘点确认</a>
                </td>
                <td style="width: 90px;">
                    <a href="#" id="btnCancel" class="LinkButton3" onclick="processClose('你确定要退出吗?');">取消</a>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
