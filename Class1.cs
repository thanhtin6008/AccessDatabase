using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Class1
    {
        #region Member

        [MaxLength(6)]
        public string ITEM_ID { get; set; }
        public string ASSET_TYPE { get; set; }
        public string ITEM_CODE { get; set; }
        public string ITEM_NAME { get; set; }
        public string PURC_NAME { get; set; }
        public string MAIN_DRAWING_NO { get; set; }
        public string DRAWING_NO { get; set; }
        public string BUSINESS_LCODE { get; set; }
        public string PRODUCT_FAMILY { get; set; }
        public string PRODUCT_TYPE { get; set; }
        public string PIN { get; set; }
        public string TYPE { get; set; }
        public string PITCH { get; set; }
        public string PART_TYPE { get; set; }
        public decimal? PILLAR { get; set; }
        public decimal? AREA { get; set; }
        public string REVISION { get; set; }
        [MaxLength(2)]
        public string REVISION_PROD { get; set; }
        [MaxLength(10)]
        public string DEVICE_GROUP { get; set; }
        [MaxLength(10)]
        public string COMPO_FLAG { get; set; }
        [MaxLength(10)]
        public string MAIN_SITE { get; set; }
        [MaxLength(10)]
        public string SC_PROCESS { get; set; }
        public decimal? THICKNESS { get; set; }
        [MaxLength(30)]
        public string MIX_CODE { get; set; }
        [MaxLength(10)]
        public string UNIT { get; set; }
        public decimal? PARA_QTY { get; set; }
        [MaxLength(1)]
        public string JIG_YN { get; set; }
        [MaxLength(10)]
        public string MATERIAL_CODE { get; set; }
        [MaxLength(30)]
        public string MEASUREMENT { get; set; }
        [MaxLength(10)]
        public string CUST_LTYPE { get; set; }
        [MaxLength(1)]
        public string SPECIAL_ITEM_YN { get; set; }
        [MaxLength(1000)]
        public string SPECIAL_REMARK { get; set; }
        [MaxLength(30)]
        public string SEND_DRAWING { get; set; }
        [MaxLength(400)]
        public string DRAWING_PDF_URL { get; set; }
        [MaxLength(400)]
        public string DRAWING_DWG_URL { get; set; }
        [MaxLength(400)]
        public string DRAWING_3DPDF_URL { get; set; }
        [MaxLength(1)]
        public string BOM_APPROVAL { get; set; }
        [MaxLength(15)]
        public string BOM_APPROVER { get; set; }
        public DateTime? BOM_APPROVAL_DATE { get; set; }
        [MaxLength(10)]
        public string PURC_TYPE1 { get; set; }
        [MaxLength(1)]
        public string REQ_YN { get; set; }
        [MaxLength(1)]
        public string USE_YN { get; set; }
        [MaxLength(400)]
        public string REMARK { get; set; }
        [MaxLength(2)]
        public string P_CODE { get; set; }
        public DateTime? MODY_DATE { get; set; }
        public string MODY_USERID { get; set; }
        public string MODY_IP_ADDR { get; set; }

        #endregion


    }
}
