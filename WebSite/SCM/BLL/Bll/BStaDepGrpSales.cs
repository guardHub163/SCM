using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SCM.DALFactory;
using SCM.IDAL;
using System.Data;
using SCM.Model;

namespace SCM.Bll
{
   public partial class BStaDepGrpSales
    {
       private readonly IStaDepGrpSales dal = DataAccess.CreateStaDepGrpSalesManage();

       //第一次插入数据
       public int InsertInfoOne() 
       {
           return dal.InsertInfoOne();
       }

         //第二次插入数据
       public int InsertInfoTwo() 
       {
           return dal.InsertInfoTwo();
       }

       //第三次插入数据
       public int InsertInfoThree() 
       {
           return dal.InsertInfoThree();
       }
    }
}
