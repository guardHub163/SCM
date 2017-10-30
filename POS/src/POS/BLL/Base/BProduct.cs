using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;
using System.Data;

namespace POS.Bll
{
    public partial class BProduct
    {
        private readonly IProduct dal = DataAccess.CreateProductManage();
        public BProduct()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string CODE)
        {
            return dal.Exists(CODE);
        }

        public bool isDelete(string CODE)
        {
            return dal.isDelete(CODE);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseProductTable model)
        {
           return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseProductTable model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string CODE)
        {

            return dal.Delete(CODE);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public POS.Model.BaseProductTable GetModel(string CODE)
        {

            return dal.GetModel(CODE);
        }        

        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public int GetRecordCount(string strWhere)
        {
            return dal.GetRecordCount(strWhere);
        }
        /// <summary>
        /// 分页获取数据列表
        /// </summary>
        public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)
        {
            return dal.GetListByPage(strWhere, orderby, startIndex, endIndex);
        }

        /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        /// <summary>
        /// 获得商品数据列表
        /// </summary>
        public DataSet GetProductList(string strWhere)
        {
            return dal.GetProductList(strWhere);
        }
        #endregion  Method
    }
}
