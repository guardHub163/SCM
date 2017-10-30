/**
*
*
*
*
*
*
*/
function checkData(strId)
{
    var fileName=document.getElementById(strId).value;
    if(fileName=="")
    {
        return;
    }
    var exName=fileName.substr(fileName.lastIndexOf(".")+1).toUpperCase();

    if(exName=="JPG"||exName=="BMP"||exName=="GIF")
    {
        document.getElementById("imgPhoto").src=fileName;
    }
    else
    {
         alert("请选择正确的图片文件!");
         document.getElementById(strId).value="";
    } 
}