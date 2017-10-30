<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpLoadInfo.aspx.cs" Inherits="SCM.Web.Common.UpLoadInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <base target="_parent" />
    <link href="../Css/CommonStyle.css" type="text/css" rel="stylesheet" />

    <script type="text/javascript" language="javascript">
     function processChange(item)
     {
         var isValid=false;
         var suffix =["xls","xlsx"]; 
         var temp = item.value.split("."); 
         var extension = temp[temp.length-1]; 
         extension = extension.toLowerCase();         
         for(i=0;i<suffix.length;i++)
          {
               if (extension==suffix[i])
               {
                    isValid=true;
               }
          }
         if(isValid==false)
            {
                 alert("上传的文件必须以xls,xlsx结尾！");
                  return false;
            }
     }

    </script>

</head>
<body>
    <form id="form1" runat="server">
    <table>
        <tr>
            <td>
                <input type="file" runat="server" onchange="processChange(this);" id="fileUpload"
                    style="width: 300px; height: 22px; border: 1px solid  Green;" />
            </td>
            <td>
                <asp:LinkButton ID="btnUpload" runat="server" Text="上传" CssClass="LinkButton2" OnClick="processClick"></asp:LinkButton>
                <%--<a href="#" id="btnCancel" class="LinkButton2" onclick="window.close();">取消</a>--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
