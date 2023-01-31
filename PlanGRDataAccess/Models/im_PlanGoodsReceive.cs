﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PlanGRDataAccess.Models
{
    public partial class IM_PlanGoodsReceive
    {

        [Key]
        public Guid PlanGoodsReceive_Index { get; set; }

        public Guid Owner_Index { get; set; }



        public string Owner_Id { get; set; }



        public string Owner_Name { get; set; }

        public Guid Vendor_Index { get; set; }



        public string Vendor_Id { get; set; }


        
        public string Vendor_Name { get; set; }

        public Guid DocumentType_Index { get; set; }



        public string DocumentType_Id { get; set; }


        
        public string DocumentType_Name { get; set; }



        public string PlanGoodsReceive_No { get; set; }

        
        public DateTime? PlanGoodsReceive_Date { get; set; }


        public string PlanGoodsReceive_Time { get; set; }

        public DateTime? PlanGoodsReceive_Due_Date { get; set; }

        
        public string DocumentRef_No1 { get; set; }

        
        public string DocumentRef_No2 { get; set; }

        
        public string DocumentRef_No3 { get; set; }

        
        public string DocumentRef_No4 { get; set; }

        
        public string DocumentRef_No5 { get; set; }

        public int? Document_Status { get; set; }

        
        public string UDF_1 { get; set; }

        
        public string UDF_2 { get; set; }

        
        public string UDF_3 { get; set; }

        
        public string UDF_4 { get; set; }

        
        public string UDF_5 { get; set; }

        public int? DocumentPriority_Status { get; set; }

        
        public string Document_Remark { get; set; }

        
        public string Create_By { get; set; }

        
        public DateTime? Create_Date { get; set; }

        
        public string Update_By { get; set; }

        
        public DateTime? Update_Date { get; set; }

        
        public string Cancel_By { get; set; }

        
        public DateTime? Cancel_Date { get; set; }

        public Guid? Warehouse_Index { get; set; }


        public string Warehouse_Id { get; set; }

        
        public string Warehouse_Name { get; set; }

        public Guid? Warehouse_Index_To { get; set; }


        public string Warehouse_Id_To { get; set; }

        
        public string Warehouse_Name_To { get; set; }

        public string UserAssign { get; set; }

        public string UserAssignKey { get; set; }

        public Guid? Dock_Index { get; set; }

        public string Dock_Id { get; set; }

        public string Dock_Name { get; set; }

        //public Guid? TypeCar_Index { get; set; }

        //public string TypeCar_Id { get; set; }

        //public string TypeCar_Name { get; set; }

        //public Guid? Driver_Index { get; set; }

        //public string Driver_Id { get; set; }

        public string Driver_Name { get; set; }

        public Guid? Transport_Index { get; set; }

        public string Transport_Id { get; set; }

        public string Transport_Name { get; set; }

        public Guid? Round_Index { get; set; }

        public string Round_Id { get; set; }

        public string Round_Name { get; set; }

        public string License_Name { get; set; }

    }
}
