using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Model
{
    [Serializable]
   public partial class BaseProductPriceTable
    {
        public BaseProductPriceTable()
		{}
		#region Model
		private decimal _id;
		private string _department_code;
		private string _style_code;
        private decimal _ori_price;
		private string _price_code;
		private decimal _sales_price;
        private decimal _discount_rate;
		private int _default_flag;
		private DateTime _start_date;
		private DateTime _end_date;
		private int _status_flag;
		private string _attribute1;
		private string _attribute2;
		private string _attribute3;
		private string _create_user;
		private DateTime _create_date_time;
		private string _last_update_user;
		private DateTime _last_update_time;

        /// <summary>
        /// 
        /// </summary>
        private string _department_name;

        public string Department_name
        {
            get { return _department_name; }
            set { _department_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _style_name;

        public string Style_name
        {
            get { return _style_name; }
            set { _style_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string price_name;

        public string Price_name
        {
            get { return price_name; }
            set { price_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _default_name;

        public string Default_name
        {
            get { return _default_name; }
            set { _default_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _creat_name;

        public string Creat_name
        {
            get { return _creat_name; }
            set { _creat_name = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        private string _update_name;

        public string Update_name
        {
            get { return _update_name; }
            set { _update_name = value; }
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
		public string DEPARTMENT_CODE
		{
			set{ _department_code=value;}
			get{return _department_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string STYLE_CODE
		{
			set{ _style_code=value;}
			get{return _style_code;}
		}
        /// <summary>
        /// 
        /// </summary>
        public decimal ORI_PRICE
        {
            get { return _ori_price; }
            set { _ori_price = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal DISCOUNT_RATE
        {
            get { return _discount_rate; }
            set { _discount_rate = value; }
        }        
		/// <summary>
		/// 
		/// </summary>
		public string PRICE_CODE
		{
			set{ _price_code=value;}
			get{return _price_code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public decimal SALES_PRICE
		{
			set{ _sales_price=value;}
			get{return _sales_price;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int DEFAULT_FLAG
		{
			set{ _default_flag=value;}
			get{return _default_flag;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime START_DATE
		{
			set{ _start_date=value;}
			get{return _start_date;}
		}
		/// <summary>
		/// 
		/// </summary>
		public DateTime END_DATE
		{
			set{ _end_date=value;}
			get{return _end_date;}
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
		/// 属性1
		/// </summary>
		public string ATTRIBUTE1
		{
			set{ _attribute1=value;}
			get{return _attribute1;}
		}
		/// <summary>
		/// 属性2
		/// </summary>
		public string ATTRIBUTE2
		{
			set{ _attribute2=value;}
			get{return _attribute2;}
		}
		/// <summary>
		/// 属性3
		/// </summary>
		public string ATTRIBUTE3
		{
			set{ _attribute3=value;}
			get{return _attribute3;}
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
