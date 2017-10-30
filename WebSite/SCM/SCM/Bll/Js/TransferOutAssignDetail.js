/*
*
*
*
*
*
*
*
*/
var currentRowId = ""; 
var currentRowBackgroundColor = "";
function processRowClick(rowId,txtId)
{
    if(rowId == currentRowId)
    {
        return;
    }
    var row = document.getElementById(rowId)
    if ( row == null){
        return;
    }                             
    if (document.getElementById(currentRowId) != null )
    {
        document.getElementById(currentRowId).style.backgroundColor =currentRowBackgroundColor;
    }
    currentRowId = rowId;
    currentRowBackgroundColor = row.style.backgroundColor
    row.style.backgroundColor = clickRowBackgroundColor;
    var txtInput = document.getElementById(txtId)
    if( txtInput != null)
    {
        txtInput.select();
        txtInput.focus();            
    }        
}