using BinbalanceBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportRebuild
{
    public class ReportRebuildViewModel : Result
    {
        public ReportRebuildViewModel()
        {
            models = new List<ReportRebuildViewModel>();
        }
         public Guid? rebuild_Index { get; set; }
                      
        public string rebuild_By { get; set; }
                      
        public string rebuild_Date_Start { get; set; }
                      
        public string rebuild_Date_End { get; set; }

        //public bool isuse_rebuild { get; set; }
        public int? rowNum { get; set; }

        public string key { get; set; }

        public List<ReportRebuildViewModel> models { get; set; }
        

    }

    //public class ReportRebuildViewModel2 
    //{
        

    //    public string rebuild_By { get; set; }
    //    public List<string> listrebuild_By { get; set; }
    //}






}
