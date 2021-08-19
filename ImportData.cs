using nsFramework.Common.Helper;
using Share.Widget.DotNetBar.ViewUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ImportData : Form
    {
        private ExcelReader _excelReader = new ExcelReader();
        private FrmLoading _frmLoading;
        private SynchronizationContext _context;
        public ImportData()
        {
            InitializeComponent();
            _context = SynchronizationContext.Current;
            this.Load += ImportData_Load;
        }

        private void ImportData_Load(object sender, EventArgs e)
        {
            cboVersion.DataSource = Enum.GetValues(typeof(EVersionExcel));
        }

        private void StartWaiting(string mess)
        {
            _frmLoading = new FrmLoading();
            new Thread(new ThreadStart(delegate { _frmLoading.SetText(mess); _frmLoading.ShowDialog(); })).Start();
        }
        private void StopWaiting()
        {
            _context.Send(state => {
                try
                {
                    _frmLoading.Close();
                }
                catch (Exception)
                {
                }
            }, null);
        }
        private void btnReadFile_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            var eVersionExcel = (EVersionExcel)cboVersion.SelectedValue;
            var path = txtPath.Text;
            var sheet = txtSheetName.Text;
            StartWaiting("Waiting");
            Task.Factory.StartNew(()=>{
                try
                {
                    var readerInfo = _excelReader.GetData(path, sheet, eVersionExcel);
                    if (readerInfo != null)
                    {
                        _context.Send(state => {
                            try
                            {
                                if (readerInfo.Status)
                                {
                                    dataGridView1.DataSource = readerInfo.Datas;
                                }
                                else
                                {
                                    MessageBox.Show(readerInfo.Message);
                                }
                                StopWaiting();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        },null);
                        
                    }
                }
                catch (Exception)
                {
                    _context.Send(state => {
                        try
                        {
                           
                            StopWaiting();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                    }, null);
                }
            });
           
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            var dataTable = dataGridView1.DataSource as DataTable;
            if(dataTable != null)
            {
                var watch = System.Diagnostics.Stopwatch.StartNew();
                using (SqlConnection sqlConnection = new SqlConnection(txtConnectString.Text))
                {
                    try
                    {
                        sqlConnection.Open();
                        SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.TableLock, null)
                        {
                            DestinationTableName = txtTableName.Text
                        };
                        sqlBulkCopy.WriteToServer(dataTable);
                        //return new ResultInfo() { Status = true, Record = dataTable.Rows.Count };
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        //return new ResultInfo() { Status = false, ErrorMessage = ex.Message };
                    }
                    finally
                    {
                        sqlConnection.Close();
                    }
                    watch.Stop();
                }


                MessageBox.Show($"Execution Time: {watch.ElapsedMilliseconds} ms");
            }
        }
        public static void ConvertColumnType111(DataTable dt, string columnName, Type newType, bool Isnull)
        {
            try
            {
                using (DataColumn dc = new DataColumn(columnName + "_new", newType))
                {
                    // Add the new column which has the new type, and move it to the ordinal of the old column
                    int ordinal = dt.Columns[columnName].Ordinal;
                    dc.AllowDBNull = Isnull;
                    dc.Expression = $"IIF(LEN(TRIM({columnName})) > 0,Convert({columnName}, '{newType.FullName}'),NUll)";
                    dt.Columns.Add(dc);
                    dc.SetOrdinal(ordinal);


                    // Get and convert the values of the old column, and insert them into the new
                    //foreach (DataRow dr in dt.Rows)
                    //    dr[dc.ColumnName] = Convert.ChangeType(dr[columnName], newType);
                    var table = dt.Clone();
                    table.AcceptChanges();
                    // Remove the old column
                    table.Columns.Remove(columnName);

                    // Give the new column the old column's name
                    dc.ColumnName = columnName;
                }
            }
            catch (Exception ex)
            {

            }
            
        }
        public static void ConvertColumnType(DataTable dt, string columnName, Type newType, bool Isnull)
        {
            using (DataColumn dc = new DataColumn(columnName + "_new", newType))
            {
                // Add the new column which has the new type, and move it to the ordinal of the old column
                int ordinal = dt.Columns[columnName].Ordinal;
                dt.Columns.Add(dc);
                dc.SetOrdinal(ordinal);

                // Get and convert the values of the old column, and insert them into the new
                foreach (DataRow dr in dt.Rows)
                {
                    try
                    {
                        if (string.IsNullOrWhiteSpace("" + dr[columnName]))
                        {
                            dr[dc.ColumnName] = DBNull.Value;
                        }
                        else
                        {
                            dr[dc.ColumnName] = Convert.ChangeType(dr[columnName], newType);
                        }
                    }
                    catch (Exception)
                    {

                       
                    }
                }
                   // dr[dc.ColumnName] = Convert.ChangeType(dr[columnName], newType);

                // Remove the old column
                dt.Columns.Remove(columnName);

                // Give the new column the old column's name
                dc.ColumnName = columnName;
            }
        }
    



    private void btnCheck_Click(object sender, EventArgs e)
        {
            var table = dataGridView1.DataSource as DataTable;
            if(table != null)
            {
                StartWaiting("Waiting");
                Task.Factory.StartNew(() => {
                    try
                    {
                        using (SqlConnection connection = new SqlConnection(txtConnectString.Text))
                        {
                            using (SqlCommand cmd = connection.CreateCommand())
                            {
                                using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                                {
                                    cmd.CommandText = $"SELECT * From {txtTableName.Text} where 1=0";
                                    cmd.CommandType = CommandType.Text;
                                    connection.Open();
                                    var datatable = new DataTable();
                                    sda.Fill(datatable);
                                    var sb = table.Clone();
                                    foreach (DataColumn item in sb.Columns)
                                    {
                                        //item.DataType = datatable.Columns[item.ColumnName].DataType;
                                        if (item.DataType != datatable.Columns[item.ColumnName].DataType)
                                        {
                                            ConvertColumnType(table, item.ColumnName, datatable.Columns[item.ColumnName].DataType, datatable.Columns[item.ColumnName].AllowDBNull);
                                        }

                                    }

                                }
                            }
                        }
                        //_context.Send(state => {
                        //    try
                        //    {
                        //        dataGridView1.DataSource = null;
                        //        dataGridView1.DataSource = table;
                        //    }
                        //    catch (Exception)
                        //    {

                              
                        //    }
                        
                        //}, null);
                        
                    }
                    catch (Exception ex)
                    {
                                
                        _context.Send(state => {
                            try
                            {
                                MessageBox.Show(ex.Message);
                            }
                            catch (Exception)
                            {


                            }

                        }, null);
                    }
                    finally
                    {
                        _context.Send(state => {
                            try
                            {
                                StopWaiting();
                                MessageBox.Show("Ok");
                               
                            }
                            catch (Exception)
                            {


                            }

                        }, null);
                    }
                
                
                });
            }
            
        }
    }
}
