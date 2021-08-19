using nsFramework.Common.Helper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form2 : Form
    {
        ExcelReader _excelReader = new ExcelReader();
        protected EVersionExcel _eVersionExcel = EVersionExcel.E2010;
        public Form2()
        {
            InitializeComponent();
        }

        private void btnReadFile_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var readerInfo = _excelReader.GetDataToList<TableInfo>(txtPath.Text, "Sheet1", _eVersionExcel);
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

        private void btnCreateTable_Click(object sender, EventArgs e)
        {
            var result = $"CREATE TABLE {txtSchema.Text}.{ txtTableName.Text}"+"(\n";
            var contrain = "";
            var key = "";
            var list = dataGridView1.DataSource as IList<TableInfo>;
            if(list != null)
            {
              
                foreach (var item in list)
                {
                    if(item.Changed == "O")
                    {
                        var isNull = "NULL";
                        if(item.NotNull == "Y")
                        {
                            isNull = "NOT NULL";
                        }
                        result += item.FieldID + " " + item.Type + " " + isNull + ",\n";
                        if (!string.IsNullOrWhiteSpace(item.DefaultValue))
                        {
                            contrain += $"ALTER TABLE {txtSchema.Text}.{txtTableName.Text} ADD  CONSTRAINT [{txtTableName.Text}_{item.FieldID}]  DEFAULT ('{item.DefaultValue}') FOR [{item.FieldID}]" + "\n";
                        }
                        if (item.KEY == "PK")
                        {
                            key += $"{item.FieldID}" +",";
                        }
                    }
                }
            }
            result += $@"CONSTRAINT [PK_{txtTableName.Text}] PRIMARY KEY CLUSTERED 
(

    [{key}] ASC
)WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]
) ON[PRIMARY]
GO ";

            result += "\n\n"+contrain;

            txtResult.Text = result;



        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            _eVersionExcel = EVersionExcel.E2016;
            dataGridView1.DataSource = null;
            Task.Run(()=> {
                try
                {

               
            var readerInfo = _excelReader.GetDataToList<Class1>(txtPath.Text, "Sheet1", _eVersionExcel);
            if (readerInfo != null)
            {
                if (readerInfo.Status)
                {
                            var lisst = new List<ITEM_INFO>();
                            foreach (var item in readerInfo.Data)
                            {
                                if (!string.IsNullOrWhiteSpace(item.ITEM_ID))
                                {
                                    var info = new ITEM_INFO();
                                    info.ITEM_ID = item.ITEM_ID;
                                    info.ASSET_TYPE = item.ASSET_TYPE;
                                    info.ITEM_CODE = item.ITEM_CODE;
                                    info.ITEM_NAME = item.ITEM_NAME;
                                    info.PURC_NAME = item.PURC_NAME;
                                    info.MAIN_DRAWING_NO = item.MAIN_DRAWING_NO;
                                    info.DRAWING_NO = item.DRAWING_NO;
                                    info.BUSINESS_LCODE = item.BUSINESS_LCODE;
                                    info.PRODUCT_FAMILY = item.PRODUCT_FAMILY;
                                    info.PRODUCT_TYPE = item.PRODUCT_TYPE;
                                    info.PIN = item.PIN;
                                    info.TYPE = item.TYPE;
                                    info.PITCH = item.PITCH;
                                    info.PART_TYPE = item.PART_TYPE;
                                    info.PILLAR = item.PILLAR;
                                    info.AREA = item.AREA;
                                    info.REVISION = item.REVISION;
                                    info.REVISION_PROD = item.REVISION_PROD;
                                    info.DEVICE_GROUP = item.DEVICE_GROUP;
                                    info.COMPO_FLAG = item.COMPO_FLAG;
                                    info.MAIN_SITE = item.MAIN_SITE;
                                    info.SC_PROCESS = item.SC_PROCESS;
                                    info.THICKNESS = item.THICKNESS;
                                    info.MIX_CODE = item.MIX_CODE;
                                    info.UNIT = item.UNIT;
                                    info.PARA_QTY = item.PARA_QTY;
                                    info.JIG_YN = item.JIG_YN;
                                    info.MATERIAL_CODE = item.MATERIAL_CODE;
                                    info.MEASUREMENT = item.MEASUREMENT;
                                    info.CUST_LTYPE = item.CUST_LTYPE;
                                    info.SPECIAL_ITEM_YN = item.SPECIAL_ITEM_YN;
                                    info.SPECIAL_REMARK = item.SPECIAL_REMARK;
                                    info.SEND_DRAWING = item.SEND_DRAWING;
                                    info.DRAWING_PDF_URL = item.DRAWING_PDF_URL;
                                    info.DRAWING_DWG_URL = item.DRAWING_DWG_URL;
                                    info.DRAWING_3DPDF_URL = item.DRAWING_3DPDF_URL;
                                    info.BOM_APPROVAL = item.BOM_APPROVAL;
                                    info.BOM_APPROVER = item.BOM_APPROVER;
                                    info.BOM_APPROVAL_DATE = item.BOM_APPROVAL_DATE;
                                    info.PURC_TYPE1 = item.PURC_TYPE1;
                                    info.REQ_YN = item.REQ_YN;
                                    info.USE_YN = item.USE_YN;
                                    info.REMARK = item.REMARK;
                                    info.P_CODE = item.P_CODE;
                                    info.MODY_DATE = item.MODY_DATE;
                                    info.MODY_USERID = item.MODY_USERID;
                                    info.MODY_IP_ADDR = item.MODY_IP_ADDR;


                                    if (info.BOM_APPROVAL_DATE == DateTime.MinValue) info.BOM_APPROVAL_DATE = null;
                                    if (info.MODY_DATE == DateTime.MinValue) info.MODY_DATE = null;
                                    lisst.Add(info);
                                }
                            }

                            //var dataa = readerInfo.Datas as DataTable;

                            var modelEntityType = typeof(ITEM_INFO);

                            // Retrieve a TableAttribute that is assigned to the class generated by LINQ to SQL (MSLinqToSQLGenerator)

                            // Retrieve all properties with ColumnAttribute
                            //var columnProperties = modelEntityType.GetProperties().Where(p => p.GetCustomAttributes(typeof(ColumnAttribute), false).Any());
                            // Retrieve all properties with ColumnAttribute
                            var columnProperties = modelEntityType.GetProperties().Where(p => p.GetCustomAttributes(typeof(System.Data.Linq.Mapping.ColumnAttribute), false).Any());

                            // Create a DataTable and initialize columns
                            var dataTable = new DataTable("ITEM_INFO");
                            foreach (var columnProperty in columnProperties)
                            {
                                var type = columnProperty.PropertyType;

                                if ((type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                                {
                                    var underlyingType = Nullable.GetUnderlyingType(columnProperty.PropertyType);
                                    var column = new DataColumn(columnProperty.Name, underlyingType);
                                    dataTable.Columns.Add(column);
                                }
                                else
                                {
                                    dataTable.Columns.Add(new DataColumn(columnProperty.Name, type));
                                }
                            }

                            // Add data (rows) to the created DataTable
                            foreach (var modelEntity in lisst)
                            {
                                var values = columnProperties.Select(p => p.GetValue(modelEntity, null)).ToArray();

                                dataTable.Rows.Add(values);
                            }

                            var watch = System.Diagnostics.Stopwatch.StartNew();
                            using (SqlConnection sqlConnection = new SqlConnection(connectString_MES_VINA))
                            {
                                try
                                {
                                    sqlConnection.Open();

                                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.TableLock, null)
                                    {
                                        DestinationTableName = "ITEM_INFO"
                                    };
                                    sqlBulkCopy.WriteToServer(dataTable);
                                    //return new ResultInfo() { Status = true, Record = dataTable.Rows.Count };
                                }
                                catch (Exception ex)
                                {
                                    //Log.SError(this.GetType().Name, ex.Message, ex.StackTrace, System.Reflection.MethodBase.GetCurrentMethod().Name);
                                    //return new ResultInfo() { Status = false, ErrorMessage = ex.Message };
                                }
                                finally
                                {
                                    sqlConnection.Close();
                                }
                                watch.Stop();
                            }


                            Console.WriteLine($"Execution Time: {watch.ElapsedMilliseconds} ms");
                         
                    //OnBulkInsert(infos);
                   // dataGridView1.DataSource = readerInfo.Data;
                }
                else
                {
                    MessageBox.Show(readerInfo.Message);
                }
            }
                }
                catch (Exception)
                {

                }
            });
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

        }

        public static string connectString_MES_VINA = "Data Source=192.168.100.11;Initial Catalog=MES_VINA;User ID=thlone;Password=123456789a;MultipleActiveResultSets=true;"; 

        public virtual void OnBulkInsert(DataTable dataTable)
        {
            //var result = new ResultInfo();
            try
            {
                if ((dataTable == null))
                {
                    //return result;
                }
                var modelEntityType = typeof(ITEM_INFO);

                // Retrieve a TableAttribute that is assigned to the class generated by LINQ to SQL (MSLinqToSQLGenerator)
                var tableAttribute = modelEntityType.GetCustomAttributes(typeof(TableAttribute), false)
                        .Cast<TableAttribute>()
                        .Single();

                // Retrieve all properties with ColumnAttribute
                var columnProperties = modelEntityType.GetProperties().Where(p => p.GetCustomAttributes(typeof(ColumnAttribute), false).Any());

                // Bulk insert data
                using (SqlConnection sqlConnection = new SqlConnection(connectString_MES_VINA))
                {
                    try
                    {
                        sqlConnection.Open();

                        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.TableLock, null)
                        {
                            DestinationTableName = tableAttribute.Name
                        };
                        sqlBulkCopy.WriteToServer(dataTable);
                        //return new ResultInfo() { Status = true, Record = dataTable.Rows.Count };
                    }
                    catch (Exception ex)
                    {
                        //Log.SError(this.GetType().Name, ex.Message, ex.StackTrace, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        //return new ResultInfo() { Status = false, ErrorMessage = ex.Message };
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.SError(this.GetType().Name, ex.Message, ex.StackTrace, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //return new ResultInfo() { Status = false, ErrorMessage = ex.Message };
            }
            //return result;
        }

        public virtual void OnBulkInsert(List<Class1> entities)
        {
            try
            {
                if ((entities == null) || (!entities.Any()))
                {
                    //return result;
                }

                var modelEntityType = typeof(ITEM_INFO);

                // Retrieve a TableAttribute that is assigned to the class generated by LINQ to SQL (MSLinqToSQLGenerator)
                var tableAttribute = modelEntityType.GetCustomAttributes(typeof(TableAttribute), false)
                        .Cast<TableAttribute>()
                        .Single();

                // Retrieve all properties with ColumnAttribute
                var columnProperties = modelEntityType.GetProperties().Where(p => p.GetCustomAttributes(typeof(ColumnAttribute), false).Any());

                // Create a DataTable and initialize columns
                var dataTable = new DataTable(tableAttribute.Name);
                foreach (var columnProperty in columnProperties)
                {
                    var type = columnProperty.PropertyType;

                    if ((type.IsGenericType) && (type.GetGenericTypeDefinition() == typeof(Nullable<>)))
                    {
                        var underlyingType = Nullable.GetUnderlyingType(columnProperty.PropertyType);
                        var column = new DataColumn(columnProperty.Name, underlyingType);
                        dataTable.Columns.Add(column);
                    }
                    else
                    {
                        dataTable.Columns.Add(new DataColumn(columnProperty.Name, type));
                    }
                }

                // Add data (rows) to the created DataTable
                foreach (var modelEntity in entities)
                {
                    var values = columnProperties.Select(p => p.GetValue(modelEntity, null)).ToArray();

                    dataTable.Rows.Add(values);
                }

                // Bulk insert data
                using (SqlConnection sqlConnection = new SqlConnection(connectString_MES_VINA))
                {
                    try
                    {
                        sqlConnection.Open();
                        using (SqlTransaction transaction = sqlConnection.BeginTransaction())
                        {
                            try
                            {
                                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.TableLock, null)
                                {
                                    DestinationTableName = tableAttribute.Name
                                };
                                sqlBulkCopy.WriteToServer(dataTable);

                                transaction.Commit();
                            }
                            catch (Exception ex)
                            {
                                transaction.Rollback();
                            }
                        }
                        // return new ResultInfo() { Status = true, Record = dataTable.Rows.Count };
                    }
                    catch (Exception ex)
                    {
                        // Log.SError(this.GetType().Name, ex.Message, ex.StackTrace, System.Reflection.MethodBase.GetCurrentMethod().Name);
                        // return new ResultInfo() { Status = false, ErrorMessage = ex.Message };
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //Log.SError(this.GetType().Name, ex.Message, ex.StackTrace, System.Reflection.MethodBase.GetCurrentMethod().Name);
                //return new ResultInfo() { Status = false, ErrorMessage = ex.Message };
            }
            // return result;
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
