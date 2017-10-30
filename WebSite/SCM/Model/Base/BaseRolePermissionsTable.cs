using System;
namespace SCM.Model
{
	/// <summary>
	/// Role_Permissions:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class BaseRolePermissionsTable
	{
		public BaseRolePermissionsTable()
		{}
		#region Model
		private int _role_id;
		private int _permission_id;
		/// <summary>
		/// 
		/// </summary>
		public int ROLE_ID
		{
			set{ _role_id=value;}
			get{return _role_id;}
		}
		/// <summary>
		/// 
		/// </summary>
		public int PERMISSION_ID
		{
			set{ _permission_id=value;}
			get{return _permission_id;}
		}
		#endregion Model

	}
}

