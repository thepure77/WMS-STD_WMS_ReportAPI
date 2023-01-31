using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using MasterDataBusiness.ViewModels;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportSummaryStockStorage
{
    public class ReportSummaryStockStorageService
    {

        #region printReportSummaryStockStorage
        //public dynamic printReportSummaryStockStorage(ReportSummaryStockStorageViewModel data, string rootPath = "")
        //{
            
        //    var MS_DBContext = new MasterDataDbContext();
        //    var BB_DBContext = new BinbalanceDbContext();
        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    var result = new List<ReportSummaryStockStorageViewModel>();

        //    try
        //    {
        //        MS_DBContext.Database.SetCommandTimeout(360);
        //        var queryLO = MS_DBContext.View_RPT_Location2.AsQueryable();
                

        //        var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0 
        //        && (new List<Guid?>
        //        { new Guid("472E5117-3A7A-4C23-B8C2-7FEA55B3E69C"),
        //          new Guid("6A1FB140-CC78-4C2B-BEC8-42B2D0AE62E9"),
        //          new Guid("550B4C74-56CC-4D11-9228-F2656D8FA3F6"),
        //          new Guid("B4F94559-B9CD-454E-BF9F-B6ACDA94F493"),
        //          new Guid("3A7D807A-9F2C-4215-8703-F51846BCC4BD"),
        //          new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF"),
        //          new Guid("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
        //          new Guid("394FF62E-693F-4C21-A370-18DCAD5B4455"),
        //          new Guid("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
        //          new Guid("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
        //          new Guid("9E19ED5F-42D7-4131-BC18-610540DE9752"),
        //          new Guid("2C8D9120-14DD-4737-BBFE-3E523D303E75"),
        //          new Guid("2B7A1377-AE93-4C07-9D79-A0CEB4C343B7"),
        //          new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
        //          new Guid("2E9338D3-0931-4E36-B240-782BF9508641"),
        //          new Guid("64341969-E596-4B8B-8836-395061777490"),
        //          new Guid("1D2DF268-F004-4820-831F-B823FF9C7564"),
        //          new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC"),
        //          new Guid("7D30298A-8BA0-47ED-8342-E3F953E11D8C"),
        //          new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7"),
        //          new Guid("76C758AA-C216-406C-BDF6-14018C87CAE1"),
        //          new Guid("65A2D25D-5520-47D3-8776-AE064D909285"),
        //          new Guid("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
        //          new Guid("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
        //          new Guid("A1F7BFA0-1429-4010-863D-6A0EB01DB61D"),
        //          new Guid("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
        //          new Guid("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
        //          new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"),
        //          new Guid("48F83BB5-7807-4B32-9E3C-74962CEF92E8"),
        //          new Guid("DDE89154-CCFA-4880-B9F7-61C284D2C91A"),
        //          new Guid("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
        //          new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35"),
        //          new Guid("6407D989-D643-45D8-9434-1176761663BC")
        //        }.Contains(c.LocationType_Index))
        //        ).ToList();
        //        var dateStart = data.date.toBetweenDate();
        //        var dateEnd = data.date.toBetweenDate();


        //        var queryLocAll = queryRPT_LO.Select(c => new
        //        {
        //            c.Warehouse_Name
        //           ,c.Zone_Name
        //           ,c.Location_Id
        //           ,c.RowIndex
        //           ,c.Yn
        //           ,c.Update_By
        //           ,c.Update_Date
        //           ,c.LocationType_Name
        //           ,c.BlockPick
        //           ,c.BlockPut
        //            }).ToList();

        //        var query = queryLocAll.Select(o => new
        //        {
        //            o.Warehouse_Name
        //           ,o.Zone_Name
        //           ,o.Location_Id
        //           ,o.RowIndex
        //           ,o.Yn
        //           ,o.Update_By
        //           ,o.Update_Date
        //           ,o.LocationType_Name
        //           ,o.BlockPick
        //           ,o.BlockPut
        //            }).ToList();

                
        //        if (!string.IsNullOrEmpty(data.warehouse_Name))
        //        {
        //            query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
        //        }

        //        if (!string.IsNullOrEmpty(data.location_Id))
        //        {
        //            query = query.Where(c => c.Location_Id == data.location_Id).ToList();
        //        }

              
        //        string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
        //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //        if (data.locationType_Name != null && data.locationType_Name != string.Empty)
        //        {
        //            var LocationType = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.LocationType_Name == data.locationType_Name).Select(s => s.LocationType_Name).ToList();
        //            query = query.Where(c => LocationType.Contains(c.LocationType_Name)).ToList();

        //        }

        //        if (data.yn != null && data.yn != string.Empty)
        //        {
        //            var YesNo = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Yn == data.yn).Select(s => s.Yn).ToList();
        //            query = query.Where(c => YesNo.Contains(c.Yn)).ToList();

        //        }

        //        if (!string.IsNullOrEmpty(data.value))
        //        {
        //            if (data.value == "BlockPick")
        //            {
        //                var Pick = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPick == "Block").Select(s => s.BlockPick).ToList();
        //                query = query.Where(c => Pick.Contains(c.BlockPick)).ToList();

        //            }

        //            if (data.value == "BlockPut")
        //            {
        //                var Put = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPut == "Block").Select(s => s.BlockPut).ToList();
        //                query = query.Where(c => Put.Contains(c.BlockPut)).ToList();

        //            }

        //        }

                


        //        if (query.Count == 0)
        //        {
        //            var resultItem = new ReportSummaryStockStorageViewModel();
        //            resultItem.date = selectDate;
        //            resultItem.checkQuery = true;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            foreach (var item in query.OrderBy(c => c.RowIndex))
        //            {
        //                var resultItem = new ReportSummaryStockStorageViewModel();

        //                resultItem.date = selectDate;
        //                resultItem.warehouse_Name = item.Warehouse_Name;
        //                resultItem.zone_Name = item.Zone_Name;
        //                resultItem.location_Id = item.Location_Id;
        //                resultItem.rowIndex = item.RowIndex;
        //                resultItem.yn = item.Yn;
        //                resultItem.update_By = item.Update_By;
        //                resultItem.update_Date = item.Update_Date == null ? "" : item.Update_Date.Value.ToString("dd/MM/yyyy");
        //                resultItem.locationType_Name = item.LocationType_Name;
        //                resultItem.blockPut = item.BlockPut;
        //                resultItem.blockPick = item.BlockPick;
        //                result.Add(resultItem);
        //            }
        //        }

        //        rootPath = rootPath.Replace("\\ReportAPI", "");
        //        var reportPath = rootPath + "\\ReportBusiness\\ReportSummaryStockStorage\\ReportSummaryStockStorage.rdlc";
        //        var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryStockStorage");
        //        LocalReport report = new LocalReport(reportPath);
        //        report.AddDataSource("DataSet1", result);

        //        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //        string fileName = "";
        //        string fullPath = "";
        //        fileName = "SummaryLocaton" + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss");

        //        var renderedBytes = report.Execute(RenderType.Pdf);

        //        Utils objReport = new Utils();
        //        fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
        //        var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
        //        return saveLocation;


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        #endregion

        #region printReportSummaryStockStorage
        //public List<ReportSummaryStockStorageViewModel> printReport2xx1(ReportSummaryStockStorageViewModel data, string rootPath = "")
        //{
        //    var MS_DBContext = new MasterDataDbContext();
        //    var BB_DBContext = new BinbalanceDbContext();
        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    var result = new List<ReportSummaryStockStorageViewModel>();

        //    try
        //    {
        //        MS_DBContext.Database.SetCommandTimeout(360);
        //        var queryLO = MS_DBContext.View_RPT_Location2.AsQueryable();


        //        var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0
        //        && (new List<Guid?>
        //        { new Guid("472E5117-3A7A-4C23-B8C2-7FEA55B3E69C"),
        //          new Guid("6A1FB140-CC78-4C2B-BEC8-42B2D0AE62E9"),
        //          new Guid("550B4C74-56CC-4D11-9228-F2656D8FA3F6"),
        //          new Guid("B4F94559-B9CD-454E-BF9F-B6ACDA94F493"),
        //          new Guid("3A7D807A-9F2C-4215-8703-F51846BCC4BD"),
        //          new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF"),
        //          new Guid("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
        //          new Guid("394FF62E-693F-4C21-A370-18DCAD5B4455"),
        //          new Guid("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
        //          new Guid("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
        //          new Guid("9E19ED5F-42D7-4131-BC18-610540DE9752"),
        //          new Guid("2C8D9120-14DD-4737-BBFE-3E523D303E75"),
        //          new Guid("2B7A1377-AE93-4C07-9D79-A0CEB4C343B7"),
        //          new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
        //          new Guid("2E9338D3-0931-4E36-B240-782BF9508641"),
        //          new Guid("64341969-E596-4B8B-8836-395061777490"),
        //          new Guid("1D2DF268-F004-4820-831F-B823FF9C7564"),
        //          new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC"),
        //          new Guid("7D30298A-8BA0-47ED-8342-E3F953E11D8C"),
        //          new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7"),
        //          new Guid("76C758AA-C216-406C-BDF6-14018C87CAE1"),
        //          new Guid("65A2D25D-5520-47D3-8776-AE064D909285"),
        //          new Guid("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
        //          new Guid("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
        //          new Guid("A1F7BFA0-1429-4010-863D-6A0EB01DB61D"),
        //          new Guid("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
        //          new Guid("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
        //          new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"),
        //          new Guid("48F83BB5-7807-4B32-9E3C-74962CEF92E8"),
        //          new Guid("DDE89154-CCFA-4880-B9F7-61C284D2C91A"),
        //          new Guid("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
        //          new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35"),
        //          new Guid("6407D989-D643-45D8-9434-1176761663BC")
        //        }.Contains(c.LocationType_Index))
        //        ).ToList();
        //        var dateStart = data.date.toBetweenDate();
        //        var dateEnd = data.date.toBetweenDate();

        //        var queryLocAll = queryRPT_LO.Select(c => new
        //        {
        //            c.Warehouse_Name
        //           ,
        //            c.Zone_Name
        //           ,
        //            c.Location_Id
        //           ,
        //            c.RowIndex
        //           ,
        //            c.Yn
        //           ,
        //            c.Update_By
        //           ,
        //            c.Update_Date
        //           ,
        //            c.LocationType_Name
        //           ,
        //            c.BlockPick
        //           ,
        //            c.BlockPut
        //        }).ToList();

        //        var query = queryLocAll.Select(o => new
        //        {
        //            o.Warehouse_Name
        //           ,
        //            o.Zone_Name
        //           ,
        //            o.Location_Id
        //           ,
        //            o.RowIndex
        //           ,
        //            o.Yn
        //           ,
        //            o.Update_By
        //           ,
        //            o.Update_Date
        //           ,
        //            o.LocationType_Name
        //           ,
        //            o.BlockPick
        //           ,
        //            o.BlockPut
        //        }).ToList();


        //        if (!string.IsNullOrEmpty(data.warehouse_Name))
        //        {
        //            query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
        //        }

        //        if (!string.IsNullOrEmpty(data.location_Id))
        //        {
        //            query = query.Where(c => c.Location_Id == data.location_Id).ToList();
        //        }

        //        string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
        //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                

        //        if (query.Count == 0)
        //        {
        //            var resultItem = new ReportSummaryStockStorageViewModel();
        //            resultItem.date = selectDate;
        //            resultItem.checkQuery = true;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            foreach (var item in query.OrderBy(c=>c.RowIndex))
        //            {
        //                var resultItem = new ReportSummaryStockStorageViewModel();

        //                resultItem.date = selectDate;
        //                resultItem.warehouse_Name = item.Warehouse_Name;
        //                resultItem.zone_Name = item.Zone_Name;
        //                resultItem.location_Id = item.Location_Id;
        //                resultItem.rowIndex = item.RowIndex;
        //                resultItem.yn = item.Yn;
        //                resultItem.update_By = item.Update_By;
        //                resultItem.update_Date = item.Update_Date == null ? "" : item.Update_Date.Value.ToString("dd/MM/yyyy");
        //                resultItem.locationType_Name = item.LocationType_Name;
        //                resultItem.blockPut = item.BlockPut;
        //                resultItem.blockPick = item.BlockPick;
        //                result.Add(resultItem);
        //            }
        //        }

               
        //        string fileName = "";
        //        return result;


        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}
        #endregion

        //public List<LocationTypeViewModel> getLocationType(LocationTypeViewModel data)
        //{
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

        //            if (data.listlocationType_Index?.Count() > 0)
        //            {
        //                query = query.Where(c => data.listlocationType_Index.Contains(c.LocationType_Index));
        //            }

        //            var xx = query.GroupBy(l => new { l.LocationType_Name, l.LocationType_Index }).Select(l => new { l.Key.LocationType_Name, l.Key.LocationType_Index }).OrderBy(o => o.LocationType_Name);
        //            var ListData = xx.Select(s => new LocationTypeViewModel
        //            {
        //                locationType_Index = s.LocationType_Index,
        //                locationType_Name = s.LocationType_Name
        //            }).ToList();
                    

        //            return ListData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<YnViewModel> getYn(YnViewModel data)
        //{
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

        //            if (data.listyn?.Count() > 0)
        //            {
        //                query = query.Where(c => data.listyn.Contains(c.Yn ));
        //            }

        //            var no = query.GroupBy(l => new { l.Yn }).Select(l => new { l.Key.Yn }).OrderBy(o => o.Yn);
        //            var ListData = no.Select(s => new YnViewModel
        //            {
        //                yn = s.Yn
        //            }).ToList();

        //            return ListData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public List<BlockViewModel> getBlock(BlockViewModel data)
        //{
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {
        //        using (var context = new MasterDataDbContext())
        //        {
        //            var query = context.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

        //            if (data.listblock?.Count() > 0)
        //            {
        //                query = query.Where(c => data.listblock.Contains(c.BlockPick ));
        //            }

        //            var block = query.GroupBy(l => new { l.BlockPick , l.BlockPut }).Select(l => new { l.Key.BlockPick , l.Key.BlockPut });
        //            var ListData = block.Select(s => new BlockViewModel
        //            //var ListData = query.OrderBy(o => o.Yn).Select(s => new YnViewModel
        //            {
        //                blockPick = s.BlockPick,
        //                blockPut = s.BlockPick
        //            }).ToList();

        //            return ListData;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        #region autoSearchLocation
        public List<ItemListViewModel> autoSearchLocation(ItemListViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.View_RPT_Location2.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Location_Id.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Location_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            name = item.Location_Id
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

        //public string ExportExcel(ReportSummaryStockStorageViewModel data, string rootPath = "")
        //{
        //    var MS_DBContext = new MasterDataDbContext();
        //    var BB_DBContext = new BinbalanceDbContext();
        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    var result = new List<ReportSummaryStockStorageViewModel>();

        //    try
        //    {
        //        MS_DBContext.Database.SetCommandTimeout(360);
        //        rootPath = rootPath.Replace("\\ReportAPI", "");
        //        var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryStockStorage");
        //        var queryLO = MS_DBContext.View_RPT_Location2.AsQueryable();
                

        //        var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0
        //        && (new List<Guid?>
        //        { new Guid("472E5117-3A7A-4C23-B8C2-7FEA55B3E69C"),
        //          new Guid("6A1FB140-CC78-4C2B-BEC8-42B2D0AE62E9"),
        //          new Guid("550B4C74-56CC-4D11-9228-F2656D8FA3F6"),
        //          new Guid("B4F94559-B9CD-454E-BF9F-B6ACDA94F493"),
        //          new Guid("3A7D807A-9F2C-4215-8703-F51846BCC4BD"),
        //          new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF"),
        //          new Guid("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
        //          new Guid("394FF62E-693F-4C21-A370-18DCAD5B4455"),
        //          new Guid("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
        //          new Guid("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
        //          new Guid("9E19ED5F-42D7-4131-BC18-610540DE9752"),
        //          new Guid("2C8D9120-14DD-4737-BBFE-3E523D303E75"),
        //          new Guid("2B7A1377-AE93-4C07-9D79-A0CEB4C343B7"),
        //          new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
        //          new Guid("2E9338D3-0931-4E36-B240-782BF9508641"),
        //          new Guid("64341969-E596-4B8B-8836-395061777490"),
        //          new Guid("1D2DF268-F004-4820-831F-B823FF9C7564"),
        //          new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC"),
        //          new Guid("7D30298A-8BA0-47ED-8342-E3F953E11D8C"),
        //          new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7"),
        //          new Guid("76C758AA-C216-406C-BDF6-14018C87CAE1"),
        //          new Guid("65A2D25D-5520-47D3-8776-AE064D909285"),
        //          new Guid("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
        //          new Guid("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
        //          new Guid("A1F7BFA0-1429-4010-863D-6A0EB01DB61D"),
        //          new Guid("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
        //          new Guid("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
        //          new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"),
        //          new Guid("48F83BB5-7807-4B32-9E3C-74962CEF92E8"),
        //          new Guid("DDE89154-CCFA-4880-B9F7-61C284D2C91A"),
        //          new Guid("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
        //          new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35"),
        //          new Guid("6407D989-D643-45D8-9434-1176761663BC")
        //        }.Contains(c.LocationType_Index))
        //        ).ToList();
        //        var dateStart = data.date.toBetweenDate();
        //        var dateEnd = data.date.toBetweenDate();

        //        var queryLocAll = queryRPT_LO.Select(c => new
        //        {
        //            c.Warehouse_Name
        //           ,
        //            c.Zone_Name
        //           ,
        //            c.Location_Id
        //           ,
        //            c.RowIndex
        //           ,
        //            c.Yn
        //           ,
        //            c.Update_By
        //           ,
        //            c.Update_Date
        //           ,
        //            c.LocationType_Name
        //           ,
        //            c.BlockPick
        //           ,
        //            c.BlockPut
        //        }).ToList();

        //        var query = queryLocAll.Select(o => new
        //        {
        //            o.Warehouse_Name
        //           ,
        //            o.Zone_Name
        //           ,
        //            o.Location_Id
        //           ,
        //            o.RowIndex
        //           ,
        //            o.Yn
        //           ,
        //            o.Update_By
        //           ,
        //            o.Update_Date
        //           ,
        //            o.LocationType_Name
        //           ,
        //            o.BlockPick
        //           ,
        //            o.BlockPut
        //        }).ToList();

        //        if (!string.IsNullOrEmpty(data.warehouse_Name))
        //        {
        //            query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
        //        }

        //        if (!string.IsNullOrEmpty(data.location_Id))
        //        {
        //            query = query.Where(c => c.Location_Id == data.location_Id).ToList();
        //        }

        //        string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
        //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //        if (data.locationType_Name != null && data.locationType_Name != string.Empty)
        //        {
        //            var LocationType = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.LocationType_Name == data.locationType_Name).Select(s => s.LocationType_Name).ToList();
        //            query = query.Where(c => LocationType.Contains(c.LocationType_Name)).ToList();

        //        }

        //        if (data.yn != null && data.yn != string.Empty)
        //        {
        //            var YesNo = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Yn == data.yn).Select(s => s.Yn).ToList();
        //            query = query.Where(c => YesNo.Contains(c.Yn)).ToList();

        //        }

        //        if (!string.IsNullOrEmpty(data.value))
        //        {
        //            if (data.value == "BlockPick")
        //            {
        //                var Pick = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPick == "Block").Select(s => s.BlockPick).ToList();
        //                query = query.Where(c => Pick.Contains(c.BlockPick)).ToList();

        //            }

        //            if (data.value == "BlockPut")
        //            {
        //                var Put = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPut == "Block").Select(s => s.BlockPut).ToList();
        //                query = query.Where(c => Put.Contains(c.BlockPut)).ToList();

        //            }

        //        }

        //        if (query.Count == 0)
        //        {
        //            var resultItem = new ReportSummaryStockStorageViewModel();
        //            resultItem.date = selectDate;
        //            resultItem.checkQuery = true;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            foreach (var item in query.OrderBy(c => c.RowIndex))
        //            {
        //                var resultItem = new ReportSummaryStockStorageViewModel();

        //                resultItem.date = selectDate;
        //                resultItem.warehouse_Name = item.Warehouse_Name;
        //                resultItem.zone_Name = item.Zone_Name;
        //                resultItem.location_Id = item.Location_Id;
        //                resultItem.rowIndex = item.RowIndex;
        //                resultItem.yn = item.Yn;
        //                resultItem.update_By = item.Update_By;
        //                resultItem.update_Date = item.Update_Date == null ? "" : item.Update_Date.Value.ToString("dd/MM/yyyy");
        //                resultItem.locationType_Name = item.LocationType_Name;
        //                resultItem.blockPut = item.BlockPut;
        //                resultItem.blockPick = item.BlockPick;
        //                result.Add(resultItem);
        //            }
        //        }

                             

        //        LocalReport report = new LocalReport(reportPath);
        //        report.AddDataSource("DataSet1", result.OrderBy(c => c.rowIndex));

        //        System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

        //        string fileName = "";
        //        string fullPath = "";
        //        fileName = "tmpReport";

        //        var renderedBytes = report.Execute(RenderType.Excel);
        //        fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);


        //        var saveLocation = rootPath + fullPath;
        //        //File.Delete(saveLocation);
        //        //ExcelRefresh(reportPath);
        //        return saveLocation;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

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


        #region printReportPutAway
        public dynamic printReportSummaryStockStorage(ReportSummaryStockStorageViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportSummaryStockStoragePrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);


                //var query_Vendor = Master_DBContext.MS_Vendor.AsQueryable();


                var businessunit_index = "";
                var product_Id = "";
                var goodsRecive_No = "";
                var purchaseOrder_No = "";
                var planGoodsReceive_No = "";
                var tag_No = "";
                var vendor_Id = "";
                var matDoc = "";

                var gr_dateStart_null = "";
                var gr_dateEnd_null = "";

                var putaway_dateStart_null = "";
                var putaway_dateEnd_null = "";


                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();




                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (data.goodsReceive_No != null)
                {
                    goodsRecive_No = data.goodsReceive_No;
                }
                if (data.purchaseOrder_No != null)
                {
                    purchaseOrder_No = data.purchaseOrder_No;
                }
                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    planGoodsReceive_No = data.planGoodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.vendorId))
                {
                    var query_Vendor = Master_DBContext.MS_Vendor.Where(c => c.Vendor_Id.Contains(data.vendorId)
                    || c.Vendor_Name.Contains(data.vendorId)).Select(s => new vendor
                    {
                        vendor_Id = s.Vendor_Id
                    }).Take(1).ToList().Distinct();

                    foreach (var vendorItem in query_Vendor)
                    {
                        vendor_Id = vendorItem.vendor_Id;
                    }
                }
                if (!string.IsNullOrEmpty(data.matdoc))
                {
                    matDoc = data.matdoc;
                }


                var gR_Date_From = new SqlParameter();
                var gR_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                    gR_Date_From = new SqlParameter("@GR_Date_From", dateStart);
                    gR_Date_To = new SqlParameter("@GR_Date_To", dateEnd);
                }
                else
                {
                    gR_Date_From = new SqlParameter("@GR_Date_From", gr_dateStart_null);
                    gR_Date_To = new SqlParameter("@GR_Date_To", gr_dateEnd_null);
                }

                if(data.report_date == null && data.report_date_to == null || data.report_date == "" && data.report_date_to == "")
                {
                    if (!string.IsNullOrEmpty(data.GR_Date_From) && !string.IsNullOrEmpty(data.GR_Date_To))
                    {
                        dateStart = data.GR_Date_From.toBetweenDate().start;
                        dateEnd = data.GR_Date_To.toBetweenDate().end;

                        gR_Date_From = new SqlParameter("@GR_Date_From", dateStart);
                        gR_Date_To = new SqlParameter("@GR_Date_To", dateEnd);
                    }
                    else
                    {
                        gR_Date_From = new SqlParameter("@GR_Date_From", gr_dateStart_null);
                        gR_Date_To = new SqlParameter("@GR_Date_To", gr_dateEnd_null);
                    }
                }
                


                var putAway_Date_From = new SqlParameter();
                var putAway_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.PutAway_Date_From) && !string.IsNullOrEmpty(data.PutAway_Date_To))
                {
                    dateStart = data.PutAway_Date_From.toBetweenDate().start;
                    dateEnd = data.PutAway_Date_To.toBetweenDate().end;

                    putAway_Date_From = new SqlParameter("@PutAway_Date_From", dateStart);
                    putAway_Date_To = new SqlParameter("@PutAway_Date_To", dateEnd);
                }
                else
                {
                    putAway_Date_From = new SqlParameter("@PutAway_Date_From", putaway_dateStart_null);
                    putAway_Date_To = new SqlParameter("@PutAway_Date_To", putaway_dateEnd_null);
                }




                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var Product_Id = new SqlParameter("@Product_Id", product_Id);
                var GR_No = new SqlParameter("@GoodsReceive_No", goodsRecive_No);
                var Po_No = new SqlParameter("@PurchaseOrder_No", purchaseOrder_No);
                var Plan_Gr_No = new SqlParameter("@PlanGoodsReceive_No", planGoodsReceive_No);
                var TAG_NO = new SqlParameter("@Tag_No", tag_No);
                var Ven_Id = new SqlParameter("@Vendor_Id", vendor_Id);
                var MatDoc = new SqlParameter("@Matdoc", matDoc);



                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_11_PutAway>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_11_PutAway.FromSql("sp_rpt_11_PutAway @BusinessUnit_Index , @Product_Id , @GoodsReceive_No , @PurchaseOrder_No , @PlanGoodsReceive_No , @GR_Date_From , @GR_Date_To , @Tag_No , @Vendor_Id , @Matdoc , @PutAway_Date_From , @PutAway_Date_To"
                        , Bu_Index , Product_Id , GR_No , Po_No , Plan_Gr_No , gR_Date_From , gR_Date_To , TAG_NO , Ven_Id , MatDoc , putAway_Date_From , putAway_Date_To).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_11_PutAway.FromSql("sp_rpt_11_PutAway @BusinessUnit_Index , @Product_Id , @GoodsReceive_No , @PurchaseOrder_No , @PlanGoodsReceive_No , @GR_Date_From , @GR_Date_To , @Tag_No , @Vendor_Id , @Matdoc , @PutAway_Date_From , @PutAway_Date_To"
                        , Bu_Index, Product_Id, GR_No, Po_No, Plan_Gr_No, gR_Date_From, gR_Date_To, TAG_NO, Ven_Id, MatDoc, putAway_Date_From, putAway_Date_To).ToList();
                }


                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportSummaryStockStoragePrintViewModel();
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in resultquery)
                    {
                        var resultItem = new ReportSummaryStockStoragePrintViewModel();
                        resultItem.row_Num = item.Row_No;
                        resultItem.warehouse_Type = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                        resultItem.asn_No = item.PlanGoodsReceive_No;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.putAwayLoaction_Id = item.PutawayLocation_Id;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.putAway_Qty = item.PutAway_Qty.ToString();
                        resultItem.putAway_Su = item.PutAway_SU;
                        resultItem.tixHi = item.TixHi.ToString();
                        resultItem.qty_Pallet = item.Qty_Pallet.ToString();
                        resultItem.palletHeight = item.PalletHeight.ToString();
                        resultItem.limitHeight = item.LimitHeight.ToString();
                        resultItem.palletWeight = item.PalletWeight.ToString();
                        resultItem.gr_Date =  item.GR_Date != null ? item.GR_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.putAway_Date = item.Putaway_Date != null ? item.Putaway_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.putAway_Time = item.Putaway_Time;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.mat_Doc = item.Matdoc;
                        resultItem.update_By = item.Update_By;
                        resultItem.sup_Name = item.Vendor_Name;
                        resultItem.summary_Qty = item.PutAway_Qty;
                        resultItem.sap_Sloc = item.SAP_Sloc;
                        resultItem.wms_Sloc = item.WMS_Sloc;
                        resultItem.exp_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.mfg_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        
                        result.Add(resultItem);
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPutAway");
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

        public string ExportExcel(ReportSummaryStockStorageViewModel data, string rootPath = "")
        {

            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();


            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportSummaryStockStoragePrintViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);


                //var query_Vendor = Master_DBContext.MS_Vendor.AsQueryable();


                var businessunit_index = "";
                var product_Id = "";
                var goodsRecive_No = "";
                var purchaseOrder_No = "";
                var planGoodsReceive_No = "";
                var tag_No = "";
                var vendor_Id = "";
                var matDoc = "";

                var gr_dateStart_null = "";
                var gr_dateEnd_null = "";

                var putaway_dateStart_null = "";
                var putaway_dateEnd_null = "";


                DateTime dateStart = DateTime.Now.toString().toBetweenDate().start;
                DateTime dateEnd = DateTime.Now.toString().toBetweenDate().end;

                var statusModels = new List<int?>();




                if (data.businessUnitList != null)
                {
                    businessunit_index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    product_Id = data.product_Id;
                }
                if (data.goodsReceive_No != null)
                {
                    goodsRecive_No = data.goodsReceive_No;
                }
                if (data.purchaseOrder_No != null)
                {
                    purchaseOrder_No = data.purchaseOrder_No;
                }
                if (!string.IsNullOrEmpty(data.planGoodsReceive_No))
                {
                    planGoodsReceive_No = data.planGoodsReceive_No;
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    tag_No = data.tag_No;
                }
                if (!string.IsNullOrEmpty(data.vendorId))
                {
                    var query_Vendor = Master_DBContext.MS_Vendor.Where(c => c.Vendor_Id.Contains(data.vendorId)
                    || c.Vendor_Name.Contains(data.vendorId)).Select(s => new vendor
                    {
                        vendor_Id = s.Vendor_Id
                    }).Take(1).ToList().Distinct();

                    foreach (var vendorItem in query_Vendor)
                    {
                        vendor_Id = vendorItem.vendor_Id;
                    }
                }
                if (!string.IsNullOrEmpty(data.matdoc))
                {
                    matDoc = data.matdoc;
                }


                var gR_Date_From = new SqlParameter();
                var gR_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.report_date) && !string.IsNullOrEmpty(data.report_date_to))
                {
                    dateStart = data.report_date.toBetweenDate().start;
                    dateEnd = data.report_date_to.toBetweenDate().end;

                    gR_Date_From = new SqlParameter("@GR_Date_From", dateStart);
                    gR_Date_To = new SqlParameter("@GR_Date_To", dateEnd);
                }
                else
                {
                    gR_Date_From = new SqlParameter("@GR_Date_From", gr_dateStart_null);
                    gR_Date_To = new SqlParameter("@GR_Date_To", gr_dateEnd_null);
                }

                if (data.report_date == null && data.report_date_to == null || data.report_date == "" && data.report_date_to == "")
                {
                    if (!string.IsNullOrEmpty(data.GR_Date_From) && !string.IsNullOrEmpty(data.GR_Date_To))
                    {
                        dateStart = data.GR_Date_From.toBetweenDate().start;
                        dateEnd = data.GR_Date_To.toBetweenDate().end;

                        gR_Date_From = new SqlParameter("@GR_Date_From", dateStart);
                        gR_Date_To = new SqlParameter("@GR_Date_To", dateEnd);
                    }
                    else
                    {
                        gR_Date_From = new SqlParameter("@GR_Date_From", gr_dateStart_null);
                        gR_Date_To = new SqlParameter("@GR_Date_To", gr_dateEnd_null);
                    }
                }



                var putAway_Date_From = new SqlParameter();
                var putAway_Date_To = new SqlParameter();

                if (!string.IsNullOrEmpty(data.PutAway_Date_From) && !string.IsNullOrEmpty(data.PutAway_Date_To))
                {
                    dateStart = data.PutAway_Date_From.toBetweenDate().start;
                    dateEnd = data.PutAway_Date_To.toBetweenDate().end;

                    putAway_Date_From = new SqlParameter("@PutAway_Date_From", dateStart);
                    putAway_Date_To = new SqlParameter("@PutAway_Date_To", dateEnd);
                }
                else
                {
                    putAway_Date_From = new SqlParameter("@PutAway_Date_From", putaway_dateStart_null);
                    putAway_Date_To = new SqlParameter("@PutAway_Date_To", putaway_dateEnd_null);
                }




                var Bu_Index = new SqlParameter("@BusinessUnit_Index", businessunit_index);
                var Product_Id = new SqlParameter("@Product_Id", product_Id);
                var GR_No = new SqlParameter("@GoodsReceive_No", goodsRecive_No);
                var Po_No = new SqlParameter("@PurchaseOrder_No", purchaseOrder_No);
                var Plan_Gr_No = new SqlParameter("@PlanGoodsReceive_No", planGoodsReceive_No);
                var TAG_NO = new SqlParameter("@Tag_No", tag_No);
                var Ven_Id = new SqlParameter("@Vendor_Id", vendor_Id);
                var MatDoc = new SqlParameter("@Matdoc", matDoc);



                var resultquery = new List<MasterDataDataAccess.Models.sp_rpt_11_PutAway>();
                if (data.ambientRoom != "02")
                {
                    resultquery = Master_DBContext.sp_rpt_11_PutAway.FromSql("sp_rpt_11_PutAway @BusinessUnit_Index , @Product_Id , @GoodsReceive_No , @PurchaseOrder_No , @PlanGoodsReceive_No , @GR_Date_From , @GR_Date_To , @Tag_No , @Vendor_Id , @Matdoc , @PutAway_Date_From , @PutAway_Date_To"
                        , Bu_Index, Product_Id, GR_No, Po_No, Plan_Gr_No, gR_Date_From, gR_Date_To, TAG_NO, Ven_Id, MatDoc, putAway_Date_From, putAway_Date_To).ToList();
                }
                else
                {
                    resultquery = temp_Master_DBContext.sp_rpt_11_PutAway.FromSql("sp_rpt_11_PutAway @BusinessUnit_Index , @Product_Id , @GoodsReceive_No , @PurchaseOrder_No , @PlanGoodsReceive_No , @GR_Date_From , @GR_Date_To , @Tag_No , @Vendor_Id , @Matdoc , @PutAway_Date_From , @PutAway_Date_To"
                        , Bu_Index, Product_Id, GR_No, Po_No, Plan_Gr_No, gR_Date_From, gR_Date_To, TAG_NO, Ven_Id, MatDoc, putAway_Date_From, putAway_Date_To).ToList();
                }


                if (resultquery.Count == 0)
                {
                    var resultItem = new ReportSummaryStockStoragePrintViewModel();
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in resultquery)
                    {
                        var resultItem = new ReportSummaryStockStoragePrintViewModel();
                        resultItem.row_Num = item.Row_No;
                        resultItem.warehouse_Type = item.Warehouse_Type;
                        resultItem.businessUnit_Name = item.BusinessUnit_Name;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.purchaseOrder_No = item.PurchaseOrder_No;
                        resultItem.asn_No = item.PlanGoodsReceive_No;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.putAwayLoaction_Id = item.PutawayLocation_Id;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.putAway_Qty = item.PutAway_Qty.ToString();
                        resultItem.putAway_Su = item.PutAway_SU;
                        resultItem.tixHi = item.TixHi.ToString();
                        resultItem.qty_Pallet = item.Qty_Pallet.ToString();
                        resultItem.palletHeight = item.PalletHeight.ToString();
                        resultItem.limitHeight = item.LimitHeight.ToString();
                        resultItem.palletWeight = item.PalletWeight.ToString();
                        resultItem.gr_Date = item.GR_Date != null ? item.GR_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.putAway_Date = item.Putaway_Date != null ? item.Putaway_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.putAway_Time = item.Putaway_Time;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.mat_Doc = item.Matdoc;
                        resultItem.update_By = item.Update_By;
                        resultItem.sup_Name = item.Vendor_Name;
                        resultItem.summary_Qty = item.PutAway_Qty;
                        resultItem.sap_Sloc = item.SAP_Sloc;
                        resultItem.wms_Sloc = item.WMS_Sloc;
                        resultItem.exp_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.mfg_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";

                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportPutAway");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

                Utils objReport = new Utils();
                var renderedBytes = report.Execute(RenderType.Excel);
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
