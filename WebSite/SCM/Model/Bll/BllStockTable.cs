using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public partial class BllStockTable
    {
        public BllStockTable()
        { }
        #region Model
        private string _warehouse_code;
        private string _product_code;
        private decimal _quantity;
        private decimal _sp_quantity;
        private decimal _rp_quantity;
        private string _warehouse_name;
        private string _product_name;
        private string _product_group_code;
        private string _product_group_name;
        private string _unit_code;
        private string _unit_name;
        private string _style_code;
        private string _style_name;
        private string _color_code;
        private string _color_name;
        private string _size_code;
        private string _size_name;
        private string _reason;
        private decimal _toquantity;
        private decimal _lastquantity;
        private string creat_name;

        public string Creat_name
        {
            get { return creat_name; }
            set { creat_name = value; }
        }

        public decimal Lastquantity
        {
            get { return _lastquantity; }
            set { _lastquantity = value; }
        }

        public decimal Toquantity
        {
            get { return _toquantity; }
            set { _toquantity = value; }
        }

        public string Reason
        {
            get { return _reason; }
            set { _reason = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WAREHOUSE_CODE
        {
            set { _warehouse_code = value; }
            get { return _warehouse_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        
        ///  /// <summary>
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
        public decimal QUANTITY
        {
            set { _quantity = value; }
            get { return _quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal SP_QUANTITY
        {
            set { _sp_quantity = value; }
            get { return _sp_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public decimal RP_QUANTITY
        {
            set { _rp_quantity = value; }
            get { return _rp_quantity; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string WAREHOUSE_NAME
        {
            set { _warehouse_name = value; }
            get { return _warehouse_name; }
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
        public string PRODUCT_GROUP_CODE
        {
            set { _product_group_code = value; }
            get { return _product_group_code; }
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
        public string UNIT_CODE
        {
            set { _unit_code = value; }
            get { return _unit_code; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string UNIT_NAME
        {
            set { _unit_name = value; }
            get { return _unit_name; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string STYLE_CODE
        {
            set { _style_code = value; }
            get { return _style_code; }
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
        public string COLOR_CODE
        {
            set { _color_code = value; }
            get { return _color_code; }
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
        public string SIZE_CODE
        {
            set { _size_code = value; }
            get { return _size_code; }
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
