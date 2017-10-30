
var curretnMenu = 0;
function init()
{

    try{
        var c_ul = document.getElementById("parentNav");
        for(var i = 0;i<parentArr.length;i++)
        {
            var arr = parentArr[i];
            c_li = document.createElement("li");
            c_li.innerHTML="<a href=\"#\" id=\"a_"+i+"\" onMouseOver=\"processMouseOver(this,"+i+")\">"+arr.cDesc+"</a>";            
            c_ul.appendChild(c_li);           
        }
    }catch(e){}
}

function processMouseOver(obj,i)
{
    try{
        document.getElementById("a_"+curretnMenu).style.background= "";//"background-image: url(Images/menu_a_2.png)";
    }catch(e){}
    curretnMenu = i;
    document.getElementById("a_"+i).style.background= "background-image: url(Images/menu_a_1.png)";
    try{
        var c_ul = document.getElementById("childNav");
        c_ul.style.visibility = "visible";
        var left = findPosX(obj);
        if(left+300>document.body.scrollWidth)
        {
            left = left-220;
        }
        c_ul.style.left = left+"px";
       
        for(var k = c_ul.childNodes.length-1;k>=0;k--)
        {
            c_ul.removeChild(c_ul.childNodes[k]);
        }
        for(var j = 0;j<parentArr[i].length;j++)
        {
            var menu = parentArr[i][j];
            c_li = document.createElement("li");
            c_li.innerHTML = "<a href=\"#\" onclick=\"processClick("+i+","+j+")\">"+menu.pDesc+"</a>"  
            c_ul.appendChild(c_li);
            if(j!=0 && (j+1)%3==0)
            {
             c_ul.appendChild(document.createElement("br"));
            }
        }
    }catch(e){}
}

function findPosX(obj) {
    var curleft = 0;
    if (obj.offsetParent) { //返回父类元素，大多说offsetParent返回body
        while (obj.offsetParent) {//遍历所有父类元素
            curleft += obj.offsetLeft;//当前元素的左边距
            obj = obj.offsetParent;        
        }
    } else if (obj.x) curleft += obj.x;
    return curleft;
}

function findPosY(obj) {
    var curtop = 0;
    if (obj.offsetParent) {
        while (obj.offsetParent) {
            curtop += obj.offsetTop;
            obj = obj.offsetParent;
        }
    } else if (obj.y) curtop += obj.y;
    return curtop;
}


function processMouseOut(obj,theEvent)
{
    var browser=navigator.userAgent;   //取得浏览器属性
    if (browser.indexOf("MSIE")>0)//如果是IE
    { 
        if (obj.contains(event.toElement) )
        {
            return; // 如果是子元素则结束函数
        }
        try
        {
            if (event.toElement.id == "childNav" || event.toElement.id =="parentNav")
            {
                return; // 如果是子元素则结束函数
            }
        }catch(e){}
    }
    else //如果是Firefox
    { 
        if (obj.contains(theEvent.relatedTarget)) return; // 如果是子元素则结束函数
    }　　    
    document.getElementById("childNav").style.visibility="hidden";
    
    try{
        document.getElementById("a_"+curretnMenu).style.background=  "";//"background-image: url(Images/menu_a_2.png)";
    }catch(e){}
}

function processClick(i,j)
{
    document.getElementById("childNav").style.visibility="hidden";
    var mainFrame =  parent.document.getElementById("ifr");
    try{
        var url = parentArr[i][j].pUrl;
        if(url != ""){
            mainFrame.src =  url+".aspx";
        }
        else
        {
         mainFrame.src = "Default.aspx";
        }
    
    }catch(e)
    {
     mainFrame.src = "Default.aspx";
    }
}

init();


