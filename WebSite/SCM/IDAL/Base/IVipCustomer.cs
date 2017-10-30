using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
    public interface IVipCustomer
    {
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
        /// <summary>
        /// 分页获取数据总记录数
        /// </summary
        int GetRecordCount(string strWhere);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex);
        /// <summary>
        /// 门店客户信息导入
        /// </summary>
        DataTable Insert(DataSet ds);
    }
}
