using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    public class BaseMaster
    {
        public BaseMaster() { }

        private string _code;        
        private string _name;

        /// <summary>
        /// 
        /// </summary>
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
    }
}
