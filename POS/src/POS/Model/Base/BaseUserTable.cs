using System;
namespace POS.Model
{
	/// <summary>
	/// Base_User:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BaseUserTable
	{
		public BaseUserTable()
		{}
		#region Model
		private int _id;
		private string _user_id;
		private string _password;
		private string _true_name;
		private string _sex;
		private string _phone;
		private string _email;
		private string _department_code;
        private string _department_name;
		private int _status_flag;
		private string _user_type;
		private int _roles_id;
		private int _style;
		private string _create_user_id;
		private DateTime _create_date;
		private string _last_update_user_id;
		private DateTime _last_update_time;
        private string _photo_path;
        private Byte[] _photo;
        private string _supplier_code;
        private string _supplier_name;

       
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string USER_ID
		{
			set{ _user_id=value;}
			get{return _user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PASSWORD
		{
			set{ _password=value;}
			get{return _password;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string TRUE_NAME
		{
			set{ _true_name=value;}
			get{return _true_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SEX
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PHONE
		{
			set{ _phone=value;}
			get{return _phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string EMAIL
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DEPARTMENT_CODE
		{
			set{ _department_code=value;}
			get{return _department_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int STATUS_FLAG
		{
			set{ _status_flag=value;}
			get{return _status_flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string USER_TYPE
		{
			set{ _user_type=value;}
			get{return _user_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int ROLES_ID
		{
			set{ _roles_id=value;}
			get{return _roles_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int STYLE
		{
			set{ _style=value;}
			get{return _style;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_USER_ID
		{
			set{ _create_user_id=value;}
			get{return _create_user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime CREATE_DATE
		{
			set{ _create_date=value;}
			get{return _create_date;}
		}
		/// <summary>
		/// 
		/// </summary>
        public string LAST_UPDATE_USER_ID
		{
			set{ _last_update_user_id=value;}
			get{return _last_update_user_id;}
		}
		/// <summary>
		/// 
		/// </summary>
        public DateTime LAST_UPDATE_TIME
		{
			set{ _last_update_time=value;}
			get{return _last_update_time;}
		}
        /// <summary>
        /// 
        /// </summary>
        public string PHOTO_PATH
        {
            get { return _photo_path; }
            set { _photo_path = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public Byte[] PHOTO
        {
            get { return _photo; }
            set { _photo = value; }
        }

        public string SUPPLIER_CODE 
        {
            get { return _supplier_code; }
            set { _supplier_code = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string DEPARTMENT_NAME
        {
            set { _department_name = value; }
            get { return _department_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SUPPLIER_NAME
        {
            set { _supplier_name = value; }
            get { return _supplier_name; }
        }
		#endregion Model

	}
}

