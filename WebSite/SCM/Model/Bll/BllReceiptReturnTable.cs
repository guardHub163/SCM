using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public class BllReceiptReturnTable
    {
        public BllReceiptReturnTable()
		{}
		#region Model
		private string _slip_number;
		private string _reciept_slip_number;
		private int _reciept_line_number;
        private string _return_reason;
		private string _supplier_code;
		private string _return_warehouse_code;
		private string _product_code;
		private string _unit_code;
		private decimal _quantity;
		private int _status_flag;
		private string _attribute1;
		private string _attribute2;
		private string _attribute3;
        private int _input_type;
       
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
		public string RECIEPT_SLIP_NUMBER
		{
			set{ _reciept_slip_number=value;}
			get{return _reciept_slip_number;}
		}

		/// <summary>
		/// 
		/// </summary>
		public int RECIEPT_LINE_NUMBER
		{
			set{ _reciept_line_number=value;}
			get{return _reciept_line_number;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string RETURN_REASON
		{
			set{ _return_reason=value;}
			get{return _return_reason;}
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
		public string RETURN_WAREHOUSE_CODE
		{
			set{ _return_warehouse_code=value;}
			get{return _return_warehouse_code;}
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
		public string UNIT_CODE
		{
			set{ _unit_code=value;}
			get{return _unit_code;}
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
        public int INPUT_TYPE
        {
            get { return _input_type; }
            set { _input_type = value; }
        }
		#endregion Model
    }//end class
}
