using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SCM.Model
{
    [Serializable]
   public class RolesTable
    {
        public RolesTable() { }
        private string _value;       
        private string _text;
        private bool _initStatus = false;
        private bool _isCheck = false;
        private List<RolesTable> _rolesList = new List<RolesTable>();
        
        /// <summary>
        /// 
        /// </summary>
        public string VALUE
        {
            get { return _value; }
            set { _value = value; }
        }        

        /// <summary>
        /// 
        /// </summary>
        public bool INIT_STATUS
        {
            get { return _initStatus; }
            set { _initStatus = value; }
        }       

        /// <summary>
        /// 
        /// </summary>
        public string TEXT
        {
            get { return _text; }
            set { _text = value; }
        }      

        /// <summary>
        /// 
        /// </summary>
        public bool IS_CHECK
        {
            get { return _isCheck; }
            set { _isCheck = value; }
        }

       
        /// <summary>
        /// 
        /// </summary>
        public List<RolesTable> ROLE_LIST
        {
            get { return _rolesList; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="roleList"></param>
        public void AddRoleList(RolesTable roleList)
        {
            _rolesList.Add(roleList);
        }

    }
}
