using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.DALFactory;
using SCM.IDAL;
using SCM.Model;
using System.Data;

namespace SCM.Bll
{
    public class BProductItem
    {
        private readonly IProductItem dal = DataAccess.CreateProductItemManage();
        public BProductItem()
        { }

        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string poductCode, string itemCode)
        {
            return dal.Exists(poductCode, itemCode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseProductItemTable model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseProductItemTable model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string PRODUCT_CODE, string ITEM_CODE)
        {

            return dal.Delete(PRODUCT_CODE, ITEM_CODE);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BaseProductItemTable GetModel(string PRODUCT_CODE, string ITEM_CODE)
        {

            return dal.GetModel(PRODUCT_CODE, ITEM_CODE);
        }

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetCount(string strWhere)
        {
            return dal.GetCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetList(strWhere, orderby, startIndex, endIndex);
        }


        #endregion  Method
    }
}
