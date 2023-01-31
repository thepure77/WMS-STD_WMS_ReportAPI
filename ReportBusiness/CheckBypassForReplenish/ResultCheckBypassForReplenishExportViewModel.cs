using System;
using System.Collections.Generic;
using System.Text;

namespace MasterDataBusiness.ViewModels
{
    public class CheckBypassForReplenishExportViewModel
    {
        public CheckBypassForReplenishExportViewModel()
        {
            status = new List<statusExportViewModel>();
        }
        public long? rowIndex { get; set; }
        public string product_Id { get; set; }
        public string product_Name { get; set; }
        public string replenishmentLocation { get; set; }
        public string location_Name { get; set; }
        public int? location_is { get; set; }
        public string tag_No { get; set; }
        public decimal? maxQty { get; set; }
        public decimal? minQty { get; set; }
        public decimal? piecePickQty { get; set; }
        public decimal? sU_BinBalance_QtyBal { get; set; }
        public decimal? sU_BinBalance_QtyReserve { get; set; }
        public decimal? sU_Qty_Remain { get; set; }
        public string saleUnit { get; set; }
        public string eRP_Location { get; set; }
        public string itemStatus_Name { get; set; }
        public string goodsReceive_EXP_Date { get; set; }
        public int? shelfLife { get; set; }
        public int? ageRemain { get; set; }
        public string key { get; set; }
        public List<statusExportViewModel> status { get; set; }
    }

    public class actionResultCheckBypassForReplenishExportViewModel
    {
        public IList<CheckBypassForReplenishExportViewModel> itemsCheckBypassForReplenish { get; set; }
    }
    public class statusExportViewModel
    {
        public int? value { get; set; }
        public string display { get; set; }
        public int seq { get; set; }
    }
}
