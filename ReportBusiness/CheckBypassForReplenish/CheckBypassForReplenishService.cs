using Business.Library;
using DataAccess;
using MasterDataBusiness.ViewModels;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using BinbalanceBusiness;

namespace MasterDataBusiness
{
    public class CheckBypassForReplenishService
    {
        private MasterDataDbContext db;

        public CheckBypassForReplenishService()
        {
            db = new MasterDataDbContext();
        }

        public CheckBypassForReplenishService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filtercheckBypassForReplenish
        public actionResultCheckBypassForReplenishViewModel filtercheckBypassForReplenish(CheckBypassForReplenishViewModel data)
        {
            try
            {
                db.Database.SetCommandTimeout(360);
                var statusModels = new List<int?>();

                var query = db.View_CheckBypassForReplenish.AsQueryable();

                if (!string.IsNullOrEmpty(data.replenishmentLocation))
                {
                    query = query.Where(c => c.ReplenishmentLocation == data.replenishmentLocation);
                }
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key) || c.Product_Name.Contains(data.key));
                }
                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {

                        if (item.value == 0)
                        {
                            statusModels.Add(0);
                        }
                        if (item.value == 1)
                        {
                            statusModels.Add(1);
                        }

                    }

                    query = query.Where(c => statusModels.Contains(c.Location_is));
                }

                var Item = new List<View_CheckBypassForReplenish>();
                var TotalRow = new List<View_CheckBypassForReplenish>();
                data.TotalRow.ToString();
                TotalRow = query.ToList();

                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.OrderBy(c => c.Product_Id).Skip(((data.CurrentPage - 1) * data.PerPage));
                }
                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }

                Item = query.ToList();

                var result = new List<CheckBypassForReplenishViewModel>();

                foreach (var item in Item)
                {

                    var resultItem = new CheckBypassForReplenishViewModel();

                    resultItem.rowIndex = item.RowIndex;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.replenishmentLocation = item.ReplenishmentLocation;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.tag_No = item.Tag_No;
                    resultItem.maxQty = item.MaxQty;
                    resultItem.minQty = item.MinQty;
                    resultItem.piecePickQty = item.PiecePickQty;
                    resultItem.sU_BinBalance_QtyBal = item.SU_BinBalance_QtyBal;
                    resultItem.sU_BinBalance_QtyReserve = item.SU_BinBalance_QtyReserve;
                    resultItem.sU_Qty_Remain = item.SU_Qty_Remain;
                    resultItem.saleUnit = item.SaleUnit;
                    resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date == null ? "" : item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.shelfLife = item.ShelfLife;
                    resultItem.ageRemain = item.AgeRemain;
                    resultItem.eRP_Location = item.ERP_Location;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;


                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResult = new actionResultCheckBypassForReplenishViewModel();
                actionResult.itemsCheckBypassForReplenish = result.ToList();
                actionResult.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return actionResult;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Export Excel
        public actionResultCheckBypassForReplenishExportViewModel ExportCheckBypassForReplenish(CheckBypassForReplenishExportViewModel data)
        {
            try
            {
                var statusModels = new List<int?>();

                var query = db.View_CheckBypassForReplenish.AsQueryable();

                if (!string.IsNullOrEmpty(data.replenishmentLocation))
                {
                    query = query.Where(c => c.ReplenishmentLocation == data.replenishmentLocation);
                }
                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key) || c.Product_Name.Contains(data.key));
                }
                if (data.status.Count > 0)
                {
                    foreach (var item in data.status)
                    {

                        if (item.value == 0)
                        {
                            statusModels.Add(0);
                        }
                        if (item.value == 1)
                        {
                            statusModels.Add(1);
                        }

                    }

                    query = query.Where(c => statusModels.Contains(c.Location_is));
                }

                var Item = new List<View_CheckBypassForReplenish>();
                var TotalRow = new List<View_CheckBypassForReplenish>();

                TotalRow = query.ToList();

                Item = query.OrderBy(o => o.Product_Id).ToList();

                var result = new List<CheckBypassForReplenishExportViewModel>();

                foreach (var item in Item)
                {

                    var resultItem = new CheckBypassForReplenishExportViewModel();

                    resultItem.rowIndex = item.RowIndex;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.replenishmentLocation = item.ReplenishmentLocation;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.tag_No = item.Tag_No;
                    resultItem.maxQty = item.MaxQty;
                    resultItem.minQty = item.MinQty;
                    resultItem.piecePickQty = item.PiecePickQty;
                    resultItem.sU_BinBalance_QtyBal = item.SU_BinBalance_QtyBal;
                    resultItem.sU_BinBalance_QtyReserve = item.SU_BinBalance_QtyReserve;
                    resultItem.sU_Qty_Remain = item.SU_Qty_Remain;
                    resultItem.saleUnit = item.SaleUnit;
                    resultItem.goodsReceive_EXP_Date = item.GoodsReceive_EXP_Date == null ? "" : item.GoodsReceive_EXP_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.shelfLife = item.ShelfLife;
                    resultItem.ageRemain = item.AgeRemain;
                    resultItem.eRP_Location = item.ERP_Location;
                    resultItem.itemStatus_Name = item.ItemStatus_Name;

                    result.Add(resultItem);
                }

                var count = TotalRow.Count;

                var actionResultCheckBypassForReplenishExportViewModel = new actionResultCheckBypassForReplenishExportViewModel();
                actionResultCheckBypassForReplenishExportViewModel.itemsCheckBypassForReplenish = result.ToList();

                return actionResultCheckBypassForReplenishExportViewModel;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
