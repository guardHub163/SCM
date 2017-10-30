using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
    public interface ISalesPromotion
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseSalesPromotionTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseSalesPromotionTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);

        POS.Model.BaseSalesPromotionTable GetModel(string CODE);

        int GetPromotionCount(string strWhere);

        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        bool isDelete(string CODE);

        #endregion  成员方法
    }
}
