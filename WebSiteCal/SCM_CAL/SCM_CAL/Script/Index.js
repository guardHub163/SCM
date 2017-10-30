/****
*
*
*
*
*/
var param = "";
var completeStatus=true;
var date=new Date();

function processPageOnload(){
     commonProcessClick("TYPE=ANALYSE_LOAD");
}

function processClick(item){
    var param="";
      var obj=document.getElementById('selDepartment');
      var data=document.getElementById("txtFromDate");
      var todata=document.getElementById("txtToDate");
      if(data.value> todata.value)
      {
        alert("起始时间不能大于结束时间");
        deleteAllInfo();
        return;
      }
      deleteAllInfo();
    switch(item.id){
        case "btnSearch":
            document.getElementById("info").style.display="";
            document.getElementById("_info").style.display="none";
            param  ="TYPE=ANALYSE_SEARCH_ONE"+"&DEPARTMENT_CODE="+obj.value
            commonProcessClick(param);            
            break;
        default:
            break;
    }
}

var currentMenu = "";
function processMenuClick(item)
{
    if(currentMenu != "")
    {
        document.getElementById(currentMenu).style.background="";
    }
    item.style.background="#0066CC";
    currentMenu = item.id;
    
    
    var departmentCode=document.getElementById("selDepartment").value;
    var fromDate=document.getElementById("txtFromDate").value;
    var toDate=document.getElementById("txtToDate").value;
    var employeeQty = document.getElementById("lblEmployeeQuantity").innerText;
    var totalAmount=document.getElementById("lblTotalAmount").innerText;
    param = "&DEPARTMENT_CODE=" + departmentCode +
            "&FROM_DATE=" + fromDate +
            "&TO_DATE=" + toDate +
            "&EMPLOYEE_QUANTITY=" + employeeQty +
            "&TOTOAL_AMOUNT=" + totalAmount ;
            
    var ifr=document.getElementById("iframe");      
    switch (item.id)
    {
        case "menuHome":
            ifr.src="Index.html";
            break;
        case "menuUser":
            ifr.src='SAR/EmployeeCompare.aspx?'+param;
            break;   
        case "menuProduct":
            ifr.src='SAR/ProductCompare.aspx?'+param;
             break;
        case "menuProductGroup":
            ifr.src='SAR/ProductGroupCompare.aspx?'+param;
            break;
        case "menuProductGroupCompares":
            ifr.src='SAR/ProductGroupCompares.aspx?'+param;
            break;
        case "menuSales":
            ifr.src='SAR/PurchaseAndSalesCompare.aspx?'+param;
            break;  
        case "menuParam":
            ifr.src='SAR/Parameter.aspx?'; 
            break;  
        case "menuMain":
            break;                                              
        default:
            break;
    }
    
    if(item.id == "menuMain")
    {
        document.getElementById("info").style.display="";
        document.getElementById("_info").style.display="none";
    }
    else 
    {
        document.getElementById("info").style.display="none";
        document.getElementById("_info").style.display="";
    }
}



function processExec(dataObj)
{
    if(dataObj != "")
    {    
        switch(dataObj.type){
            case "ANALYSE_LOAD" :
                var selDepartment = document.getElementById("selDepartment");
                document.getElementById("txtFromDate").value = dataObj.frmDate;
                document.getElementById("txtToDate").value = dataObj.toDate;;
                 $.each(dataObj.root,function(idx,pItem){
                    if(idx != 0 )
                    {                 
                        selDepartment.add(new Option(pItem.NAME,pItem.CODE));
                    } 
                });              
                break;
             case"ANALYSE_SEARCH_ONE":
                var DepartmentName = document.getElementById("lblDepartmentName");
                var Head = document.getElementById("lblHead");
                var Address = document.getElementById("lblAddress");
                var HeadPhoto = document.getElementById("lblHeadPhoto");
                var Telephoto = document.getElementById("lblTelephoto");
                var Area = document.getElementById("lblArea");
                var Faxs = document.getElementById("lblFaxs");
                var Employee = document.getElementById("lblEmployeeQuantity");
                 $.each(dataObj.root,function(idx,pItem){
                    DepartmentName.innerText=pItem.NAME;    
                    Head.innerText=pItem.HEAD;
                    Address.innerText=pItem.ADDRESS;
                    HeadPhoto.innerText=pItem.HEADTELEPHONE;
                    Telephoto.innerText=pItem.TELEPHONE;
                    Area.innerText=pItem.AREA;
                    Faxs.innerText=pItem.FAX;
                    Employee.innerText=pItem.Employee ;  
                    searchTwo();
                });              
                break;
             case"ANALYSE_SEARCH_TWO":
               var amount=document.getElementById("lblTotalAmount");//营业额
               var standardRate=document.getElementById("lblStandardRate");//达标率
               var discountRate=document.getElementById("lblDiscountRate");//折扣率
               var salesRatio=document.getElementById("lblSalesRatio");//进销比
               var compared=document.getElementById("lblCompared");//同比
               var jointSalesRate=document.getElementById("lblJointSalesRate");//连带销售
               var atv=document.getElementById("lblAtv");//ATV
               var VIP=document.getElementById("lblVIP");//VIP
               var asp=document.getElementById("lblASP");//ASP
               var lossRate=document.getElementById("lblLossRate");//报损率
//               var classificationSalesRatio=document.getElementById("lblClassificationSalesRatio");//分类货品销售比
               var humanEffect=document.getElementById("lblHumanEffect");//人效
               var ping=document.getElementById("lblPing");//坪效
               var lianx=document.getElementById("lbllianx");//连续比
               var miss=document.getElementById("lblMiss");//丢失率
               var storePerformance=document.getElementById("lblStorePerformanceJudge");//门店业绩达成能力评判
               var samePerformance=document.getElementById("lblSamePerformanceJudge");//同期业绩增长率评判
               var indicator=document.getElementById("lblindicatorJudge"); //业绩指标综合判断评判
               var productMix=document.getElementById("ProductMixJudge");//货品组合的合理性评判
               var lblLossRate=document.getElementById("lblLossRateJudge");//质量状况评判
               var storeVIP=document.getElementById("lblStoreVIPJudge");// 门店VIP状况评判
               var discount=document.getElementById("lblDiscountJudge");//折扣率状况判断
               var price=document.getElementById("lblPriceJudge");//定价合理性判断
               var balance=document.getElementById("lblbalanceJudge"); //进销平衡判断
               var pirates=document.getElementById("lblPiratesJudge");//防盗状况判断
               var  fraction=document.getElementById("lblindicator")//隐藏lbl，为了取到业绩指标的评分
                $.each(dataObj.root,function(idx,pItem){
                    amount.innerText=pItem.AMOUNT;    
                    standardRate.innerText=pItem.StandardRate;
                    discountRate.innerText=pItem.DiscountRate;
                    compared.innerText=pItem.Compared;
                    jointSalesRate.innerText=pItem.JointSalesRate;
                    atv.innerText=pItem.ATV;
                    asp.innerText=pItem.ASP;
                    VIP.innerText=pItem.VIP ;
                    salesRatio.innerText=pItem.SalesRatio;  
                  //lossRate.innerText=pItem.ATV;
//                    classificationSalesRatio.innerText=pItem.ClassificationSalesRatio;
                    humanEffect.innerText=pItem.HumanEffect ;  
                    ping.innerText=pItem.Ping ;
                    storePerformance.innerText=pItem.StorePerformance;
                    storeVIP.innerText=pItem.StoreVIP;
                    discount.innerText=pItem.Discount;
                    price.innerText=pItem.PriceText;
                    lianx.innerText=pItem.Lianx ; 
                    productMix.innerText=pItem.ProductMix;
                    samePerformance.innerText=pItem.SamePerformance;
                    indicator.innerText=pItem.Indicator;
                    fraction.innerText=pItem.Fraction;
                    balance.innerText=pItem.SalesRatioinfo;
//                    searchThree();
                   searchFour();
                });   
                break;
//             case"ANALYSE_SEARCH_THREE":
//               
//                 var tab = document.getElementById("StaffTable");
//                 if(tab.rows.length>=0)
//                 {
//                   for(var i=tab.rows.length-1;i>=0;i--)
//                   {
//                   tab.deleteRow(i);
//                   }
//                   
//                 }
//                 var number=document.getElementById("lblAverAgeaAount");
//                 var quantity=document.getElementById("lblAverageQuantity");
//                 $.each(dataObj.root,function(idx,pItem){
//                  number.innerText="平均销售金额："+pItem.AVERAGEAMOUNT+"元";
//                  quantity.innerText="平均销售数量："+pItem.AVERAGEQUANTITY+"件";
//                  addItemRow(tab,pItem);
//                  searchFour();
//                  }); 
//                break;
             case"ANALYSE_SEARCH_FOUR":
                var score=document.getElementById("lblScore");
                var grade=document.getElementById("lblGrade");
                var departmentname=document.getElementById("lblDepartmentName");
                $.each(dataObj.root,function(idx,pItem){
                  score.innerText=departmentname.innerText+ "门店经营状况综合评判分数为"+pItem.SALESRATINONUMBER+"分";
                  grade.innerText=pItem.SALESRATINOINFO;
                   searchSix();
                 });
                break;
//             case"ANALYSE_SEARCH_FIVE":
//                var tab=document.getElementById("ProductTable");
//                 if(tab.rows.length>=0)
//                 {
//                   for(var i=tab.rows.length-1;i>=0;i--)
//                   {
//                   tab.deleteRow(i);
//                   }
//                   
//                 }
//                tab.datasoure="";
//                 $.each(dataObj.root,function(idx,pItem){
//                  addProductRow(tab,pItem);
//                  searchSix();
//                  }); 
//                  break; 
             case"ANALYSE_SEARCH_SIX":
                  var tab=document.getElementById("InfoTable");
                   if(tab.rows.length>=0)
                 {
                   for(var i=tab.rows.length-1;i>=0;i--)
                   {
                   tab.deleteRow(i);
                   }
                   
                 }
                    $.each(dataObj.root,function(idx,pItem){
                        addAllInfoRow(tab,pItem);
                  }); 
                  break;                             
             default:
             break;                
        }//end switch
    }//end if 
}
function searchTwo()
{
    var code=document.getElementById("selDepartment");
    var date=document.getElementById("txtFromDate");
    var todate=document.getElementById("txtToDate");
    var employee=document.getElementById("lblEmployeeQuantity");
    var area=document.getElementById("lblArea");
    param  ="TYPE=ANALYSE_SEARCH_TWO"+
            "&DEPARTMENT_CODE="+code.value+
            "&DATE="+date.value+
            "&TODATE="+todate.value+
            "&EMPLOYEE="+employee.innerText+
            "&AREA="+area.innerText
    commonProcessClick(param);
}

function searchThree()
{
 var code=document.getElementById("selDepartment");
    var date=document.getElementById("txtFromDate");
     var todate=document.getElementById("txtToDate");
    var total=document.getElementById("lblEmployeeQuantity");
    param  ="TYPE=ANALYSE_SEARCH_THREE"+
        "&DEPARTMENT_CODE="+code.value+
        "&DATE="+date.value+
         "&TODATE="+todate.value+
        "&TOTAL="+total.innerText
    commonProcessClick(param);
}

function searchFour()
{
 var standardrate=document.getElementById("lblStandardRate");
 var lianx=document.getElementById("lbllianx");
 var ping=document.getElementById("lblPing");
 var humaneffect=document.getElementById("lblHumanEffect");
 var asp=document.getElementById("lblASP");
 var jointsalesrate=document.getElementById("lblJointSalesRate");
 var vip=document.getElementById("lblVIP");
 var lossrate=document.getElementById("lblLossRate");
 var missing=document.getElementById("lblMiss");
 var salesration=document.getElementById("lblSalesRatio");
 var discountrate=document.getElementById("lblDiscountRate");
 var fraction=document.getElementById("lblindicator");
  var compared=document.getElementById("lblCompared"); 
    param  ="TYPE=ANALYSE_SEARCH_FOUR"+
        "&STANDARDRATE="+standardrate.innerText.replace("%","")+
        "&LIANX="+lianx.innerText.replace("%","")+
        "&PING="+ping.innerText.replace("%","")+
        "&HUMANEFFECT="+humaneffect.innerText.replace("%","")+
        "&ASP="+asp.innerText.replace("%","")+
        "&JOINTSALESRATE="+jointsalesrate.innerText.replace("%","")+
        "&VIP="+vip.innerText.replace("%","")+
        "&LOSSRATE="+lossrate.innerText.replace("%","")+
        "&MISSING="+missing.innerText.replace("%","")+
        "&SALESRATION="+salesration.innerText.replace("%","")+
        "&DISCOUNTRATE="+discountrate.innerText.replace("%","")+
        "&FRACTION="+fraction.innerText+
        "&COMPARED="+compared.innerText.replace("%","")
   commonProcessClick(param);
}

function searchSix()
{
  var standardrate=document.getElementById("lblStandardRate");
 var asp=document.getElementById("lblASP");
 var atv=document.getElementById("lblAtv");
 var jointsalesrate=document.getElementById("lblJointSalesRate");
 var vip=document.getElementById("lblVIP");
 var lossrate=document.getElementById("lblLossRate");
 var missing=document.getElementById("lblMiss");
 var salesration=document.getElementById("lblSalesRatio");
 var discountrate=document.getElementById("lblDiscountRate");
 var fraction=document.getElementById("lblindicator");
  var compared=document.getElementById("lblCompared");
    param  ="TYPE=ANALYSE_SEARCH_SIX"+
        "&STANDARDRATE="+standardrate.innerText.replace("%","")+
        "&ASP="+asp.innerText.replace("%","")+
        "&ATV="+atv.innerText.replace("%","")+
        "&JOINTSALESRATE="+jointsalesrate.innerText.replace("%","")+
        "&VIP="+vip.innerText.replace("%","")+
        "&LOSSRATE="+lossrate.innerText.replace("%","")+
        "&MISSING="+missing.innerText.replace("%","")+
        "&SALESRATION="+salesration.innerText.replace("%","")+
        "&DISCOUNTRATE="+discountrate.innerText.replace("%","")+
        "&FRACTION="+fraction.innerText+
        "&COMPARED="+compared.innerText.replace("%","")
  commonProcessClick(param);
  
}

function searchFive()
{
 var amount=document.getElementById("lblTotalAmount");
 var code=document.getElementById("selDepartment");
 var date=document.getElementById("txtFromDate");
  var todate=document.getElementById("txtToDate");
 param="TYPE=ANALYSE_SEARCH_FIVE"+
        "&DEPARTMENT_CODE="+code.value+
        "&DATE="+date.value+
         "&TODATE="+todate.value+
        "&AMOUNT="+amount.innerText
        commonProcessClick(param);
}

function addItemRow(tab,cItem)
{
    var row = tab.insertRow();
    row.style.heiht="28px";
  
   // row.setAttribute("class", "JsTabRow");
    var cell = row.insertCell();
    cell.className="JsTabRow";
    cell.width="125px";
    cell.innerText =cItem.USERNAME;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText = cItem.AMOUNT;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText=cItem.AMOUNT_SORT;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText=cItem.AMOUNT_COMPARE;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText=cItem.QUANTITY;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText=cItem.QUANTITY_SORT;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText=cItem.QUANTITY_COMPARE;
    cell.className="JsTabRow";
    cell.width="125px";
    cell = row.insertCell();
    cell.innerText=cItem.JOINTSALESRATE;
     cell.className="JsTabRow";
    cell.width="125px";
    
}

function addProductRow(tab,cItem)
{
  var row = tab.insertRow();
    row.style.heiht="28px";
    var cell = row.insertCell();
    cell.width="200px";
    cell.className="JsTabRow";
    cell.innerText =cItem.NUMBER;
    cell.width="200px";
    cell.className="JsTabRow";
    cell = row.insertCell();
    cell.innerText = cItem.NAME;
    cell.width="200px";
    cell.className="JsTabRow";
    cell = row.insertCell();
    cell.innerText=cItem.AMOUNT;
    cell.width="200px";
    cell.className="JsTabRow";
    cell = row.insertCell();
    cell.innerText=cItem.SORT;
    cell.width="200px";
    cell.className="JsTabRow";
    cell = row.insertCell();
    cell.innerText=cItem.QUANTITY;
    cell.width="200px";
    cell.className="JsTabRow";
}

function addAllInfoRow(tab,cItem)
{
    var row = tab.insertRow();
    row.style.heiht="28px";
    var cell=row.insertCell();
    cell.className="JsTabRows";
    cell.innerText=cItem.Info;
}

function deleteAllInfo()
{
    var DepartmentName = document.getElementById("lblDepartmentName");
    var Head = document.getElementById("lblHead");
    var Address = document.getElementById("lblAddress");
    var HeadPhoto = document.getElementById("lblHeadPhoto");
    var Telephoto = document.getElementById("lblTelephoto");
    var Area = document.getElementById("lblArea");
    var Faxs = document.getElementById("lblFaxs");
    var Employee = document.getElementById("lblEmployeeQuantity");
    var amount=document.getElementById("lblTotalAmount");//营业额
    var standardRate=document.getElementById("lblStandardRate");//达标率
    var discountRate=document.getElementById("lblDiscountRate");//折扣率
    var salesRatio=document.getElementById("lblSalesRatio");//进销比
    var compared=document.getElementById("lblCompared");//同比
    var jointSalesRate=document.getElementById("lblJointSalesRate");//连带销售
    var atv=document.getElementById("lblAtv");//ATV
    var VIP=document.getElementById("lblVIP");//VIP
    var asp=document.getElementById("lblASP");//ASP
    var lossRate=document.getElementById("lblLossRate");//报损率
//    var classificationSalesRatio=document.getElementById("lblClassificationSalesRatio");//分类货品销售比
    var humanEffect=document.getElementById("lblHumanEffect");//人效
    var ping=document.getElementById("lblPing");//坪效
    var lianx=document.getElementById("lbllianx");//连续比
    var miss=document.getElementById("lblMiss");//丢失率
    var storePerformance=document.getElementById("lblStorePerformanceJudge");//门店业绩达成能力评判
    var samePerformance=document.getElementById("lblSamePerformanceJudge");//同期业绩增长率评判
    var indicator=document.getElementById("lblindicatorJudge"); //业绩指标综合判断评判
    var productMix=document.getElementById("ProductMixJudge");//货品组合的合理性评判
    var lblLossRate=document.getElementById("lblLossRateJudge");//质量状况评判
    var storeVIP=document.getElementById("lblStoreVIPJudge");// 门店VIP状况评判
    var discount=document.getElementById("lblDiscountJudge");//折扣率状况判断
    var price=document.getElementById("lblPriceJudge");//定价合理性判断
    var balance=document.getElementById("lblbalanceJudge"); //进销平衡判断
    var pirates=document.getElementById("lblPiratesJudge");//防盗状况判断
    var fraction=document.getElementById("lblindicator")//隐藏lbl，为了取到业绩指标的评分
    var score=document.getElementById("lblScore");
    var grade=document.getElementById("lblGrade");
//    var number=document.getElementById("lblAverAgeaAount");
//    var quantity=document.getElementById("lblAverageQuantity");
    var tdSarText
    DepartmentName.innerText="";
    Head.innerText="";
    Address.innerText="";
    HeadPhoto.innerText="";
    Telephoto.innerText="";
    Area.innerText="";
    Faxs.innerText="";
    Employee.innerText="";
    amount.innerText="";
    standardRate.innerText="";
    discountRate.innerText="";
    salesRatio.innerText="";
    compared.innerText="";
    jointSalesRate.innerText="";
    atv.innerText="";
    VIP.innerText="";
    asp.innerText="";
    lossRate.innerText="";
//    classificationSalesRatio.innerText="";
    humanEffect.innerText="";
    ping.innerText="";
    lianx.innerText="";
    miss.innerText="";
    storePerformance.innerText="";
    samePerformance.innerText="";
    indicator.innerText="";
    productMix.innerText="";
    lblLossRate.innerText="";
    storeVIP.innerText="";
    discount.innerText="";
    price.innerText="";
    balance.innerText="";
    pirates.innerText="";
    fraction.innerText="";
    score.innerText="";
    grade.innerText="";
//    number.innerText="";
//    quantity.innerText="";
//    var tab = document.getElementById("StaffTable");
//    if(tab.rows.length>=0)
//    {
//    for(var i=tab.rows.length-1;i>=0;i--)
//    {
//    tab.deleteRow(i);
//    }
//    }
//    var tab1=document.getElementById("ProductTable");
//     if(tab1.rows.length>=0)
//     {
//       for(var e=tab1.rows.length-1;e>=0;e--)
//       {
//       tab1.deleteRow(e);
//       }
//     }
    var tab2=document.getElementById("InfoTable");
       if(tab2.rows.length>=0)
     {
       for(var a=tab2.rows.length-1;a>=0;a--)
       {
       tab2.deleteRow(a);
       }
     }
     
     
}


///AJAX
function commonProcessClick(param)
{
  $(document).ready(function(){
        $.ajax({
            type:"POST",
            url:"Index.aspx?"+param,          
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
