using System;
using System.Collections.Generic;
using System.Text;

namespace POS.Model
{
    /// <summary>
    /// 分页存储过程查询参数类
    /// </summary>
    public class QueryParam
    {

        #region "Private Variables"
        private string _TableName;
        private string _ReturnFields = "*";
        private string _Where;
        private string _Orderfld = "ID";
        private int _OrderType = 1;
        private int _PageIndex = 1;
        private int _PageSize = 20;//int.MaxValue;
        private int _IsPage = 1;
        private string _primarykey = "ID";
        #endregion

        #region "Public Variables"

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get
            {
                return _TableName;
            }
            set
            {
                _TableName = value;
            }

        }



        /// <summary>
        /// 返回字段
        /// </summary>
        public string ReturnFields
        {
            get
            {
                return _ReturnFields;
            }
            set
            {
                _ReturnFields = value;
            }
        }




        /// <summary>
        /// 查询条件 需带Where
        /// </summary>
        public string Where
        {
            get
            {
                return _Where;
            }
            set
            {
                _Where = value;
            }
        }





        /// <summary>
        /// 排序字段
        /// </summary>
        public string Orderfld
        {
            get
            {
                return _Orderfld;
            }
            set
            {
                _Orderfld = value;
            }
        }


        /// <summary>
        /// 排序类型 1:降序 其它为升序
        /// </summary>
        public int OrderType
        {
            get
            {
                return _OrderType;
            }
            set
            {
                _OrderType = value;
            }
        }


        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex
        {
            get
            {
                return _PageIndex;
            }
            set
            {
                _PageIndex = value;
            }

        }


        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize
        {
            get
            {
                return _PageSize;
            }
            set
            {
                _PageSize = value;
            }
        }

        /// <summary>
        /// 是否分页
        /// </summary>
        public int IsPage
        {
            get { return _IsPage; }
            set { _IsPage = value; }
        }

        /// <summary>
        /// 主键
        /// </summary>
        public string PrimaryKey
        {
            get { return _primarykey; }
            set { _primarykey = value; }
        }
        #endregion
    }
}
