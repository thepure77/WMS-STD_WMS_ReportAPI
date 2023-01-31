using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportBusiness.ConfigModel
{
    public class DropdownService
    {
        public List<DocumentTypeViewModel> dropdownDocumentType(DocumentTypeViewModel data)
        {
            try
            {
                var result = new List<DocumentTypeViewModel>();

                var filterModel = new DocumentTypeViewModel();


                filterModel.process_Index = new Guid("5F147725-520C-4CA6-B1D2-2C0E65E7AAAA");


                //GetConfig
                result = utils.SendDataApi<List<DocumentTypeViewModel>>(new AppSettingConfig().GetUrl("DropDownDocumentType"), filterModel.sJson());

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<LocationTypeViewModel> dropdownLocationType(LocationTypeViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var result = new List<LocationTypeViewModel>();

                    var query = context.MS_LocationType.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    var queryResult = query.OrderBy(o => o.LocationType_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new LocationTypeViewModel();
                        resultItem.locationType_Index = item.LocationType_Index;
                        resultItem.locationType_Id = item.LocationType_Id;
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_By = item.Create_By;
                        resultItem.create_Date = item.Create_Date;

                        result.Add(resultItem);
                    }

                    return result;
                }
                    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region autoUser
        public List<ItemListViewModel> autoUser(ItemListViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_User.Where(c => c.IsActive == 1 || c.IsActive == 0 && c.IsDelete == 0);

                    if (data.key == "-")
                    {

                    }

                    else if (!string.IsNullOrEmpty(data.key))
                    {
                        query = query.Where(c => c.User_Id.Contains(data.key)
                                                || c.User_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();
                    var result = query.Select(c => new { c.User_Name, c.User_Index, c.User_Id }).Distinct().Take(10).ToList();
                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            //index = new Guid(item.User_Name),
                            index = item.User_Index,
                            id = item.User_Id,
                            name = item.User_Name,
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

        #region businessUnit
        public List<BusinessUnitViewModel> dropdownBusinessUnit(BusinessUnitViewModel data)
        {
            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var result = new List<BusinessUnitViewModel>();

                    var query = context.MS_BusinessUnit.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    var queryResult = query.OrderBy(o => o.BusinessUnit_Id).ToList();

                    foreach (var item in queryResult)
                    {
                        var resultItem = new BusinessUnitViewModel();
                        resultItem.BusinessUnit_Index = item.BusinessUnit_Index;
                        resultItem.BusinessUnit_Id = item.BusinessUnit_Id;
                        resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                        resultItem.BusinessUnit_SecondName = item.BusinessUnit_SecondName;
                        resultItem.Ref_No1 = item.Ref_No1;
                        resultItem.Ref_No2 = item.Ref_No2;
                        resultItem.Ref_No3 = item.Ref_No3;
                        resultItem.Ref_No4 = item.Ref_No4;
                        resultItem.Ref_No5 = item.Ref_No5;
                        resultItem.Remark = item.Remark;
                        resultItem.UDF_1 = item.UDF_1;
                        resultItem.UDF_2 = item.UDF_2;
                        resultItem.UDF_3 = item.UDF_3;
                        resultItem.UDF_4 = item.UDF_4;
                        resultItem.UDF_5 = item.UDF_5;
                        resultItem.isActive = item.IsActive;
                        resultItem.isDelete = item.IsDelete;
                        resultItem.isSystem = item.IsSystem;
                        resultItem.status_Id = item.Status_Id;
                        resultItem.create_By = item.Create_By;
                        resultItem.create_Date = item.Create_Date;
                        result.Add(resultItem);
                    }

                    return result;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region autoVendor
        public List<ItemListViewModel> autoVendor(ItemListViewModel data)
        {
            var items = new List<ItemListViewModel>();
            try
            {
                using (var context = new MasterDataDbContext())
                    if (!string.IsNullOrEmpty(data.key))
                {
                    var query1 = context.MS_Vendor.Where(c => c.Vendor_Id.Contains(data.key) ).Select(s => new ItemListViewModel
                    {
                        index = s.Vendor_Index,
                        name = s.Vendor_Name,
                        key = s.Vendor_Name
                    }).Distinct();

                        var query2 = context.MS_Vendor.Where(c => c.Vendor_Name.Contains(data.key)).Select(s => new ItemListViewModel
                        {
                            index = s.Vendor_Index,
                            name = s.Vendor_Name,
                            key = s.Vendor_Name
                        }).Distinct();

                        var query3 = context.MS_Vendor.Where(c => c.VendorType_Name.Contains(data.key)).Select(s => new ItemListViewModel
                    {
                        index = s.Vendor_Index,
                        name = s.VendorType_Name,
                        key = s.VendorType_Name
                    }).Distinct();

                        var query = query1.Union(query2).Union(query3);

                        items = query.OrderBy(c => c.name).Take(10).ToList();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return items;
        }

        #endregion

    }
}
