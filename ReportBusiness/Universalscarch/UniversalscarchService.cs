using DataAccess;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


using Newtonsoft.Json;
using System.Data.SqlClient;

namespace MasterDataBusiness
{
    public class UniversalscarchService
    {
        #region UniversalscarchService
        private MasterDataDbContext db;

        public UniversalscarchService()
        {
            db = new MasterDataDbContext();
        }

        public UniversalscarchService(MasterDataDbContext db)
        {
            this.db = db;
        }
        #endregion

        #region Universalscarch
        public actionResultUniversalscarchViewModel Universalscarch(UniversalscarchViewModel data)
        {
            try
            {
                var Master_DBContext = new MasterDataDbContext();
                Master_DBContext.Database.SetCommandTimeout(360);

                var Tag_No = new SqlParameter("@Tag_No", "");
                var Product_Id = new SqlParameter("@Product_Id", "");
                var Product_Name = new SqlParameter("@Product_Name", "");
                var Product_Lot = new SqlParameter("@Product_Lot", "");
                var Location_Id = new SqlParameter("@Location_Id", "");
                if (data.type == "Tag No")
                {
                    Tag_No = new SqlParameter("@Tag_No", data.input);
                }
                else if (data.type == "Product Id")
                {
                    Product_Id = new SqlParameter("@Product_Id", data.input);
                }
                else if (data.type == "Product Name")
                {
                    Product_Name = new SqlParameter("@Product_Name", data.input);
                }
                else if (data.type == "Product Lot")
                {
                    Product_Lot = new SqlParameter("@Product_Lot", data.input);
                }
                else if (data.type == "Location Id")
                {
                    Location_Id = new SqlParameter("@Location_Id", data.input);
                }

                var resultquery = new List<MasterDataDataAccess.Models.sp_GetUniversalSearch>();
                resultquery = Master_DBContext.sp_GetUniversalSearch.FromSql("sp_GetUniversalSearch @Tag_No , @Product_Id , @Product_Name , @Product_Lot , @Location_Id", Tag_No, Product_Id, Product_Name , Product_Lot , Location_Id).ToList();


                var Item = new List<sp_GetUniversalSearch>();

                Item = resultquery.OrderBy(o => o.Receive_Date).ToList();

                var result = new List<SearchUniversalscarchViewModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchUniversalscarchViewModel();

                    resultItem.RowIndex = item.RowIndex;
                    resultItem.location = item.Location_Id;
                    resultItem.tag_no = item.Tag_No;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.qtySaleUnit = item.QtySaleUnit;
                    resultItem.sale_unit = item.Sale_Unit;
                    resultItem.mfg_date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.exp_date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.receive_date = item.Receive_Date != null ? item.Receive_Date.Value.ToString("dd/MM/yyyy") : "";
                    resultItem.product_Lot = item.Product_Lot;
                    resultItem.erp_location = item.ERP_Location;
                    resultItem.qty_base_unit = item.QtyBaseUnit;
                    resultItem.base_unit = item.Base_Unit;
                    resultItem.business_unit = item.Business_Unit;
                    resultItem.tempcondition_name = item.TempCondition_Name;
                    resultItem.vendor_name = item.Vendor_Name;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;

                    result.Add(resultItem);
                }

                var actionResultUniversalscarchViewModel = new actionResultUniversalscarchViewModel();
                actionResultUniversalscarchViewModel.itemsUniversalscarch = result.ToList();

                return actionResultUniversalscarchViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
