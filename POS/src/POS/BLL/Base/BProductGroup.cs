using System;
using System.Data;
using System.Collections.Generic;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;

namespace POS.Bll
{
    public partial class BProductGroup
    {
        private readonly IProductGroup dal = DataAccess.CreateProductGroupManage();
        public BProductGroup()
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
        public int Add(BaseProductGroupTable model)
        {
           return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseProductGroupTable model)
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
        public BaseProductGroupTable GetModel(string CODE)
        {

            return dal.GetModel(CODE);
        }

                /// <summary>
        /// 获得数据列表
        /// </summary>
        public DataSet GetList(string strWhere)
        {
            return dal.GetList(strWhere);
        }

        #endregion  Method
    }
}
