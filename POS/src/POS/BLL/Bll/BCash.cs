using System;
using System.Data;
using System.Collections.Generic;
using POS.Common;
using POS.Model;
using POS.DALFactory;
using POS.IDAL;
namespace POS.Bll
{
	/// <summary>
	/// BCash
	/// </summary>
	public class BCash
	{
		private readonly ICash dal=DataAccess.CreateCashManage();
		public BCash()
		{}
		#region  Method		

		/// <summary>
        /// ����һ������
		/// </summary>
        public int Insert(CashTable model)
		{
			return dal.Insert(model);
		}

        /// <summary>
        /// ����һ������
        /// </summary>
        public int Update(CashTable cashTable)
        {
            return dal.Update(cashTable);
        }

		/// <summary>
		/// ���ǰ��������
		/// </summary>
		public DataSet GetList(int Top,string strWhere,string filedOrder)
		{
			return dal.GetList(Top,strWhere,filedOrder);
		}
		/// <summary>
		/// ��ö���ʵ��
		/// </summary>
        public CashTable GetModel(string strWhere)
		{
            return dal.GetModel(strWhere);
		}

        ///<summary>
        ///���״̬Ϊ1������
        ///<summary>
        public DataSet GetCashInfo(string strWhere) 
        {
            return dal.GetCashInfo(strWhere);
        }

        /// <summary>
        /// ��Ϣ���غ��޸�״̬
        /// </summary>
        public bool UpdateFlag(int send_flag, string slip_number) 
        {
            return dal.UpdateFlag(send_flag, slip_number);
        }
		#endregion  Method
	}
}

