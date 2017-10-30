using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace POS.IDAL
{
    /// <summary>
    /// 接口层IBASE_PRODUCT 
    /// </summary>
    public interface IProduct
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(string CODE);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(POS.Model.BaseProductTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(POS.Model.BaseProductTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(string CODE);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        POS.Model.BaseProductTable GetModel(string CODE);

        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);

        /// <summary>
        /// 获得数据列表
        /// </summary>
        DataSet GetList(string strWhere);

        /// <summary>
        /// 获得商品数据列表
        /// </summary>
        DataSet GetProductList(string strWhere);

        bool isDelete(string CODE);

        #endregion  成员方法
    }
}
