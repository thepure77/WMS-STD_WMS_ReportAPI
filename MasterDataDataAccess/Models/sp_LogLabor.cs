using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MasterDataDataAccess.Models
{
    public class sp_LogLabor
    {
        [Key]
        public long? RowIndex { get; set; }
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        //public string Create_By { get; set; }
        public DateTime? Create_Date { get; set; }
        public int? UsedTime { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string UserGroup_Name { get; set; }
        public string Menu_Name { get; set; }
        public string Sub_Menu_Name { get; set; }
        public DateTime? StartProcess { get; set; }
        public DateTime? EndProcess { get; set; }
        public string Operations { get; set; }


    }
}
