using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public class BaseInventoryScheduleTable
    {
        public BaseInventoryScheduleTable()
		{}
		#region Model
		private string _slip_number;
		private string _warehouse_code;
		private DateTime _inventory_start_date;
		private DateTime _inventory_end_date;
		private int _status_flag;
		private DateTime _create_date_time;
		private string _create_user;
		private DateTime _last_update_time;
		private string _last_update_user;
		private string _warehouse_name;
		private string _status_name;
        private string _group_name;

        public string GROUP_NAME
        {
            get { return _group_name; }
            set { _group_name = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string SLIP_NUMBER
		{
			set{ _slip_number=value;}
			get{return _slip_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WAREHOUSE_CODE
		{
			set{ _warehouse_code=value;}
			get{return _warehouse_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime INVENTORY_START_DATE
		{
			set{ _inventory_start_date=value;}
			get{return _inventory_start_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime INVENTORY_END_DATE
		{
			set{ _inventory_end_date=value;}
			get{return _inventory_end_date;}
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
		public DateTime CREATE_DATE_TIME
		{
			set{ _create_date_time=value;}
			get{return _create_date_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CREATE_USER
		{
			set{ _create_user=value;}
			get{return _create_user;}
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
		public string LAST_UPDATE_USER
		{
			set{ _last_update_user=value;}
			get{return _last_update_user;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string WAREHOUSE_NAME
		{
			set{ _warehouse_name=value;}
			get{return _warehouse_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string STATUS_NAME
		{
			set{ _status_name=value;}
			get{return _status_name;}
		}
		#endregion Model
    }
}
