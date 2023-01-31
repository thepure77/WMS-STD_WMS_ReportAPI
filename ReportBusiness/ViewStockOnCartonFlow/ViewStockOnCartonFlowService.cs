using BinbalanceBusiness;
using BinBalanceBusiness;
using DataAccess;
using GIBusiness;
using MasterDataDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using static ReportBusiness.ViewStockOnCartonFlow.ViewStockOnCartonFlowModel;

namespace ReportBusiness.ViewStockOnCartonFlow
{
    public class ViewStockOnCartonFlowService
    {
        private MasterDataDbContext db;

        public ViewStockOnCartonFlowService()
        {
            db = new MasterDataDbContext();
        }

        public ViewStockOnCartonFlowService(MasterDataDbContext db)
        {
            this.db = db;
        }

        #region filterStock
        //filter
        public ViewStockOnCartonFlowModelAct filterview(ViewStockOnCartonFlowModel data)
        {
            try
            {
                var query = db.View_StockOnCartonFlow.AsQueryable();

                var sortModels = new List<SortModel>();

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key)
                                        || c.Product_Name.Contains(data.key)
                                        || c.Location_Name.Contains(data.key));
                }

                if (!string.IsNullOrEmpty(data.locType))
                {
                    query = query.Where(c => c.LocType == data.locType);
                }

                if (data.selectSort.Count > 0)
                {
                    foreach (var item in data.selectSort)
                    {
                        if (item.value == "Product_Id")
                        {
                            sortModels.Add(new SortModel
                            {
                                ColId = "Product_Id",
                                Sort = "desc"
                            });
                        }
                        if (item.value == "Prec_Qty_Desc")
                        {
                            sortModels.Add(new SortModel
                            {
                                ColId = "percQty",
                                Sort = "desc"
                            });
                        }
                        if (item.value == "Prec_Qty_Asc")
                        {
                            sortModels.Add(new SortModel
                            {
                                ColId = "percQty",
                                Sort = "asc"
                            });
                        }
                    }
                    query = query.KWOrderBy(sortModels);
                }


                var Item = new List<View_StockOnCartonFlow>();
                var TotalRow = new List<View_StockOnCartonFlow>();
                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }


                Item = query.ToList();

                var result = new List<ViewStockOnCartonFlowModel>();

                foreach (var item in Item)
                {
                    var resultItem = new ViewStockOnCartonFlowModel();
                    resultItem.row_Index = item.Row_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.max_Qty = item.MaxQty;
                    resultItem.min_Qty = item.MinQty;
                    resultItem.piecepick_Qty = item.ppQty;
                    resultItem.perc_Qty = item.percQty;
                    resultItem.replen_Qty = item.ReplenQty;
                    result.Add(resultItem);
                }


                var count = TotalRow.Count;

                var ViewStockOnCartonFlowModelAct = new ViewStockOnCartonFlowModelAct();
                ViewStockOnCartonFlowModelAct.itemsview = result.ToList();
                ViewStockOnCartonFlowModelAct.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return ViewStockOnCartonFlowModelAct;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region exportExcel
        //export
        public ViewStockOnCartonFlowModelAct ExportThanawat(ViewStockOnCartonFlowModel data)
        {
            try
            {
                var query = db.View_StockOnCartonFlow.AsQueryable();

                var sortModels = new List<SortModel>();

                if (!string.IsNullOrEmpty(data.key))
                {
                    query = query.Where(c => c.Product_Id.Contains(data.key)
                                        || c.Product_Name.Contains(data.key)
                                        || c.Location_Name.Contains(data.key));
                }

                if (!string.IsNullOrEmpty(data.locType))
                {
                    query = query.Where(c => c.LocType == data.locType);
                }

                if (data.selectSort.Count > 0)
                {
                    foreach (var item in data.selectSort)
                    {
                        if (item.value == "Product_Id")
                        {
                            sortModels.Add(new SortModel
                            {
                                ColId = "Product_Id",
                                Sort = "desc"
                            });
                        }
                        if (item.value == "Prec_Qty_Desc")
                        {
                            sortModels.Add(new SortModel
                            {
                                ColId = "percQty",
                                Sort = "desc"
                            });
                        }
                        if (item.value == "Prec_Qty_Asc")
                        {
                            sortModels.Add(new SortModel
                            {
                                ColId = "percQty",
                                Sort = "asc"
                            });
                        }
                    }
                    query = query.KWOrderBy(sortModels);
                }


                var Item = new List<View_StockOnCartonFlow>();
                var TotalRow = new List<View_StockOnCartonFlow>();
                TotalRow = query.ToList();


                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }


                Item = query.ToList();

                var result = new List<ViewStockOnCartonFlowModel>();
                foreach (var item in Item)
                {
                    var resultItem = new ViewStockOnCartonFlowModel();
                    resultItem.row_Index = item.Row_Index;
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.productConversion_Name = item.ProductConversion_Name;
                    resultItem.max_Qty = item.MaxQty;
                    resultItem.min_Qty = item.MinQty;
                    resultItem.piecepick_Qty = item.ppQty;
                    resultItem.perc_Qty = item.percQty;
                    resultItem.replen_Qty = item.ReplenQty;
                    result.Add(resultItem);


                }

                var count = TotalRow.Count;

                var ViewStockOnCartonFlowModelAct = new ViewStockOnCartonFlowModelAct();
                ViewStockOnCartonFlowModelAct.itemsview = result.ToList();
                ViewStockOnCartonFlowModelAct.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };


                return ViewStockOnCartonFlowModelAct;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region getLocationTypeFlowCa
        //dropdownlocation
        public List<LocationTypeFlowCaViewModel> getLocationTypeFlowCa(LocationTypeFlowCaViewModel data)
        {
            try
            {
                using (var db = new MasterDataDbContext())
                {
                    var query = db.View_StockOnCartonFlow.AsQueryable();

                    if (data.locType?.Count() > 0)
                    {
                        query = query.Where(c => data.locType.Contains(c.LocType));
                    }

                    var xx = query.GroupBy(l => new { l.LocType }).Select(l => new { l.Key.LocType }).OrderBy(o => o.LocType);
                    var ListData = xx.Select(s => new LocationTypeFlowCaViewModel
                    {
                        locType = s.LocType
                    }).ToList();


                    return ListData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}