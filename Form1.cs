using AutoGeneratedClass;
using DevComponents.DotNetBar.Controls;
using nsFramework.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Tables;
using System.Linq;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        ExcelReader _excelReader = new ExcelReader();
        protected EVersionExcel _eVersionExcel = EVersionExcel.E2003;
        string ConnectString = "";
        string table = " SELECT      c.name AS ColumName, y.name AS TypeName, c.is_nullable as IsNull,convert(int,c.max_length) as MaxLen,convert(bit,1) as OnChanged, convert(bit,0) as IsVNull,convert(bit,0) as IsVDouble,convert(bit,0) as IsCopyUpdate,convert(bit,0) as IsAddBinding,convert(bit,0) as IsShow" +
             " FROM         sys.tables AS t INNER JOIN " +
             " sys.columns AS c ON t.object_id = c.object_id INNER JOIN " +
             " sys.types AS y ON c.user_type_id = y.user_type_id " +
             " WHERE     (t.name = '{0}')";
        string proc = "usp_get_sproc_column_names '{0}'";
        string sql = "";
        IList<InfoColum> list;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView.AutoGenerateColumns = false;
            cboType.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //string path = @"C:\Users\admin\Desktop\SAP_Data\data.xls";
            //string sheetNames = "";
            //string strConnString = _excelReader.GetConnectString(_eVersionExcel, path);
            ////using (OleDbConnection excelConnection = new OleDbConnection(strConnString))
            ////{
            ////    System.Data.DataTable dtSheets = new System.Data.DataTable();
            ////    try
            ////    {
            ////        excelConnection.Open();
            ////        dtSheets = excelConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            ////        if (dtSheets == null)
            ////        {
            ////            sheetNames = null;
            ////        }
            ////        sheetNames = dtSheets.Rows[0]["TABLE_NAME"].ToString().Trim('\'').Replace("$", "");
            ////    }
            ////    finally
            ////    {
            ////        dtSheets.Dispose();
            ////    }

            ////    OleDbCommand command = new OleDbCommand();
            ////    string sSQL = "SELECT * FROM [" + sheetNames + "$]";

            ////    var adapter = new OleDbDataAdapter(sSQL, strConnString);
            ////    System.Data.DataTable table = new System.Data.DataTable();
            ////    adapter.Fill(table);
            ////}


            dataGridView1.DataSource = null;
                var readerInfo = _excelReader.GetDataToList<MappingInfo>(txtPath.Text, "Sheet1", _eVersionExcel);
            if (readerInfo != null)
            {
                if (readerInfo.Status)
                {
                    dataGridView1.DataSource = readerInfo.Data;
                }
                else
                {
                    MessageBox.Show(readerInfo.Message);
                }
            }
        }

        private void btnToEntity_Click(object sender, EventArgs e)
        {
            var list = dataGridView1.DataSource as IList<MappingInfo>;
            if(list != null)
            {
                var result = "";
                var resultNotMap = "";
                foreach (var item in list)
                {

                    if (!string.IsNullOrWhiteSpace(item.SAPFieldName))
                    {
                        var left = ("" + item.SAPFieldName.Substring(0, 2)).ToUpper();
                        if (left != "U_")
                        {
                            result += $"                {item.MESFieldID} = info.{item.SAPFieldName},\n";
                        }
                        else
                        {
                            resultNotMap += $"                {item.MESFieldID} =" + DefaultValue(item.Type) + ",\n";
                        }
                    }
                    else
                    {
                        resultNotMap += $"                {item.MESFieldID} ="+ DefaultValue(item.Type) + ",\n";
                    }
                        
                }
                richTextBox1.Text = result + "\n" + resultNotMap;
            }
        }
        private void btnToSynInfo_Click(object sender, EventArgs e)
        {
            var list = dataGridView1.DataSource as IList<MappingInfo>;
            if (list != null)
            {
                var result = "";
                foreach (var item in list)
                {

                    if (!string.IsNullOrWhiteSpace(item.SAPFieldName))
                    {
                        var left = ("" + item.SAPFieldName.Substring(0, 2)).ToUpper();
                        if (left != "U_")
                        {
                            result += $"                {item.SAPFieldName} = info.{item.MESFieldID},\n";
                        }
                    }

                }
                richTextBox1.Text = result;
            }
        }

        private void btnCreateSynInfo_Click(object sender, EventArgs e)
        {
            var result = @"    public class "+ txtName.Text + @": IIdentifyTable
    {
                #region member";
            result += "\n";
                var list = dataGridView1.DataSource as IList<MappingInfo>;
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (!string.IsNullOrWhiteSpace(item.SAPFieldName))
                    {
                        var left = (""+item.SAPFieldName.Substring(0, 2)).ToUpper();
                        if (left != "U_")
                        {
                            result += $"        public " + ConvertType(item.SAPType, ("" + item.NotNull).ToUpper() != "Y") + " " + item.SAPFieldName + " { get; set; }\n";
                        }
                    }

                    
                }
                result += @"#endregion

        //-------------------------------------------------------------  

                #region Constructor

                #endregion

                //-------------------------------------------------------------  

                #region Propertise

        public object PKID => EmployeeID;

        #endregion

        //-------------------------------------------------------------  


    }
";
                richTextBox1.Text = result;
            }

        }

        private void CreateInfo()
        {
            var result = @"    public class " + txtName.Text + @": IIdentifyTable
    {
                #region member";
            result += "\n";
            var list = dataGridView1.DataSource as IList<MappingInfo>;
            if (list != null)
            {
                foreach (var item in list)
                {
                    result += $"        public " + ConvertType(item.Type, ("" + item.NotNull).ToUpper() != "Y") + " " + item.MESFieldID + " { get; set; }\n";
                }
                result += @"#endregion

        //-------------------------------------------------------------  

                #region Constructor

                #endregion

                //-------------------------------------------------------------  

                #region Propertise

        public object PKID => EmployeeID;

        #endregion

        //-------------------------------------------------------------  


    }
";

                richTextBox1.Text = result;
            }

        }

        private string ConvertType(string value, bool isNull)
        {
            var snull = (isNull == true ? "?" : "");
            switch (value)
            {
                case "time":
                    return $"TimeSpan{snull}";
                case "smallint":
                    return $"byte{snull}";
                case "float":
                    return $"double{snull}";
                case "real":
                    return $"double{snull}";
                case "uniqueidentifier":
                    return $"string{snull}";
                case "tinyint":
                    return $"byte{snull}";
                case "smallmoney":
                case "numeric":
                case "money":
                case "decimal":
                    return $"decimal{snull}";
                case "datetimeoffset":
                    return $"DateTime{snull}";
                case "date":
                case "timestamp":
                case "smalldatetime":
                case "datetime2":
                case "datetime":
                    return $"DateTime{snull}";
                case "varbinary":
                case "binary":
                case "image":
                    return $"TimeSpan{snull}";
                case "bigint":
                    return $"long{snull}";
                case "bit":
                    return $"bool{snull}";
                case "text":
                case "ntext":
                case "nchar":
                case "char":
                case "nvarchar":
                case "varchar":
                    return $"string";
                case "int":
                    return $"int{snull}";
                default:
                    return $"string";
            }
       
        }
        private string DefaultValue(string value)
        {
            switch (value)
            {
                case "smallint":
                case "float":
                case "tinyint":
                case "real":
                case "smallmoney":
                case "numeric":
                case "money":
                case "decimal":
                case "bigint":
                case "int":
                    return $"0";
                case "datetimeoffset":
                case "date":
                case "timestamp":
                case "smalldatetime":
                case "datetime2":
                case "datetime":
                case "time":
                    return $"DateTime.MinValue";
                case "varbinary":
                case "binary":
                case "image":
                    return $"";
                case "bit":
                    return $"False";
                case "text":
                case "ntext":
                case "nchar":
                case "char":
                case "nvarchar":
                case "varchar":
                case "uniqueidentifier":
                default:
                    return @"""" + @"""";
            }
            
        }
 
        private string GetColumnMap()
        {
            var ressultMap = "";
            var listMap = dataGridView1.DataSource as IList<MappingInfo>;
            if (listMap != null)
            {

                var map = listMap.Where(t => !string.IsNullOrWhiteSpace(t.SAPFieldName) && (("" + t.SAPFieldName.Substring(0, 2)).ToUpper() != "U_")).ToList();
                if (map != null)
                {
                    foreach (var item in map)
                    {
                        ressultMap += "    " + item.SAPFieldName + "," + @"""" + "|" + @"""" + "," + "\n";
                    }
                }
            }
            return ressultMap;
        }
        private void GetInfoColumn(DataGridViewX gridView, string proce)
        {
            try
            {
                ConnectString = "Data Source=" + txtServer.Text + ";Initial Catalog=" + txtDatabase.Text + ";Persist Security Info=True;User ID=" + txtUser.Text + ";Password=" + txtPass.Text + ";Connection Timeout=5;";
                DataDataContext data = new DataDataContext(ConnectString);
                switch (cboType.Text)
                {
                    case "Table":
                        sql = string.Format(table, proce);
                        break;
                    case "Procedure":
                        sql = string.Format(proc, proce);
                        break;
                }
                IList<InfoColum> listOld = gridView.DataSource as List<InfoColum>;
                list = data.ExecuteQuery<InfoColum>(sql).ToList();
                data.Dispose();

                if (listOld == null)
                {
                    gridView.DataSource = null;
                    gridView.DataSource = list;

                    return;
                }
                if (listOld.Count == 0)
                {
                    gridView.DataSource = null;
                    gridView.DataSource = list;
                    return;
                }
                if (listOld.Count > 0)
                {
                    foreach (var item in list)
                    {
                        InfoColum info = listOld.FirstOrDefault<InfoColum>(t => t.ColumName == item.ColumName);
                        if (info != null)
                        {
                            item.IsAddBinding = info.IsAddBinding;
                            item.IsCopyUpdate = info.IsCopyUpdate;
                            item.IsNull = info.IsNull;
                            item.IsVDouble = info.IsVDouble;
                            item.IsShow = info.IsShow;
                            item.TextControl = info.TextControl;
                            item.Tooltip = info.Tooltip;
                            item.ControlType = info.ControlType;
                            item.ServiceReference = info.ServiceReference;
                            item.ServiceReferenceAdd = info.ServiceReferenceAdd;
                            item.ServiceReferenceRemove = info.ServiceReferenceRemove;
                            item.ServiceReferenceUpdate = info.ServiceReferenceUpdate;
                            item.Tooltip = info.Tooltip;
                        }
                    }
                    gridView.DataSource = null;
                    gridView.DataSource = list;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnCreateEntity_Click(object sender, EventArgs e)
        {
                string result = "";
            var list = dataGridView.DataSource as List<InfoColum>;
            if (list != null)
            {
                result = @"using Framework.Common.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace AsyncDatabaseProvider.Table
{
    [Table(" + @"""" + txtClassNameInfo.Text + @"""" + @")]
    public class " + txtClassNameInfo.Text + @" : IIdentifyMapTable
    {
        #region member";
                result += "\n";
                var maxlen = "";
                var typeName = "";
                foreach (var item in list)
                {
                    maxlen = "";
                    typeName = "";
                    switch (item.TypeName)
                    {
                        case "numeric":
                        case "money":
                        case "decimal":
                            typeName = @"""" + item.TypeName + "(" + item.MaxLen + ")" + @"""";
                            break;
                        case "smallint":
                        case "float":
                        case "tinyint":
                        case "real":
                        case "smallmoney":
                        case "bigint":
                        case "int":
                            //typeColumn = ".HasColumnType(" + @"""" + item.Type + "(" + item.Length + ")" + @"""" + ")";
                            //typeName = @"""" + item.TypeName + "(" + item.MaxLen + ")" + @"""";
                            break;
                        case "datetimeoffset":
                        case "date":
                        case "timestamp":
                        case "smalldatetime":
                        case "datetime2":
                        case "datetime":
                        case "time":
                            break;
                        case "varbinary":
                        case "binary":
                        case "image":
                            break;
                        case "bit":
                            break;
                        case "text":
                        case "ntext":
                        case "nchar":
                        case "char":
                        case "nvarchar":
                        case "varchar":
                        case "uniqueidentifier":
                        default:
                            maxlen = "        [MaxLength(" + item.MaxLen + ")]\n";
                            typeName = @"""" + item.TypeName + "(" + item.MaxLen + ")" + @"""";
                            break;
                    }
                    if(maxlen.Length > 0)
                    {
                        result += maxlen;
                    }
                    if (item.IsNull)
                    {
                        result += "        [Required]\n";
                    }
                    result += "        [Column(" + @"""" + item.ColumName + @"""" + ",TypeName = " + typeName + ")]\n";
                    result += "        public "+ ConvertType(item.TypeName,item.IsNull) + " " + item.ColumName + " { get; set; }\n";

                }

                result += "\n";
                result += "        #endregion\n\n\n";
                result += $"        public object MapIDValue => {txtColumnMap.Text};\n";
                result += $"        public string ColumnMap => nameof({txtColumnMap.Text}); \n";
                result += $"        public object PKID => {txtKeyID.Text};\n";
                result += $"        public string ValueColumnMap => string.Concat({GetColumnMap()});\n";
                result += "    }\n";
                result += "}";
            }

            richTextBox1.Text = result;
        }
        private void cmdGetColum_Click(object sender, EventArgs e)
        {
            GetInfoColumn(dataGridView, txtProcMain.Text);
        }
        private void btnCreateTableHQ_Click(object sender, EventArgs e)
        {
            string result = "";
            var list = dataGridView1.DataSource as List<MappingInfo>;
            if (list != null)
            {
                var column = "";
                foreach (var item in list)
                {
                    var len = "";
                    if (!string.IsNullOrWhiteSpace(item.Length))
                    {
                        len = "(" + item.Length + ")";
                    }
                    var notnull = " NULL";
                    if((""+ item.NotNull).ToUpper() == "Y")
                    {
                        notnull = " NOT NULL";
                    }
                    column += "	[" + item.MESFieldID + "] ["+ item.Type +"]"+ len + notnull + " ,\n";
                }
                result = @"CREATE TABLE [if].["+ txtName.Text + @"](
" + column + @"
    [IF_REFKEY] [varchar](265) NULL,
	[IF_SEQ] [varchar](20) NOT NULL,
	[IF_FRSYS] [char](1) NOT NULL,
	[IF_TOSYS] [char](1) NOT NULL,
	[IF_STAT] [smallint] NOT NULL,
	[IF_TYPE] [char](1) NOT NULL,
	[IF_STATMSG] [nvarchar](256) NULL,
	[IF_EXC_TIME] [smallint] NULL,
	[IF_CRE_TIME] [datetime] NOT NULL,
	[IF_MOD_TIME] [datetime] NULL,
 CONSTRAINT [PK_" + txtName.Text + @"] PRIMARY KEY CLUSTERED 
(
	[IF_SEQ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_SEQ]  DEFAULT ([dbo].[HQ_ORDER_TEMP_AutogenKey]((1))) FOR [IF_SEQ]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_FRSYS_1]  DEFAULT ('H') FOR [IF_FRSYS]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_TOSYS_1]  DEFAULT ('M') FOR [IF_TOSYS]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_STAT_1]  DEFAULT ((0)) FOR [IF_STAT]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_TYPE_1]  DEFAULT ('I') FOR [IF_TYPE]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_EXC_TIME_1]  DEFAULT ((0)) FOR [IF_EXC_TIME]
GO

ALTER TABLE [if].[" + txtName.Text + @"] ADD  CONSTRAINT [" + txtName.Text + @"_IF_CRE_TIME]  DEFAULT (getdate()) FOR [IF_CRE_TIME]
GO";

                richTextBox1.Text = result;
            }
        }
        private void btnMapColumn_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = GetColumnMap();
        }

        private void btnCreateInfoHQ_Click(object sender, EventArgs e)
        {
            var result = @"    public class " + txtName.Text + @": IIdentifyTable
    {
                #region member";
            result += "\n";
            var list = dataGridView1.DataSource as IList<MappingInfo>;
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (!string.IsNullOrWhiteSpace(item.MESFieldID))
                    {
                        var left = ("" + item.MESFieldID.Substring(0, 2)).ToUpper();
                        if (left != "U_")
                        {
                            result += $"        public " + ConvertType(item.Type, ("" + item.NotNull).ToUpper() != "Y") + " " + item.MESFieldID + " { get; set; }\n";
                        }
                    }


                }
                result += @"#endregion

        //-------------------------------------------------------------  

                #region Constructor

                #endregion

                //-------------------------------------------------------------  

                #region Propertise

        public string ObjectName() { return ; }
        object ISynIdentify.PKID() { return ; }

#endregion

                //-------------------------------------------------------------  


            }
";
                richTextBox1.Text = result;
            }

        }

        private void btnToEntityHQ_Click(object sender, EventArgs e)
        {
            var ressultMap = "";
            var listMap = dataGridView1.DataSource as IList<MappingInfo>;
            if (listMap != null)
            {
                var map = listMap.Where(t => !string.IsNullOrWhiteSpace(t.MESFieldID)&& !string.IsNullOrWhiteSpace(t.MESFieldID) && (("" + t.SAPFieldName.Substring(0, 2)).ToUpper() != "U_")).ToList();
                if (map != null)
                {
                    foreach (var item in map)
                    {
                        ressultMap += $"    entity.{item.SAPFieldName} = info.{item.SAPFieldName};\n";

                    }
                }
            }
            richTextBox1.Text = ressultMap;
        }

        private void btnCreateConfigHQ_Click(object sender, EventArgs e)
        {
            var ressultMap = "";
            var listMap = dataGridView1.DataSource as IList<MappingInfo>;
            if (listMap != null)
            {
                var map = listMap.Where(t => !string.IsNullOrWhiteSpace(t.MESFieldID) && !string.IsNullOrWhiteSpace(t.MESFieldID)).ToList();
                if (map != null)
                {
                    foreach (var item in map)
                    {
                        var maxlen = "";
                        var typeColumn = "";
                        switch (item.Type)
                        {
                            case "smallint":
                            case "float":
                            case "tinyint":
                            case "real":
                            case "smallmoney":
                            case "numeric":
                            case "money":
                            case "decimal":
                            case "bigint":
                            case "int":
                                typeColumn = ".HasColumnType("+ @""""+ item.Type +"("+ item.Length +")" +@"""" +")";
                                break;
                            case "datetimeoffset":
                            case "date":
                            case "timestamp":
                            case "smalldatetime":
                            case "datetime2":
                            case "datetime":
                            case "time":
                                break;
                            case "varbinary":
                            case "binary":
                            case "image":
                                break;
                            case "bit":
                                break;
                            case "text":
                            case "ntext":
                            case "nchar":
                            case "char":
                            case "nvarchar":
                            case "varchar":
                            case "uniqueidentifier":
                            default:
                                maxlen = ".HasMaxLength("+ item.Length +")";
                                    break;
                        }

                        var req = "";
                        if((""+ item.NotNull).ToUpper() == "Y")
                        {
                            req = ".IsRequired()";
                        }

                        var temp = maxlen + req + typeColumn;
                        if(temp.Length > 0)
                        {
                            ressultMap += "builder.Property(e => e."+ item.MESFieldID + ")" + temp + ";\n";
                        }
                    }
                }
            }
            richTextBox1.Text = ressultMap;
        }

        private void btnToEntitySAP_Click(object sender, EventArgs e)
        {
            var ressultMap = "";
            var listMap = dataGridView1.DataSource as IList<MappingInfo>;
            if (listMap != null)
            {
                var map = listMap.Where(t => !string.IsNullOrWhiteSpace(t.MESFieldID) && !string.IsNullOrWhiteSpace(t.SAPFieldName)).ToList();
                if (map != null)
                {
                    foreach (var item in map)
                    {
                        ressultMap += $"    entity.{item.MESFieldID} = info.{item.SAPFieldName};\n";

                    }
                }
            }
            richTextBox1.Text = ressultMap;
        }

        private void btnCallFormCreateTable_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.ShowDialog();
        }

        private void btnCreateInfoEntity_Click(object sender, EventArgs e)
        {
            var result1 = "";
            var result = "#region Member\n";
            var list = dataGridView.DataSource as IList<InfoColum>;
            if(list != null)
            {
                result += "\n";
                var maxlen = "";
                var typeName = "";
                foreach (var item in list)
                {
                    result1 += $"info.{item.ColumName} = item.{item.ColumName}" + ";\n";
                    switch (item.TypeName)
                    {
                        case "numeric":
                        case "money":
                        case "decimal":
                            typeName = @"""" + item.TypeName + "(" + item.MaxLen + ")" + @"""";
                            break;
                        case "smallint":
                        case "float":
                        case "tinyint":
                        case "real":
                        case "smallmoney":
                        case "bigint":
                        case "int":
                            //typeColumn = ".HasColumnType(" + @"""" + item.Type + "(" + item.Length + ")" + @"""" + ")";
                            //typeName = @"""" + item.TypeName + "(" + item.MaxLen + ")" + @"""";
                            break;
                        case "datetimeoffset":
                        case "date":
                        case "timestamp":
                        case "smalldatetime":
                        case "datetime2":
                        case "datetime":
                        case "time":
                            break;
                        case "varbinary":
                        case "binary":
                        case "image":
                            break;
                        case "bit":
                            break;
                        case "text":
                        case "ntext":
                        case "nchar":
                        case "char":
                        case "nvarchar":
                        case "varchar":
                        case "uniqueidentifier":
                        default:
                            maxlen = "        [MaxLength(" + item.MaxLen + ")]\n";
                            typeName = @"""" + item.TypeName + "(" + item.MaxLen + ")" + @"""";
                            break;
                    }
                    if (maxlen.Length > 0)
                    {
                        result += maxlen;
                    }
                    //result += "        [Column(" + @"""" + item.ColumName + @"""" + ",TypeName = " + typeName + ")]\n";
                    result += "        public string " + item.ColumName + " { get; set; }\n";
                }

                result += "\n";
                result += "        #endregion\n";

                //richTextBox1.Text = result;
                richTextBox1.Text = result1;
            }
        }

        private void btnToEntityRight_Click(object sender, EventArgs e)
        {
            var list = dataGridView1.DataSource as IList<MappingInfo>;
            if (list != null)
            {
                var result = "";
                var resultNotMap = "";
                foreach (var item in list)
                {

                    if (!string.IsNullOrWhiteSpace(item.SAPFieldName))
                    {
                        var left = ("" + item.SAPFieldName.Substring(0, 2)).ToUpper();
                        if (left != "U_")
                        {
                            result += $"                {item.SAPFieldName} = info.{item.MESFieldID},\n";
                        }
                        else
                        {
                            //resultNotMap += $"                {item.SAPFieldName} =" + DefaultValue(item.Type) + ",\n";
                        }
                    }
                    else
                    {
                        //resultNotMap += $"                {item.MESFieldID} =" + DefaultValue(item.Type) + ",\n";
                    }

                }
                richTextBox1.Text = result + "\n" + resultNotMap;
            }
        }

        private void btnCreateConfigRight_Click(object sender, EventArgs e)
        {
            var ressultMap = "";
            var listMap = dataGridView1.DataSource as IList<MappingInfo>;
            if (listMap != null)
            {
                var map = listMap.Where(t => !string.IsNullOrWhiteSpace(t.MESFieldID) && !string.IsNullOrWhiteSpace(t.MESFieldID)).ToList();
                if (map != null)
                {
                    foreach (var item in map)
                    {
                        var maxlen = "";
                        var typeColumn = "";
                        switch (item.Type)
                        {
                            case "smallint":
                            case "float":
                            case "tinyint":
                            case "real":
                            case "smallmoney":
                            case "numeric":
                            case "money":
                            case "decimal":
                            case "bigint":
                            case "int":
                                typeColumn = ".HasColumnType(" + @"""" + item.SAPType + "(" + item.SAPLength + ")" + @"""" + ")";
                                break;
                            case "datetimeoffset":
                            case "date":
                            case "timestamp":
                            case "smalldatetime":
                            case "datetime2":
                            case "datetime":
                            case "time":
                                break;
                            case "varbinary":
                            case "binary":
                            case "image":
                                break;
                            case "bit":
                                break;
                            case "text":
                            case "ntext":
                            case "nchar":
                            case "char":
                            case "nvarchar":
                            case "varchar":
                            case "uniqueidentifier":
                            default:
                                maxlen = ".HasMaxLength(" + item.SAPLength + ")";
                                break;
                        }

                        var req = "";
                        if (("" + item.NotNull).ToUpper() == "Y")
                        {
                            req = ".IsRequired()";
                        }

                        var temp = maxlen + req + typeColumn;
                        if (temp.Length > 0)
                        {
                            ressultMap += "builder.Property(e => e." + item.SAPFieldName + ")" + temp + ";\n";
                        }
                    }
                }
            }
            richTextBox1.Text = ressultMap;
        }
    }
    

}
