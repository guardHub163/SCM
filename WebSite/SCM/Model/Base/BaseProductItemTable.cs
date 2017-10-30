using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public partial class BaseProductItemTable
    {
        public BaseProductItemTable()
        { }
        #region Model
        private string _product_code;
        private string _item_code;
        private decimal _quantity;
        private int _status_flag;
        private string _supplier_code;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private string _create_user;
        private DateTime _create_date_time;
        private DateTime _last_update_time;
        private string _last_update_user;
        private string _product_name;
        private string _item_name;
        private string _supplier_name;
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string PRODUCT_NAME
        {
            set { _product_name = value; }
            get { return _product_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string ITEM_NAME
        {
            set { _item_name = value; }
            get { return _item_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// 
        public string SUPPLIER_NAME
        {
            set { _supplier_name = value; }
            get { return _supplier_name; }
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
        public string ITEM_CODE
        {
            set { _item_code = value; }
            get { return _item_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal QUANTITY
        {
            set { _quantity = value; }
            get { return _quantity; }
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
        public string SUPPLIER_CODE
        {
            set { _supplier_code = value; }
            get { return _supplier_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ATTRIBUTE1
        {
            set { _attribute1 = value; }
            get { return _attribute1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ATTRIBUTE2
        {
            set { _attribute2 = value; }
            get { return _attribute2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ATTRIBUTE3
        {
            set { _attribute3 = value; }
            get { return _attribute3; }
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
