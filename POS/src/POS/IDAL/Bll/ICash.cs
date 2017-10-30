using System;
using System.Data;
namespace POS.IDAL
{
	/// <summary>
	/// �ӿڲ�ICash 
	/// </summary>
	public interface ICash
	{
		#region  ��Ա����
		
		/// <summary>
		/// ����һ������
		/// </summary>
		int Insert(POS.Model.CashTable model);
		
		/// <summary>
		/// ���ǰ��������
		/// </summary>
		DataSet GetList(int Top,string strWhere,string filedOrder);

        /// <summary>
        /// ��ö���ʵ��
        /// </summary>
        POS.Model.CashTable GetModel(string strWhere);       

        /// <summary>
        /// ����һ������
        /// </summary>
        int Update(POS.Model.CashTable cashTable);

        /// <summary>
        /// ���״̬Ϊ1������
        /// </summary>
        DataSet GetCashInfo(string strWhere);

        /// <summary>
        /// �޸�״̬
        /// </summary>
        bool UpdateFlag(int send_flag, string slip_number);

        #endregion  ��Ա����
    }
}
