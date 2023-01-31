using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MasterDataDataAccess.Models
{


    public partial class View_ProductCategory
    {
        //[Key]
        public Guid ProductCategory_Index { get; set; }
        public string ProductCategory_Id { get; set; }
        public string ProductCategory_Name { get; set; }
        public int ProductCategory_IsActive { get; set; }
        public int ProductCategory_IsDelete { get; set; }
        [Key]
        public Guid Product_Index { get; set; }
        public string Product_Id { get; set; }
        public string Product_Name { get; set; }
        public string Product_SecondName { get; set; }
        public string Product_ThirdName { get; set; }
        public Guid ProductConversion_Index { get; set; }
        public string ProductConversion_Id { get; set; }
        public string ProductConversion_Name { get; set; }
        public Guid ProductType_Index { get; set; }
        public Guid ProductSubType_Index { get; set; }
        public int? ProductItemLife_Y { get; set; }
        public int? ProductItemLife_M { get; set; }
        public int? ProductItemLife_D { get; set; }
        public string ProductImage_Path { get; set; }
        public int? IsLot { get; set; }
        public int? IsExpDate { get; set; }
        public int? IsMfgDate { get; set; }
        public int? IsCatchWeight { get; set; }
        public int? IsPack { get; set; }
        public int? IsSerial { get; set; }
        public int Product_IsActive { get; set; }
        public int Product_IsDelete { get; set; }
    }
}
