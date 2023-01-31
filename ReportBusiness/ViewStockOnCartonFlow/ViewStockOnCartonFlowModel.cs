using BinbalanceBusiness;
using System;
using System.Collections.Generic;
using System.Text;

namespace ReportBusiness.ViewStockOnCartonFlow
{
    public class ViewStockOnCartonFlowModel : Pagination
    {
        public ViewStockOnCartonFlowModel()
        {
            selectSort = new List<sortViewModel>();

        }
        public string key { get; set; }

        public long? row_Index { get; set; }

        public string product_Id { get; set; }

        public string product_Name { get; set; }

        public string location_Name { get; set; }

        public string locType { get; set; }

        public string productConversion_Name { get; set; }

        public decimal max_Qty { get; set; }

        public decimal min_Qty { get; set; }

        public decimal piecepick_Qty { get; set; }

        public decimal perc_Qty { get; set; }

        public decimal replen_Qty { get; set; }

        public List<sortViewModel> selectSort { get; set; }

    

        public class sortViewModel
        {
            public string value { get; set; }
            public string display { get; set; }
            public int seq { get; set; }
        }
        public class SortModel
        {
            public string ColId { get; set; }
            public string Sort { get; set; }

            public string PairAsSqlExpression
            {
                get
                {
                    return $"{ColId} {Sort}";
                }
            }
        }  
    }

    public class LocationTypeFlowCaViewModel
    {

        public string locType { get; set; }

    }
  
    public class ViewStockOnCartonFlowModelAct
    {
        public IList<ViewStockOnCartonFlowModel> itemsview { get; set; }
        public Pagination pagination { get; set; }
    }
}
