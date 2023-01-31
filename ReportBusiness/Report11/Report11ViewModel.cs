using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.Report11
{
    public class Report11ViewModel
    {

        public string date { get; set; }

        public string warehouse_Name { get; set; }

        public decimal? count { get; set; }

        public decimal? number { get; set; }
        
        public decimal? countAll { get; set; }

        public decimal? countUse { get; set; }

        public decimal? countEmpty { get; set; }

        public decimal? percenAll { get; set; }

        public decimal? percenUse { get; set; }

        public decimal? percenEmpty { get; set; }

        public bool checkQuery { get; set; }

        public int? BlockPut { get; set; }

        public int? BlockPick { get; set; }

        public decimal? percenBlockPut { get; set; }

        public decimal? percenBlockPick { get; set; }
        public decimal? qtyBlock { get; set; }
        public decimal? percenBlock { get; set; }
    }


}
