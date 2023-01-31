using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace GRDataAccess.Models
{
    public partial class sp_Inventory_Accuracy
    {
        [Key]
        public Guid Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_Lot { get; set; }
        public Guid? ItemStatus_Index { get; set; }
        public string ItemStatus_Name { get; set; }
        public decimal? CBM { get; set; }
        public decimal? SU_QtyBal { get; set; }
        public decimal? SU_QtyReserve { get; set; }
        public decimal? SU_QtyOnHand { get; set; }
        public decimal? Per_SU_QtyReserve { get; set; }
        public decimal? Per_SU_QtyOnHand { get; set; }
        public string SU_UNIT { get; set; }
        public string ERP_Location { get; set; }

    }
}
