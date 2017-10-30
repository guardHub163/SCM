using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public partial class BllPurchaseRequisitionTable
    {
        public BllPurchaseRequisitionTable()
        { }
        #region Model
        private string _slip_number;
        private string _from_warehouse_code;
        private string _to_warehouse_code;
        private DateTime _departual_date;
        private DateTime _arrival_date;
        private string _product_group_code;
        private string _requisition_period;
        private decimal _group_stock;
        private decimal _area_percentage;
        private decimal _area_max_quantity;
        private decimal _shop_percentage;
        private decimal _shop_max_quantity;
        private int _status_flag;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private string _create_user;
        private DateTime _create_date_time;
        private string _last_update_user;
        private DateTime _last_update_time;
        private string _status_name;
        private string _from_warehouse_name;
        private string _to_warehouse_name;
        private string _product_group_name;
        private string _user_name;
        private decimal _apply_quantity; //申请数量
        private decimal _auditing_quantity;//审核数量

        private List<BllPurchaseRequisitionLineTable> _lines = new List<BllPurchaseRequisitionLineTable>();

        /// <summary>
        /// 
        /// </summary>
        public decimal Auditing_quantity
        {
            get { return _auditing_quantity; }
            set { _auditing_quantity = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal Apply_quantity
        {
            get { return _apply_quantity; }
            set { _apply_quantity = value; }
        }     
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
        public string FROM_WAREHOUSE_CODE
        {
            set { _from_warehouse_code = value; }
            get { return _from_warehouse_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TO_WAREHOUSE_CODE
        {
            set { _to_warehouse_code = value; }
            get { return _to_warehouse_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime DEPARTUAL_DATE
        {
            set { _departual_date = value; }
            get { return _departual_date; }
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
        public string PRODUCT_GROUP_CODE
        {
            set { _product_group_code = value; }
            get { return _product_group_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string REQUISITION_PERIOD
        {
            set { _requisition_period = value; }
            get { return _requisition_period; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal GROUP_STOCK
        {
            set { _group_stock = value; }
            get { return _group_stock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AREA_PERCENTAGE
        {
            set { _area_percentage = value; }
            get { return _area_percentage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AREA_MAX_QUANTITY
        {
            set { _area_max_quantity = value; }
            get { return _area_max_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SHOP_PERCENTAGE
        {
            set { _shop_percentage = value; }
            get { return _shop_percentage; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SHOP_MAX_QUANTITY
        {
            set { _shop_max_quantity = value; }
            get { return _shop_max_quantity; }
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
        /// <summary>
        /// 
        /// </summary>
        public string CREATE_USER
        {
            set { _create_user = value; }
            get { return _create_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime CREATE_DATE_TIME
        {
            set { _create_date_time = value; }
            get { return _create_date_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string LAST_UPDATE_USER
        {
            set { _last_update_user = value; }
            get { return _last_update_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public DateTime LAST_UPDATE_TIME
        {
            set { _last_update_time = value; }
            get { return _last_update_time; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STATUS_NAME
        {
            set { _status_name = value; }
            get { return _status_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string FROM_WAREHOUSE_NAME
        {
            set { _from_warehouse_name = value; }
            get { return _from_warehouse_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string TO_WAREHOUSE_NAME
        {
            set { _to_warehouse_name = value; }
            get { return _to_warehouse_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRODUCT_GROUP_NAME
        {
            set { _product_group_name = value; }
            get { return _product_group_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string USER_NAME
        {
            set { _user_name = value; }
            get { return _user_name; }
        }

        /// <summary>
        /// 
        /// </summary>
        public List<BllPurchaseRequisitionLineTable> LINES
        {
            get { return _lines; }
            set { _lines = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public void ADD_LINES(BllPurchaseRequisitionLineTable line)
        {
            _lines.Add(line);
        }
        #endregion Model

    }



    public partial class BllPurchaseRequisitionLineTable
    {
        public BllPurchaseRequisitionLineTable()
        { }

        #region Model
        private string _slip_number;
        private int _line_number;
        private decimal _transfer_order_id;
        private int _transfer_order_line_numer;
        private string _product_code;
        private string _unit_code;
        private decimal _requistion_quantity;   //申请数量
        private decimal _confirm_quantity;  //审核数量
        private decimal _shop_stock;     //当前门店库存
        private decimal _warehouse_stock;   //补货仓库库存        
        private decimal _before_sales_quantity; //到货前销量
        private decimal _after_sales_quantity;  //到货后销量
        private int _box_number;    //箱数
        private string _product_name;
        private string _style_name;
        private string _color_name;
        private string _size_name;
        private decimal _quantity;

        public decimal Quantity
        {
            get { return _quantity; }
            set { _quantity = value; }
        }
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
        public decimal TRANSFER_ORDER_ID
        {
            set { _transfer_order_id = value; }
            get { return _transfer_order_id; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int TRANSFER_ORDER_LINE_NUMER
        {
            set { _transfer_order_line_numer = value; }
            get { return _transfer_order_line_numer; }
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
        public decimal REQUISTION_QUANTITY
        {
            set { _requistion_quantity = value; }
            get { return _requistion_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CONFIRM_QUANTITY
        {
            set { _confirm_quantity = value; }
            get { return _confirm_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SHOP_STOCK
        {
            set { _shop_stock = value; }
            get { return _shop_stock; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal WAREHOUSE_STOCK
        {
            get { return _warehouse_stock; }
            set { _warehouse_stock = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal BEFORE_SALES_QUANTITY
        {
            set { _before_sales_quantity = value; }
            get { return _before_sales_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal AFTER_SALES_QUANTITY
        {
            set { _after_sales_quantity = value; }
            get { return _after_sales_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int BOX_NUMBER
        {
            set { _box_number = value; }
            get { return _box_number; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string PRODUCT_NAME
        {
            set { _product_name = value; }
            get { return _product_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STYLE_NAME
        {
            set { _style_name = value; }
            get { return _style_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COLOR_NAME
        {
            set { _color_name = value; }
            get { return _color_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SIZE_NAME
        {
            set { _size_name = value; }
            get { return _size_name; }
        }
        #endregion Model
    }
}
