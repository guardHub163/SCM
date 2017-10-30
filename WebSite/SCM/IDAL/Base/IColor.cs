using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Model;
using System.Data;

namespace SCM.IDAL
{
    public interface IColor
    {
       # region  成员方法
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		bool Exists(string CODE);
		/// <summary>
		/// 增加一条数据
		/// </summary>
		int Add(BaseColorTable model);
		/// <summary>
		/// 更新一条数据
		/// </summary>
        bool Update(BaseColorTable model);
		/// <summary>
		/// 删除一条数据
		/// </summary>
		bool Delete(string CODE);

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        BaseColorTable GetModel(string CODE);

        int GetRecordCount(string strWhere);
        DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);
		#endregion  成员方法
    }
}
