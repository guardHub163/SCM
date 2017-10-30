/**
*
*
*
*
*
*
*/
  
  function CheckDatetime(item)
    {
    var yyyy="";
    var mm="";
    var dd="";
    var strDate = document.getElementById(item.id).value;
    if(strDate !="")
    {
        if
        (strDate.length!=10)
        {
         alert ("日期的长度有误！");
         item.value="";
         return false ;
        }
        yyyy =strDate.substring(0,4);
        mm=strDate.substring(5,7)
        dd=strDate.substring(8,10)
	    if (isNaN(yyyy)||isNaN(mm)||isNaN(dd)){
	    alert ("日期格式错误！");
	        item.value="";
		    return false
	    }
	    var y=parseInt(yyyy, 10)
	    var m=parseInt(mm, 10)
	    var d=parseInt(dd, 10)
	    if (y<1900 || y>2100){
	    alert ("日期的范围有误！");
	        item.value="";
		    return false
	    }
	    var vDate=new Date(y,m-1,d)
	    if(vDate.getYear()<1000){	
		    if((vDate.getYear()+1900)!=y){
		    alert ("日期格式错误！");
		         item.value="";
			    return false
		    }
	    }		
	    else{
		    if(vDate.getYear()!=y){
		    alert ("日期格式错误！");
		        item.value="";
			    return false	
		    }
	    }
	    if(vDate.getMonth()!=m-1){
	    alert ("日期格式错误！");
	        item.value="";
		    return false
	    }
	    if(vDate.getDate()!=d){
	    alert ("日期格式错误！");
	        item.value="";
		    return false
	    }
	    return true
  }
}



function CheckDatetime1(item)
{
    var yyyy="";
    var mm="";
    var dd="";
    var strDate = document.getElementById(item.id).value;
    if(strDate !="")
    {
        if
        (strDate.length!=7)
        {
         alert ("日期的长度有误！");
         item.value="";
         return false ;
        }
        yyyy =strDate.substring(0,4);
        mm=strDate.substring(5,7)
        if (isNaN(yyyy)||isNaN(mm)||isNaN(dd)){
            alert ("日期格式错误！");
            item.value="";
	        return false;
        }
        var y=parseInt(yyyy, 10)
        var m=parseInt(mm, 10)
        if (y<1900 || y>2100){
        alert ("日期的范围有误！");
            item.value="";
	        return false;
        }
        var vDate=new Date(y,m-1)
        if(vDate.getYear()<1000){	
	        if((vDate.getYear()+1900)!=y){
	        alert ("日期格式错误！");
	             item.value="";
		        return false;
	        }
        }		
        else{
	        if(vDate.getYear()!=y){
	        alert ("日期格式错误！");
	            item.value="";
		        return false;
	        }
        }
        if(vDate.getMonth()!=m-1){
        alert ("日期格式错误！");
            item.value="";
	        return false;
        }
        return true;
    }
}
