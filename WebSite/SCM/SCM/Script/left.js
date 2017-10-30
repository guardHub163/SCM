/*
*
*
*
*
*
*/
var curretnMenuID = "";
function init()
{
    try{
        var nav = document.getElementById("nav");
        for(var i = 0;i<parentArr.length;i++)
        {
            var arr = parentArr[i];
            p_ul = document.createElement("ul");
            p_ul.id = "p_ul_"+i;
            p_ul.innerHTML="<a href=\"#\"onClick=\"processUlClick("+i+")\">&nbsp;&nbsp;"+arr.cDesc+"</a>";            
            nav.appendChild(p_ul);           
        }
    }catch(e){}
}

function processUlClick(i)
{
    //原有子菜单的删除
    try
    {
        var old_p_ul = document.getElementById(curretnMenuID);
        for(var k = old_p_ul.childNodes.length-1;k>=0;k--)
        {
            var node = old_p_ul.childNodes[k];
            if(node.tagName == "LI"){
                old_p_ul.removeChild(node);
            }
        }
    }catch(e){}
    
    //同一菜单再次点击时菜单收起
    var clickMenuID = "p_ul_"+i;
    if(clickMenuID == curretnMenuID)
    {
        curretnMenuID = "";
        return;
    }
    curretnMenuID = clickMenuID;
    //新的子菜单的创建
    var p_ul = document.getElementById(curretnMenuID)
    try{
        for(var j = 0;j<parentArr[i].length;j++)
        {
            var menu = parentArr[i][j];
            c_li = document.createElement("li");
            c_li.innerHTML = "<a href=\"#\" onclick=\"processClick("+i+","+j+")\">"+menu.pDesc+"</a>"  
            p_ul.appendChild(c_li);
        }
    }catch(e){}
}

function processClick(i,j)
{
    var mainFrame =  parent.document.getElementById("mainFrame");
    try{
        //var url = arr[parseInt(item.id)].pUrl;
        var url = parentArr[i][j].pUrl;
        if(url != ""){
            mainFrame.src =  url+".aspx";
        }
        else
        {
         mainFrame.src = "DBDInformix.aspx";
        }
    
    }catch(e)
    {
     mainFrame.src = "DBDInformix.aspx";
    }
}


function processChange(item)
{
    var filePath = item.value;
    document.getElementById('photo2').src = filePath;
    return false;
} 

function processShowLoad(item)
{
    document.getElementById("upload").style.visibility = "visible"; 
    document.getElementById("form1").reset();
    document.getElementById("photo2").src="";
    return false;
}

function processUpdatePassWord(item)
{
    document.getElementById("update").style.visibility = "visible"; 
    document.getElementById("form1").reset();
    return false;
}

function processUpdateToPassWord()
{
  document.getElementById("update").style.visibility = "hidden"; 
  return false;
}

init();
//GetMenuID(); //*这两个function的顺序要注意一下，不然在Firefox里GetMenuID()不起效果
//menuFix();