using AspNetCore.Reporting;
using BinBalanceBusiness;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportAutocomplete
{
    public class ReportAutocompleteService
    {

        #region autoGINo
        public List<ReportAutocompleteViewModel> autoGINo(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new GIDbContext())
                {

                    var query = context.IM_GoodsIssue.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.GoodsIssue_No.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.GoodsIssue_Index, c.GoodsIssue_No }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.GoodsIssue_Index,
                            id = item.GoodsIssue_No,
                            name = item.GoodsIssue_No

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoPlanGINo
        public List<ReportAutocompleteViewModel> autoPlanGINo(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new PlanGIDbContext())
                {

                    var query = context.im_PlanGoodsIssue.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.PlanGoodsIssue_No.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.PlanGoodsIssue_Index, c.PlanGoodsIssue_No }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.PlanGoodsIssue_Index,
                            id = item.PlanGoodsIssue_No,
                            name = item.PlanGoodsIssue_No

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoProductType
        public List<ReportAutocompleteViewModel> autoProductType(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_ProductType.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductType_Name.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.ProductType_Index, c.ProductType_Id, c.ProductType_Name }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.ProductType_Index,
                            id = item.ProductType_Id,
                            name = item.ProductType_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoBinCardYear
        public List<ReportAutocompleteViewModel> autoBinCardYear(ReportAutocompleteViewModel data)
        { 
            try
            {

                using (var context = new BinbalanceDbContext())
                {

                    var query = context.View_RPT18_BinCard_Year.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {

                        query = query.Where(c => c.Bin_year.ToString().Contains(data.key));

                    }

                    var result = query.Select(c => new { c.Bin_year }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            id = item.Bin_year.ToString(),
                            name = item.Bin_year.ToString()

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoWarehouse
        public List<ReportAutocompleteViewModel> autoWarehouse(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Warehouse.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Warehouse_Name.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.Warehouse_Index, c.Warehouse_Name, c.Warehouse_Id }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.Warehouse_Index,
                            id = item.Warehouse_Id,
                            name = item.Warehouse_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoOwner
        public List<ReportAutocompleteViewModel> autoOwner(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Owner.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Owner_Name.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoOwnerBOM
        public List<ReportAutocompleteViewModel> autoOwnerBOM(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new TransferDbContext())
                {

                    var query = context.im_BOM.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoOwner
        public List<ReportAutocompleteViewModel> autoOwnerID(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Owner.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Id

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoOwner
        public List<ReportAutocompleteViewModel> autoMC(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_ProductCategory.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ProductCategory_Id.Contains(data.key)
                        || c.ProductCategory_Name.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.ProductCategory_Index, c.ProductCategory_Id, c.ProductCategory_Name }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.ProductCategory_Index,
                            id = item.ProductCategory_Id,
                            name = item.ProductCategory_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region AutoProductIdStockOnCartonFlow
        public List<ReportAutocompleteViewModel> AutoProductIdStockOnCartonFlow(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.View_StockOnCartonFlow.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key.ToString()) && data.key != "-")
                    {
                        query = query.Where(c => c.Product_Id.Contains(data.key));
                    }

                    var result = query.Select(c => new { c.Product_Id }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            key = item.Product_Id,
                            id = item.Product_Id,
                            name = item.Product_Id
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region AutoShipTo
        public List<ReportAutocompleteViewModel> AutoShipTo(ReportAutocompleteViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())

                {
                    var query = context.MS_ShipTo.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.ShipTo_Id.Contains(data.key)
                                                || c.ShipTo_Name.Contains(data.key));
                    }

                    var items = new List<ReportAutocompleteViewModel>();

                    var result = query.Select(c => new { c.ShipTo_Index, c.ShipTo_Id, c.ShipTo_Name, c.Contact_Person, c.ShipTo_Address, c.ShipToType_Index,}).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.ShipTo_Index,
                            id = item.ShipTo_Id,
                            name = item.ShipTo_Name,
                            address = item.ShipTo_Address
                        };

                        items.Add(resultItem);
                    }
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region autoSearchVendor
        public List<ReportAutocompleteViewModel> autoSearchVendor(ReportAutocompleteViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {

                    var query = context.MS_Vendor.AsQueryable();
                    if (data.key == "-")
                    {


                    }
                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.Vendor_Name.Contains(data.key) || c.Vendor_Id.Contains(data.key));

                    }

                    var result = query.Select(c => new { c.Vendor_Index, c.Vendor_Id, c.Vendor_Name }).Distinct().Take(10).ToList();

                    var items = new List<ReportAutocompleteViewModel>();

                    foreach (var item in result)
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.Vendor_Index,
                            id = item.Vendor_Id,
                            name = item.Vendor_Name

                        };

                        items.Add(resultItem);
                    }
                    return items;
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
