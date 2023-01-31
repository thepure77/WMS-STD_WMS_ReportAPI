using System;
using System.Collections.Generic;

namespace MasterDataBusiness.ViewModels
{
    public class UniversalscarchViewModel
    {
        public Guid RowIndex { get; set; }

        public string location { get; set; }

        public string tag_no { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public string qtySaleUnit { get; set; }

        public string sale_unit { get; set; }

        public string mfg_date { get; set; }

        public string exp_date { get; set; }

        public string receive_date { get; set; }

        public string product_Lot { get; set; }

        public string erp_location { get; set; }

        public string qty_base_unit { get; set; }

        public string base_unit { get; set; }

        public string tempcondition_name { get; set; }

        public string vendor_name { get; set; }

        public string itemStatus_Name { get; set; }

        public string type { get; set; }

        public string input { get; set; }

        public List<UniversalscarchViewModel> listUniversalscarchViewModel { get; set; }
    }

}
