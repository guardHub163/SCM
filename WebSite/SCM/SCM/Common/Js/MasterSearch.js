/**
*
*
*
*
*/
var obj = new Object(); 
obj.key = "CANCEL";
var currentRowId = ""; 
var currentRowBackgroundColor = "";
function processClick(item,gridView)
{
    switch(item.id)
    {
        case "btnOK" :            
            obj.key = "OK";
            break;
        case "btnCancel" :
            break;
    }      
    window.close();
}

function processUnload(item)
{
   window.returnValue = obj;
}

function processRowClick(rowId)
{
    if(rowId == currentRowId)
    {
        return;
    }
    
   document.getElementById("btnOK").disabled = false;
    var row = document.getElementById(rowId)
    if ( row == null){
        return;
    }                             
    if (document.getElementById(currentRowId) != null )
    {
        document.getElementById(currentRowId).style.backgroundColor =currentRowBackgroundColor;
    }
    currentRowId = rowId;
    currentRowBackgroundColor = row.style.backgroundColor;
    row.style.backgroundColor = clickRowBackgroundColor;
    obj.code = row.cells[0].innerHTML;
    obj.name = row.cells[1].innerHTML;
}

function processSearch()
{
    var currentRowId = ""; 
    var currentRowBackgroundColor = "";
    obj.code = "";
    obj.name = "";
}