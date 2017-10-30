using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    [Serializable]
    public partial class BaseItemTable
    {
        public BaseItemTable()
        { }
        #region Model
        private string _code;
        private string _name;
        private string _spec;
        private string _unit_code;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private string _attribute4;
        private string _attribute5;
        private string _create_user;
        private DateTime _create_date_time;
        private DateTime _last_update_time;
        private string _last_update_user;
        private string creat_name;

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
        private string unit_name;

        public string Unit_name
        {
            get { return unit_name; }
            set { unit_name = value; }
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
        public string SPEC
        {
            set { _spec = value; }
            get { return _spec; }
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
        public string ATTRIBUTE4
        {
            set { _attribute4 = value; }
            get { return _attribute4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string ATTRIBUTE5
        {
            set { _attribute5 = value; }
            get { return _attribute5; }
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
