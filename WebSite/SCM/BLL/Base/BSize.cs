using System;
using System.Data;
using System.Collections.Generic;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;

namespace SCM.Bll
{
    public partial class BSize
    {
        private readonly ISize dal = DataAccess.CreateSizeManage();
        public BSize()
        { }
        #region  Method
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(string code, string groupCode)
        {
            return dal.Exists(code, groupCode);
        }

        /// <summary>
        /// 增加一条数据
        /// </summary>
        public int Add(BaseSizeTable model)
        {
            return dal.Add(model);
        }

        /// <summary>
        /// 更新一条数据
        /// </summary>
        public bool Update(BaseSizeTable model)
        {
            return dal.Update(model);
        }

        /// <summary>
        /// 删除一条数据
        /// </summary>
        public bool Delete(string code, string groupCode)
        {
            return dal.Delete(code, groupCode);
        }

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public BaseSizeTable GetModel(string code, string groupCode)
        {
            return dal.GetModel(code, groupCode);
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
        /// 根据商品种类获得尺寸
        /// </summary>
        public DataSet GetSizeByGroupCode(string groupCode)
        {
            return dal.GetSizeByGroupCode(groupCode);
        }

        #endregion

    }
}
