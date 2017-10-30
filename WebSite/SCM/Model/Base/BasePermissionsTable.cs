using System;
namespace SCM.Model
{
	/// <summary>
	/// Base_Permissions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BasePermissionsTable
	{
		public BasePermissionsTable()
		{}
		#region Model
		private int _id;
		private string _description;
		private int _category_id;
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string DESCRIPTION
		{
			set{ _description=value;}
			get{return _description;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int CATEGORY_ID
		{
			set{ _category_id=value;}
			get{return _category_id;}
		}
		#endregion Model

	}
}

