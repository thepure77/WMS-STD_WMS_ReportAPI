using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report9
{
    public class Report9Service
    {

        #region printReport9
        public dynamic printReport9(Report9ViewModel data, string rootPath = "")
        {

            var BB_DBContext = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report9ViewModel>();

            try
            {
                var queryCC = BB_DBContext.View_RPT9_CycleCountDetail.AsQueryable();
                var productowner = M_DBContext.View_ProductOwner.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryCC = queryCC.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    productowner = productowner.Where(c => c.Owner_Id == data.owner_Id);
                }

                if (!string.IsNullOrEmpty(data.cycleCount_date) && !string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var dateStart = data.cycleCount_date.toBetweenDate();
                    var dateEnd = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date))
                {
                    var cycleCount_date_From = data.cycleCount_date.toBetweenDate();
                    queryCC = queryCC.Where(c => c.Create_Date >= cycleCount_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var cycleCount_date_To = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.Create_Date <= cycleCount_date_To.start);
                }

                //if (data.userGroup_Index != null && data.userGroup_Index != Guid.Empty)
                //{
                //    var user = M_DBContext.MS_User.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.UserGroup_Index == data.userGroup_Index).Select(s => s.User_Name).ToList();
                //    queryCC = queryCC.Where(c => user.Contains(c.Create_By));
                //}

                if (data.zone_Index != null && data.zone_Index != Guid.Empty)
                {
                    var Zonelocation = M_DBContext.MS_ZoneLocation.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Zone_Index == data.zone_Index).Select(s => s.Location_Index).ToList();
                    queryCC = queryCC.Where(c => Zonelocation.Contains(c.Location_Index));
                }

                var query = (from CC in queryCC.ToList()
                             join PRDCAT in M_DBContext.MS_Product.Join(productowner
                             , p => p.Product_Index
                             ,po => po.Product_Index
                             ,(p,po)=> new
                             {
                                 p.Product_Index,
                                 p.Product_Id,
                                 p.ProductCategory_Index,
                                 p.ProductCategory_Id,
                                 p.ProductCategory_Name,
                                 p.Ref_No3,
                                 p.ProductConversion_Index,
                                 p.ProductConversion_Id,
                                 p.ProductConversion_Name,
                                 po.Owner_Index,
                                 po.Owner_Id,
                                 po.Owner_Name,
                             }).ToList() on CC.Product_Index equals PRDCAT.Product_Index //into PDC
                             //from PRDCAT in PDC.DefaultIfEmpty()
                             select new
                             {
                                 CC.CycleCountItem_Index,
                                 CC.CycleCountDetail_Index,
                                 CC.CycleCount_Index,
                                 CC.Location_Index,
                                 CC.Location_Id,
                                 CC.Location_Name,
                                 CC.Product_Index,
                                 CC.Product_Id,
                                 CC.Product_Name,
                                 PRDCAT?.Owner_Index,
                                 PRDCAT?.Owner_Id,
                                 PRDCAT?.Owner_Name,
                                 PRDCAT?.ProductCategory_Id,
                                 PRDCAT?.ProductCategory_Name,
                                 PRDCAT?.Ref_No3,
                                 CC.ItemStatus_Index,
                                 CC.ItemStatus_Id,
                                 CC.ItemStatus_Name,
                                 CC.Qty_Bal,
                                 CC.Qty_Count,
                                 CC.Qty_Diff,
                                 CC.Count_status,
                                 CC.Create_Date,
                                 CC.Create_By
                             }).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report9ViewModel();
                    var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.qty_Bal = 0;
                    resultItem.qty_Count = 0;
                    resultItem.cycleCount_date = startDate;
                    resultItem.cycleCount_date_To = endDate;

                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        string date = item.Create_Date.toString();
                        string ccDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report9ViewModel();

                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name.Replace(" ", "");
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.productCategory_Name = item.ProductCategory_Id;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.qty_Bal = item.Qty_Bal;
                        resultItem.qty_Count = item.Qty_Count;
                        resultItem.qty_Diff = item.Qty_Diff;
                        resultItem.count_status = item.Count_status;
                        resultItem.create_Date = item.Create_Date == null ? "" : item.Create_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.create_By = item.Create_By;

                        var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.cycleCount_date = startDate;
                        resultItem.cycleCount_date_To = endDate;

                        result.Add(resultItem);
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report9");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion


        #region autoSearchOwner
        public List<ItemListViewModel> autoSearchOwnerOld(ItemListViewModel data)
        {
            try
            {

                using (var context = new BinbalanceDbContext())
                {


                    var query = context.View_RPT9_CycleCountDetail.Where(c => c.Owner_Index != null).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                   || c.Owner_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name,
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

        #region autoSearchOwner
        public List<ItemListViewModel> autoSearchOwner(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.MS_Owner.Where(c => c.Owner_Index != null && c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                   || c.Owner_Name.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Owner_Index, c.Owner_Id, c.Owner_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Owner_Index,
                            id = item.Owner_Id,
                            name = item.Owner_Name,
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

        public List<ConfigUserGroupViewModel> getUserGroupMenu(ConfigUserGroupMenuViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_UserGroup.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (data.listUserGroup_Index?.Count() > 0)
                    {
                        query = query.Where(c => data.listUserGroup_Index.Contains(c.UserGroup_Index));
                    }


                    var ListData = query.Select(s => new ConfigUserGroupViewModel
                    {
                        userGroup_Index = s.UserGroup_Index,
                        userGroup_Id = s.UserGroup_Id,
                        userGroup_Name = s.UserGroup_Name,
                        isActive = s.IsActive,
                        isDelete = s.IsDelete
                    }).OrderBy(o => o.userGroup_Name).ToList();

                    return ListData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ZoneViewModel> getZone(ZoneViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_Zone.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (data.listzone_Index?.Count() > 0)
                    {
                        query = query.Where(c => data.listzone_Index.Contains(c.Zone_Index));
                    }


                    var ListData = query.OrderBy(o => o.Zone_Id).Select(s => new ZoneViewModel
                    {
                        zone_Index = s.Zone_Index,
                        zone_Id = s.Zone_Id,
                        zone_Name = s.Zone_Name
                    }).ToList();

                    return ListData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ExportExcel(Report9ViewModel data, string rootPath = "")
        {
            var BB_DBContext = new BinbalanceDbContext();
            var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report9ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report9");

            try
            {


                var queryCC = BB_DBContext.View_RPT9_CycleCountDetail.AsQueryable();
                var productowner = M_DBContext.View_ProductOwner.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryCC = queryCC.Where(c => c.Product_Id == data.product_Id);
                }

                if (!string.IsNullOrEmpty(data.owner_Id))
                {
                    productowner = productowner.Where(c => c.Owner_Id == data.owner_Id);
                }

                if (!string.IsNullOrEmpty(data.cycleCount_date) && !string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var dateStart = data.cycleCount_date.toBetweenDate();
                    var dateEnd = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.Create_Date >= dateStart.start && c.Create_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date))
                {
                    var cycleCount_date_From = data.cycleCount_date.toBetweenDate();
                    queryCC = queryCC.Where(c => c.Create_Date >= cycleCount_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.cycleCount_date_To))
                {
                    var cycleCount_date_To = data.cycleCount_date_To.toBetweenDate();
                    queryCC = queryCC.Where(c => c.Create_Date <= cycleCount_date_To.start);
                }

                //if (data.userGroup_Index != null && data.userGroup_Index != Guid.Empty)
                //{
                //    var user = M_DBContext.MS_User.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.UserGroup_Index == data.userGroup_Index).Select(s => s.User_Name).ToList();
                //    queryCC = queryCC.Where(c => user.Contains(c.Create_By));
                //}

                if (data.zone_Index != null && data.zone_Index != Guid.Empty)
                {
                    var Zonelocation = M_DBContext.MS_ZoneLocation.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Zone_Index == data.zone_Index).Select(s => s.Location_Index).ToList();
                    queryCC = queryCC.Where(c => Zonelocation.Contains(c.Location_Index));
                }

                var query = (from CC in queryCC.ToList()
                             join PRDCAT in M_DBContext.MS_Product.Join(productowner
                             , p => p.Product_Index
                             , po => po.Product_Index
                             , (p, po) => new
                             {
                                 p.Product_Index,
                                 p.Product_Id,
                                 p.ProductCategory_Index,
                                 p.ProductCategory_Id,
                                 p.ProductCategory_Name,
                                 p.Ref_No3,
                                 p.ProductConversion_Index,
                                 p.ProductConversion_Id,
                                 p.ProductConversion_Name,
                                 po.Owner_Index,
                                 po.Owner_Id,
                                 po.Owner_Name,
                             }).ToList() on CC.Product_Index equals PRDCAT.Product_Index //into PDC
                             //from PRDCAT in PDC.DefaultIfEmpty()
                             select new
                             {
                                 CC.CycleCountItem_Index,
                                 CC.CycleCountDetail_Index,
                                 CC.CycleCount_Index,
                                 CC.Location_Index,
                                 CC.Location_Id,
                                 CC.Location_Name,
                                 CC.Product_Index,
                                 CC.Product_Id,
                                 CC.Product_Name,
                                 PRDCAT?.Owner_Index,
                                 PRDCAT?.Owner_Id,
                                 PRDCAT?.Owner_Name,
                                 PRDCAT?.ProductCategory_Id,
                                 PRDCAT?.ProductCategory_Name,
                                 PRDCAT?.Ref_No3,
                                 CC.ItemStatus_Index,
                                 CC.ItemStatus_Id,
                                 CC.ItemStatus_Name,
                                 CC.Qty_Bal,
                                 CC.Qty_Count,
                                 CC.Qty_Diff,
                                 CC.Count_status,
                                 CC.Create_Date,
                                 CC.Create_By
                             }).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report9ViewModel();
                    var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                    System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                    resultItem.qty_Bal = 0;
                    resultItem.qty_Count = 0;
                    resultItem.cycleCount_date = startDate;
                    resultItem.cycleCount_date_To = endDate;

                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        string date = item.Create_Date.toString();
                        string ccDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report9ViewModel();

                        resultItem.location_Id = item.Location_Id;
                        resultItem.location_Name = item.Location_Name.Replace(" ", "");
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.productCategory_Name = item.ProductCategory_Id;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.qty_Bal = item.Qty_Bal;
                        resultItem.qty_Count = item.Qty_Count;
                        resultItem.qty_Diff = item.Qty_Diff;
                        resultItem.count_status = item.Count_status;
                        resultItem.create_Date = item.Create_Date == null ? "" : item.Create_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.create_By = item.Create_By;

                        var startDate = DateTime.ParseExact(data.cycleCount_date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var endDate = DateTime.ParseExact(data.cycleCount_date_To.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        resultItem.cycleCount_date = startDate;
                        resultItem.cycleCount_date_To = endDate;

                        result.Add(resultItem);
                    }
                }

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport";

                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


                var saveLocation = rootPath + fullPath;
                //File.Delete(saveLocation);
                //ExcelRefresh(reportPath);
                return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string saveReport(byte[] file, string name, string rootPath)
        {
            var saveLocation = PhysicalPath(name, rootPath);
            FileStream fs = new FileStream(saveLocation, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            try
            {
                try
                {
                    bw.Write(file);
                }
                finally
                {
                    fs.Close();
                    bw.Close();
                }
            }
            catch (Exception ex)
            {
            }
            return VirtualPath(name);
        }

        public string PhysicalPath(string name, string rootPath)
        {
            var filename = name;
            var vPath = ReportPath;
            var path = rootPath + vPath;
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }

            var saveLocation = System.IO.Path.Combine(path, filename);
            return saveLocation;
        }
        public string VirtualPath(string name)
        {
            var filename = name;
            var vPath = ReportPath;
            vPath = vPath.Replace("~", "");
            return vPath + filename;
        }
        private string ReportPath
        {
            get
            {
                var url = "\\ReportGenerator\\";
                return url;
            }
        }
    }
}
