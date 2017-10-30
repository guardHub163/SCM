using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
using System.Data;

namespace SCM.Bll
{
   public partial class BSarParameter
    {
       private readonly ISarParameter dal = DataAccess.CreateSarParameterManage();
           /// <summary>
        /// 增加一条数据
        /// </summary>
       public int Add(SarParameterTable model) 
       {
           return dal.Add(model);
       }

         /// <summary>
        /// 更新一条数据
        /// </summary>
       public int Update(List<SarParameterTable> sartablelist)
       {
           return dal.Update(sartablelist);
       }

        ///<summary>
        ///获得所有的参数信息
        ///</summary>
       public DataSet GetAllParameterInfo() 
       {
           return dal.GetAllParameterInfo();
       }

       ///<summary>
       ///根据条件获得参数
       ///</summary>
       public DataSet GetParameterNumber(string code) 
       {
           return dal.GetParameterNumber(code);
       }
    }
}
