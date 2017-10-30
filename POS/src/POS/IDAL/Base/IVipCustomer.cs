using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
    public interface IVipCustomer
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseVipCustomerTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseVipCustomerTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseVipCustomerTable GetModel(string CODE);

        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        DataSet GetDepartmetnCode();

        DataSet GetAllInfo(string strWhere);

        bool UpdateFlag(int status_flag, string Code);

        DataSet GetVipInfo(string strWhere);

        bool isDelete(string CODE);

        int ToAdd(BaseVipCustomerTable model);

        bool ToUpdate(BaseVipCustomerTable model);
        #endregion  成员方法
    }
}
