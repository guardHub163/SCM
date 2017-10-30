<%@ Page Language="C#" %>

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string path = "";
        if (Request.Params["TYPE"] != null && Request.Params["TYPE"].ToString() != "")
        {
            string fileName = "";
            if (Request.Params["FILE_NAME"] != null && Request.Params["FILE_NAME"].ToString() != "")
            {
                fileName = Request.Params["FILE_NAME"].ToString();
                fileName = fileName.Replace("/", "\\");
            }
            path = Server.MapPath("~").ToString() + "\\" + "UploadFiles" + "\\" + "Images" + "\\" + Request.Params["TYPE"].ToString();
            //商品显示
            if (Request.Params["PRODUCT_CODE"] != null && Request.Params["PRODUCT_CODE"].ToString() != "")
            {
                path += "\\" + Request.Params["PRODUCT_CODE"].ToString();
            }
            path += "\\" + fileName;
        }
        Response.ClearContent();
        Response.ContentType = "image/jpeg";
        if (!System.IO.File.Exists(path))
        {
            path = Server.MapPath("~").ToString() + "\\" + "Images" + "\\" + "nopic.png";
        }
        try
        {
            System.IO.FileStream buffer = new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            System.IO.BinaryReader br = new System.IO.BinaryReader(buffer);
            byte[] FileByte = br.ReadBytes((int)buffer.Length);
            br.Close();
            buffer.Close();
            buffer.Dispose();
            Response.BinaryWrite(FileByte);
        }
        catch (Exception EX) { }

    }
</script>

