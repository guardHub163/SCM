
//页面关闭，SESSION清空
window.onbeforeunload = function(){   
    var n = window.event.screenX - window.screenLeft; 
    var b = n > document.documentElement.scrollWidth-20; 
    if(b && window.event.clientY < 0 || window.event.altKey){   
      creatReq("?_cmd=clear_session&time="+Math.random());
    }   
}

//商品添加
function processProductClick(){ 
    var ret = processProductTwoClick();
    var param = "";
    for(var i = 0;i< ret.length;i++){
        var obj = ret[i];　   
        param += obj.code+","
        param += obj.name+","
        param += obj.quantity+","
        param += obj.unitCode+"|"
    }
    
    if(param != "")    {
        param =encodeURI(encodeURI(param.substring(0,param.length-1)));            
    }
    
    type = "showProduct"; 
    var url="?_cmd=add_row&time="+Math.random()+"&param="+param; 
    creatReq(url);
}    
//商品删除
function processDelete(rowId){
    if(confirm("你确定要删除吗？")){
        creatReq("?_cmd=delete_row&time="+Math.random()+"&row_id="+rowId);
    }
}

//接受服务端返回的数据，对其进行显示
function ExecSucces(){
    if(req.responseText == "SESSION_LOSS"){
        alert("登录超时，请重新登录！");
        processClose();
    }
    else if(req.responseText == "SAVE_OK"){
        alert("保存成功！");
        processCloseAndRefreshParent();
    }
    else if(req.responseText == "SAVE_NG"){
        alert("保存失败！");
    }
    else if(req.responseText == "NO_PRODUCT"){
        alert("请添加商品！");
    }
    else{
        document.getElementById ("ctl00_bodyPlace_Panel1").innerHTML =req.responseText;        
    }
}

//商品数量修改
function processChange(rowId,item){
    creatReq("?_cmd=quantity_change&time="+Math.random()+"&row_id="+rowId+"&qty="+item.value);
}

//保存数据
function processSave(){
    if(document.getElementById("txtArrivalDate").value=="")
    {
        alert("预定到货日期不能为空！");
        return;
    }    
    creatReq("?_cmd=save"+
                "&time="+Math.random()+
                "&whCode="+document.getElementById("ctl00_bodyPlace_txtWarehouseCode").value+
                "&arrivalDate="+document.getElementById("txtArrivalDate").value);
}

function processSearchStock()
{
    return winOpen("../Stock/StockList.aspx?",'',550,1024);
}

//日期CHECK
function processCheckDate(item){
    if(item.value != ""){
        if( !isDate(item.value)){
            alert("请填写正确的预定到货日期");
            item.value = "";
            item.focus();
        }
    }
}

function isDate(str){
    try {
        var str = str.replace("-", "/");
        var SplitValue = str.split("/");
        if(SplitValue.length != 3){
            return false;
        }
        var year = parseInt(SplitValue[0],10);
        var month = parseInt(SplitValue[1],10);
        var day = parseInt(SplitValue[2],10);
        var a = new Date(year,month-1,day);
    }
    catch(e){
        return false;
    }
    return true;
}


