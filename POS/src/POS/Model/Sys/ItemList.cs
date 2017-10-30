using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace POS.Model
{
    public class ItemList
    {
        private string _value;
        private string _text;

        public ItemList() 
        {
        
        }

        public ItemList(string value, string text)
        {
            this._value = value;
            this._text = text;
        }
        /// <summary>
        /// 
        /// </summary>
        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        public override string ToString()
        {
            return _text;
        }
    }
}
