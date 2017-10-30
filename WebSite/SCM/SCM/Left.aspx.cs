using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using SCM.Model;
using SCM.Bll;
using SCM.Common;
using System.Text;
using System.IO;
using log4net;
using System.Reflection;


namespace SCM.Web
{
    public partial class Left : BaseModalDialogPage
    {
        BCommon bCommon = new BCommon();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                updatepassword.Attributes.Add("onclick", "return processUpdatePassWord(this);");
                photo1.Attributes.Add("onclick", "return processShowLoad(this);");
                txtFile.Attributes.Add("onchange", "processChange(this);");
                init();
            }
        }

        private void init()
        {
            try
            {
                BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
                lblName.Text = userTable.TRUE_NAME;
                photo1.Src = "";
                photo1.Src = "Image.aspx?TYPE=USER&FILE_NAME=" + userTable.PHOTO_PATH;
                DataSet ds = bCommon.GetMenu(userTable.USER_TYPE);

                StringBuilder sb = new StringBuilder();
                
                sb.Append("<script>");
                sb.Append(" var parentArr = new Array();");
                sb.Append(" var childArr = new Array();");
                int cId = Convert.ToInt32(ds.Tables[0].Rows[0]["CATEGORY_ID"]);
                sb.Append("childArr.cDesc = '" + Convert.ToString(ds.Tables[0].Rows[0]["C_DESC"]) + "';");
                int i = 0;
                int j = 0;
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    int categoryId = Convert.ToInt32(row["CATEGORY_ID"]);
                    if (cId != categoryId)
                    {
                        cId = categoryId;
                        j = 0;
                        sb.AppendFormat(" parentArr[{0}] = childArr;", i++);
                        sb.Append("childArr = new Array();");
                        sb.AppendFormat("childArr.cDesc = '{0}';", Convert.ToString(row["C_DESC"]));
                    }
                    sb.Append("var menu = new Object();");
                    sb.AppendFormat(" menu.pDesc = '{0}';", Convert.ToString(row["P_DESC"]));
                    sb.AppendFormat(" menu.pUrl= '{0}';", Convert.ToString(row["FUNCTION_URL"]));
                    sb.AppendFormat(" childArr[{0}] = menu;", j++);
                }
                if (ds.Tables[0].Rows.Count != 0)
                {
                    sb.AppendFormat(" parentArr[{0}] = childArr;", i++);
                }
                sb.Append("</script>");
                Response.Write(sb.ToString());
            }
            catch (Exception ex) { }
        }

        /// <summary>
        /// 文件上传
        /// </summary>
        protected void Upload_Click(object sender, EventArgs e)
        {
            UpLoadAndSaveImage img = new UpLoadAndSaveImage();
            BUser bUser = new BUser();
            BaseUserTable userTable = (BaseUserTable)Session["UserInfo"];
            if (userTable == null)
            {
                Response.Write("<script>parent.document.location.href='ParentTimeOut.aspx?';</script> ");
                return;
            }

            try
            {
                string path = HttpContext.Current.Server.MapPath("~") + "\\" + "UploadFiles" + "\\" + "Images" + "\\" + "USER" + "\\";
                img.FormFile = this.txtFile;
                img.SavePath = path;
                img.InFileName = userTable.USER_ID + ".jpg";
                img.SaveType = 1;
                img.sHeight = 70;
                img.sWidth = 70;
                img.CreateThumbnails();
                switch (img.Error) // Error返回值，1、没有上传的文件。2、类型不允许。3、大小超限。4、未知错误。0、上传成功。 
                {
                    case 0:
                        userTable.PHOTO_PATH = img.OutFileName;
                        Session["UserInfo"] = userTable;
                        BaseUserTable uTable = bUser.GetModel(userTable.ID);
                        uTable.PASSWORD = DESEncrypt.Decrypt(uTable.PASSWORD);
                        uTable.PHOTO_PATH = userTable.PHOTO_PATH;
                        bUser.Update(uTable);
                        break;
                    case 1:
                        Response.Write("<script>alert('没有上传的文件');</script> ");
                        break;
                    case 2:
                        Response.Write("<script>alert('类型不允许');</script> ");
                        break;
                    case 3:
                        Response.Write("<script>alert('大小超限');</script> ");
                        break;
                    case 4:
                        Response.Write("<script>alert('未知错误');</script> ");
                        break;
                }
            }
            catch { }
            init();
        }


        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            return false;
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            BaseUserTable busertable = new BaseUserTable();
            BUser bll = new BUser();
            string message = "";
            if (this.txtOldPassWord.Text.Trim().Length == 0)
            {
                message += "原密码不能为空\\n";
            }
            else if (DESEncrypt.Encrypt(this.txtOldPassWord.Text.Trim()) != UserTable.PASSWORD)
            {
                message += "原密码不正确\\n";
            }
            if (this.txtNewPassWord.Text.Trim().Length == 0)
            {
                message += "新密码不能为空\\n";
            }
            else if (this.txtNewPassWord.Text.Trim().Length < 6)
            {
                message += "密码长度不能小于6位\\n";
            }
            if (this.txtRePassWord.Text.Trim().Length == 0)
            {
                message += "确认密码不能为空\\n";
            }
            else if (this.txtRePassWord.Text != this.txtNewPassWord.Text)
            {
                message += "两次输入密码不一致";
            }
            if (message != "")
            {
                // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"" + message + "\");", true);
                Response.Write("<script>alert('" + message + "');</script>");
            }
            else
            {
                busertable.USER_ID = UserTable.USER_ID;
                busertable.PASSWORD = this.txtNewPassWord.Text;
                if (bll.UpdatePassWord(busertable))
                {
                    // ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"密码修改成功!\");processCloseAndRefreshParent();", true);
                    Response.Write("<script>alert('密码修改成功');</script>");
                }
                else
                {
                    //ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"密码修改失败\");", true);
                    Response.Write("<script>alert('密码修改失败');</script>");
                }
            }
            init();
        }
}//end class
}
