using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public class BllShipmentTable
    {
        public BllShipmentTable()
		{}
		#region Model
        private string _slip_number;
        private int _shipment_type;        
		private string _from_warehouse_code;
		private DateTime _departual_date;
		private DateTime _arrival_date;
		private string _to_warehouse_code;
		private int _status_flag;
		private string _attribute1;
		private string _attribute2;
		private string _attribute3;
		private string _create_user;
		private DateTime _create_date_time;
		private string _last_update_user;
		private DateTime _last_update_time;
        private string _from_warehouse_name;
        private string _to_warehouse_name;        
        private List<BllShipmentLineTable> _shipmentLine = new List<BllShipmentLineTable>();

		/// <summary>
		/// -1
		/// </summary>
		public string SLIP_NUMBER
		{
			set{ _slip_number=value;}
			get{return _slip_number;}
		}
        /// <summary>
        /// 
        /// </summary>
        public int SHIPMENT_TYPE
        {
            get { return _shipment_type; }
            set { _shipment_type = value; }
        }
		/// <summary>
		/// -1
		/// </summary>
		public string FROM_WAREHOUSE_CODE
		{
			set{ _from_warehouse_code=value;}
			get{return _from_warehouse_code;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public DateTime DEPARTUAL_DATE
		{
			set{ _departual_date=value;}
			get{return _departual_date;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public DateTime ARRIVAL_DATE
		{
			set{ _arrival_date=value;}
			get{return _arrival_date;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public string TO_WAREHOUSE_CODE
		{
			set{ _to_warehouse_code=value;}
			get{return _to_warehouse_code;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public int STATUS_FLAG
		{
			set{ _status_flag=value;}
			get{return _status_flag;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public string ATTRIBUTE1
		{
			set{ _attribute1=value;}
			get{return _attribute1;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public string ATTRIBUTE2
		{
			set{ _attribute2=value;}
			get{return _attribute2;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public string ATTRIBUTE3
		{
			set{ _attribute3=value;}
			get{return _attribute3;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public string CREATE_USER
		{
			set{ _create_user=value;}
			get{return _create_user;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public DateTime CREATE_DATE_TIME
		{
			set{ _create_date_time=value;}
			get{return _create_date_time;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public string LAST_UPDATE_USER
		{
			set{ _last_update_user=value;}
			get{return _last_update_user;}
		}
		/// <summary>
		/// -1
		/// </summary>
		public DateTime LAST_UPDATE_TIME
		{
			set{ _last_update_time=value;}
			get{return _last_update_time;}
		}

        /// <summary>
        /// 
        /// </summary>
        public List<BllShipmentLineTable> SHIPMENT_LINE
        {
            get { return _shipmentLine; }            
        }

        /// <summary>
        /// 
        /// </summary>
        public void AddShipmentLine(BllShipmentLineTable model)
        {
            _shipmentLine.Add(model);
        }

        public string FROM_WAREHOUSE_NAME
        {
            get { return _from_warehouse_name; }
            set { _from_warehouse_name = value; }
        }


        public string TO_WAREHOUSE_NAME
        {
            get { return _to_warehouse_name; }
            set { _to_warehouse_name = value; }
        }
		#endregion Model
    }



    public class BllShipmentLineTable
    {
        public BllShipmentLineTable()
        { }
        #region Model
        private string _slip_number;
        private int _line_number;
        private decimal _shipment_plan_slip_number;
        private string _product_code;
        private string _unit_code;
        private decimal _quantity;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        /// <summary>
        /// -1
        /// </summary>
        public string SLIP_NUMBER
        {
            set { _slip_number = value; }
            get { return _slip_number; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public int LINE_NUMBER
        {
            set { _line_number = value; }
            get { return _line_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SHIPMENT_PLAN_SLIP_NUMBER
        {
            set { _shipment_plan_slip_number = value; }
            get { return _shipment_plan_slip_number; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public string PRODUCT_CODE
        {
            set { _product_code = value; }
            get { return _product_code; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public string UNIT_CODE
        {
            set { _unit_code = value; }
            get { return _unit_code; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public decimal QUANTITY
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public int STATUS_FLAG
        {
            set { _status_flag = value; }
            get { return _status_flag; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public string ATTRIBUTE1
        {
            set { _attribute1 = value; }
            get { return _attribute1; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public string ATTRIBUTE2
        {
            set { _attribute2 = value; }
            get { return _attribute2; }
        }
        /// <summary>
        /// -1
        /// </summary>
        public string ATTRIBUTE3
        {
            set { _attribute3 = value; }
            get { return _attribute3; }
        }
        #endregion Model
    }
}
