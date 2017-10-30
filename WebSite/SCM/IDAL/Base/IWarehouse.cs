using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SCM.IDAL
{
    public interface IWarehouse
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(SCM.Model.BaseWarehouseTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(SCM.Model.BaseWarehouseTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        SCM.Model.BaseWarehouseTable GetModel(string CODE);

        /// <summary>
        /// 分页获取数据记录总数
        /// </summary>
        int GetRecordCount(string strWhere);
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 根据部门得到一个对象实体
        /// </summary>
        SCM.Model.BaseWarehouseTable GetModelByDepartment(string departmentCode);
        #endregion  成员方法
    }
}
