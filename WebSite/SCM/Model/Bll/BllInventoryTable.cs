using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    /// <summary>
    /// BASE_INVENTORY:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public partial class BllInventoryTable
    {
        public BllInventoryTable()
        { }
        #region Model
        private string _slip_number;
        private int _line_number;
        private string _product_code;
        private string _unit_code;
        private decimal _inventory;
        private decimal _real_inventory;
        private int _status_flag;
        private DateTime _create_date_time;
        private string _create_user;
        private DateTime _last_update_time;
        private string _last_update_user;
        private string _product_group_code;

        public string PRODUCT_GROUP_CODE
        {
            get { return _product_group_code; }
            set { _product_group_code = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SLIP_NUMBER
        {
            set { _slip_number = value; }
            get { return _slip_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LINE_NUMBER
        {
            set { _line_number = value; }
            get { return _line_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRODUCT_CODE
        {
            set { _product_code = value; }
            get { return _product_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UNIT_CODE
        {
            set { _unit_code = value; }
            get { return _unit_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal INVENTORY
        {
            set { _inventory = value; }
            get { return _inventory; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal REAL_INVENTORY
        {
            set { _real_inventory = value; }
            get { return _real_inventory; }
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
