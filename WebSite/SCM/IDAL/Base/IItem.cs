using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
    public interface IItem
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseItemTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseItemTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseItemTable GetModel(string CODE);

        int GetProductItemCount(string strWhere);
        DataSet GetProductItemList(string strWhere, string orderby, int startIndex, int endIndex);
        #endregion  成员方法
    }
}
