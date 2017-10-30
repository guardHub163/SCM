using System;
namespace POS.Model
{
	/// <summary>
	/// BllCashTable:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class BllCashTable
	{
		public BllCashTable()
		{}
		#region Model
		private string _slip_number;
		private DateTime _cash_date;
		private decimal _profit_cash;
		private decimal _last_cash;
		private decimal _take_cash;
		private decimal _balance_cash;
		private string _sales_slip_number;
        private string _bank_name;
        private string _bank_slip_number;       
		private string _memo;
		private int _status_flag;
		private int _send_flag;
		private DateTime _create_date_time;
		private string _create_user;
		private DateTime _last_update_time;
		private string _last_update_user;
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
		public DateTime CASH_DATE
		{
			set{ _cash_date=value;}
			get{return _cash_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal PROFIT_CASH
		{
			set{ _profit_cash=value;}
			get{return _profit_cash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal LAST_CASH
		{
			set{ _last_cash=value;}
			get{return _last_cash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal TAKE_CASH
		{
			set{ _take_cash=value;}
			get{return _take_cash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal BALANCE_CASH
		{
			set{ _balance_cash=value;}
			get{return _balance_cash;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SALES_SLIP_NUMBER
		{
			set{ _sales_slip_number=value;}
			get{return _sales_slip_number;}
		}

        /// <summary>
        /// 
        /// </summary>
        public string BANK_NAME
        {
            get { return _bank_name; }
            set { _bank_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string BANK_SLIP_NUMBER
        {
            get { return _bank_slip_number; }
            set { _bank_slip_number = value; }
        }
		/// <summary>
		/// 
		/// </summary>
		public string MEMO
		{
			set{ _memo=value;}
			get{return _memo;}
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
		public int SEND_FLAG
		{
			set{ _send_flag=value;}
			get{return _send_flag;}
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
		#endregion Model

	}
}

