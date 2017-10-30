using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
   public interface IStyle
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseStyleTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseStyleTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseStyleTable GetModel(string CODE);

        DataSet GetList(string strWhere);

        bool isDelete(string CODE);
        #endregion  成员方法
    }
}
