using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
   public partial class BaseNewsTable
    {
       public BaseNewsTable()
		{}
		#region Model
		private decimal _id;
		private decimal _parent_id;
		private DateTime _publish_date;
		private string _news_title;
		private string _news_content;
		private int _news_type;
		private int _status_flag;
		private string _create_user;
		private DateTime _create_date_time;
		private string _last_update_user;
		private DateTime _last_update_time;
        private string _create_name;
        private string _type_name;

        public string TYPE_NAME
        {
            get { return _type_name; }
            set { _type_name = value; }
        }
        public string CREAT_NAME
        {
            get { return _create_name; }
            set { _create_name = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public decimal ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal PARENT_ID
		{
			set{ _parent_id=value;}
			get{return _parent_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime PUBLISH_DATE
		{
			set{ _publish_date=value;}
			get{return _publish_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NEWS_TITLE
		{
			set{ _news_title=value;}
			get{return _news_title;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NEWS_CONTENT
		{
			set{ _news_content=value;}
			get{return _news_content;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int NEWS_TYPE
		{
			set{ _news_type=value;}
			get{return _news_type;}
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
		/// 创建人员
		/// </summary>
		public string CREATE_USER
		{
			set{ _create_user=value;}
			get{return _create_user;}
		}
		/// <summary>
		/// 创建时间
		/// </summary>
		public DateTime CREATE_DATE_TIME
		{
			set{ _create_date_time=value;}
			get{return _create_date_time;}
		}
		/// <summary>
		/// 最后更新人员
		/// </summary>
		public string LAST_UPDATE_USER
		{
			set{ _last_update_user=value;}
			get{return _last_update_user;}
		}
		/// <summary>
		/// 更新时间
		/// </summary>
		public DateTime LAST_UPDATE_TIME
		{
			set{ _last_update_time=value;}
			get{return _last_update_time;}
		}
		#endregion Model

    }
}
