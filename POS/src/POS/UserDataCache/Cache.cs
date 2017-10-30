using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using POS.Bll;
using System.Collections;

namespace UserCache
{
    public class Cache
    {
        private static BCommon bCommon = new BCommon();
        private static DataSet _bll_style = null;
        private static DataSet _bank = null;
        private static DataSet _last_update = null;
        private static DataSet _promotioninfo = null;
        private static string _department_code = "";
        private static string _prnPort = "";
        private static string _screenPort = "";
        private static Hashtable _printHt = new Hashtable();

        /// <summary>
        /// SLIP_NUMBER 的所有规则的取得
        /// </summary>
        public static DataSet BLL_STYLE
        {
            get
            {
                if (_bll_style == null)
                {
                    try
                    {
                        _bll_style = bCommon.GetNames("BLL_STYLE");
                    }
                    catch { }
                }
                return _bll_style;
            }
            set { _bll_style = value; }
        }

        /// <summary>
        /// 单个SLIP_NUMBER规则的取得
        /// </summary>
        public static string GetBllStyleName(string code)
        {
            try
            {
                foreach (DataRow row in Cache.BLL_STYLE.Tables[0].Rows)
                {
                    if (code.Equals(row["CODE"]))
                    {
                        return Convert.ToString(row["NAME"]);
                    }
                }
            }
            catch { }
            return "";
        }

        /// <summary>
        /// 部门CODE的取得
        /// </summary>
        public static string DEPARTMENT_CODE
        {
            get
            {
                if (_department_code == "")
                {
                    try
                    {
                        _department_code = Convert.ToString(bCommon.GetNames("DEPARTMENT_CODE").Tables[0].Rows[0]["CODE"]);
                    }
                    catch { }
                }
                return _department_code;
            }
            set { _department_code = value; }
        }

        /// <summary>
        /// 银行信息的取得
        /// </summary>
        public static DataSet BANK
        {
            get
            {
                if (_bank == null)
                {
                    try
                    {
                        _bank = bCommon.GetNames("BANK");
                    }
                    catch { }
                }
                return _bank;
            }
            set { _bank = value; }
        }

        /// <summary>
        /// 所有基础信息表最后同步时间取得
        /// </summary>
        public static DataSet LAST_UPDATE
        {
            get
            {
                if (_last_update == null)
                {
                    _last_update = bCommon.GetNames("LAST_UPDATE");
                }
                return _last_update;
            }
            set { _last_update = value; }
        }

        /// <summary>
        /// 单张表的最后同步时间取得
        /// </summary>
        public static string GetLastUpdate(string code)
        {
            try
            {
                foreach (DataRow row in Cache.LAST_UPDATE.Tables[0].Rows)
                {
                    if (code.Equals(row["CODE"]))
                    {
                        return Convert.ToString(row["NAME"]);
                    }
                }
            }
            catch { }
            return "";
        }

        ///<summary>
        ///所有促销信息的同步取得
        ///</summary>
        public static DataSet PROMOTIONINFO
        {
            get
            {
                if (_promotioninfo == null)
                {
                    _promotioninfo = bCommon.GetPromotion();
                }
                return _promotioninfo;
            }
            set { _promotioninfo = value; }
        }

        ///<SUMMARY>
        ///单个促销信息的取得
        ///</SUMMARY>
        public static string GetPromotion(string code)
        {
            try
            {
                foreach (DataRow row in Cache.PROMOTIONINFO.Tables[0].Rows)
                {
                    if (code.Equals(row["CODE"]))
                    {
                        return Convert.ToString(row["NAME"]);
                    }
                }
            }
            catch { }
            return "";
        }

        ///<summary>
        ///单个促销价格的取得
        ///</summary>
        public static string GetPromotionPrice(string code)
        {
            try
            {
                foreach (DataRow row in Cache.PROMOTIONINFO.Tables[0].Rows)
                {
                    if (code.Equals(row["CODE"]))
                    {
                        return Convert.ToString(row["PROPERTY2"]);
                    }
                }
            }
            catch { }
            return "";
        }

        /// <summary>
        /// 打印机端口
        /// </summary>
        public static string PRN_PORT
        {
            get
            {
                if ("".Equals(_prnPort))
                {
                    _prnPort = Convert.ToString(PRINT_HT["PRINT_PORT"]);
                }
                return _prnPort;
            }
            set { Cache._prnPort = value; }

        }

        /// <summary>
        /// 顾客屏显端口
        /// </summary>
        public static string SCREEN_PORT
        {
            get
            {
                if ("".Equals(_screenPort))
                {
                    _screenPort = Convert.ToString(PRINT_HT["SCREEN_PORT"]);
                }
                return _screenPort;
            }
            set { Cache._screenPort = value; }
        }

        /// <summary>
        /// 打印小票信息
        /// </summary>
        public static Hashtable PRINT_HT
        {
            get
            {
                if (_printHt.Count < 1)
                {
                    DataSet ds = bCommon.GetNames("PRINT_PARAM");
                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        _printHt.Add(row["CODE"], row["NAME"]);
                    }
                }
                return _printHt;
            }
            set { _printHt = value; }
        }



    }//end class
}
