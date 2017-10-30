using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
   public interface INews
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(decimal ID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseNewsTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseNewsTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(decimal ID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseNewsTable GetModel(decimal ID);

        int GetNewsCount(string strWhere);
        DataSet GetNewsListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        DataSet NewsInfo();

        DataSet UserPhoto(string username);

        DataSet NewsSystemInfo();
        #endregion  成员方法
    }
}
