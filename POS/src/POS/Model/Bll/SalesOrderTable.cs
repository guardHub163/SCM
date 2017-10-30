using System;
namespace POS.Model
{
    /// <summary>
    /// BllSalesOrderTable:实体类(属性说明自动提取数据库字段的描述信息)
    /// </summary>
    [Serializable]
    public class SalesOrderTable
    {
        public SalesOrderTable()
        { }
        #region Model
        private string _slip_number;
        private string _department_code;
        private string _sales_employee;
        private string _customer_code;
        private string _product_code;
        private int _line_number;
        private string _attribute1;
        private string _attribute2;
        private string _attribute3;
        private decimal _discount_rate;
        private decimal _ori_price;
        private decimal _price;
        private decimal _quantity;
        private string _unit_code;
        private decimal _amount;
        private int _points;
        private int _used_points;
        private int _status_flag;
        private int _send_flag;
        private string _memo;
        private DateTime _create_date_time;
        private string _create_user;
        private DateTime _last_update_time;
        private string _last_update_user;
        private string _name;
        private string _group_code;
        private string _style;
        private string _color;
        private string _size;
        private string _color_name;
        private string _style_name;
        private decimal _cash_amount;
        private decimal _bank_amount;
        private decimal _change;
        private decimal _promotion_discounts;
        private decimal _promotion_price;
        /// <summary>
        /// 
        /// </summary>
        public decimal PROMOTION_DISCOUNTS
        {
            get { return _promotion_discounts; }
            set { _promotion_discounts = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal PROMOTION_AMOUNT
        {
            get { return _promotion_price; }
            set { _promotion_price = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal BANK_AMOUNT
        {
            get { return _bank_amount; }
            set { _bank_amount = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CHANGE
        {
            get { return _change; }
            set { _change = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal CASH_AMOUNT
        {
            get { return _cash_amount; }
            set { _cash_amount = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string STYLE_NAME
        {
            get { return _style_name; }
            set { _style_name = value; }
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
        public string DEPARTMENT_CODE
        {
            set { _department_code = value; }
            get { return _department_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SALES_EMPLOYEE
        {
            set { _sales_employee = value; }
            get { return _sales_employee; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CUSTOMER_CODE
        {
            set { _customer_code = value; }
            get { return _customer_code; }
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
        public int LINE_NUMBER
        {
            set { _line_number = value; }
            get { return _line_number; }
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
        public decimal DISCOUNT_RATE
        {
            set { _discount_rate = value; }
            get { return _discount_rate; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal ORI_PRICE
        {
            set { _ori_price = value; }
            get { return _ori_price; }
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
        public decimal QUANTITY
        {
            set { _quantity = value; }
            get { return _quantity; }
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
        public decimal AMOUNT
        {
            set { _amount = value; }
            get { return _amount; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int POINTS
        {
            set { _points = value; }
            get { return _points; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int USED_POINTS
        {
            set { _used_points = value; }
            get { return _used_points; }
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
        public int SEND_FLAG
        {
            set { _send_flag = value; }
            get { return _send_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string MEMO
        {
            set { _memo = value; }
            get { return _memo; }
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
        public string CREATE_USER
        {
            set { _create_user = value; }
            get { return _create_user; }
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
        public string LAST_UPDATE_USER
        {
            set { _last_update_user = value; }
            get { return _last_update_user; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NAME
        {
            set { _name = value; }
            get { return _name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string GROUP_CODE
        {
            set { _group_code = value; }
            get { return _group_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STYLE
        {
            set { _style = value; }
            get { return _style; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COLOR
        {
            set { _color = value; }
            get { return _color; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string SIZE
        {
            set { _size = value; }
            get { return _size; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string COLOR_NAME
        {
            set { _color_name = value; }
            get { return _color_name; }
        }
        #endregion Model

    }
}

