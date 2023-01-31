using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class sp_ReportGIByShipmentDateAndBusinessUnit
    {
        [Key]
        public long? RowIndex { get; set; }
        public DateTime? TruckLoad_Date { get; set; }
        public string BusinessUnit_Name { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public decimal? SU_order { get; set; }
        public decimal? SU_WH_GI_Qty { get; set; }
        public decimal? SU_TR_GI_Qty { get; set; }
        public string SU_unit { get; set; }
        public decimal? SU_CBM { get; set; }
        public decimal? SU_Volume { get; set; }
        public decimal? BU_WH_GI_Qty { get; set; }
        public string BU_unit { get; set; }
    }

}
