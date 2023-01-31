using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class View_ProductDetailV2
    {
        [Key]
        public Guid Product_Index { get; set; }
              
        public string Product_Id { get; set; }

        public string Product_Name { get; set; }

        public string Product_SecondName { get; set; }

        public string Product_ThirdName { get; set; }

        public string ProductImage_Path { get; set; }
        
        public Guid ProductConversion_Index { get; set; }
        
        public string ProductConversion_Id { get; set; }

        public string ProductConversion_Name { get; set; }
                       
        public Guid ProductType_Index { get; set; }
        
        public string ProductType_Id { get; set; }

        public string ProductType_Name { get; set; }

        public Guid ProductSubType_Index { get; set; }

        public string ProductSubType_Id { get; set; }
        
        public string ProductSubType_Name { get; set; }

        public Guid ProductCategory_Index { get; set; }

        public string ProductCategory_Id { get; set; }

        public string ProductCategory_Name { get; set; }

        public int? IsExpDate { get; set; }

        public int? IsLot { get; set; }

        public int? ProductItemLife_Y { get; set; }

        public int? ProductItemLife_M { get; set; }

        public int? ProductItemLife_D { get; set; }

        public int? IsMfgDate { get; set; }

        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
    }
}
