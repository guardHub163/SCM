using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCM.Model;

namespace SCM.IDAL
{

        /// <summary>
        /// 接口层IBASE_PARAMETER 
        /// </summary>
    public interface ISarParameter
        {
            #region  成员方法
            /// <summary>
            /// 增加一条数据
            /// </summary>
            int Add(SCM.Model.SarParameterTable model);
            /// <summary>
            /// 更新一条数据
            /// </summary>
            int Update(List<SarParameterTable> sartablelist);

            DataSet GetAllParameterInfo();

            DataSet GetParameterNumber(string code);
            #endregion  成员方法
        }
}
