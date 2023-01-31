using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public partial class ms_TypeCar
    {
        [Key]
        public Guid? TypeCar_Index { get; set; }
        public string TypeCar_Id { get; set; }
        public string TypeCar_Name { get; set; }


    }
}
