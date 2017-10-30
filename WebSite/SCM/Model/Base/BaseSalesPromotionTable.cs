using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    /// <summary>
    /// BASE_SALES_PROMOTION:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BaseSalesPromotionTable
    {
        public BaseSalesPromotionTable()
        { }
        #region Model
        private string _code;
        private string _name;
        private string _department_code;
        private string _property1;
        private string _property2;
        private string _property3;
        private string _property4;
        private string _property5;
        private int _status_flag;
        private DateTime _start_time;
        private DateTime _end_time;
        private string _create_user;
        private DateTime _create_date_time;
        private string _last_update_user;
        private DateTime _last_update_time;
        private string _department_name;

        public string DEPARTMENT_NAME
        {
            get { return _department_name; }
            set { _department_name = value; }
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
        public string DEPARTMENT_CODE
        {
            set { _department_code = value; }
            get { return _department_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROPERTY1
        {
            set { _property1 = value; }
            get { return _property1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROPERTY2
        {
            set { _property2 = value; }
            get { return _property2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROPERTY3
        {
            set { _property3 = value; }
            get { return _property3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROPERTY4
        {
            set { _property4 = value; }
            get { return _property4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PROPERTY5
        {
            set { _property5 = value; }
            get { return _property5; }
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
        /// 
        /// </summary>
        public DateTime START_TIME
        {
            set { _start_time = value; }
            get { return _start_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime END_TIME
        {
            set { _end_time = value; }
            get { return _end_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATE_USER
        {
            set { _create_user = value; }
            get { return _create_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CREATE_DATE_TIME
        {
            set { _create_date_time = value; }
            get { return _create_date_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LAST_UPDATE_USER
        {
            set { _last_update_user = value; }
            get { return _last_update_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LAST_UPDATE_TIME
        {
            set { _last_update_time = value; }
            get { return _last_update_time; }
        }
        #endregion Model

    }
}
