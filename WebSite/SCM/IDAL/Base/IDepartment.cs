using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace SCM.IDAL
{
    public interface IDepartment
    {
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(SCM.Model.BaseDepartmentTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(SCM.Model.BaseDepartmentTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        SCM.Model.BaseDepartmentTable GetModel(string CODE);

        DataSet GetDepartmentInfo();

        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
    }
}
