/**
*
*
*
*
*
*
*/
window.returnValue=false;
var clickRowBackgroundColor = '#A2DDC3';
var mouseOverBackgroundColor = '#C5F3D1';

//不响应服务器端调用
function processMasterClick(item,inputId,lblId,type)
{   
    var ret = "";
    switch(item.toUpperCase())
    {
        case "WAREHOUSE":
            ret  = openMasterSearch("WAREHOUSE",type);            
            break;           
        case "SUPPLIER":  
            ret  = openMasterSearch("SUPPLIER");                
            break;
        case "DEPARTMENT":  
            ret  = openMasterSearch("DEPARTMENT");                
            break;
        case "PRODUCT":
                ret = winOpen("../../Common/ProductSearch.aspx?",'',360,815);
            break;
        case "PRODUCT_GROUP":  
            ret  = openMasterSearch("PRODUCT_GROUP");                
            break;
        case "STYLE":  
            ret  = openMasterSearch("STYLE");                
            break;
        case "COLOR":  
            ret  = openMasterSearch("COLOR");                
            break;
        case "SIZE":  
            ret  = openMasterSearch("SIZE");                
            break;
        case "UNIT":
            ret=openMasterSearch("UNIT");
            break;
        case "ITEM":
            ret=winOpen("../../Common/ItemSearch.aspx?",'',320,505);
            break;
        case "USER":
            ret=openMasterSearch("USER");
            break;   
    } 
    try{
        if(ret.key == 'OK')
        {
            document.getElementById(inputId).value = ret.code;
            document.getElementById(lblId).innerHTML = ret.name;                           
        }
     }catch(e){ }       
}

//不响应服务器端调用
function processMasterClickTwo(item,inputId,lblId,type)
{   
    var ret = "";
    switch(item.toUpperCase())
    {
        case "WAREHOUSE":
            ret  = openMasterSearchTwo("WAREHOUSE",type);            
            break;           
        case "SUPPLIER":  
            ret  = openMasterSearchTwo("SUPPLIER");                
            break;
        case "DEPARTMENT":  
            ret  = openMasterSearchTwo("DEPARTMENT");                
            break;
        case "PRODUCT":
                ret = winOpen("../Common/ProductSearch.aspx?",'',360,815);
            break;
        case "PRODUCT_GROUP":  
            ret  = openMasterSearchTwo("PRODUCT_GROUP");                
            break;
        case "STYLE":  
            ret  = openMasterSearchTwo("STYLE");                
            break;
        case "COLOR":  
            ret  = openMasterSearchTwo("COLOR");                
            break;
        case "SIZE":  
            ret  = openMasterSearchTwo("SIZE");                
            break;
        case "UNIT":
            ret=openMasterSearchTwo("UNIT");
            break;
        case "ITEM":
            ret=winOpen("../Common/ItemSearch.aspx?",'',320,505);
            break;
        case "USER":
            ret=openMasterSearchTwo("USER");
            break;   
    } 
    try{
        if(ret.key == 'OK')
        {
            document.getElementById(inputId).value = ret.code;
            document.getElementById(lblId).innerHTML = ret.name;                           
        }
     }catch(e){ }       
}

//响应服务器端调用
function processMasterClickByServer(id,inputId,lblId,type)
{   
    var returnFlag = false;
    var ret = "";
    switch(id.toUpperCase())
    {
        case "WAREHOUSE":
            ret  = openMasterSearch("WAREHOUSE",type);            
            break;
        case "PRODUCT_GROUP":
            ret  = openMasterSearch("PRODUCT_GROUP",type);            
            break;
        case "PRODUCT":
            ret = winOpen("../../Common/ProductSearch.aspx?",'',360,815);
            break;       
    } 
    try{
        if(ret.key == 'OK')
        {
            document.getElementById(inputId).value = ret.code;
            document.getElementById(lblId).innerHTML = ret.name;  
            returnFlag = true;                         
        }
     }catch(e){ }
     return returnFlag;      
}

//
function processProductTwoClick(gridViewId)
{
   return ret = winOpen("../../Common/ProductSearchTwo.aspx?",'',360,735);
}



//检索窗口打开
function openMasterSearch(table,type)
{
    return winOpen("../../Common/MasterSearch.aspx?","table="+table+"&type="+type,310,490);
}

//检索窗口打开
function openMasterSearchTwo(table,type)
{
    return winOpen("../Common/MasterSearch.aspx?","table="+table+"&type="+type,310,490);
}

//打开窗口
function winOpen(url,param,height,width)
{
    var t = "callTime="+new Date().getTime() ;
    if(param != "")
    {
        url += param+"&"+t;
    }
    else 
    {
        url +=t;
    }
    return window.showModalDialog(url,window,"dialogWidth:"+width+"px;dialogHeight:"+height+"px; status:0;location:no;");
}

//关闭当前窗口
 function processClose(str)
 { 
     if(str != "" && str != undefined && str != 'undefined')
     {
         if(confirm(str))
         {
            window.opener=null;
            window.close();
            return true;
         } 
         else
         {
            return false;
         } 
     }  
     else
     {
         window.opener=null;
         window.close();
         return true;
     }     
 }
 
 //关闭当前窗口，并刷新父页面
 function processCloseAndRefreshParent()
 {    
     window.returnValue=true;
     window.opener=null;
     window.close();
     return true;
 }

// //ajax
// 
//var req; //定义变量，用来创建xmlhttprequest对象
//function creatReq(url) // 创建xmlhttprequest,ajax开始
//{
//    //var url="PurchaseRequiditionAdd.aspx?ajax=1"; //要请求的服务端地址
//    if(window.XMLHttpRequest) //非IE浏览器及IE7(7.0及以上版本)，用xmlhttprequest对象创建
//    {
//        req=new XMLHttpRequest();
//    }
//    else if(window.ActiveXObject) //IE(6.0及以下版本)浏览器用activexobject对象创建,如果用户浏览器禁用了ActiveX,可能会失败.
//    {
//        req=new ActiveXObject("Microsoft.XMLHttp");
//    }
//    
//    if(req) //成功创建xmlhttprequest
//    {
//        req.open("GET",url,true); //与服务端建立连接(请求方式post或get，地址,true表示异步)
//        req.onreadystatechange = callback; //指定回调函数            
//        req.send(null); //发送请求
//    }
//}

//function callback() //回调函数，对服务端的响应处理，监视response状态
//{
//    if(req.readyState == 4) //请求状态为4表示成功
//    {
//        if(req.status==200) //http状态200表示OK
//        {            
//            try{                
//                ExecSucces(); 
//            }catch(e){}
//        }
//        else //http返回状态失败
//        {
//             try{
//                ExecFailed(); 
//            }catch(e){}
//        }
//    }
//    else //请求状态还没有成功，页面等待
//    {
//        try{
//            RequestFailed(); 
//        }catch(e){}
//    }
//}
// //end ajax
