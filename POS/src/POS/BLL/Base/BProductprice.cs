using System;
using System.Data;
using System.Collections.Generic;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;

namespace POS.Bll
{
   public partial class BProductPrice
    {
       private readonly IProductPrice dal = DataAccess.CreateProductpriceManage();
       public BProductPrice()
		{}
		#region  Method
		/// <summary>
		/// 是否存在该记录
		/// </summary>
		public bool Exists(decimal ID)
		{
			return dal.Exists(ID);
		}

        public bool isDelete(decimal ID)
        {
            return dal.isDelete(ID);
        }
		/// <summary>
		/// 增加一条数据
		/// </summary>
		public int  Add(BaseProductPriceTable model)
		{
			return dal.Add(model);
		}

		/// <summary>
		/// 更新一条数据
		/// </summary>
        public bool Update(BaseProductPriceTable model)
		{
			return dal.Update(model);
		}

		/// <summary>
		/// 删除一条数据
		/// </summary>
		public bool Delete(decimal ID)
		{
			
			return dal.Delete(ID);
		}

		/// <summary>
		/// 得到一个对象实体
		/// </summary>
        public BaseProductPriceTable GetModel(decimal ID)
		{
			
			return dal.GetModel(ID);
		}
       
       /// <summary>
       /// 
       /// </summary>
        public DataSet getSalesPrice(string styleCode, string departmentCode)
        {
            return dal.getSalesPrice(styleCode, departmentCode);
        }
		#endregion  Method
    }
}
