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
using System.Drawing;
using System.Text;
using SCM.Common;
using SCM.Model;
using System.Collections.Generic;
using System.Reflection;
using log4net;

namespace SCM.Web.User
{
    [Serializable]
    public partial class UserType : BasePage
    {
        BUser buser = new BUser();
        BCommon bCommon = new BCommon();
        Hashtable userType = new Hashtable();
        private static ILog _log = log4net.LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //页面加载
        protected void Page_Load(object sender, EventArgs e)
        {
            base._log = _log;
            if (!Page.IsPostBack)
            {
                //HttpContext.Current.Session["userType"] = GetHashUserType();
                ViewState["userType"] = GetHashUserType();
                selUserType.DataSource = bCommon.GetNames("USER_TYPE").Tables[0];
                selUserType.DataTextField = "NAME"; //dropdownlist的Text的字段 
                selUserType.DataValueField = "CODE";//dropdownlist的Value的字段 
                selUserType.DataBind();
                selUserType.SelectedIndex = 0;
                selUserType_SelectedIndexChanged(selUserType, EventArgs.Empty);
            }

        }

        #region 获得整个数据hashtable
        //存储所有数据的hashtable
        public Hashtable GetHashUserType()
        {
            Hashtable hs = new Hashtable();
            DataSet dsa = buser.GetAllPower();
            string currentCategory = "";
            RolesTable parentRoles = new RolesTable();
            RolesTable childRoles = new RolesTable();
            try
            {
                foreach (DataRow row in dsa.Tables[0].Rows)
                {
                    if (!currentCategory.Equals(Convert.ToString(row["CATEGORY_ID"])))
                    {
                        if (!currentCategory.Equals(""))
                        {
                            hs.Add(parentRoles.VALUE, parentRoles);
                        }
                        currentCategory = Convert.ToString(row["CATEGORY_ID"]);
                        parentRoles = new RolesTable();
                        parentRoles.VALUE = currentCategory;
                        parentRoles.TEXT = Convert.ToString(row["CATEGORY_DESCIRIPTION"]);
                        childRoles = new RolesTable();
                        childRoles.VALUE = Convert.ToString(row["ID"]);
                        childRoles.TEXT = Convert.ToString(row["DESCRIPTION"]);
                        parentRoles.AddRoleList(childRoles);
                    }
                    else
                    {
                        childRoles = new RolesTable();
                        childRoles.VALUE = Convert.ToString(row["ID"]);
                        childRoles.TEXT = Convert.ToString(row["DESCRIPTION"]);
                        parentRoles.AddRoleList(childRoles);
                    }
                }


                hs.Add(parentRoles.VALUE, parentRoles);
                Hashtable temp = new Hashtable();
                DataSet ds = buser.GetSmallPower();
                string usertype = "";
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (!usertype.Equals(Convert.ToString(row["USER_TYPE"])))
                    {
                        if (!usertype.Equals(""))
                        {
                            userType.Add(usertype, temp);
                        }
                        usertype = Convert.ToString(row["USER_TYPE"]);
                        temp = new Hashtable();
                        //temp = (Hashtable)hs.Clone();
                        temp = GetHsCopy(hs);

                        if (row["CATEGORY_ID"].ToString() != "")
                        {
                            foreach (RolesTable roles in ((RolesTable)temp[row["CATEGORY_ID"]]).ROLE_LIST)
                            {
                                if (roles.VALUE.Equals(row["PERMISSION_ID"]))
                                {
                                    roles.IS_CHECK = true;
                                    roles.INIT_STATUS = true;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (row["CATEGORY_ID"].ToString() != "")
                        {
                            foreach (RolesTable roles in ((RolesTable)temp[row["CATEGORY_ID"]]).ROLE_LIST)
                            {
                                if (roles.VALUE.Equals(row["PERMISSION_ID"]))
                                {
                                    roles.IS_CHECK = true;
                                    roles.INIT_STATUS = true;
                                }
                            }
                        }
                    }
                }
                userType.Add(usertype, temp);

            }
            catch { }
            return userType;

        }

        private Hashtable GetHsCopy(Hashtable hs)
        {
            Hashtable temp = new Hashtable();
            foreach (DictionaryEntry d in hs)
            {
                RolesTable parentRolesTable = new RolesTable();
                RolesTable oldParentRolesTable = (RolesTable)d.Value;
                parentRolesTable.TEXT = oldParentRolesTable.TEXT;
                parentRolesTable.VALUE = oldParentRolesTable.VALUE;
                foreach (RolesTable oldChildRolesTable in oldParentRolesTable.ROLE_LIST)
                {
                    RolesTable childRolesTable = new RolesTable();
                    childRolesTable.TEXT = oldChildRolesTable.TEXT;
                    childRolesTable.VALUE = oldChildRolesTable.VALUE;
                    childRolesTable.IS_CHECK = oldChildRolesTable.IS_CHECK;
                    childRolesTable.INIT_STATUS = oldChildRolesTable.INIT_STATUS;
                    parentRolesTable.AddRoleList(childRolesTable);
                }
                temp.Add(d.Key, parentRolesTable);
            }
            return temp;
        }

        #endregion
        //用户类型的Change事件
        protected void selUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //userType = (Hashtable)HttpContext.Current.Session["userType"];
            userType = (Hashtable)ViewState["userType"];
            string type = selUserType.SelectedItem.Value.ToString();
            try
            {
                if (type != "")
                {
                    Hashtable hsParentRoles = (Hashtable)userType[type];
                    ListFatherPower.Items.Clear();
                    List<ListItemStort> list = new List<ListItemStort>();
                    foreach (DictionaryEntry db in hsParentRoles)
                    {
                        RolesTable role = (RolesTable)db.Value;
                        list.Add(new ListItemStort(Convert.ToInt32(role.VALUE), role.TEXT));
                    }
                    list.Sort();
                    foreach(ListItemStort obj in list)
                    {
                        ListFatherPower.Items.Add(new ListItem(obj.TEXT, obj.VALUE.ToString()));
                    }
                    ListFatherPower.SelectedIndex = 0;
                    ListFatherPower_SelectedIndexChanged(ListFatherPower, EventArgs.Empty);
                    
                }
            }
            catch { }

        }

        //父窗口的chang事件
        protected void ListFatherPower_SelectedIndexChanged(object sender, EventArgs e)
        {
            string type = selUserType.SelectedItem.Value.ToString();
            userType = (Hashtable)ViewState["userType"];
            Hashtable hsParentRoles = (Hashtable)userType[type];
            RolesTable parentRoles = (RolesTable)hsParentRoles[((ListItem)ListFatherPower.SelectedItem).Value];
            FinelyType.Items.Clear();
            try
            {
                foreach (RolesTable roles in parentRoles.ROLE_LIST)
                {
                    FinelyType.Items.Add(new ListItem(roles.TEXT, roles.VALUE));
                    FinelyType.Items[FinelyType.Items.Count - 1].Selected = roles.IS_CHECK;
                }
            }
            catch { }
        }

        protected override bool processBtnClick(string btnId, object sender, EventArgs e)
        {
            switch (btnId)
            {
                case "btnSave":
                    Save(sender, e);
                    break;
            }
            return true;
        }

        //保存方法
        private void Save(object sender, EventArgs e)
        {
            string type = selUserType.SelectedItem.Value.ToString();
            userType = (Hashtable)ViewState["userType"];
            if (buser.GetUserPower(userType, type))
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"保存成功！\");processCloseAndRefreshParent();", true);
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(UpdatePanel1, this.GetType(), "click", "alert(\"保存失败！\");processCloseAndRefreshParent();", true);
            }
        }

        //单个选中权限的点击事件
        protected void FinelyType_SelectedIndexChanged(object sender, EventArgs e)
        {
            userType = (Hashtable)ViewState["userType"];
            string type = selUserType.SelectedItem.Value.ToString();
            Hashtable hsParentRoles = (Hashtable)userType[type];
            try
            {
                RolesTable parentRoles = (RolesTable)hsParentRoles[((ListItem)ListFatherPower.SelectedItem).Value];
                foreach (ListItem li in FinelyType.Items)
                {
                    if (li.Selected)
                    {
                        string value = li.Value;
                        foreach (RolesTable roles in parentRoles.ROLE_LIST)
                        {
                            if (roles.VALUE == value)
                            {
                                roles.IS_CHECK = true;
                            }
                        }
                    }
                    else
                    {
                        string text = li.Text;
                        string value = li.Value;
                        foreach (RolesTable roles in parentRoles.ROLE_LIST)
                        {
                            if (roles.VALUE == value)
                            {
                                roles.IS_CHECK = false;
                            }
                        }
                    }

                }
            }
            catch { }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class ListItemStort : IComparable
    {
        #region 类字段定义
        private int _value;
        private string _text;
        #endregion
        public ListItemStort()
        {
        }

        public ListItemStort(int value, string text)
        {
            _value = value;
            _text = text;
        }

        #region 属性定义
        public int VALUE
        {
            set { this._value = value; }
            get { return this._value; }
        }
        public string TEXT
        {
            set { this._text = value; }
            get { return this._text; }
        }
        #endregion

        #region 实现比较接口的CompareTo方法
        public int CompareTo(object obj)
        {
            int res = 0;
            try
            {
                ListItemStort obj2 = (ListItemStort)obj;
                if (this._value > obj2._value)
                {
                    res = 1;
                }
                else if (this._value < obj2._value)
                {
                    res = -1;
                }
            }
            catch (Exception ex)
            {
                //throw new Exception("比较异常", ex.InnerException);
            }
            return res;
        }
        #endregion
    }

}
