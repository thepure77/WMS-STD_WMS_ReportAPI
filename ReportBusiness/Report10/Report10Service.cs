using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using ReportBusiness.ReportAutocomplete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report10
{
    public class Report10Service
    {

        #region printReport10
        public dynamic printReport10(Report10ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report10ViewModel>();

            try
            {
                var queryLO = MS_DBContext.View_RPT_Location.AsQueryable();

                //var queryLO = MS_DBContext.View_RPT10_Location.AsQueryable();
                var queryBC = BB_DBContext.wm_BinCard.AsQueryable();

                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    queryLO = queryLO.Where(c => c.Warehouse_Name == data.warehouse_Name);
                }

                if (!string.IsNullOrEmpty(data.zone_Id))
                {
                    queryLO = queryLO.Where(c => c.Zone_Id == data.zone_Id);
                }

                if (!string.IsNullOrEmpty(data.location_Level))
                {
                    queryLO = queryLO.Where(c => c.Location_Level == data.location_Level.sParse<int>());
                }

                if (!string.IsNullOrEmpty(data.locationLock_Id))
                {
                    queryLO = queryLO.Where(c => c.LocationLock_Id == data.locationLock_Id);
                }

                if (!string.IsNullOrEmpty(data.location_Prefix_Desc))
                {
                    queryLO = queryLO.Where(c => c.Location_Prefix == data.location_Prefix_Desc);
                }

                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                var queryRPT_BC = queryBC.ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();

                //queryRPT_LO.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Zone_Id );
                var queryLocAll = queryRPT_LO.GroupBy(c => new
                {
                    c.Warehouse_Name,
                    c.Zone_Id,
                    c.Zone_Name,
                    c.Location_Aisle,
                    c.Location_Level,
                    c.Location_Prefix
                }).Select(c => new
                {
                    c.Key.Warehouse_Name,
                    c.Key.Zone_Id,
                    c.Key.Zone_Name,
                    c.Key.Location_Aisle,
                    c.Key.Location_Level,
                    c.Key.Location_Prefix,
                    Count_Location_ALL = c.Select(s => s.Location_Name).Count(),
                    Count_Location_USE = 0
                }).ToList();


                var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                {
                    c.Location_Index,
                }).Where(w => w.Sum(s => s.BinCard_QtySign) > 0)
                 .Select(c => new
                 {
                     c.Key.Location_Index,
                 }).ToList();
                //var queryBin = queryBinCard.Where(c => c.BinCard_QtySign > 0).ToList();

                var joinBin = (from BC in queryBinCard
                               join LC in queryRPT_LO on BC.Location_Index equals LC.Location_Index
                               select new
                               {
                                   Bin = BC,
                                   Loc = LC
                               }).ToList();

                var queryLocUse = joinBin.GroupBy(c => new
                {
                    c.Loc?.Warehouse_Name,
                    c.Loc?.Zone_Id,
                    c.Loc?.Zone_Name,
                    c.Loc?.Location_Aisle,
                    c.Loc?.Location_Level,
                    c.Loc?.Location_Prefix
                }).Select(c => new
                {
                    c.Key.Warehouse_Name,
                    c.Key.Zone_Id,
                    c.Key.Zone_Name,
                    c.Key.Location_Aisle,
                    c.Key.Location_Level,
                    c.Key.Location_Prefix,
                    Count_Location_ALL = 0,
                    Count_Location_USE = c.Select(s => s.Loc?.Location_Index).Count(),
                }).ToList();


                var query = queryLocAll.Union(queryLocUse).GroupBy(o => new
                {
                    o.Warehouse_Name,
                    o.Zone_Id,
                    o.Zone_Name,
                    o.Location_Aisle,
                    o.Location_Level,
                    o.Location_Prefix
                }).Select(o => new
                {
                    o.Key.Warehouse_Name,
                    o.Key.Zone_Id,
                    o.Key.Zone_Name,
                    o.Key.Location_Aisle,
                    o.Key.Location_Level,
                    o.Key.Location_Prefix,
                    Count_Location_ALL = o.Sum(s => s.Count_Location_ALL),
                    Count_Location_USE = o.Sum(s => s.Count_Location_USE),
                    Count_Location_REMAIN = o.Sum(s => s.Count_Location_ALL) - o.Sum(s => s.Count_Location_USE)
                }).ToList();


                // var queryLocAll = queryRPT_LO.GroupBy(c => new
                // {
                //     c.Warehouse_Index,
                //     c.Warehouse_Name,
                //     c.Zone_Index,
                //     c.Zone_Id,
                //     c.Zone_Name,

                // })
                // .Select(c => new
                // {
                //     c.Key.Warehouse_Index,
                //     c.Key.Warehouse_Name,
                //     c.Key.Zone_Index,
                //     c.Key.Zone_Id,
                //     c.Key.Zone_Name,

                //     loCountAll = c.Select(s => s.Location_Index).Count(),

                // }).ToList();


                // var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                // {
                //     c.Location_Index,
                //     c.Location_Name,
                // })
                //  .Select(c => new
                //  {
                //      c.Key.Location_Index,
                //      c.Key.Location_Name,
                //      BinCard_QtyIn = c.Sum(s => s.BinCard_QtyIn),
                //      BinCard_QtyOut = c.Sum(s => s.BinCard_QtyOut),
                //      BinCard_QtySign = c.Sum(s => s.BinCard_QtySign),

                //      loCountUse = c.Select(s => s.Location_Index).Count(),

                //  }).ToList();

                // var queryBin = queryBinCard.Where(c => c.BinCard_QtySign > 0).ToList();

                // var joinBin = (from BC in queryBin
                //                    join LC in queryRPT_LO on BC.Location_Index equals LC.Location_Index into ps
                //                    from r in ps.DefaultIfEmpty()
                //                      select new
                //                    {
                //                        Bin = BC,
                //                        Loc = r
                //                    }).ToList();

                // var queryLocUse = joinBin.GroupBy(c => new
                // {
                //     c.Loc.Warehouse_Index,
                //     c.Loc.Warehouse_Name,
                //     c.Loc.Zone_Index,
                //     c.Loc.Zone_Id,
                //     c.Loc.Zone_Name,

                // })
                //.Select(c => new
                //{
                //    c.Key.Warehouse_Index,
                //    c.Key.Warehouse_Name,
                //    c.Key.Zone_Index,
                //    c.Key.Zone_Id,
                //    c.Key.Zone_Name,

                //    loCountUse = c.Select(s => s.Loc.Location_Index).Count(),

                //}).ToList();


                // var query = (from locALL in queryLocAll
                //              join locUSE in queryLocUse on new { locALL.Warehouse_Index, locALL.Zone_Index } equals new { locUSE.Warehouse_Index, locUSE.Zone_Index } into W
                //              from a in W.DefaultIfEmpty()
                //              orderby locALL.Warehouse_Name ascending, locALL.Zone_Index ascending
                //              select new
                //              {
                //                  all = locALL,
                //                  use = a

                //              }).ToList();

                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
                }

                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new Report10ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    resultItem.countAll = 0;
                    resultItem.countUse = 0;
                    resultItem.countEmpty = 0;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        var resultItem = new Report10ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.Zone_Id = item.Zone_Id;
                        resultItem.Zone_name = item.Zone_Name;
                        resultItem.countAll = item.Count_Location_ALL;
                        resultItem.percenAll = 100;
                        resultItem.Location_Aisle = item.Location_Aisle;
                        resultItem.Location_Level = item.Location_Level;
                        resultItem.Location_Prefix = item.Location_Prefix;
                        if (item.Count_Location_USE == 0 || item.Count_Location_ALL == 0)
                        {
                            resultItem.countUse = null;
                            resultItem.percenUse = null;
                            resultItem.countEmpty = null;
                            resultItem.percenEmpty = null;
                        }
                        else
                        {

                            resultItem.countUse = item.Count_Location_USE;
                            resultItem.percenUse = (resultItem.countUse / item.Count_Location_ALL) * 100;
                            resultItem.countEmpty = item.Count_Location_ALL - item.Count_Location_USE;
                            resultItem.percenEmpty = (resultItem.countEmpty / item.Count_Location_ALL) * 100;
                        }

                        result.Add(resultItem);
                    }
                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report10\\Report10.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report10");
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

        #region autoSearchAisle
        public List<ReportAutocompleteViewModel> autoSearchAisle(Report10ViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {
                    var query = context.MS_LocationAisle.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.LocationLock_Id.Contains(data.key));
                    }

                    var items = new List<ReportAutocompleteViewModel>();

                    var result = query.Select(c => new { c.LocationAisle_Index,c.LocationLock_Name, c.LocationLock_Id }).Distinct().Take(10).ToList();

                    foreach (var item in result.OrderBy(o => o.LocationLock_Id))
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            index = item.LocationAisle_Index,
                            name = item.LocationLock_Id,
                            id = item.LocationLock_Id
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

        #region autoSearchlayer
        public List<ReportAutocompleteViewModel> autoSearchlayer(Report10ViewModel data)
        {
            try
            {

                using (var context = new MasterDataDbContext())
                {


                    var query = context.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0).AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Location_Level.Value.ToString().Contains(data.key));
                    }

                    var items = new List<ReportAutocompleteViewModel>();

                    var result = query.Select(c => new { c.Location_Level }).Distinct().Take(10).ToList();

                    foreach (var item in result.OrderBy(o => o.Location_Level))
                    {
                        var resultItem = new ReportAutocompleteViewModel
                        {
                            name = item.Location_Level.Value.ToString(),
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

        public string ExportExcel(Report10ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report10ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report10");

            try
            {

                var queryLO = MS_DBContext.View_RPT_Location.AsQueryable();

                //var queryLO = MS_DBContext.View_RPT10_Location.AsQueryable();
                var queryBC = BB_DBContext.wm_BinCard.AsQueryable();

                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    queryLO = queryLO.Where(c => c.Warehouse_Name == data.warehouse_Name);
                }

                if (!string.IsNullOrEmpty(data.zone_Id))
                {
                    queryLO = queryLO.Where(c => c.Zone_Id == data.zone_Id);
                }

                if (!string.IsNullOrEmpty(data.location_Level))
                {
                    queryLO = queryLO.Where(c => c.Location_Level == data.location_Level.sParse<int>());
                }

                if (!string.IsNullOrEmpty(data.locationLock_Id))
                {
                    queryLO = queryLO.Where(c => c.LocationLock_Id == data.locationLock_Id);
                }

                if (!string.IsNullOrEmpty(data.location_Prefix_Desc))
                {
                    queryLO = queryLO.Where(c => c.Location_Prefix == data.location_Prefix_Desc);
                }

                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                var queryRPT_BC = queryBC.ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();

                //queryRPT_LO.Where(c => c.IsActive == 1 && c.IsDelete == 0 && c.Zone_Id );
                var queryLocAll = queryRPT_LO.GroupBy(c => new
                {
                    c.Warehouse_Name,
                    c.Zone_Id,
                    c.Zone_Name,
                    c.Location_Aisle,
                    c.Location_Level,
                    c.Location_Prefix
                }).Select(c => new
                {
                    c.Key.Warehouse_Name,
                    c.Key.Zone_Id,
                    c.Key.Zone_Name,
                    c.Key.Location_Aisle,
                    c.Key.Location_Level,
                    c.Key.Location_Prefix,
                    Count_Location_ALL = c.Select(s => s.Location_Name).Count(),
                    Count_Location_USE = 0
                }).ToList();


                var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                {
                    c.Location_Index,
                }).Where(w => w.Sum(s => s.BinCard_QtySign) > 0)
                 .Select(c => new
                 {
                     c.Key.Location_Index,
                 }).ToList();
                //var queryBin = queryBinCard.Where(c => c.BinCard_QtySign > 0).ToList();

                var joinBin = (from BC in queryBinCard
                               join LC in queryRPT_LO on BC.Location_Index equals LC.Location_Index
                               select new
                               {
                                   Bin = BC,
                                   Loc = LC
                               }).ToList();

                var queryLocUse = joinBin.GroupBy(c => new
                {
                    c.Loc?.Warehouse_Name,
                    c.Loc?.Zone_Id,
                    c.Loc?.Zone_Name,
                    c.Loc?.Location_Aisle,
                    c.Loc?.Location_Level,
                    c.Loc?.Location_Prefix
                }).Select(c => new
                {
                    c.Key.Warehouse_Name,
                    c.Key.Zone_Id,
                    c.Key.Zone_Name,
                    c.Key.Location_Aisle,
                    c.Key.Location_Level,
                    c.Key.Location_Prefix,
                    Count_Location_ALL = 0,
                    Count_Location_USE = c.Select(s => s.Loc?.Location_Index).Count(),
                }).ToList();


                var query = queryLocAll.Union(queryLocUse).GroupBy(o => new
                {
                    o.Warehouse_Name,
                    o.Zone_Id,
                    o.Zone_Name,
                    o.Location_Aisle,
                    o.Location_Level,
                    o.Location_Prefix
                }).Select(o => new
                {
                    o.Key.Warehouse_Name,
                    o.Key.Zone_Id,
                    o.Key.Zone_Name,
                    o.Key.Location_Aisle,
                    o.Key.Location_Level,
                    o.Key.Location_Prefix,
                    Count_Location_ALL = o.Sum(s => s.Count_Location_ALL),
                    Count_Location_USE = o.Sum(s => s.Count_Location_USE),
                    Count_Location_REMAIN = o.Sum(s => s.Count_Location_ALL) - o.Sum(s => s.Count_Location_USE)
                }).ToList();

                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
                }

                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new Report10ViewModel();
                    resultItem.date = selectDate;
                    resultItem.checkQuery = true;
                    resultItem.countAll = 0;
                    resultItem.countUse = 0;
                    resultItem.countEmpty = 0;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        var resultItem = new Report10ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.Zone_Id = item.Zone_Id;
                        resultItem.Zone_name = item.Zone_Name;
                        resultItem.countAll = item.Count_Location_ALL;
                        resultItem.percenAll = 100;
                        resultItem.Location_Aisle = item.Location_Aisle;
                        resultItem.Location_Level = item.Location_Level;
                        resultItem.Location_Prefix = item.Location_Prefix;
                        if (item.Count_Location_USE == 0 || item.Count_Location_ALL == 0)
                        {
                            resultItem.countUse = null;
                            resultItem.percenUse = null;
                            resultItem.countEmpty = null;
                            resultItem.percenEmpty = null;
                        }
                        else
                        {

                            resultItem.countUse = item.Count_Location_USE;
                            resultItem.percenUse = (resultItem.countUse / item.Count_Location_ALL) * 100;
                            resultItem.countEmpty = item.Count_Location_ALL - item.Count_Location_USE;
                            resultItem.percenEmpty = (resultItem.countEmpty / item.Count_Location_ALL) * 100;
                        }

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
