using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public class SarParameterTable
    {
        public SarParameterTable()
		{}
        #region Model
        private string _code_type;
        private string _code;
        private string _name;
        private int _status_flag;
        private string _number1;
        private string _number2;
        private string _number3;
        private string _number4;
        private string _number5;
        /// <summary>
        /// 
        /// </summary>
        public string CODE_TYPE
        {
            set { _code_type = value; }
            get { return _code_type; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string CODE
        {
            set { _code = value; }
            get { return _code; }
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
        public int STATUS_FLAG
        {
            set { _status_flag = value; }
            get { return _status_flag; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NUMBER1
        {
            set { _number1 = value; }
            get { return _number1; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NUMBER2
        {
            set { _number2 = value; }
            get { return _number2; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NUMBER3
        {
            set { _number3 = value; }
            get { return _number3; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NUMBER4
        {
            set { _number4 = value; }
            get { return _number4; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string NUMBER5
        {
            set { _number5 = value; }
            get { return _number5; }
        }
        #endregion Model
    }
}
