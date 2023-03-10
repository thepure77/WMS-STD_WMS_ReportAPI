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
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report21
{
    public class Report21Service
    {

        #region printReport21
        public dynamic printReport21(Report21ViewModel data, string rootPath = "")
        {
            
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report21ViewModel>();

            try
            {
                MS_DBContext.Database.SetCommandTimeout(360);
                var queryLO = MS_DBContext.View_RPT_Location2.AsQueryable();
                

                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0 
                && (new List<Guid?>
                { new Guid("472E5117-3A7A-4C23-B8C2-7FEA55B3E69C"),
                  new Guid("6A1FB140-CC78-4C2B-BEC8-42B2D0AE62E9"),
                  new Guid("550B4C74-56CC-4D11-9228-F2656D8FA3F6"),
                  new Guid("B4F94559-B9CD-454E-BF9F-B6ACDA94F493"),
                  new Guid("3A7D807A-9F2C-4215-8703-F51846BCC4BD"),
                  new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF"),
                  new Guid("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
                  new Guid("394FF62E-693F-4C21-A370-18DCAD5B4455"),
                  new Guid("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
                  new Guid("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
                  new Guid("9E19ED5F-42D7-4131-BC18-610540DE9752"),
                  new Guid("2C8D9120-14DD-4737-BBFE-3E523D303E75"),
                  new Guid("2B7A1377-AE93-4C07-9D79-A0CEB4C343B7"),
                  new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
                  new Guid("2E9338D3-0931-4E36-B240-782BF9508641"),
                  new Guid("64341969-E596-4B8B-8836-395061777490"),
                  new Guid("1D2DF268-F004-4820-831F-B823FF9C7564"),
                  new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC"),
                  new Guid("7D30298A-8BA0-47ED-8342-E3F953E11D8C"),
                  new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7"),
                  new Guid("76C758AA-C216-406C-BDF6-14018C87CAE1"),
                  new Guid("65A2D25D-5520-47D3-8776-AE064D909285"),
                  new Guid("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
                  new Guid("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
                  new Guid("A1F7BFA0-1429-4010-863D-6A0EB01DB61D"),
                  new Guid("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
                  new Guid("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
                  new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"),
                  new Guid("48F83BB5-7807-4B32-9E3C-74962CEF92E8"),
                  new Guid("DDE89154-CCFA-4880-B9F7-61C284D2C91A"),
                  new Guid("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
                  new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35"),
                  new Guid("6407D989-D643-45D8-9434-1176761663BC")
                }.Contains(c.LocationType_Index))
                ).ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();


                var queryLocAll = queryRPT_LO.Select(c => new
                {
                    c.Warehouse_Name
                   ,c.Zone_Name
                   ,c.Location_Id
                   ,c.RowIndex
                   ,c.Yn
                   ,c.Update_By
                   ,c.Update_Date
                   ,c.LocationType_Name
                   ,c.BlockPick
                   ,c.BlockPut
                    }).ToList();

                var query = queryLocAll.Select(o => new
                {
                    o.Warehouse_Name
                   ,o.Zone_Name
                   ,o.Location_Id
                   ,o.RowIndex
                   ,o.Yn
                   ,o.Update_By
                   ,o.Update_Date
                   ,o.LocationType_Name
                   ,o.BlockPick
                   ,o.BlockPut
                    }).ToList();

                
                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
                }

                if (!string.IsNullOrEmpty(data.location_Id))
                {
                    query = query.Where(c => c.Location_Id == data.location_Id).ToList();
                }

              
                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (data.locationType_Name != null && data.locationType_Name != string.Empty)
                {
                    //var LocationType = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.LocationType_Name == data.locationType_Name).Select(s => s.LocationType_Name).ToList();
                    //query = query.Where(c => LocationType.Contains(c.LocationType_Name)).ToList();
                    query = query.Where(c => c.LocationType_Name == data.locationType_Name).ToList();

                }

                //if (data.yn != null && data.yn != string.Empty)
                //{
                //    var YesNo = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Yn == data.yn).Select(s => s.Yn).ToList();
                //    query = query.Where(c => YesNo.Contains(c.Yn)).ToList();

                //}
                if (!string.IsNullOrEmpty(data.yn) )
                {
                    if ( data.yn == "Reserve")
                    {
                        //var Not = MS_DBContext.View_RPT_Location2.Where(c => c.Yn.Contains("Not empty : ")).Select(s => s.Yn).ToList();
                        query = query.Where(c => c.Yn.Contains("Reserve")).ToList();
                    }
                    else
                    {
                        //var YesNo = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Yn == data.yn).Select(s => s.Yn).ToList();
                        //query = query.Where(c => YesNo.Contains(c.Yn)).ToList();
                        query = query.Where(c => c.Yn == data.yn).ToList();
                    }
                }
                
                //else
                //{
                //    var Not empty = MS_DBContext.View_RPT_Location2.Where(c => c.Yn == "Empty").Select(s => s.Yn).ToList();
                //}

                if (!string.IsNullOrEmpty(data.value))
                {
                    if (data.value == "BlockPick")
                    {
                        var Pick = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPick == "Block").Select(s => s.BlockPick).ToList();
                        query = query.Where(c => Pick.Contains(c.BlockPick)).ToList();

                    }

                    if (data.value == "BlockPut")
                    {
                        var Put = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPut == "Block").Select(s => s.BlockPut).ToList();
                        query = query.Where(c => Put.Contains(c.BlockPut)).ToList();

                    }

                }

                


                if (query.Count == 0)
                {
                    var resultItem = new Report21ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query.OrderBy(c => c.RowIndex))
                    {
                        var resultItem = new Report21ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.zone_Name = item.Zone_Name;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.rowIndex = item.RowIndex;
                        resultItem.yn = item.Yn;
                        resultItem.update_By = item.Update_By;
                        resultItem.update_Date = item.Update_Date == null ? "" : item.Update_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.blockPut = item.BlockPut;
                        resultItem.blockPick = item.BlockPick;
                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report21\\Report21.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report21");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "SummaryLocaton" + DateTime.Now.ToString("_yyyy-MM-dd_HH.mm.ss");

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

        #region printReport21
        public List<Report21ViewModel> printReport2xx1(Report21ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report21ViewModel>();

            try
            {
                MS_DBContext.Database.SetCommandTimeout(360);
                var queryLO = MS_DBContext.View_RPT_Location2.AsQueryable();


                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0
                && (new List<Guid?>
                { new Guid("472E5117-3A7A-4C23-B8C2-7FEA55B3E69C"),
                  new Guid("6A1FB140-CC78-4C2B-BEC8-42B2D0AE62E9"),
                  new Guid("550B4C74-56CC-4D11-9228-F2656D8FA3F6"),
                  new Guid("B4F94559-B9CD-454E-BF9F-B6ACDA94F493"),
                  new Guid("3A7D807A-9F2C-4215-8703-F51846BCC4BD"),
                  new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF"),
                  new Guid("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
                  new Guid("394FF62E-693F-4C21-A370-18DCAD5B4455"),
                  new Guid("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
                  new Guid("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
                  new Guid("9E19ED5F-42D7-4131-BC18-610540DE9752"),
                  new Guid("2C8D9120-14DD-4737-BBFE-3E523D303E75"),
                  new Guid("2B7A1377-AE93-4C07-9D79-A0CEB4C343B7"),
                  new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
                  new Guid("2E9338D3-0931-4E36-B240-782BF9508641"),
                  new Guid("64341969-E596-4B8B-8836-395061777490"),
                  new Guid("1D2DF268-F004-4820-831F-B823FF9C7564"),
                  new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC"),
                  new Guid("7D30298A-8BA0-47ED-8342-E3F953E11D8C"),
                  new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7"),
                  new Guid("76C758AA-C216-406C-BDF6-14018C87CAE1"),
                  new Guid("65A2D25D-5520-47D3-8776-AE064D909285"),
                  new Guid("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
                  new Guid("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
                  new Guid("A1F7BFA0-1429-4010-863D-6A0EB01DB61D"),
                  new Guid("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
                  new Guid("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
                  new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"),
                  new Guid("48F83BB5-7807-4B32-9E3C-74962CEF92E8"),
                  new Guid("DDE89154-CCFA-4880-B9F7-61C284D2C91A"),
                  new Guid("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
                  new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35"),
                  new Guid("6407D989-D643-45D8-9434-1176761663BC")
                }.Contains(c.LocationType_Index))
                ).ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();

                var queryLocAll = queryRPT_LO.Select(c => new
                {
                    c.Warehouse_Name
                   ,
                    c.Zone_Name
                   ,
                    c.Location_Id
                   ,
                    c.RowIndex
                   ,
                    c.Yn
                   ,
                    c.Update_By
                   ,
                    c.Update_Date
                   ,
                    c.LocationType_Name
                   ,
                    c.BlockPick
                   ,
                    c.BlockPut
                }).ToList();

                var query = queryLocAll.Select(o => new
                {
                    o.Warehouse_Name
                   ,
                    o.Zone_Name
                   ,
                    o.Location_Id
                   ,
                    o.RowIndex
                   ,
                    o.Yn
                   ,
                    o.Update_By
                   ,
                    o.Update_Date
                   ,
                    o.LocationType_Name
                   ,
                    o.BlockPick
                   ,
                    o.BlockPut
                }).ToList();


                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
                }

                if (!string.IsNullOrEmpty(data.location_Id))
                {
                    query = query.Where(c => c.Location_Id == data.location_Id).ToList();
                }

                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                

                if (query.Count == 0)
                {
                    var resultItem = new Report21ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query.OrderBy(c=>c.RowIndex))
                    {
                        var resultItem = new Report21ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.zone_Name = item.Zone_Name;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.rowIndex = item.RowIndex;
                        resultItem.yn = item.Yn;
                        resultItem.update_By = item.Update_By;
                        resultItem.update_Date = item.Update_Date == null ? "" : item.Update_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.blockPut = item.BlockPut;
                        resultItem.blockPick = item.BlockPick;
                        result.Add(resultItem);
                    }
                }

               
                string fileName = "";
                return result;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        #endregion

        public List<LocationTypeViewModel> getLocationType(LocationTypeViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (data.listlocationType_Index?.Count() > 0)
                    {
                        query = query.Where(c => data.listlocationType_Index.Contains(c.LocationType_Index));
                    }

                    var xx = query.GroupBy(l => new { l.LocationType_Name, l.LocationType_Index }).Select(l => new { l.Key.LocationType_Name, l.Key.LocationType_Index }).OrderBy(o => o.LocationType_Name);
                    var ListData = xx.Select(s => new LocationTypeViewModel
                    {
                        locationType_Index = s.LocationType_Index,
                        locationType_Name = s.LocationType_Name
                    }).ToList();
                    

                    return ListData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<YnViewModel> getYn(YnViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (data.listyn?.Count() > 0)
                    {
                        query = query.Where(c => data.listyn.Contains(c.Yn ));
                    }

                    var no = query.GroupBy(l => new { l.Yn }).Select(l => new { l.Key.Yn }).OrderBy(o => o.Yn);
                    var ListData = no.Select(s => new YnViewModel
                    {
                        yn = s.Yn
                    }).ToList();

                    return ListData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<BlockViewModel> getBlock(BlockViewModel data)
        {
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                using (var context = new MasterDataDbContext())
                {
                    var query = context.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (data.listblock?.Count() > 0)
                    {
                        query = query.Where(c => data.listblock.Contains(c.BlockPick ));
                    }

                    var block = query.GroupBy(l => new { l.BlockPick , l.BlockPut }).Select(l => new { l.Key.BlockPick , l.Key.BlockPut });
                    var ListData = block.Select(s => new BlockViewModel
                    //var ListData = query.OrderBy(o => o.Yn).Select(s => new YnViewModel
                    {
                        blockPick = s.BlockPick,
                        blockPut = s.BlockPick
                    }).ToList();

                    return ListData;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        public string ExportExcel(Report21ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report21ViewModel>();

            try
            {
                MS_DBContext.Database.SetCommandTimeout(360);
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report21");
                var queryLO = MS_DBContext.View_RPT_Location2.AsQueryable();


                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0
                && (new List<Guid?>
                { new Guid("472E5117-3A7A-4C23-B8C2-7FEA55B3E69C"),
                  new Guid("6A1FB140-CC78-4C2B-BEC8-42B2D0AE62E9"),
                  new Guid("550B4C74-56CC-4D11-9228-F2656D8FA3F6"),
                  new Guid("B4F94559-B9CD-454E-BF9F-B6ACDA94F493"),
                  new Guid("3A7D807A-9F2C-4215-8703-F51846BCC4BD"),
                  new Guid("D4DFC92C-C5DC-4397-BF87-FEEEB579C0AF"),
                  new Guid("7F3E1BC2-F18B-4B16-80A9-2394EB8BBE63"),
                  new Guid("394FF62E-693F-4C21-A370-18DCAD5B4455"),
                  new Guid("8A545442-77A3-43A4-939A-6B9102DFE8C6"),
                  new Guid("A706D789-F5C9-41A6-BEC7-E57034DFC166"),
                  new Guid("9E19ED5F-42D7-4131-BC18-610540DE9752"),
                  new Guid("2C8D9120-14DD-4737-BBFE-3E523D303E75"),
                  new Guid("2B7A1377-AE93-4C07-9D79-A0CEB4C343B7"),
                  new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0"),
                  new Guid("2E9338D3-0931-4E36-B240-782BF9508641"),
                  new Guid("64341969-E596-4B8B-8836-395061777490"),
                  new Guid("1D2DF268-F004-4820-831F-B823FF9C7564"),
                  new Guid("E4310B71-D6A7-4FF6-B4A8-EACBDFADAFFC"),
                  new Guid("7D30298A-8BA0-47ED-8342-E3F953E11D8C"),
                  new Guid("DEA384FD-3EEF-49A2-A88C-04ABA5C114A7"),
                  new Guid("76C758AA-C216-406C-BDF6-14018C87CAE1"),
                  new Guid("65A2D25D-5520-47D3-8776-AE064D909285"),
                  new Guid("02F5CBFC-769A-411B-9146-1D27F92AE82D"),
                  new Guid("E4F856EA-9685-45A4-995C-C05FF9E499C4"),
                  new Guid("A1F7BFA0-1429-4010-863D-6A0EB01DB61D"),
                  new Guid("DB5D9770-F087-4D5C-89DF-5F87BDD0BC02"),
                  new Guid("BA0142A8-98B7-4E0B-A1CE-6266716F5F67"),
                  new Guid("14C5F85D-137D-470E-8C70-C1E535005DC3"),
                  new Guid("48F83BB5-7807-4B32-9E3C-74962CEF92E8"),
                  new Guid("DDE89154-CCFA-4880-B9F7-61C284D2C91A"),
                  new Guid("E77778D2-7A8E-448D-BA31-CD35FD938FC3"),
                  new Guid("94D86CEA-3D04-4304-9E97-28E954F03C35"),
                  new Guid("6407D989-D643-45D8-9434-1176761663BC")
                }.Contains(c.LocationType_Index))
                ).ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();


                var queryLocAll = queryRPT_LO.Select(c => new
                {
                    c.Warehouse_Name
                   ,
                    c.Zone_Name
                   ,
                    c.Location_Id
                   ,
                    c.RowIndex
                   ,
                    c.Yn
                   ,
                    c.Update_By
                   ,
                    c.Update_Date
                   ,
                    c.LocationType_Name
                   ,
                    c.BlockPick
                   ,
                    c.BlockPut
                }).ToList();

                var query = queryLocAll.Select(o => new
                {
                    o.Warehouse_Name
                   ,
                    o.Zone_Name
                   ,
                    o.Location_Id
                   ,
                    o.RowIndex
                   ,
                    o.Yn
                   ,
                    o.Update_By
                   ,
                    o.Update_Date
                   ,
                    o.LocationType_Name
                   ,
                    o.BlockPick
                   ,
                    o.BlockPut
                }).ToList();


                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
                }

                if (!string.IsNullOrEmpty(data.location_Id))
                {
                    query = query.Where(c => c.Location_Id == data.location_Id).ToList();
                }


                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (data.locationType_Name != null && data.locationType_Name != string.Empty)
                {
                    //var LocationType = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.LocationType_Name == data.locationType_Name).Select(s => s.LocationType_Name).ToList();
                    //query = query.Where(c => LocationType.Contains(c.LocationType_Name)).ToList();
                    query = query.Where(c => c.LocationType_Name == data.locationType_Name).ToList();

                }

                //if (data.yn != null && data.yn != string.Empty)
                //{
                //    var YesNo = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Yn == data.yn).Select(s => s.Yn).ToList();
                //    query = query.Where(c => YesNo.Contains(c.Yn)).ToList();

                //}
                if (!string.IsNullOrEmpty(data.yn))
                {
                    if (data.yn == "Reserve")
                    {
                        //var Not = MS_DBContext.View_RPT_Location2.Where(c => c.Yn.Contains("Not empty : ")).Select(s => s.Yn).ToList();
                        query = query.Where(c => c.Yn.Contains("Reserve")).ToList();
                    }
                    else
                    {
                        //var YesNo = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Yn == data.yn).Select(s => s.Yn).ToList();
                        //query = query.Where(c => YesNo.Contains(c.Yn)).ToList();
                        query = query.Where(c => c.Yn == data.yn).ToList();
                    }
                }

                //else
                //{
                //    var Not empty = MS_DBContext.View_RPT_Location2.Where(c => c.Yn == "Empty").Select(s => s.Yn).ToList();
                //}

                if (!string.IsNullOrEmpty(data.value))
                {
                    if (data.value == "BlockPick")
                    {
                        var Pick = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPick == "Block").Select(s => s.BlockPick).ToList();
                        query = query.Where(c => Pick.Contains(c.BlockPick)).ToList();

                    }

                    if (data.value == "BlockPut")
                    {
                        var Put = MS_DBContext.View_RPT_Location2.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.BlockPut == "Block").Select(s => s.BlockPut).ToList();
                        query = query.Where(c => Put.Contains(c.BlockPut)).ToList();

                    }

                }




                if (query.Count == 0)
                {
                    var resultItem = new Report21ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query.OrderBy(c => c.RowIndex))
                    {
                        var resultItem = new Report21ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.zone_Name = item.Zone_Name;
                        resultItem.location_Id = item.Location_Id;
                        resultItem.rowIndex = item.RowIndex;
                        resultItem.yn = item.Yn;
                        resultItem.update_By = item.Update_By;
                        resultItem.update_Date = item.Update_Date == null ? "" : item.Update_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.locationType_Name = item.LocationType_Name;
                        resultItem.blockPut = item.BlockPut;
                        resultItem.blockPick = item.BlockPick;
                        result.Add(resultItem);
                    }
                }

                             

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result.OrderBy(c => c.rowIndex));

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
