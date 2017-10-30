using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
   public interface IProductGroup
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseProductGroupTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseProductGroupTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseProductGroupTable GetModel(string CODE);
               /// <summary>
        /// 获得数据列表
        /// </summary>
       DataSet GetList(string strWhere);

       bool isDelete(string CODE);

        #endregion  成员方法
    }
}
