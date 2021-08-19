using System;
using System.Collections.Generic;
using System.Text;

namespace WinFormsApp1.Tables
{
    public class MappingInfo
    {
        public int NO { get; set; }
        public string PK { get; set; }
        public string MESFieldID { get; set; }
        public string FieldNameEng { get; set; }
        public string FieldNameKor { get; set; }
        public string RelationtableSAPB1 { get; set; }
        public string Type { get; set; }
        public string Length { get; set; }
        public string DefaultValue { get; set; }
        public string require { get; set; }
        public string NotNull { get; set; }
        public string KEY { get; set; }
        public string SAPFieldName { get; set; }
        public string SAPFieldNameEng { get; set; }
        public string SAPFieldNameKor { get; set; }
        public string SAPType { get; set; }
        public string SAPLength { get; set; }
        public string SAPKEY { get; set; }
        public string Remark { get; set; }
        public string Version { get; set; }
    }
}
