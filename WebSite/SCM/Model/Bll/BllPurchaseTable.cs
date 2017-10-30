using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public class BllPurchaseTable
    {
        public BllPurchaseTable()
		{}
		#region Model
		private string _slip_number;
		private int _input_type;
		private string _supplier_code;
		private string _warehouse_code;
		private DateTime _purchase_date;
		private int _status_flag;
		private string _attribute1;
		private string _attribute2;
		private string _attribute3;
		private string _create_user;
		private DateTime _create_date_time;
		private string _last_update_user;
		private DateTime _last_update_time;
        private List<BllPurchaseLineTable> _purchaseLine = new List<BllPurchaseLineTable>();
        private string _parent_slip_number;
        private int _parent_line_number;
        private bool _chk_item;
        

        /// <summary>
        /// 
        /// </summary>
        public List<BllPurchaseLineTable> PURCHASE_LINE
        {
            get { return _purchaseLine; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddPurchaseLine(BllPurchaseLineTable model)
        {
            _purchaseLine.Add(model);
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
		public int INPUT_TYPE
		{
			set{ _input_type=value;}
			get{return _input_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string SUPPLIER_CODE
		{
			set{ _supplier_code=value;}
			get{return _supplier_code;}
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
		public DateTime PURCHASE_DATE
		{
			set{ _purchase_date=value;}
			get{return _purchase_date;}
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
		public string ATTRIBUTE1
		{
			set{ _attribute1=value;}
			get{return _attribute1;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ATTRIBUTE2
		{
			set{ _attribute2=value;}
			get{return _attribute2;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string ATTRIBUTE3
		{
			set{ _attribute3=value;}
			get{return _attribute3;}
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
        /// <summary>
        /// 
        /// </summary>
        public string PARENT_SLIP_NUMBER
        {
            get { return _parent_slip_number; }
            set { _parent_slip_number = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int PARENT_LINE_NUMBER
        {
            get { return _parent_line_number; }
            set { _parent_line_number = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public bool CHK_ITEM
        {
            get { return _chk_item; }
            set { _chk_item = value; }
        }
		#endregion Model
    }

    public class BllPurchaseLineTable
    {
        public BllPurchaseLineTable()
        { }

        #region Model
        private string _slip_number;
        private int _line_number;
        private DateTime _arrival_date;
        private string _product_code;
        private string _unit_code;
        private decimal _quantity;
        private decimal _price;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        /// <summary>
        /// 
        /// </summary>
        public string SLIP_NUMBER
        {
            set { _slip_number = value; }
            get { return _slip_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int LINE_NUMBER
        {
            set { _line_number = value; }
            get { return _line_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ARRIVAL_DATE
        {
            set { _arrival_date = value; }
            get { return _arrival_date; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRODUCT_CODE
        {
            set { _product_code = value; }
            get { return _product_code; }
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
        public decimal QUANTITY
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PRICE
        {
            set { _price = value; }
            get { return _price; }
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
        #endregion Model
    }
}
