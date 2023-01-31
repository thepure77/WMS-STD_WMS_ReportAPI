using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ConfigModel
{
    public class BusinessUnitViewModel
    {
        public Guid BusinessUnit_Index { get; set; }
        public string BusinessUnit_Id { get; set; }
        public string BusinessUnit_Name { get; set; }
        public string BusinessUnit_SecondName { get; set; }
        public string Ref_No1 { get; set; }
        public string Ref_No2 { get; set; }
        public string Ref_No3 { get; set; }
        public string Ref_No4 { get; set; }
        public string Ref_No5 { get; set; }
        public string Remark { get; set; }
        public string UDF_1 { get; set; }
        public string UDF_2 { get; set; }
        public string UDF_3 { get; set; }
        public string UDF_4 { get; set; }
        public string UDF_5 { get; set; }
        public int? isActive { get; set; }
        public int? isDelete { get; set; }
        public int? isSystem { get; set; }
        public int? status_Id { get; set; }
        public string create_By { get; set; }
        public DateTime? create_Date { get; set; }
        public string update_By { get; set; }
        public DateTime? update_Date { get; set; }
        public string cancel_By { get; set; }
        public DateTime? cancel_Date { get; set; }
    }
}
