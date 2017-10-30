using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Model
{
    public class TmpSalesOrder
    {
        public TmpSalesOrder()
		{}
		#region Model
		private string _slip_number;
		private int _line_number;
		private string _customer_code;
		private string _sales_employee;
		private string _product_code;
		private string _style_name;
		private string _color_name;
		private string _size;
		private decimal? _ori_price;
		private decimal? _discount_rate;
		private decimal? _price;
		private decimal? _quantity;
		private decimal? _amount;
		private string _memo;
		private string _memo2;
		private DateTime? _create_date_time;
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
		public string CUSTOMER_CODE
		{
			set{ _customer_code=value;}
			get{return _customer_code;}
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
		public string PRODUCT_CODE
		{
			set{ _product_code=value;}
			get{return _product_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string STYLE_NAME
		{
			set{ _style_name=value;}
			get{return _style_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string COLOR_NAME
		{
			set{ _color_name=value;}
			get{return _color_name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SIZE
		{
			set{ _size=value;}
			get{return _size;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? ORI_PRICE
		{
			set{ _ori_price=value;}
			get{return _ori_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? DISCOUNT_RATE
		{
			set{ _discount_rate=value;}
			get{return _discount_rate;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? PRICE
		{
			set{ _price=value;}
			get{return _price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? QUANTITY
		{
			set{ _quantity=value;}
			get{return _quantity;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal? AMOUNT
		{
			set{ _amount=value;}
			get{return _amount;}
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
		public string MEMO2
		{
			set{ _memo2=value;}
			get{return _memo2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime? CREATE_DATE_TIME
		{
			set{ _create_date_time=value;}
			get{return _create_date_time;}
		}
		#endregion Model
    }
}
