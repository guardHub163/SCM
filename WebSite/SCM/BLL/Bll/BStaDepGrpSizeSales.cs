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
   public partial class BStaDepGrpSizeSales
    {
       private readonly IStaDepGrpSizeSales dal = DataAccess.CreateStaDepGrpSizeSalesManage();

       public int InsertOneDepGrpSize() 
       {
           return dal.InsertOneDepGrpSize();
       }

       public int InsertTwoDepGrpSize() 
       {
           return dal.InsertTwoDepGrpSize();
       }

       public int InsertThreeDepGrpSize() 
       {
           return dal.InsertThreeDepGrpSize();
       }
    }
}
