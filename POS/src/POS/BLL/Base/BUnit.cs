using System;
using System.Data;
using System.Collections.Generic;
using POS.Common;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;

namespace POS.Bll
{
    public partial class BUnit
    {
        private readonly IUnit dal = DataAccess.CreateUnitManage();
        public BUnit()
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
        public int Add(BaseUnitTable model)
        {
           return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseUnitTable model)
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
        public BaseUnitTable GetModel(string CODE)
        {

            return dal.GetModel(CODE);
        }


        #endregion  Method
    }
}
