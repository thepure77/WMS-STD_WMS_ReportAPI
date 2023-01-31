using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_RPT11_Location
    {
        [Key]
        public long? Row_Index { get; set; }
        public Guid? Warehouse_Index { get; set; }
        public string Warehouse_Id { get; set; }
        public string Warehouse_Name { get; set; }
        public Guid? Location_Index { get; set; }
        public string Location_Id { get; set; }
        public string Location_Name { get; set; }
        public decimal? Max_Qty { get; set; }
        public decimal? Max_Weight { get; set; }
        public decimal? Max_Volume { get; set; }
        public decimal? Max_Pallet { get; set; }

    }
}
