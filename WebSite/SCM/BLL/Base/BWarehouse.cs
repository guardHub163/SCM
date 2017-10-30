using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.Common;
using SCM.Model;
using SCM.DALFactory;
using SCM.IDAL;
using System.Data;

namespace SCM.Bll
{
   public partial class BWarehouse
    {
       private readonly IWarehouse dal = DataAccess.CreateWarehouseManage();
       public BWarehouse() { }

       #region  Method
       /// <summary>
       /// 是否存在该记录
       /// </summary>
       public bool Exists(string CODE)
       {
           return dal.Exists(CODE);
       }

       /// <summary>
       /// 增加一条数据
       /// </summary>
       public int Add(BaseWarehouseTable model)
       {
           return dal.Add(model);
       }

       /// <summary>
       /// 更新一条数据
       /// </summary>
       public bool Update(BaseWarehouseTable model)
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
       public BaseWarehouseTable GetModel(string CODE)
       {
           return dal.GetModel(CODE);
       }       

       /// <summary>
       /// 分页获取数据记录总数
       /// </summary>
       public int GetRecordCount(string strWhere)
       {
           return dal.GetRecordCount(strWhere);
       }
       /// <summary>
       /// 分页获取数据列表
       /// </summary>
       public DataSet GetList(string strWhere, string orderby, int startIndex, int endIndex)
       {
           return dal.GetList(strWhere, orderby, startIndex, endIndex);
       }

       /// <summary>
       /// 根据部门得到一个对象实体
       /// </summary>
       public BaseWarehouseTable GetModelByDepartment(string departmentCode)
       {
           return dal.GetModelByDepartment(departmentCode);
       }
       #endregion  Method
    }
}
