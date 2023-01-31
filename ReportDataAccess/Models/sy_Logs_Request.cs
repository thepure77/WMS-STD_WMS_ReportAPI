using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportDataAccess.Models
{
    public partial class sy_Logs_Request
    {
        [Key]
        public Guid Log_Index { get; set; }
        public Guid? UserGroup_Index { get; set; }
        public string UserGroup_Id { get; set; }
        public string UserGroup_Name { get; set; }
        public Guid? User_Index { get; set; }
        public string User_Id { get; set; }
        public string User_Name { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public Guid? Menu_Index { get; set; }
        public Guid? MenuType_Index { get; set; }
        public string Menu_Id { get; set; }
        public string Menu_Name { get; set; }
        public Guid? Sub_Menu_Index { get; set; }
        public Guid? Sub_MenuType_Index { get; set; }
        public string Sub_Menu_Id { get; set; }
        public string Sub_Menu_Name { get; set; }
        public string Operations { get; set; }
        public Guid? Ref_Document_Index { get; set; }
        public string Ref_Document_No { get; set; }
        public string Request_URL { get; set; }
        public string Request_Body { get; set; }
        public string UDF_1{ get; set; }
        public string UDF_2{ get; set; }
        public string UDF_3{ get; set; }
        public string UDF_4{ get; set; }
        public string UDF_5 { get; set; }
        public int? IsActive { get; set; }
        public int? IsDelete { get; set; }
        public int? IsSystem { get; set; }
        public int? Status_Id { get; set; }
        public string Create_By { get; set; }
        public DateTime Create_Date { get; set; }
        public string Update_By { get; set; }
        public DateTime? Update_Date { get; set; }
        public string Cancel_By { get; set; }
        public DateTime? Cancel_Date { get; set; }
    }
}
