using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportKPI
{
    public class ReportKPIViewModel
    {

        public string Report_Date { get; set; }

        public string Report_Date_To { get; set; }

        public string Current_Date { get; set; }

        public string Current_Time { get; set; }

        public string Kpi_Date { get; set; }

        public int? Inbound_Receiving_WU { get; set; }

        public int? Inbound_Receiving_AVG { get; set; }

        public string Inbound_Receiving_Rating { get; set; }

        public int? Inbound_Putaway_WU { get; set; }

        public int? Inbound_Putaway_AVG { get; set; }

        public string Inbound_Putaway_Rating { get; set; }

        public int? Production_Receiving_WU { get; set; }

        public int? Production_Receiving_AVG { get; set; }

        public string Production_Receiving_Rating { get; set; }

        public int? Production_Putaway_WU { get; set; }

        public int? Production_Putaway_AVG { get; set; }

        public string Production_Putaway_Rating { get; set; }

        public int? Outbound_QI_WU { get; set; }

        public int? Outbound_QI_AVG { get; set; }

        public string Outbound_QI_Rating { get; set; }

        public int? Outbound_Picking_WU { get; set; }

        public int? Outbound_Picking_AVG { get; set; }

        public string Outbound_Picking_Rating { get; set; }

        public int? Outbound_Block_WU { get; set; }

        public int? Outbound_Block_AVG { get; set; }

        public string Outbound_Block_Rating { get; set; }

    }
}
