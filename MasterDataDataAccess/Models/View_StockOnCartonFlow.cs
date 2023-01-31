using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class View_StockOnCartonFlow
    {
        [Key]

        public long? Row_Index { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Location_Name { get; set; }

        public string LocType { get; set; }

        public string ProductConversion_Name { get; set; }

        public decimal MaxQty { get; set; }

        public decimal MinQty { get; set; }

        public decimal ppQty { get; set; }

        public decimal percQty { get; set; }

        public decimal ReplenQty { get; set; }

    }
}
