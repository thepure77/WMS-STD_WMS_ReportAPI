using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class sy_SuggestPutawayByProduct
    {
        [Key]
        public Guid SuggestPutawayByProduct_Index { get; set; }

        public Guid Product_Index { get; set; }

        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public Guid Location_Index { get; set; }

        public string Location_Id { get; set; }

        public string Location_Name { get; set; }

        public int IsActive { get; set; }

        public int IsDelete { get; set; }

        public int IsSystem { get; set; }

        public int Status_Id { get; set; }

        public string Create_By { get; set; }

        public DateTime Create_Date { get; set; }

        public string Update_By { get; set; }

        public DateTime Update_Date { get; set; }

        public string Cancel_By { get; set; }

        public DateTime Cancel_Date { get; set; }

        public decimal Min_Stock { get; set; }

        public decimal Max_Stock { get; set; }


    }
}
