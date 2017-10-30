/**
*
*
*
*
*/
var arr = new Array();
var obj = new Object(); 
var isOK = false;   
function processClick(btnId,gvId)
{
    switch(btnId)
    {
        case "btnOK" :            
            isOK = true;
            createData(gvId);
            break;
        case "btnCancel" :
            break;
    }      
    window.close();
}
function createData(gvId)
{
    var gv=document.getElementById(gvId);
    var j=0;
    for( i=1;i<gv.rows.length;i++)
    {
        var chk=document.getElementById(gv.rows[i].cells[0].firstElementChild.id);
        if(chk.checked)
        {
            var quantity = document.getElementById(gv.rows[i].cells[11].firstElementChild.id);
            obj.quantity = quantity.value;
        }
        else
        {
            continue;
        }
        obj.code = gv.rows[i].cells[1].innerText;
        obj.name = gv.rows[i].cells[2].innerText;
        obj.styleCode = gv.rows[i].cells[3].innerText;
        obj.styleName = gv.rows[i].cells[4].innerText;
        obj.colorCode = gv.rows[i].cells[5].innerText;
        obj.colorName = gv.rows[i].cells[6].innerText;
        obj.sizeCode = gv.rows[i].cells[7].innerText;
        obj.sizeName = gv.rows[i].cells[8].innerText;
        obj.unitCode = gv.rows[i].cells[9].innerText;
        obj.unitName = gv.rows[i].cells[10].innerText;
        
        arr[j++] = obj;
    }
       
}

function processUnload(item)
{
    if(isOK)
    {
        window.returnValue = arr;
    }else
    {
        window.returnValue = new Array();
    }
}

function processSearch()
{
    isOK = false;
}

