using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ReportDataAccess.Models
{
    public partial class View_KPI
    {
        [Key]
        public Guid Row_Index { get; set; }
        public DateTime Activity_Date { get; set; }
        public int? InB_RECV_WorkUnit { get; set; }
        public int? InB_RECV_AVG { get; set; }
        public string InB_RECV_Rating { get; set; }
        public int? InB_PUT_WorkUnit { get; set; }
        public int? InB_PUT_AVG { get; set; }
        public string InB_PUT_Rating { get; set; }
        public int? Prod_RECV_WorkUnit { get; set; }
        public int? Prod_RECV_AVG { get; set; }
        public string Prod_RECV_Rating { get; set; }
        public int? Prod_PUT_WorkUnit { get; set; }
        public int? Prod_PUT_AVG { get; set; }
        public string Prod_PUT_Rating { get; set; }
        public int? OutB_PICK_WorkUnit { get; set; }
        public int? OutB_PICK_AVG { get; set; }
        public string OutB_PICK_Rating { get; set; }
        public int? TF_QI_WorkUnit { get; set; }
        public int? TF_QI_AVG { get; set; }
        public string TF_QI_Rating { get; set; }
        public int? TF_BLOCK_WorkUnit { get; set; }
        public int? TF_BLOCK_AVG { get; set; }
        public string TF_BLOCK_Rating { get; set; }
    }
}
