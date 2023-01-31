using ReportBusiness.ConfigModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ReportSumSpaceUtilization
{
    public class ReportResultSumSpaceUtilizationViewModel
    {
        public List<ReportSumSpaceUtilizationViewModel> location_type { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> owner { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> location_type_per { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> owner_per { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> location_type_all { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> owner_all { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> location_type_per_all { get; set; }
        public List<ReportSumSpaceUtilizationViewModel> owner_per_all { get; set; }

    }
}
