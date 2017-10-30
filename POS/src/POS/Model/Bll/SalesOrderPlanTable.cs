using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Model
{
    public class SalesOrderPlanTable
    {
        public SalesOrderPlanTable()
		{}
		#region Model
		private string _slip_number;
		private int _line_number;
		private string _sales_order_slip_number;
		private string _department_code;
		private string _sales_employee;
		private string _customer_code;
		private string _customer_phone;
		private DateTime _end_date_time;
		private string _product_code;
		private decimal _ori_price;
		private decimal _discount_rate;
		private decimal _price;
		private decimal _quantity;
		private string _unit_code;
		private decimal _amount;
		private decimal _deposit;
		private decimal _balance;
		private int _status_flag;
		private int _send_flag;
		private string _memo;
		private string _create_user;
		private DateTime _create_date_time;
		private string _last_update_user;
		private DateTime _last_update_time;
        private decimal? _ori_amount;

        /// <summary>
        /// 
        /// </summary>
        public decimal? ORI_AMOUNT
        {
            get { return _ori_amount; }
            set { _ori_amount = value; }
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
		public int LINE_NUMBER
		{
			set{ _line_number=value;}
			get{return _line_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SALES_ORDER_SLIP_NUMBER
		{
			set{ _sales_order_slip_number=value;}
			get{return _sales_order_slip_number;}
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
		public string SALES_EMPLOYEE
		{
			set{ _sales_employee=value;}
			get{return _sales_employee;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CUSTOMER_CODE
		{
			set{ _customer_code=value;}
			get{return _customer_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CUSTOMER_PHONE
		{
			set{ _customer_phone=value;}
			get{return _customer_phone;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime END_DATE_TIME
		{
			set{ _end_date_time=value;}
			get{return _end_date_time;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PRODUCT_CODE
		{
			set{ _product_code=value;}
			get{return _product_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal ORI_PRICE
		{
			set{ _ori_price=value;}
			get{return _ori_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal DISCOUNT_RATE
		{
			set{ _discount_rate=value;}
			get{return _discount_rate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal PRICE
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal QUANTITY
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string UNIT_CODE
		{
			set{ _unit_code=value;}
			get{return _unit_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal AMOUNT
		{
			set{ _amount=value;}
			get{return _amount;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal DEPOSIT
		{
			set{ _deposit=value;}
			get{return _deposit;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal BALANCE
		{
			set{ _balance=value;}
			get{return _balance;}
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
		public string MEMO
		{
			set{ _memo=value;}
			get{return _memo;}
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
		public DateTime CREATE_DATE_TIME
		{
			set{ _create_date_time=value;}
			get{return _create_date_time;}
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
		public DateTime LAST_UPDATE_TIME
		{
			set{ _last_update_time=value;}
			get{return _last_update_time;}
		}
		#endregion Model
    }
}
