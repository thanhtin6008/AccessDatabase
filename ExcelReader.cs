
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using WindowsFormsApp1;

namespace nsFramework.Common.Helper
{
    public class ExcelReaderInfo<T>
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public IList<T> Data { get; set; }
        public object Datas { get; set; }
        public ExcelReaderInfo()
        {
            Status = false;
        }
    }
    public enum EVersionExcel { E2003, E2007, E2010, E2013, E2015, E2016, E2018 }
    public class ExcelReader
    {
        public string GetConnectString(EVersionExcel eVersionExcel, string filePath)
        {
            string _result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 8.0;", filePath);
            switch (eVersionExcel)
            {
                case EVersionExcel.E2007:
                case EVersionExcel.E2010:
                case EVersionExcel.E2013:
                case EVersionExcel.E2015:
                case EVersionExcel.E2016:
                case EVersionExcel.E2018:
                    _result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", filePath);
                    break;
                case EVersionExcel.E2003:
                default:
                    _result = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 8.0;", filePath);
                    break;
            }
            return _result;
        }

        public const string SheefName = "ItemTemplate";
        public string OpenShowDialog()
        {
            String _result = "";
            try
            {
                OpenFileDialog fileDialog = new OpenFileDialog();
                if (fileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    return _result = fileDialog.FileName;
                }
            }
            catch
            {
                DialogResult messageResult = MessageBox.Show("không thể truy cập file.");
            }
            return _result;
        }
        public IList<T> GetDataToList<T>(string filePath) where T : new()
        {
            List<T> _listInfo = new List<T>();
            var fileName = string.Format(filePath);
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", fileName);

            var adapter = new OleDbDataAdapter("SELECT * FROM [" + SheefName + "$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "hardcatAssets");
            DataTable data = ds.Tables["hardcatAssets"];

            foreach (DataRow row in data.Rows)
            {
                T info = new T();
                int columIndex = 0;
                foreach (DataColumn item in data.Columns)
                {
                    PropertyInfo propertyInfos = info.GetType().GetProperty(item.ColumnName);
                    if (propertyInfos != null)
                    {
                        if (propertyInfos.PropertyType == typeof(DateTime) || propertyInfos.PropertyType == typeof(Nullable<DateTime>))
                        {
                            propertyInfos.SetValue(info, convertToDateTime(row.ItemArray[columIndex]), null);
                        }

                        if (propertyInfos.PropertyType == typeof(Guid) || propertyInfos.PropertyType == typeof(Nullable<Guid>))
                        {
                            propertyInfos.SetValue
                            (info, convertToGuid(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int) || propertyInfos.PropertyType == typeof(Nullable<int>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToInt(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long) || propertyInfos.PropertyType == typeof(Nullable<long>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToLong(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal) || propertyInfos.PropertyType == typeof(Nullable<decimal>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToDecimal(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Nullable<Boolean>) || propertyInfos.PropertyType == typeof(System.Boolean))
                        {
                            propertyInfos.SetValue(info, ConvertToBoolean(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(byte[]))
                        {
                            propertyInfos.SetValue(info, convertToByteArray(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(double))
                        {
                            propertyInfos.SetValue(info, ConvertToDouble(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (row.ItemArray[columIndex].GetType() == typeof(DateTime) || row.ItemArray[columIndex].GetType() == typeof(Nullable<DateTime>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToDateString(row.ItemArray[columIndex]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (info, ConvertToString(row.ItemArray[columIndex]), null);
                            }
                        }

                    }
                    columIndex++;
                }
                _listInfo.Add(info);
            }

            return _listInfo;
        }
        public IList<T> GetDataToList<T>(string filePath, EVersionExcel eVersionExcel) where T : new()
        {
            List<T> _listInfo = new List<T>();
            var fileName = string.Format(filePath);
            var connectionString = GetConnectString(eVersionExcel, filePath);

            var adapter = new OleDbDataAdapter("SELECT * FROM [" + SheefName + "$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "hardcatAssets");
            DataTable data = ds.Tables["hardcatAssets"];

            foreach (DataRow row in data.Rows)
            {
                T info = new T();
                int columIndex = 0;
                foreach (DataColumn item in data.Columns)
                {
                    PropertyInfo propertyInfos = info.GetType().GetProperty(item.ColumnName);
                    if (propertyInfos != null)
                    {
                        if (propertyInfos.PropertyType == typeof(DateTime) || propertyInfos.PropertyType == typeof(Nullable<DateTime>))
                        {
                            propertyInfos.SetValue(info, convertToDateTime(row.ItemArray[columIndex]), null);
                        }

                        if (propertyInfos.PropertyType == typeof(Guid) || propertyInfos.PropertyType == typeof(Nullable<Guid>))
                        {
                            propertyInfos.SetValue
                            (info, convertToGuid(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int) || propertyInfos.PropertyType == typeof(Nullable<int>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToInt(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long) || propertyInfos.PropertyType == typeof(Nullable<long>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToLong(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal) || propertyInfos.PropertyType == typeof(Nullable<decimal>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToDecimal(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Nullable<Boolean>) || propertyInfos.PropertyType == typeof(System.Boolean))
                        {
                            propertyInfos.SetValue(info, ConvertToBoolean(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(byte[]))
                        {
                            propertyInfos.SetValue(info, convertToByteArray(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(double))
                        {
                            propertyInfos.SetValue(info, ConvertToDouble(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (row.ItemArray[columIndex].GetType() == typeof(DateTime) || row.ItemArray[columIndex].GetType() == typeof(Nullable<DateTime>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToDateString(row.ItemArray[columIndex]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (info, ConvertToString(row.ItemArray[columIndex]), null);
                            }
                        }

                    }
                    columIndex++;
                }
                _listInfo.Add(info);
            }

            return _listInfo;
        }
        public IList<T> GetDataToList<T>(string filePath, string sheetname) where T : new()
        {
            List<T> _listInfo = new List<T>();
            var fileName = string.Format(filePath);
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Extended Properties=Excel 12.0;", fileName);

            var adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetname + "$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "hardcatAssets");
            DataTable data = ds.Tables["hardcatAssets"];

            foreach (DataRow row in data.Rows)
            {
                T info = new T();
                int columIndex = 0;
                foreach (DataColumn item in data.Columns)
                {
                    PropertyInfo propertyInfos = info.GetType().GetProperty(item.ColumnName);
                    if (propertyInfos != null)
                    {
                        if (propertyInfos.PropertyType == typeof(DateTime) || propertyInfos.PropertyType == typeof(Nullable<DateTime>))
                        {
                            propertyInfos.SetValue(info, convertToDateTime(row.ItemArray[columIndex]), null);
                        }

                        if (propertyInfos.PropertyType == typeof(Guid) || propertyInfos.PropertyType == typeof(Nullable<Guid>))
                        {
                            propertyInfos.SetValue
                            (info, convertToGuid(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(int) || propertyInfos.PropertyType == typeof(Nullable<int>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToInt(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(long) || propertyInfos.PropertyType == typeof(Nullable<long>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToLong(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(decimal) || propertyInfos.PropertyType == typeof(Nullable<decimal>))
                        {
                            propertyInfos.SetValue
                            (info, ConvertToDecimal(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(Nullable<Boolean>) || propertyInfos.PropertyType == typeof(System.Boolean))
                        {
                            propertyInfos.SetValue(info, ConvertToBoolean(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(byte[]))
                        {
                            propertyInfos.SetValue(info, convertToByteArray(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(double))
                        {
                            propertyInfos.SetValue(info, ConvertToDouble(row.ItemArray[columIndex]), null);
                        }
                        else if (propertyInfos.PropertyType == typeof(String))
                        {
                            if (row.ItemArray[columIndex].GetType() == typeof(DateTime) || row.ItemArray[columIndex].GetType() == typeof(Nullable<DateTime>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToDateString(row.ItemArray[columIndex]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (info, ConvertToString(row.ItemArray[columIndex]), null);
                            }
                        }

                    }
                    columIndex++;
                }
                _listInfo.Add(info);
            }

            return _listInfo;
        }
        public ExcelReaderInfo<T> GetDataToList<T>(string filePath, string sheetname, EVersionExcel eVersionExcel) where T : new()
        {
            ExcelReaderInfo<T> _excelReaderInfo = new ExcelReaderInfo<T>();
            try
            {
                var connectionString = GetConnectString(eVersionExcel, filePath);

                var adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetname + "$]", connectionString);
                var ds = new DataSet();

                adapter.Fill(ds, "hardcatAssets");
                DataTable data = ds.Tables["hardcatAssets"];
                List<T> list = new List<T>();
                if (data == null)
                {
                    _excelReaderInfo.Data = list;
                    _excelReaderInfo.Status = true;
                    return _excelReaderInfo;
                }
                if (data.Rows.Count == 0)
                {
                    _excelReaderInfo.Data = list;
                    _excelReaderInfo.Status = true;
                    return _excelReaderInfo;
                }
                foreach (DataRow row in data.Rows)
                {
                    T info = new T();
                    bool isEmpty = false;
                    foreach (DataColumn item in data.Columns)
                    {
                        PropertyInfo propertyInfos = info.GetType().GetProperty(item.ColumnName);
                        if (propertyInfos != null)
                        {
                            if (("" + row[item.ColumnName]).Length > 0) isEmpty = true;
                            var propertyType = Nullable.GetUnderlyingType(propertyInfos.PropertyType) ?? propertyInfos.PropertyType;
                            if (propertyType == typeof(DateTime) || propertyInfos.PropertyType == typeof(Nullable<DateTime>))
                            {
                                propertyInfos.SetValue(info, convertToDateTime(row[item.ColumnName]), null);
                            }

                            if (propertyType == typeof(Guid) || propertyInfos.PropertyType == typeof(Nullable<Guid>))
                            {
                                propertyInfos.SetValue(info, convertToGuid(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(int) || propertyInfos.PropertyType == typeof(Nullable<int>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToInt(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(long) || propertyInfos.PropertyType == typeof(Nullable<long>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToLong(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(decimal) || propertyInfos.PropertyType == typeof(Nullable<decimal>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToDecimal(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(Nullable<Boolean>) || propertyInfos.PropertyType == typeof(System.Boolean))
                            {
                                propertyInfos.SetValue(info, ConvertToBoolean(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(byte[]))
                            {
                                propertyInfos.SetValue(info, convertToByteArray(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(double) || propertyInfos.PropertyType == typeof(System.Double))
                            {
                                propertyInfos.SetValue(info, ConvertToDouble(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(String))
                            {
                                if (row[item.ColumnName].GetType() == typeof(DateTime) || row[item.ColumnName].GetType() == typeof(Nullable<DateTime>))
                                {
                                    propertyInfos.SetValue
                                    (info, ConvertToDateString(row[item.ColumnName]), null);
                                }
                                else
                                {
                                    propertyInfos.SetValue(info, ConvertToString(row[item.ColumnName]), null);
                                }
                            }

                        }
                    }
                    if (isEmpty) list.Add(info);

                }
                _excelReaderInfo.Data = list;
                _excelReaderInfo.Status = true;
            }
            catch (Exception ex)
            {
                _excelReaderInfo.Status = false;
                _excelReaderInfo.Message = ex.Message;
            }
            return _excelReaderInfo;
        }
        public ExcelReaderInfo<DataTable> GetData(string filePath, string sheetname, EVersionExcel eVersionExcel)
        {
            ExcelReaderInfo<DataTable> _excelReaderInfo = new ExcelReaderInfo<DataTable>();
            try
            {
                var connectionString = GetConnectString(eVersionExcel, filePath);

                var adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetname + "$]", connectionString);
                var ds = new DataSet();

                adapter.Fill(ds, "hardcatAssets");
                DataTable data = ds.Tables["hardcatAssets"];
                _excelReaderInfo.Datas = data;
                _excelReaderInfo.Status = true;
            }
            catch (Exception ex)
            {
                _excelReaderInfo.Status = false;
                _excelReaderInfo.Message = ex.Message;
            }
            return _excelReaderInfo;
        }
        public ExcelReaderInfo<T> GetDataToList1<T>( string filePath, EVersionExcel eVersionExcel) where T : new()
        {
            ExcelReaderInfo<T> _excelReaderInfo = new ExcelReaderInfo<T>();
            try
            {
                var connectionString = GetConnectString(eVersionExcel, filePath);
                string sheetname = "";
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    sheetname = dtSchema.Rows[0]["TABLE_NAME"] + "";
                }
                var adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetname + "]", connectionString);
                var ds = new DataSet();

                adapter.Fill(ds, "hardcatAssets");
                DataTable data = ds.Tables["hardcatAssets"];
                List<T> list = new List<T>();
                if (data == null)
                {
                    _excelReaderInfo.Data = list;
                    _excelReaderInfo.Status = true;
                    return _excelReaderInfo;
                }
                if (data.Rows.Count == 0)
                {
                    _excelReaderInfo.Data = list;
                    _excelReaderInfo.Status = true;
                    return _excelReaderInfo;
                }
                foreach (DataRow row in data.Rows)
                {
                    T info = new T();
                    bool isEmpty = false;
                    foreach (DataColumn item in data.Columns)
                    {
                        PropertyInfo propertyInfos = info.GetType().GetProperty(item.ColumnName);
                        if (propertyInfos != null)
                        {
                            if (("" + row[item.ColumnName]).Length > 0) isEmpty = true;
                            var propertyType = Nullable.GetUnderlyingType(propertyInfos.PropertyType) ?? propertyInfos.PropertyType;
                            if (propertyType == typeof(DateTime) || propertyInfos.PropertyType == typeof(Nullable<DateTime>))
                            {
                                propertyInfos.SetValue(info, convertToDateTime(row[item.ColumnName]), null);
                            }

                            if (propertyType == typeof(Guid) || propertyInfos.PropertyType == typeof(Nullable<Guid>))
                            {
                                propertyInfos.SetValue(info, convertToGuid(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(int) || propertyInfos.PropertyType == typeof(Nullable<int>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToInt(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(long) || propertyInfos.PropertyType == typeof(Nullable<long>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToLong(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(decimal) || propertyInfos.PropertyType == typeof(Nullable<decimal>))
                            {
                                propertyInfos.SetValue
                                (info, ConvertToDecimal(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(Nullable<Boolean>) || propertyInfos.PropertyType == typeof(System.Boolean))
                            {
                                propertyInfos.SetValue(info, ConvertToBoolean(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(byte[]))
                            {
                                propertyInfos.SetValue(info, convertToByteArray(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(double) || propertyInfos.PropertyType == typeof(System.Double))
                            {
                                propertyInfos.SetValue(info, ConvertToDouble(row[item.ColumnName]), null);
                            }
                            else if (propertyType == typeof(String))
                            {
                                if (row[item.ColumnName].GetType() == typeof(DateTime) || row[item.ColumnName].GetType() == typeof(Nullable<DateTime>))
                                {
                                    propertyInfos.SetValue
                                    (info, ConvertToDateString(row[item.ColumnName]), null);
                                }
                                else
                                {
                                    propertyInfos.SetValue(info, ConvertToString(row[item.ColumnName]), null);
                                }
                            }

                        }
                    }
                    if (isEmpty) list.Add(info);

                }
                _excelReaderInfo.Data = list;
                _excelReaderInfo.Status = true;
            }
            catch (Exception ex)
            {
                _excelReaderInfo.Status = false;
                _excelReaderInfo.Message = ex.Message;
            }
            return _excelReaderInfo;
        }

        private void OpenFile(string pPath)
        {
            try
            {
                string _path = pPath;
                if (System.IO.File.Exists(_path) == true)
                {
                    System.Diagnostics.Process.Start(_path);
                }
                else
                {
                    DialogResult messageResult = MessageBox.Show("Không tìm thấy File.");
                }
            }
            catch (Exception)
            {

                DialogResult messageResult = MessageBox.Show("không thể truy cập file.");
            }

        }
        //**-------------------------------------------------------------------------------------
        public static void ConvertEVersionExcel(EVersionExcel eVersionExcel, out String st)
        {
            st = "";
            switch (eVersionExcel)
            {
                case EVersionExcel.E2003:
                    st = "Excel 2003";
                    break;
                case EVersionExcel.E2007:
                    st = "Excel 2007";
                    break;
                case EVersionExcel.E2010:
                    st = "Excel 2010";
                    break;
                case EVersionExcel.E2013:
                    st = "Excel 2013";
                    break;
                case EVersionExcel.E2015:
                    st = "Excel 2015";
                    break;
                case EVersionExcel.E2016:
                    st = "Excel 2016";
                    break;
                case EVersionExcel.E2018:
                    st = "Excel 2018";
                    break;
                default:
                    st = "Excel 2003";
                    break;
            }
        }
        public static EVersionExcel ToEVersionExcel(String st)
        {
            switch (st)
            {
                case "E2003":
                    return EVersionExcel.E2003;
                case "E2007":
                    return EVersionExcel.E2007;
                case "E2010":
                    return EVersionExcel.E2010;
                case "E2013":
                    return EVersionExcel.E2013;
                case "E2015":
                    return EVersionExcel.E2015;
                case "E2016":
                    return EVersionExcel.E2016;
                case "E2018":
                    return EVersionExcel.E2018;
                default:
                    return EVersionExcel.E2003;
            }
        }
        //**-------------------------------------------------------------------------------------
        public DialogInfo SaveAsPathExcel()
        {
            DialogInfo _result = null;
            try
            {
                using (var dialog = new System.Windows.Forms.SaveFileDialog())
                {
                    dialog.DefaultExt = "*.xlsx";
                    dialog.Filter = "xlsx (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        FileInfo file = new FileInfo(dialog.FileName);
                        _result = new DialogInfo();
                        _result.FileName = file.Name;
                        _result.FullPath = dialog.FileName;
                        _result.PathFile = file.DirectoryName + @"\\";

                        return _result;
                    }
                }
            }
            catch (Exception)
            {
                DialogResult messageResult = MessageBox.Show("không thể mở dialog.");
            }
            return _result;
        }
        private string SaveAsPath()
        {
            string _pathFile = "";
            try
            {
                using (var dialog = new System.Windows.Forms.SaveFileDialog())
                {
                    dialog.DefaultExt = "*.xls";
                    dialog.Filter = "Excel (*.xls)|*.xls|All files (*.*)|*.*";
                    DialogResult result = dialog.ShowDialog();
                    if (result == DialogResult.OK)
                    {
                        _pathFile = dialog.FileName;
                    }
                }
            }
            catch (Exception)
            {
                DialogResult messageResult = MessageBox.Show("không thể mở dialog.");
            }
            return _pathFile;
        }
        private string SplitCamelCase(string input)
        {
            return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
        }
        private string ConvertToDateString(object date)
        {
            return date == null ? string.Empty : Convert.ToDateTime(date).ToString();
        }

        private string ConvertToString(object value)
        {
            return Convert.ToString(ReturnEmptyIfNull(value));

        }

        private int ConvertToInt(object value)
        {
            return Convert.ToInt32(ReturnZeroIfNull(value));
        }

        private double ConvertToDouble(object value)
        {
            return Convert.ToDouble(ReturnZeroIfNull(value));
        }

        private long ConvertToLong(object value)
        {
            return Convert.ToInt64(ReturnZeroIfNull(value));
        }

        private decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(ReturnZeroIfNull(value));
        }

        private Boolean? ConvertToBoolean(object value)
        {
            try
            {
                return Convert.ToBoolean(value);
            }
            catch (Exception)
            {
                return false;

            }

        }

        private Guid convertToGuid(object value)
        {
            if (value != null)
            {
                if (value.ToString().Length > 0)
                    return new Guid("" + value);
                else
                    return Guid.Empty;
            }
            return Guid.Empty;
        }

        private DateTime? convertToDateTime(object date)
        {
            try
            {
                return Convert.ToDateTime(ReturnDateTimeMinIfNull(date));
            }
            catch (Exception)
            {
                return null;
            }

        }

        private byte[] convertToByteArray(object value)
        {
            if (value != DBNull.Value)
            {
                return (byte[])value;
            }
            return null;
        }

        public object ReturnZeroIfNull(object value)
        {
            if (value == "") return 0;
            if (value == DBNull.Value)
                return 0;
            if (value == null)
                return 0;
            return value;
        }

        public object ReturnEmptyIfNull(object value)
        {
            if (value == DBNull.Value)
                return string.Empty;
            if (value == null)
                return string.Empty;
            return value;
        }

        public object ReturnFalseIfNull(object value)
        {
            if (value == DBNull.Value)
                return false;
            if (value == null)
                return false;
            return value;
        }

        public object ReturnDateTimeMinIfNull(object value)
        {
            if (value == "") return DateTime.MinValue;
            if (value == DBNull.Value)
                return DateTime.MinValue;
            if (value == null)
                return DateTime.MinValue;
            return value;
        }

        public object ReturnNullIfDbNull(object value)
        {
            if (value == DBNull.Value)
                return '\0';
            if (value == null)
                return '\0';
            return value;
        }

    }

}