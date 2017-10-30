using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Model
{
   public partial class NamesTable
    {
        public NamesTable()
		{}

        public NamesTable(string code_type,string code,string name,int status_flag)
        {
            _code_type = code_type;
            _code = code;
            _name = name;
            _status_flag = status_flag;
        }
		#region Model
		private string _code_type;
		private string _code;
		private string _name;
		private int _status_flag;
		/// <summary>
		/// 
		/// </summary>
		public string CODE_TYPE
		{
			set{ _code_type=value;}
			get{return _code_type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string CODE
		{
			set{ _code=value;}
			get{return _code;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string NAME
		{
			set{ _name=value;}
			get{return _name;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int STATUS_FLAG
		{
			set{ _status_flag=value;}
			get{return _status_flag;}
		}
		#endregion Model
    }
}
