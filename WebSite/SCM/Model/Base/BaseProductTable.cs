using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    [Serializable]
    public partial class BaseProductTable
    {
        public BaseProductTable()
        { }
        private string _code;
        private string _name;
        private string _product_spec;
        private string _group_code;
        private string _style;
        private string _color;
        private string _size;
        private string _unit_code;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private string _create_user;
        private DateTime _create_date_time;
        private DateTime _last_update_time;
        private string _last_update_user;
        private string _style_name;
        private string _product_group_name;
        private string _color_name;
        private string _size_name;
        private string _unit_name;
        private string _create_user_name;
        private string _update_user_name;
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
        public string PRODUCT_SPEC
        {
            set { _product_spec = value; }
            get { return _product_spec; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GROUP_CODE
        {
            set { _group_code = value; }
            get { return _group_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STYLE
        {
            set { _style = value; }
            get { return _style; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COLOR
        {
            set { _color = value; }
            get { return _color; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SIZE
        {
            set { _size = value; }
            get { return _size; }
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
        /// <summary>
        /// 
        /// </summary>
        public string STYLE_NAME
        {
            set { _style_name = value; }
            get { return _style_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRODUCT_GROUP_NAME
        {
            set { _product_group_name = value; }
            get { return _product_group_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COLOR_NAME
        {
            set { _color_name = value; }
            get { return _color_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SIZE_NAME
        {
            set { _size_name = value; }
            get { return _size_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UNIT_NAME
        {
            set { _unit_name = value; }
            get { return _unit_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CREATE_USER_NAME
        {
            set { _create_user_name = value; }
            get { return _create_user_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UPDATE_USER_NAME
        {
            set { _update_user_name = value; }
            get { return _update_user_name; }
        }


    }
}
