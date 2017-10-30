using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Common
{
    public class CConvert
    {
        #region ToDecimal
        /// <summary>
        /// ToDecimal
        /// </summary>
        public static decimal ToDecimal(object obj)
        {
            decimal number = 0;
            if (obj != null)
            {
                try
                {
                    number = decimal.Parse(obj.ToString());
                }
                catch { }
            }
            return number;
        }
        #endregion

        #region ToDouble
        /// <summary>
        /// ToDouble
        /// </summary>
        public static double ToDouble(object obj)
        {
            double number = 0;
            if (obj != null)
            {
                try
                {
                    number = double.Parse(obj.ToString());
                }
                catch { }
            }
            return number;
        }
        #endregion

        #region ToFloat
        /// <summary>
        /// ToFloat
        /// </summary>
        public static float ToFloat(object obj)
        {
            float number = 0;
            if (obj != null)
            {
                try
                {
                    number = float.Parse(obj.ToString());
                }
                catch { }
            }
            return number;
        }
        #endregion

        #region ToString
        /// <summary>
        /// ToString
        /// </summary>
        public static string ToString(object obj)
        {
            if (obj == null)
            {
                return "";
            }
            return obj.ToString();
        }

        /// <summary>
        /// 返回指定长度的字符串，不足是原样返回
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="lenght"></param>
        /// <returns></returns>
        public static string ToString(object obj, int lenght)
        {
            if (obj == null)
            {
                return "";
            }
            string str = obj.ToString();
            if (str.Length > lenght)
            {
                str.Substring(0, lenght);
            }
            return str;
        }
        #endregion

        #region ToInt32
        public static int ToInt32(object obj)
        {
            int number = 0;
            if (obj != null)
            {
                try
                {
                    number = Int32.Parse(obj.ToString());
                }
                catch { }
            }

            return number;
        }
        #endregion

        #region DateTimeToString
        /// <summary>
        /// DateTimeToString
        /// </summary>
        public static string DateTimeToString(object obj, string formatStr)
        {
            string ret = "";
            if (obj != null && !"".Equals(obj))
            {
                try
                {
                    ret = DateTime.Parse(obj.ToString()).ToString(formatStr);
                }
                catch { }
            }
            return ret;
        }
        #endregion

        #region ToDateTime
        /// <summary>
        /// ToDateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object obj)
        {
            DateTime dt = DateTime.Now;
            if (obj != null)
            {
                try
                {
                    dt = DateTime.Parse(obj.ToString());
                }
                catch { }
            }
            return dt;
        }
        #endregion

        #region ToDateTime
        /// <summary>
        /// ToDateTime
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool ToBoolean(object obj)
        {
            bool b = false;
            if (obj != null)
            {
                try
                {
                    b = Boolean.Parse(obj.ToString());
                }
                catch { }
            }
            return b;
        }
        #endregion
    }
}
