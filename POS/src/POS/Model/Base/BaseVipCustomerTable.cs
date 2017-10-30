using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Model
{
    public partial class BaseVipCustomerTable
    {
        public BaseVipCustomerTable()
        { }
        #region Model
        private string _code;
        private int _vip_level;
        private string _name;
        private string _department_code;
        private string _address;
        private string _qq;
        private string _email;
        private string _ww;
        private DateTime _birth_date;
        private DateTime _last_sales_date;
        private decimal _discount_rate;
        private int _points;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private int _status_flag;
        private DateTime _create_date_time;
        private string _create_user;
        private DateTime _last_update_time;
        private string _last_update_user;
        private string creat_name;
        private string department;
        private int _used_points;

        public int USED_POINTS
        {
            get { return _used_points; }
            set { _used_points = value; }
        }

        public string Department
        {
            get { return department; }
            set { department = value; }
        }
        public string Creat_name
        {
            get { return creat_name; }
            set { creat_name = value; }
        }
        private string update_name;

        public string Update_name
        {
            get { return update_name; }
            set { update_name = value; }
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
        public int VIP_LEVEL
        {
            set { _vip_level = value; }
            get { return _vip_level; }
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

        public int STATUS_FLAG
        {
            set { _status_flag = value; }
            get { return _status_flag; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ADDRESS
        {
            set { _address = value; }
            get { return _address; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string QQ
        {
            set { _qq = value; }
            get { return _qq; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string EMAIL
        {
            set { _email = value; }
            get { return _email; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WW
        {
            set { _ww = value; }
            get { return _ww; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime BIRTH_DATE
        {
            set { _birth_date = value; }
            get { return _birth_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LAST_SALES_DATE
        {
            set { _last_sales_date = value; }
            get { return _last_sales_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DISCOUNT_RATE
        {
            set { _discount_rate = value; }
            get { return _discount_rate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int POINTS
        {
            set { _points = value; }
            get { return _points; }
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
        public string CREATE_USER
        {
            set { _create_user = value; }
            get { return _create_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LAST_UPDATE_TIME
        {
            set { _last_update_time = value; }
            get { return _last_update_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LAST_UPDATE_USER
        {
            set { _last_update_user = value; }
            get { return _last_update_user; }
        }
        #endregion Model
    }
}
