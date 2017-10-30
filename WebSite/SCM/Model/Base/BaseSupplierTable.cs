using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model.Base
{
    [Serializable]
    public partial class BaseSupplierTable
    {
        public BaseSupplierTable()
        { }
        #region Model
        private string _code;
        private string _name;
        private string _name_short;
        private string _address;
        private string _post_code;
        private string _tel;
        private string _fax;
        private string _contact;
        private string _email;
        private int _type;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private string _create_user;
        private DateTime _create_date_time;
        private string _last_update_user;
        private DateTime _last_update_time;
        private string _creat_name;
        private string typenama;
        private string warehouse_name;
        private string _warehouse_code;

        public string Warehouse_name
        {
            get { return warehouse_name; }
            set { warehouse_name = value; }
        }
       
        public string Typenama
        {
            get { return typenama; }
            set { typenama = value; }
        }

        public string WAREHOUSE_CODE 
        {
            get { return _warehouse_code; }
            set { _warehouse_code = value; }
        }

        public string Creat_name
        {
            get { return _creat_name; }
            set { _creat_name = value; }
        }
        private string _last_creat_name;

        public string Last_creat_name
        {
            get { return _last_creat_name; }
            set { _last_creat_name = value; }
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
        public string NAME_SHORT
        {
            set { _name_short = value; }
            get { return _name_short; }
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
        public string POST_CODE
        {
            set { _post_code = value; }
            get { return _post_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TEL
        {
            set { _tel = value; }
            get { return _tel; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FAX
        {
            set { _fax = value; }
            get { return _fax; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CONTACT
        {
            set { _contact = value; }
            get { return _contact; }
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
        public int TYPE
        {
            set { _type = value; }
            get { return _type; }
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
