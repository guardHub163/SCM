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
using SCM.Bll;
using SCM.Model;
using System.Text;
using SCM.Common;
using log4net;
using System.Reflection;

namespace SCM.Web.TransferIn
{
    public partial class TransferRelationSearch : BasePage
    {
        BTransferRelation bll = new BTransferRelation();
        DataSet ds = new DataSet();
        BCommon bCommon = new BCommon();
        ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        protected void Page_Load(object sender, EventArgs e)
        {
             base._log = _log;
            ValidateRole(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName);
            if (!Page.IsPostBack)
            {
                gridView.DataSource = InitDataTable();
                gridView.DataBind();
                // btnNew.Attributes.Add("onclick", "return winOpen('TransferRelationInput.aspx?','','570','1000');");
                this.txtOutFromDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.txtOutToDate.Text = DateTime.Now.ToString("yyyy/MM/dd");
            }
            this.paging.PageChanged += new PageControl.PageChangedEventHandler(PageChanged);

        }

        protected void PageChanged(object sender, int e)
        {
            BindData();
        }

        private DataTable InitDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("FROM_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("TO_WAREHOUSE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("SHIPMENT_SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("TRANSFER_IN_SLIP_NUMBER", Type.GetType("System.String"));
            dt.Columns.Add("CREATE_DATE_TIME", Type.GetType("System.DateTime"));
            dt.Columns.Add("CREATE_NAME", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE1", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE2", Type.GetType("System.String"));
            dt.Columns.Add("ATTRIBUTE3", Type.GetType("System.String"));
            for (int i = 1; i <= PageSize; i++)
            {
                dt.Rows.Add(dt.NewRow());

            }
            return dt;
        }
        #region change 事件
        protected void From_Warehouse_Change(object sender, EventArgs e)
        {
            if (txtFromWarehouseCode.Text.Trim() == "")
            {
                this.txtFromWarehouseCode.Text = "";
                this.lblFromWarehouseName.Text = "";
                return;
            }
            BaseMaster table = bCommon.GetBaseMaster("BASE_WAREHOUSE", txtFromWarehouseCode.Text.Trim(), "");
            if (table != null)
            {
                this.txtFromWarehouseCode.Text = table.Code;
                this.lblFromWarehouseName.Text = table.Name;
            }
            else
            {
                this.lblFromWarehouseName.Text = "";
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"出库仓库不存在!\");document.getElementById('" + txtFromWarehouseCode.ClientID + "').value='';", true);
            }

        }
        protected void OutFromDate_Changed(object sender, EventArgs e)
        {
            if (txtOutFromDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtOutFromDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtOutFromDate.ClientID + "').value='';", true);
                }
                else
                {
                    txtOutFromDate.Text = Convert.ToDateTime(txtOutFromDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
            }
        }
        protected void OutToDate_Changed(object sender, EventArgs e)
        {
            if (txtOutToDate.Text.Trim() != "")
            {
                if (!PageValidate.IsDateTime(txtOutToDate.Text.Trim()))
                {
                    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"日期格式错误!\");document.getElementById('" + txtOutToDate.ClientID + "').value='';", true);
                    return;
                }
                else
                {
                    txtOutToDate.Text = Convert.ToDateTime(txtOutToDate.Text.Trim()).ToString("yyyy/MM/dd");
                }
                if (this.txtOutFromDate.Text.Trim() != "" && this.txtOutToDate.Text.Trim() != "")
                {
                    if (Convert.ToDateTime(txtOutToDate.Text) < Convert.ToDateTime(txtOutFromDate.Text))
                    {
                        ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"起始时间不能大于截止时间!\");document.getElementById('" + txtOutToDate.ClientID + "').value='';", true);
                    }
                }
            }
        }
        #endregion
        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSearch":                   //查询
                    Search(sender, e);
                    break;
                //case "btnNew":                      //新建
                //    BindData();
                //    break;
                //case "btnDelete":
                //    try
                //    {
                //        LinkButton btn = (LinkButton)sender;
                //        bll.Delete(btn.CommandArgument);
                //        BindData();
                //    }
                //    catch { }
                //    break;
            }
            return true;
        }

        private void BindData()
        {
            string strWhere = getConduction();
            ds = bll.GetTranferRelationByPage(strWhere, "", (this.paging.CurrentPage - 1) * PageSize + 1, this.paging.CurrentPage * PageSize);
            for (int i = ds.Tables[0].Rows.Count; i < PageSize; i++)
            {
                ds.Tables[0].Rows.Add(ds.Tables[0].NewRow());
            }
            gridView.DataSource = ds;
            gridView.DataBind();
        }

        private string getConduction()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" STATUS_FLAG <> {0}", CConstant.DELETE);
            if (this.txtSlipNumber.Text.Trim() != "")
            {
                sb.AppendFormat(" AND SLIP_NUMBER = {0}", txtSlipNumber.Text.Trim());
            }
            if (this.txtFromWarehouseCode.Text.Trim() != "")
            {
                sb.AppendFormat(" AND FROM_WAREHOUSE_CODE={0}", txtFromWarehouseCode.Text.Trim());
            }
            if (txtOutFromDate.Text.Trim() != "" && txtOutToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME BETWEEN '{0}' AND '{1}'", txtOutFromDate.Text.Trim(), Convert.ToDateTime(txtOutToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            else if (txtOutFromDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME >= '{0}'", txtOutFromDate.Text.Trim());
            }
            else if (txtOutToDate.Text.Trim() != "")
            {
                sb.AppendFormat(" AND CREATE_DATE_TIME < '{0}'", Convert.ToDateTime(txtOutToDate.Text.Trim()).AddDays(1).ToString("yyyy/MM/dd"));
            }
            return sb.ToString();
        }

        private void Search(object sender, EventArgs e)
        {
            int recordCount = bll.GetTranferRelationCount(getConduction());
            if (recordCount > 0)
            {
                panelPage.Visible = true;
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"您查询的信息不存在！\");processCloseAndRefreshParent();", true);
                panelPage.Visible = false;
            }
            //将每页显示的数量保存在用户控件
            this.paging.PageSize = PageSize;
            //将数据总条数保存在用户控件
            this.paging.RecorderCount = recordCount;
            BindData();
        }
        protected void gridView_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //详细按钮
                LinkButton btnS = (LinkButton)e.Row.FindControl("btnShow");
                if (btnS.Text.Trim() != "&nbsp;" && btnS.Text.Trim() != "")
                {
                    btnS.Attributes.Add("onclick", "return winOpen('TransferRelationShow.aspx?','SN=" + btnS.CommandArgument + "','610','1020')");

                    //光标移动事件
                    e.Row.Attributes.Add("OnMouseOver", "c=this.style.backgroundColor;this.style.backgroundColor=mouseOverBackgroundColor;");
                    e.Row.Attributes.Add("OnMouseOut", "this.style.backgroundColor=c;");
                }
     
            }
        }
    }
}
