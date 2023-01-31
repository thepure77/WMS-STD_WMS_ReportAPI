using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;


namespace MasterDataDataAccess.Models
{
    public partial class sp_rpt_04_Pickingplan
    {
        [Key]
        public Guid RowIndex { get; set; }
        public string TempCondition { get; set; }
        public string Business_Unit { get; set; }
        public string DO_NO { get; set; }
        public string SO_NO { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public DateTime? Doc_date { get; set; }
        public string Shipto_Address { get; set; }
        public string Tote { get; set; }
        public decimal? Total_qty { get; set; }
        public string Unit_BU { get; set; }
        public decimal? Qty { get; set; }
        public string Unit_SU { get; set; }
        public decimal? Bu_Qty { get; set; }
        public string BU_SU { get; set; }
        public decimal? WEIGHT_PC_KG { get; set; }
        public decimal? NetWeight_KG { get; set; }
        public decimal? GrsWeight_KG { get; set; }
        public decimal? CBM_SU { get; set; }
        public decimal? CBM { get; set; }
        public decimal? ProductConversion_Width { get; set; }
        public decimal? ProductConversion_Length { get; set; }
        public decimal? ProductConversion_Height { get; set; }
        public string Ref_No1 { get; set; }
        public string Ref_No2 { get; set; }
        



    }
}
