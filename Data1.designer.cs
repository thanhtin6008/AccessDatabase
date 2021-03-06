#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="HRMDB")]
	public partial class DataDataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertSecurityPolicy(SecurityPolicy instance);
    partial void UpdateSecurityPolicy(SecurityPolicy instance);
    partial void DeleteSecurityPolicy(SecurityPolicy instance);
    partial void InsertSecurityPolicyDetail(SecurityPolicyDetail instance);
    partial void UpdateSecurityPolicyDetail(SecurityPolicyDetail instance);
    partial void DeleteSecurityPolicyDetail(SecurityPolicyDetail instance);
    partial void InsertGroupSecurity(GroupSecurity instance);
    partial void UpdateGroupSecurity(GroupSecurity instance);
    partial void DeleteGroupSecurity(GroupSecurity instance);
    partial void InsertGroupSecurityDetail(GroupSecurityDetail instance);
    partial void UpdateGroupSecurityDetail(GroupSecurityDetail instance);
    partial void DeleteGroupSecurityDetail(GroupSecurityDetail instance);
    #endregion
		
		public DataDataContext() : 
				base(global::WindowsFormsApp1.Properties.Settings.Default.HRMDBConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataDataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<SecurityPolicy> SecurityPolicies
		{
			get
			{
				return this.GetTable<SecurityPolicy>();
			}
		}
		
		public System.Data.Linq.Table<SecurityPolicyDetail> SecurityPolicyDetails
		{
			get
			{
				return this.GetTable<SecurityPolicyDetail>();
			}
		}
		
		public System.Data.Linq.Table<GroupSecurity> GroupSecurities
		{
			get
			{
				return this.GetTable<GroupSecurity>();
			}
		}
		
		public System.Data.Linq.Table<GroupSecurityDetail> GroupSecurityDetails
		{
			get
			{
				return this.GetTable<GroupSecurityDetail>();
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SecurityPolicies")]
	public partial class SecurityPolicy : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SecurityPolicyID;
		
		private int _GroupKey;
		
		private string _Type;
		
		private string _FormName;
		
		private bool _IsActivity;
		
		private string _FormDescription;
		
		private System.Nullable<System.DateTime> _LastModified;
		
		private string _LastModifiedBy;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSecurityPolicyIDChanging(int value);
    partial void OnSecurityPolicyIDChanged();
    partial void OnGroupKeyChanging(int value);
    partial void OnGroupKeyChanged();
    partial void OnTypeChanging(string value);
    partial void OnTypeChanged();
    partial void OnFormNameChanging(string value);
    partial void OnFormNameChanged();
    partial void OnIsActivityChanging(bool value);
    partial void OnIsActivityChanged();
    partial void OnFormDescriptionChanging(string value);
    partial void OnFormDescriptionChanged();
    partial void OnLastModifiedChanging(System.Nullable<System.DateTime> value);
    partial void OnLastModifiedChanged();
    partial void OnLastModifiedByChanging(string value);
    partial void OnLastModifiedByChanged();
    #endregion
		
		public SecurityPolicy()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecurityPolicyID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int SecurityPolicyID
		{
			get
			{
				return this._SecurityPolicyID;
			}
			set
			{
				if ((this._SecurityPolicyID != value))
				{
					this.OnSecurityPolicyIDChanging(value);
					this.SendPropertyChanging();
					this._SecurityPolicyID = value;
					this.SendPropertyChanged("SecurityPolicyID");
					this.OnSecurityPolicyIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupKey", DbType="Int NOT NULL")]
		public int GroupKey
		{
			get
			{
				return this._GroupKey;
			}
			set
			{
				if ((this._GroupKey != value))
				{
					this.OnGroupKeyChanging(value);
					this.SendPropertyChanging();
					this._GroupKey = value;
					this.SendPropertyChanged("GroupKey");
					this.OnGroupKeyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Type", DbType="VarChar(50) NOT NULL", CanBeNull=false)]
		public string Type
		{
			get
			{
				return this._Type;
			}
			set
			{
				if ((this._Type != value))
				{
					this.OnTypeChanging(value);
					this.SendPropertyChanging();
					this._Type = value;
					this.SendPropertyChanged("Type");
					this.OnTypeChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FormName", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string FormName
		{
			get
			{
				return this._FormName;
			}
			set
			{
				if ((this._FormName != value))
				{
					this.OnFormNameChanging(value);
					this.SendPropertyChanging();
					this._FormName = value;
					this.SendPropertyChanged("FormName");
					this.OnFormNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActivity", DbType="Bit NOT NULL")]
		public bool IsActivity
		{
			get
			{
				return this._IsActivity;
			}
			set
			{
				if ((this._IsActivity != value))
				{
					this.OnIsActivityChanging(value);
					this.SendPropertyChanging();
					this._IsActivity = value;
					this.SendPropertyChanged("IsActivity");
					this.OnIsActivityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_FormDescription", DbType="NVarChar(250)")]
		public string FormDescription
		{
			get
			{
				return this._FormDescription;
			}
			set
			{
				if ((this._FormDescription != value))
				{
					this.OnFormDescriptionChanging(value);
					this.SendPropertyChanging();
					this._FormDescription = value;
					this.SendPropertyChanged("FormDescription");
					this.OnFormDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModified", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> LastModified
		{
			get
			{
				return this._LastModified;
			}
			set
			{
				if ((this._LastModified != value))
				{
					this.OnLastModifiedChanging(value);
					this.SendPropertyChanging();
					this._LastModified = value;
					this.SendPropertyChanged("LastModified");
					this.OnLastModifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModifiedBy", DbType="NVarChar(100)")]
		public string LastModifiedBy
		{
			get
			{
				return this._LastModifiedBy;
			}
			set
			{
				if ((this._LastModifiedBy != value))
				{
					this.OnLastModifiedByChanging(value);
					this.SendPropertyChanging();
					this._LastModifiedBy = value;
					this.SendPropertyChanged("LastModifiedBy");
					this.OnLastModifiedByChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.SecurityPolicyDetails")]
	public partial class SecurityPolicyDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _SecurityPolicyDetailID;
		
		private int _SecurityPolicyID;
		
		private string _ActionName;
		
		private bool _IsActivity;
		
		private string _Description;
		
		private System.Nullable<int> _Organize;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnSecurityPolicyDetailIDChanging(int value);
    partial void OnSecurityPolicyDetailIDChanged();
    partial void OnSecurityPolicyIDChanging(int value);
    partial void OnSecurityPolicyIDChanged();
    partial void OnActionNameChanging(string value);
    partial void OnActionNameChanged();
    partial void OnIsActivityChanging(bool value);
    partial void OnIsActivityChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnOrganizeChanging(System.Nullable<int> value);
    partial void OnOrganizeChanged();
    #endregion
		
		public SecurityPolicyDetail()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecurityPolicyDetailID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int SecurityPolicyDetailID
		{
			get
			{
				return this._SecurityPolicyDetailID;
			}
			set
			{
				if ((this._SecurityPolicyDetailID != value))
				{
					this.OnSecurityPolicyDetailIDChanging(value);
					this.SendPropertyChanging();
					this._SecurityPolicyDetailID = value;
					this.SendPropertyChanged("SecurityPolicyDetailID");
					this.OnSecurityPolicyDetailIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecurityPolicyID", DbType="Int NOT NULL")]
		public int SecurityPolicyID
		{
			get
			{
				return this._SecurityPolicyID;
			}
			set
			{
				if ((this._SecurityPolicyID != value))
				{
					this.OnSecurityPolicyIDChanging(value);
					this.SendPropertyChanging();
					this._SecurityPolicyID = value;
					this.SendPropertyChanged("SecurityPolicyID");
					this.OnSecurityPolicyIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_ActionName", DbType="VarChar(250) NOT NULL", CanBeNull=false)]
		public string ActionName
		{
			get
			{
				return this._ActionName;
			}
			set
			{
				if ((this._ActionName != value))
				{
					this.OnActionNameChanging(value);
					this.SendPropertyChanging();
					this._ActionName = value;
					this.SendPropertyChanged("ActionName");
					this.OnActionNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActivity", DbType="Bit NOT NULL")]
		public bool IsActivity
		{
			get
			{
				return this._IsActivity;
			}
			set
			{
				if ((this._IsActivity != value))
				{
					this.OnIsActivityChanging(value);
					this.SendPropertyChanging();
					this._IsActivity = value;
					this.SendPropertyChanged("IsActivity");
					this.OnIsActivityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(250)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Organize", DbType="Int")]
		public System.Nullable<int> Organize
		{
			get
			{
				return this._Organize;
			}
			set
			{
				if ((this._Organize != value))
				{
					this.OnOrganizeChanging(value);
					this.SendPropertyChanging();
					this._Organize = value;
					this.SendPropertyChanged("Organize");
					this.OnOrganizeChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GroupSecurities")]
	public partial class GroupSecurity : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GroupSecurityID;
		
		private string _GroupSecurityNumber;
		
		private string _GroupSecurityName;
		
		private string _Description;
		
		private bool _IsActivity;
		
		private string _CreateBy;
		
		private System.DateTime _CreateDate;
		
		private System.Nullable<System.DateTime> _LastModified;
		
		private string _LastModifiedBy;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGroupSecurityIDChanging(int value);
    partial void OnGroupSecurityIDChanged();
    partial void OnGroupSecurityNumberChanging(string value);
    partial void OnGroupSecurityNumberChanged();
    partial void OnGroupSecurityNameChanging(string value);
    partial void OnGroupSecurityNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    partial void OnIsActivityChanging(bool value);
    partial void OnIsActivityChanged();
    partial void OnCreateByChanging(string value);
    partial void OnCreateByChanged();
    partial void OnCreateDateChanging(System.DateTime value);
    partial void OnCreateDateChanged();
    partial void OnLastModifiedChanging(System.Nullable<System.DateTime> value);
    partial void OnLastModifiedChanged();
    partial void OnLastModifiedByChanging(string value);
    partial void OnLastModifiedByChanged();
    #endregion
		
		public GroupSecurity()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupSecurityID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int GroupSecurityID
		{
			get
			{
				return this._GroupSecurityID;
			}
			set
			{
				if ((this._GroupSecurityID != value))
				{
					this.OnGroupSecurityIDChanging(value);
					this.SendPropertyChanging();
					this._GroupSecurityID = value;
					this.SendPropertyChanged("GroupSecurityID");
					this.OnGroupSecurityIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupSecurityNumber", DbType="VarChar(50)")]
		public string GroupSecurityNumber
		{
			get
			{
				return this._GroupSecurityNumber;
			}
			set
			{
				if ((this._GroupSecurityNumber != value))
				{
					this.OnGroupSecurityNumberChanging(value);
					this.SendPropertyChanging();
					this._GroupSecurityNumber = value;
					this.SendPropertyChanged("GroupSecurityNumber");
					this.OnGroupSecurityNumberChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupSecurityName", DbType="NVarChar(250) NOT NULL", CanBeNull=false)]
		public string GroupSecurityName
		{
			get
			{
				return this._GroupSecurityName;
			}
			set
			{
				if ((this._GroupSecurityName != value))
				{
					this.OnGroupSecurityNameChanging(value);
					this.SendPropertyChanging();
					this._GroupSecurityName = value;
					this.SendPropertyChanged("GroupSecurityName");
					this.OnGroupSecurityNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(250)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IsActivity", DbType="Bit NOT NULL")]
		public bool IsActivity
		{
			get
			{
				return this._IsActivity;
			}
			set
			{
				if ((this._IsActivity != value))
				{
					this.OnIsActivityChanging(value);
					this.SendPropertyChanging();
					this._IsActivity = value;
					this.SendPropertyChanged("IsActivity");
					this.OnIsActivityChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateBy", DbType="NVarChar(100) NOT NULL", CanBeNull=false)]
		public string CreateBy
		{
			get
			{
				return this._CreateBy;
			}
			set
			{
				if ((this._CreateBy != value))
				{
					this.OnCreateByChanging(value);
					this.SendPropertyChanging();
					this._CreateBy = value;
					this.SendPropertyChanged("CreateBy");
					this.OnCreateByChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_CreateDate", DbType="SmallDateTime NOT NULL")]
		public System.DateTime CreateDate
		{
			get
			{
				return this._CreateDate;
			}
			set
			{
				if ((this._CreateDate != value))
				{
					this.OnCreateDateChanging(value);
					this.SendPropertyChanging();
					this._CreateDate = value;
					this.SendPropertyChanged("CreateDate");
					this.OnCreateDateChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModified", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> LastModified
		{
			get
			{
				return this._LastModified;
			}
			set
			{
				if ((this._LastModified != value))
				{
					this.OnLastModifiedChanging(value);
					this.SendPropertyChanging();
					this._LastModified = value;
					this.SendPropertyChanged("LastModified");
					this.OnLastModifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModifiedBy", DbType="NVarChar(100)")]
		public string LastModifiedBy
		{
			get
			{
				return this._LastModifiedBy;
			}
			set
			{
				if ((this._LastModifiedBy != value))
				{
					this.OnLastModifiedByChanging(value);
					this.SendPropertyChanging();
					this._LastModifiedBy = value;
					this.SendPropertyChanged("LastModifiedBy");
					this.OnLastModifiedByChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.GroupSecurityDetails")]
	public partial class GroupSecurityDetail : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _GroupSecurityDetailID;
		
		private int _SecurityPolicyDetailID;
		
		private int _GroupSecurityID;
		
		private bool _Allow;
		
		private bool _Deny;
		
		private System.Nullable<System.DateTime> _LastModified;
		
		private string _LastModifiedBy;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnGroupSecurityDetailIDChanging(int value);
    partial void OnGroupSecurityDetailIDChanged();
    partial void OnSecurityPolicyDetailIDChanging(int value);
    partial void OnSecurityPolicyDetailIDChanged();
    partial void OnGroupSecurityIDChanging(int value);
    partial void OnGroupSecurityIDChanged();
    partial void OnAllowChanging(bool value);
    partial void OnAllowChanged();
    partial void OnDenyChanging(bool value);
    partial void OnDenyChanged();
    partial void OnLastModifiedChanging(System.Nullable<System.DateTime> value);
    partial void OnLastModifiedChanged();
    partial void OnLastModifiedByChanging(string value);
    partial void OnLastModifiedByChanged();
    #endregion
		
		public GroupSecurityDetail()
		{
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupSecurityDetailID", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int GroupSecurityDetailID
		{
			get
			{
				return this._GroupSecurityDetailID;
			}
			set
			{
				if ((this._GroupSecurityDetailID != value))
				{
					this.OnGroupSecurityDetailIDChanging(value);
					this.SendPropertyChanging();
					this._GroupSecurityDetailID = value;
					this.SendPropertyChanged("GroupSecurityDetailID");
					this.OnGroupSecurityDetailIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_SecurityPolicyDetailID", DbType="Int NOT NULL")]
		public int SecurityPolicyDetailID
		{
			get
			{
				return this._SecurityPolicyDetailID;
			}
			set
			{
				if ((this._SecurityPolicyDetailID != value))
				{
					this.OnSecurityPolicyDetailIDChanging(value);
					this.SendPropertyChanging();
					this._SecurityPolicyDetailID = value;
					this.SendPropertyChanged("SecurityPolicyDetailID");
					this.OnSecurityPolicyDetailIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_GroupSecurityID", DbType="Int NOT NULL")]
		public int GroupSecurityID
		{
			get
			{
				return this._GroupSecurityID;
			}
			set
			{
				if ((this._GroupSecurityID != value))
				{
					this.OnGroupSecurityIDChanging(value);
					this.SendPropertyChanging();
					this._GroupSecurityID = value;
					this.SendPropertyChanged("GroupSecurityID");
					this.OnGroupSecurityIDChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Allow", DbType="Bit NOT NULL")]
		public bool Allow
		{
			get
			{
				return this._Allow;
			}
			set
			{
				if ((this._Allow != value))
				{
					this.OnAllowChanging(value);
					this.SendPropertyChanging();
					this._Allow = value;
					this.SendPropertyChanged("Allow");
					this.OnAllowChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Name="[Deny]", Storage="_Deny", DbType="Bit NOT NULL")]
		public bool Deny
		{
			get
			{
				return this._Deny;
			}
			set
			{
				if ((this._Deny != value))
				{
					this.OnDenyChanging(value);
					this.SendPropertyChanging();
					this._Deny = value;
					this.SendPropertyChanged("Deny");
					this.OnDenyChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModified", DbType="SmallDateTime")]
		public System.Nullable<System.DateTime> LastModified
		{
			get
			{
				return this._LastModified;
			}
			set
			{
				if ((this._LastModified != value))
				{
					this.OnLastModifiedChanging(value);
					this.SendPropertyChanging();
					this._LastModified = value;
					this.SendPropertyChanged("LastModified");
					this.OnLastModifiedChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_LastModifiedBy", DbType="VarChar(100)")]
		public string LastModifiedBy
		{
			get
			{
				return this._LastModifiedBy;
			}
			set
			{
				if ((this._LastModifiedBy != value))
				{
					this.OnLastModifiedByChanging(value);
					this.SendPropertyChanging();
					this._LastModifiedBy = value;
					this.SendPropertyChanged("LastModifiedBy");
					this.OnLastModifiedByChanged();
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
