/****
*
*
*
*
*/
function processPageOnload()
{
  commonProcessClick("TYPE=PARAMETER_LOAD")
}

function processClick(item)
{
    var Indicator = document.getElementById("txtIndicator");
    var Asp=document.getElementById("txtAsp");
    var Atv=document.getElementById("txtAtv");
    var Ping=document.getElementById("txtPing");
    var Humaneffect=document.getElementById("txtHumaneffect");
    var Miss=document.getElementById("txtMiss1");
    var Performance=document.getElementById("txtPerformance");
    var Vip1=document.getElementById("txtVip1");
    var Vip2=document.getElementById("txtVip2");
    var LordProductRatio1=document.getElementById("txtLordProductRatio1");
    var LordProductRatio2=document.getElementById("txtLordProductRatio2");
    var Salesratio1=document.getElementById("txtSalesratio1");
    var Salesratio2=document.getElementById("txtSalesratio2");
    var Discount1=document.getElementById("txtDiscount1");
    var Discount2=document.getElementById("txtDiscount2");
    var Lossratel1=document.getElementById("txtLossratel1");
    var Lossratel2=document.getElementById("txtLossratel2");
    var Compart1=document.getElementById("txtCompart1");
    var Compart2=document.getElementById("txtCompart2");
    var OneIndicator=document.getElementById("txtOneIndicator");
    var TwoIndicator=document.getElementById("txtTwoIndicator");
    var ThreeIndicator=document.getElementById("txtThreeIndicator");
    var FourIndicator=document.getElementById("txtFourIndicator");
    var FiveIndicator=document.getElementById("txtFiveIndicator");
    var SixIndicator=document.getElementById("txtSixIndicator");
    var SevenIndicator=document.getElementById("txtSevenIndicator");
    var EightIndicator=document.getElementById("txtEightIndicator");
    var NineIndicator=document.getElementById("txtNineIndicator");
    var ElevenIndicator=document.getElementById("txtElevenIndicator");
    var TenIndicator=document.getElementById("txtTenIndicator");
    var TwelveIndicator=document.getElementById("txtTwelveIndicator");
    if(Indicator.value=="")
    {
         alert ("业绩指标参数不能为空。");
         return;
    }
    if(Asp.value=="")
    {
         alert("ASP参数不能为空。");
         return;
    }
    if(Atv.value=="")
    {
         alert ("Atv参数不能为空。");
         return;
    }
    if(Ping.value=="")
    {
        alert("坪效参数不能为空。");
        return;
    }
    if(Humaneffect.value=="")
    {
         alert("人效参数不能为空。");
         return;
    }
    if(Miss.value=="")
    {
         alert("丢失率参数不能为空。");
         return;
    }
    if(Performance.value=="")
    {
         alert("达标率参数不能为空。");
         return;
    }
    if(Vip1.value==""||Vip2.value=="")
    {
        alert("VIP参数不能为空。");
        return;
    }
    else if(parseInt(Vip1.value)>parseInt(Vip2.value))
    {
        alert("VIP参数输入有误。")
        return;
    }
    if(LordProductRatio1.value==""||LordProductRatio2.value=="")
    {
        alert("商品配比参数不能为空。");
        return;
    }
    else if(parseInt(LordProductRatio1.value)>parseInt(LordProductRatio2.value))
    {
        alert("商品配比参数输入有误。");
        return;
    }
    if(Salesratio1.value==""||Salesratio2.value=="")
    {
        alert("进销比参数不能为空。");
        return;
    }
    else if(parseInt(Salesratio1.value)>parseInt(Salesratio2.value))
    {
        alert("进销比参数输入有误。");
        return;
    }
    if(Discount1.value==""||Discount2.value=="")
    {
        alert("折扣参数不能为空。");
        return;
    }
    else if(parseInt(Discount1.value)>parseInt(Discount2.value))
    {
        alert("折扣参数输入有误。");
        return;
    }
    if(Lossratel1.value==""||Lossratel2.value=="")
    {
        alert("报损率参数不能为空。");
        return;
    }
    else if(parseInt(Lossratel1.value)>parseInt(Lossratel2.value))
    {
        alert("报损率参数输入有误。");
        return;
    }
    if(Compart1.value==""||Compart2.value=="")
    {
        alert("同比参数不能为空。");
        return;
    }
    else if(parseInt(Compart1.value)>parseInt(Compart2.value))
    {
        alert("同比参数输入有误。");
        return;
    }
     var param="";
     switch(item.id){
        case "btnSave":
             param  ="TYPE=PARAMETER_ONE"+
            "&INDICATOR="+Indicator.value+
            "&ASP="+Asp.value+
            "&ATV="+Atv.value+
            "&PING="+Ping.value+
            "&HUMANEFFECT="+Humaneffect.value+
            "&Miss="+Miss.value+
            "&PERFORMANCE="+Performance.value+
            "&VIP1="+Vip1.value+
            "&VIP2="+Vip2.value+
            "&LORDPRODUCTRATIO1="+LordProductRatio1.value+
            "&LORDPRODUCTRATIO2="+LordProductRatio2.value+
            "&SALESRATIO1="+Salesratio1.value+
            "&SALESRATIO2="+Salesratio2.value+
            "&DISCOUNT1="+Discount1.value+
            "&DISCOUNT2="+Discount2.value+
            "&LOSSRATEL1="+Lossratel1.value+
            "&LOSSRATEL2="+Lossratel2.value+
            "&COMPART1="+Compart1.value+
            "&COMPART2="+Compart2.value+
            "&ONEINDICATOR="+OneIndicator.value+
            "&TWOINDICATOR="+TwoIndicator.value+
            "&THREEINDICATOR="+ThreeIndicator.value+
            "&FOURINDICATOR="+FourIndicator.value+
            "&FIVEINDICATOR="+FiveIndicator.value+
            "&SIXINDICATOR="+SixIndicator.value+
            "&SEVENINDICATOR="+SevenIndicator.value+
            "&EIGHTINDICATOR="+EightIndicator.value+
            "&NINEINDICATOR="+NineIndicator.value+
            "&TENINDICATOR="+TenIndicator.value+
            "&ELEVENINDICATOR="+ElevenIndicator.value+
            "&TWELVEINDICATOR="+TwelveIndicator.value
            commonProcessClick(param);
            break;
        default:
            break;
    }
    
    }

function processExec(dataObj)
{  
if(dataObj != "")
    { 
       switch(dataObj.type){
          case "PARAMETER_LOAD" :
                var Indicator = document.getElementById("txtIndicator");
                var Asp=document.getElementById("txtAsp");
                var Atv=document.getElementById("txtAtv");
                var Ping=document.getElementById("txtPing");
                var Humaneffect=document.getElementById("txtHumaneffect");
                var Miss=document.getElementById("txtMiss1");
                var Performance=document.getElementById("txtPerformance");
                var Vip1=document.getElementById("txtVip1");
                var Vip2=document.getElementById("txtVip2");
                var LordProductRatio1=document.getElementById("txtLordProductRatio1");
                var LordProductRatio2=document.getElementById("txtLordProductRatio2");
                var Salesratio1=document.getElementById("txtSalesratio1");
                var Salesratio2=document.getElementById("txtSalesratio2");
                var Discount1=document.getElementById("txtDiscount1");
                var Discount2=document.getElementById("txtDiscount2");
                var Lossratel1=document.getElementById("txtLossratel1");
                var Lossratel2=document.getElementById("txtLossratel2");
                var Compart1=document.getElementById("txtCompart1");
                var Compart2=document.getElementById("txtCompart2");
                var OneIndicator=document.getElementById("txtOneIndicator");
                var TwoIndicator=document.getElementById("txtTwoIndicator");
                var ThreeIndicator=document.getElementById("txtThreeIndicator");
                var FourIndicator=document.getElementById("txtFourIndicator");
                var FiveIndicator=document.getElementById("txtFiveIndicator");
                var SixIndicator=document.getElementById("txtSixIndicator");
                var SevenIndicator=document.getElementById("txtSevenIndicator");
                var EightIndicator=document.getElementById("txtEightIndicator");
                var NineIndicator=document.getElementById("txtNineIndicator");
                var ElevenIndicator=document.getElementById("txtElevenIndicator");
                var TenIndicator=document.getElementById("txtTenIndicator");
                var TwelveIndicator=document.getElementById("txtTwelveIndicator");
                 $.each(dataObj.root,function(idx,pItem){           
                    Indicator.innerText=pItem.INDICATOR; 
                    Asp.innerText=pItem.ASP;
                    Atv.innerText=pItem.ATV;
                    Ping.innerText=pItem.PING;
                    Humaneffect.innerText=pItem.HUMANEFFECT;
                    Miss.innerText=pItem.MISS;
                    Performance.innerText=pItem.PERFORMANCE
                    Vip1.innerText=pItem.VIP1;
                    Vip2.innerText=pItem.VIP2;
                    LordProductRatio1.innerText=pItem.LORD_PRODUCT_RATIO1;
                    LordProductRatio2.innerText=pItem.LORD_PRODUCT_RATIO2;
                    Salesratio1.innerText=pItem.SALESRATIO1;
                    Salesratio2.innerText=pItem.SALESRATIO2;
                    Discount1.innerText=pItem.DISCOUNT1;
                    Discount2.innerText=pItem.DISCOUNT2;
                    Lossratel1.innerText=pItem.LOSSRATEL1;
                    Lossratel2.innerText=pItem.LOSSRATEL2;
                    Compart1.innerText=pItem.COMPARED1;
                    Compart2.innerText=pItem.COMPARED2;
                    OneIndicator.innerText=pItem.ONEINDICATOR;
                    TwoIndicator.innerText=pItem.TWOINDICATOR;
                    ThreeIndicator.innerText=pItem.THREEINDICATOR;
                    FourIndicator.innerText=pItem.FOURINDICATOR;
                    FiveIndicator.innerText=pItem.FIVEINDICATOR;
                    SixIndicator.innerText=pItem.SIXINDICATOR;
                    SevenIndicator.innerText=pItem.SEVENINDICATOR;
                    EightIndicator.innerText=pItem.EIGHTINDICATOR;
                    NineIndicator.innerText=pItem.NINEINDICATOR;
                    TenIndicator.innerText=pItem.TENINDICATOR;
                    ElevenIndicator.innerText=pItem.ELEVENINDICATOR;
                    TwelveIndicator.innerText=pItem.TWELVEINDICATOR;
                });              
                break;
        case "PARAMETER_ONE":
             $.each(dataObj.root,function(idx,pItem)
             {
                alert(pItem.message);
             });
             break;
                
       }
      }
     }
     
     
     
///AJAX
function commonProcessClick(param)
{
  $(document).ready(function(){
        $.ajax({
            type:"POST",
            url:"Parameter.aspx?"+param,          
            data:null,
            contentType: "application/json; charset=utf-8",
            timeout:10000,      
            success:function(result)
            {
                var dataObj="";
                if(result != "")
                {
                    dataObj=eval("("+result+")");//转换为json对象 
                }
                try
                {        
                    processExec(dataObj);
                }catch(e){}                    
            },
            failure:function(){           
                alert("failure");
            }
        });      
  });
}
     