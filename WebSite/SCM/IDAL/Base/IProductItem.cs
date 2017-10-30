using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using SCM.Model;

namespace SCM.IDAL
{
    public interface IProductItem
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string poductCode, string itemCode);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseProductItemTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseProductItemTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string PRODUCT_CODE, string ITEM_CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseProductItemTable GetModel(string PRODUCT_CODE, string ITEM_CODE);

        int GetCount(string strWhere);
        DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex);
        #endregion  成员方法
    }
}
