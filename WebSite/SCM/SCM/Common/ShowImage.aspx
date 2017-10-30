<%@ Page Language="C#" MasterPageFile="~/AJAXMasterPage.master" AutoEventWireup="true"
    CodeFile="ShowImage.aspx.cs" Inherits="SCM.Web.Common.ShowImage" Title="图片上传" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headPlace" runat="Server">
    <base target="_self" />
    <script language="javascript" type="text/javascript" src="../Script/ComScript.js"></script>
    <script type="text/javascript" language="javascript">
        function processClick(item)
        {
            document.getElementById("divImgList").style.visibility = "hidden";
            document.getElementById("divOperate").style.visibility = "hidden";
            document.getElementById("divUpLoadImg").style.visibility = "visible";
        }
        
        function processClickImg(item,bigSrc,fileName)
        {
            var txtCurrentImage =document.getElementById("ctl00_bodyPlace_txtCurrentImage");
            if(txtCurrentImage.value != fileName){
                document.getElementById("ctl00_bodyPlace_maxImg").src = bigSrc;
                txtCurrentImage.value = fileName;
            }
        }
        
        function processAddLi(productCode,fileName)
        {
            var cul  = document.getElementById("ctl00_bodyPlace_myUl");
            cli = document.createElement("li"); 
            cli.innerHTML = "<img src=\"../Image.aspx?TYPE=PRODUCT&PRODUCT_CODE="+productCode+"&FILE_NAME=s_"+fileName+"\" alt=\"加载失败\" class=\"smallPhoto\" onclick=\"processClickImg(this,'../Image.aspx?TYPE=PRODUCT&PRODUCT_CODE="+productCode+"&FILE_NAME="+fileName+"','"+fileName+"');\" />";         
            cul.appendChild(cli);
            
            document.getElementById("ctl00_bodyPlace_txtCurrentImage").value = fileName;
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyPlace" runat="Server">
    <asp:TextBox ID="txtProductCode" runat="server" Enabled="false" Visible="false"></asp:TextBox>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div style="margin-left: 5px; margin-top: 2px; border-style: double; border-color: Green;
                width: 800px;">
                <img id="maxImg" src="" style="height: 600px; width: 800px;" alt=""  runat="server"/>
            </div>            
            <div id="divImgList" class="imgList" style="position: absolute; top: 610px; left: 5px;">
                <ul id="myUl" runat="server">
                    <asp:Repeater ID="Repeater1" runat="server" OnItemDataBound="Repeater_ItemDataBound">
                        <ItemTemplate>
                            <li>
                                <img src='<%# Eval("SRC") %>' alt="加载失败" class="smallPhoto" onclick="processClickImg(this,'<%# Eval("BIG_SRC") %>','<%# Eval("FILE_NAME") %>');" />
                            </li>
                        </ItemTemplate>
                    </asp:Repeater>
                </ul>
            </div>
            <div id="divOperate" style="position: absolute; top: 720px; left: 5px; width: 802px;
                text-align: right;">
                <asp:TextBox ID="txtCurrentImage" runat="server" Style="position: absolute; top: 2px;
                    left: 0px; border:0px Green solid; width:300px;" Enabled="false"></asp:TextBox>
                <a href="#" id="btnCancel" class="LinkButton3" onclick="processClick(this);">上传照片</a>
                <asp:LinkButton ID="btnDelete" runat="server" Text="删除照片" CssClass="LinkButton3" OnClick="Delete_Click"></asp:LinkButton>
                <a href="#" id="A1" class="LinkButton3" onclick="processClose('你确定要关闭吗?');">关闭</a>
            </div>
            <div id="divUpLoadImg" style="position: absolute; width: 802px; top: 610px; left: 5px;
                visibility: hidden;">
                <iframe id="Iframe1" src="UpLoadImage.aspx?PC=<%=txtProductCode.Text%>" style="margin: 0px;
                    border: 0px; width: 802px;" scrolling="no"></iframe>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
