using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
    public interface ISize
    {

        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string code, string groupCode);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseSizeTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseSizeTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string code, string groupCode);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseSizeTable GetModel(string code, string groupCode);

        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);


        DataSet GetSizeByGroupCode(string groupCode);

        #endregion  成员方法

    }
}
