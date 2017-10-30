using System;
namespace SCM.Model
{
	/// <summary>
	/// Base_Permissions_Categories:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BasePermissionsCategoriesTable
	{
		public BasePermissionsCategoriesTable()
		{}
		#region Model
		private int _id;
		private string _desciription;
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
		public string DESCIRIPTION
		{
			set{ _desciription=value;}
			get{return _desciription;}
		}
		#endregion Model

	}
}

