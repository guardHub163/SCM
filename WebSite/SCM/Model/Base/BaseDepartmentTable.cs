using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    [Serializable]
    public partial class BaseDepartmentTable
    {
        /// <summary>
        /// BASE_DEPARTMENT:实体类(属性说明自动提取数据库字段的描述信息)
        /// </summary>
        public BaseDepartmentTable()
        { }
        #region Model
        private string _code;
        private string _name;
        private string _parent_code;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private string _create_user;
        private DateTime _create_date_time;
        private string _last_update_user;
        private DateTime _last_update_time;
        private string creat_user_name;
        private string _department_type;

        public string Department_type
        {
            get { return _department_type; }
            set { _department_type = value; }
        }

      

        public string Creat_user_name
        {
            get { return creat_user_name; }
            set { creat_user_name = value; }
        }
        private string update_user_name;

        public string Update_user_name
        {
            get { return update_user_name; }
            set { update_user_name = value; }
        }
        private string parent_name;

        public string Parent_name
        {
            get { return parent_name; }
            set { parent_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CODE
        {
            set { _code = value; }
            get { return _code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PARENT_CODE
        {
            set { _parent_code = value; }
            get { return _parent_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int STATUS_FLAG
        {
            set { _status_flag = value; }
            get { return _status_flag; }
        }
        /// <summary>
        /// 属性1
        /// </summary>
        public string ATTRIBUTE1
        {
            set { _attribute1 = value; }
            get { return _attribute1; }
        }
        /// <summary>
        /// 属性2
        /// </summary>
        public string ATTRIBUTE2
        {
            set { _attribute2 = value; }
            get { return _attribute2; }
        }
        /// <summary>
        /// 属性3
        /// </summary>
        public string ATTRIBUTE3
        {
            set { _attribute3 = value; }
            get { return _attribute3; }
        }
        /// <summary>
        /// 创建人员
        /// </summary>
        public string CREATE_USER
        {
            set { _create_user = value; }
            get { return _create_user; }
        }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CREATE_DATE_TIME
        {
            set { _create_date_time = value; }
            get { return _create_date_time; }
        }
        /// <summary>
        /// 最后更新人员
        /// </summary>
        public string LAST_UPDATE_USER
        {
            set { _last_update_user = value; }
            get { return _last_update_user; }
        }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime LAST_UPDATE_TIME
        {
            set { _last_update_time = value; }
            get { return _last_update_time; }
        }

        #endregion Model

    }
}

