using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using System.Data;

namespace POS.IDAL
{
    public interface IProductPrice
    {
        #region  成员方法
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        bool Exists(decimal ID);
        /// <summary>
        /// 增加一条数据
        /// </summary>
        int Add(BaseProductPriceTable model);
        /// <summary>
        /// 更新一条数据
        /// </summary>
        bool Update(BaseProductPriceTable model);
        /// <summary>
        /// 删除一条数据
        /// </summary>
        bool Delete(decimal ID);
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        BaseProductPriceTable GetModel(decimal ID);

        /// <summary>
        /// 
        /// </summary>
        DataSet getSalesPrice(string styleCode, string departmentCode);

        bool isDelete(decimal ID);

        #endregion  成员方法
    }
}
