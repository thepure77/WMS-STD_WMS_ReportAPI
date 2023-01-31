using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.Report11
{
    public class Report11Service
    {

        #region printReport11
        public dynamic printReport11(Report11ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report11ViewModel>();

            try
            {
                var queryLO = MS_DBContext.View_RPT_Location.AsQueryable();
                //var queryLO = MS_DBContext.View_RPT11_Location.AsQueryable();
                var queryBC = BB_DBContext.wm_BinCard.AsQueryable();

                //if (!string.IsNullOrEmpty(data.warehouse_Name))
                //{
                //    queryLO = queryLO.Where(c => c.Warehouse_Name == data.warehouse_Name);
                //}

                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0 && (new List<Guid?>
                { new Guid("F9EDDAEC-A893-4F63-A700-526C69CC0774"),
                    new Guid("F9EDDAEC-A893-4F63-A700-526C69CC08C0")
                }.Contains(c.LocationType_Index))
                ).ToList();
                var queryRPT_BC = queryBC.ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();

                var queryLocAll = queryRPT_LO.GroupBy(c => new
                {
                    c.Warehouse_Name
                }).Select(c => new
                {
                    c.Key.Warehouse_Name
                    ,Count_Location_ALL = c.Select(s => s.Location_Name).Count()
                    ,Count_Location_USE = 0
                    ,Count_BlockPut = c.Where(w => w.BlockPut == 1).Select(s => s.BlockPut).Count()
                    ,Count_BlockPick = c.Where(w => w.BlockPick == 1).Select(s => s.BlockPick).Count()
                }).ToList();


                var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                {
                    c.Location_Index,
                }).Where(w => w.Sum(s => s.BinCard_QtySign) >0)
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
                    c.Loc?.Warehouse_Name
                }).Select(c => new
                {
                    c.Key.Warehouse_Name,
                    Count_Location_ALL = 0,
                    Count_Location_USE = c.Select(s => s.Loc?.Location_Index).Count(),
                    Count_BlockPut = 0,// c.Where(w => w.Loc?.BlockPut == 1).Select(s => s.Loc?.BlockPut).Count(),
                    Count_BlockPick = 0//c.Where(w => w.Loc?.BlockPick == 1).Select(s => s.Loc?.BlockPick).Count()
                }).ToList();


                var query = queryLocAll.Union(queryLocUse).GroupBy(o => new
                {
                    o.Warehouse_Name
                }).Select(o => new
                {
                    o.Key.Warehouse_Name 
                    ,Count_Location_ALL = o.Sum(s => s.Count_Location_ALL)
                    ,Count_Location_USE = o.Sum(s => s.Count_Location_USE)
                    ,Count_Location_REMAIN = o.Sum(s => s.Count_Location_ALL) - o.Sum(s => s.Count_Location_USE)
                    ,Count_BlockPut = o.Sum(s => s.Count_BlockPut)
                    ,Count_BlockPick = o.Sum(s => s.Count_BlockPick)
                }).ToList();

                // var queryRPT_LO = queryLO.ToList();
                // var queryRPT_BC = queryBC.ToList();
                // var dateStart = data.date.toBetweenDate();
                // var dateEnd = data.date.toBetweenDate();
                // var queryLocAll = queryRPT_LO.GroupBy(c => new
                // {
                //     c.Warehouse_Index,
                //     c.Warehouse_Name,

                // })
                // .Select(c => new
                // {
                //     c.Key.Warehouse_Index,
                //     c.Key.Warehouse_Name,

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
                //                join LC in queryRPT_LO on BC.Location_Index equals LC.Location_Index into ps
                //                from r in ps.DefaultIfEmpty()
                //                select new
                //                {
                //                    Bin = BC,
                //                    Loc = r
                //                }).ToList();

                // var queryLocUse = joinBin.GroupBy(c => new
                // {
                //     c?.Loc?.Warehouse_Index,
                //     c?.Loc?.Warehouse_Name,

                // })
                //.Select(c => new
                //{
                //    c?.Key?.Warehouse_Index,
                //    c?.Key?.Warehouse_Name,

                //    loCountUse = c.Select(s => s?.Loc?.Location_Index).Count(),

                //}).ToList();


                // var query = (from locALL in queryLocAll
                //              join locUSE in queryLocUse on locALL.Warehouse_Index  equals locUSE.Warehouse_Index into W
                //              from a in W.DefaultIfEmpty()
                //              orderby locALL.Warehouse_Name ascending
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
                    var resultItem = new Report11ViewModel();
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
                        var resultItem = new Report11ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.countAll = item.Count_Location_ALL;
                        resultItem.percenAll = 110;
                        resultItem.BlockPut = item.Count_BlockPut;
                        resultItem.BlockPick = item.Count_BlockPick;
                        resultItem.qtyBlock = (item.Count_BlockPut + item.Count_BlockPick);
                        resultItem.percenBlockPick = null;
                        resultItem.percenBlockPut = null;
                        resultItem.percenBlock = null;
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
                            resultItem.percenUse = (resultItem.countUse / item.Count_Location_ALL.sParse<decimal>()) * 100;
                            resultItem.countEmpty = item.Count_Location_ALL - item.Count_Location_USE - item.Count_BlockPut - item.Count_BlockPick;
                            resultItem.percenEmpty = (resultItem.countEmpty / item.Count_Location_ALL.sParse<decimal>()) * 100;
                        }
                        if (item.Count_BlockPut != 0)
                        { resultItem.percenBlockPut = (resultItem.BlockPut.sParse<decimal>() / item.Count_Location_ALL.sParse<decimal>()) * 100; }
                        if (item.Count_BlockPick != 0)
                        { resultItem.percenBlockPick = (resultItem.BlockPick.sParse<decimal>() / item.Count_Location_ALL.sParse<decimal>()) * 100; }
                        if ((item.Count_BlockPut + item.Count_BlockPick) != 0)
                        {
                            { resultItem.percenBlock = (resultItem.qtyBlock / item.Count_Location_ALL.sParse<decimal>()) * 100; }
                        }
                        result.Add(resultItem);
                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report11\\Report11.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report11");
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

        public string ExportExcel(Report11ViewModel data, string rootPath = "")
        {
            var MS_DBContext = new MasterDataDbContext();
            var BB_DBContext = new BinbalanceDbContext();
            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report11ViewModel>();

            try
            {
                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report11");

                var queryLO = MS_DBContext.View_RPT_Location.AsQueryable();
                var queryBC = BB_DBContext.wm_BinCard.AsQueryable();


                var queryRPT_LO = queryLO.Where(c => c.IsActive == 1 && c.IsDelete == 0).ToList();
                var queryRPT_BC = queryBC.ToList();
                var dateStart = data.date.toBetweenDate();
                var dateEnd = data.date.toBetweenDate();

                var queryLocAll = queryRPT_LO.GroupBy(c => new
                {
                    c.Warehouse_Name
                }).Select(c => new
                {
                    c.Key.Warehouse_Name
                    ,
                    Count_Location_ALL = c.Select(s => s.Location_Name).Count()
                    ,
                    Count_Location_USE = 0
                    ,
                    Count_BlockPut = c.Where(w => w.BlockPut == 1).Select(s => s.BlockPut).Count()
                    ,
                    Count_BlockPick = c.Where(w => w.BlockPick == 1).Select(s => s.BlockPick).Count()
                }).ToList();


                var queryBinCard = queryRPT_BC.Where(c => c.BinCard_Date <= dateEnd.end).GroupBy(c => new
                {
                    c.Location_Index,
                }).Where(w => w.Sum(s => s.BinCard_QtySign) > 0)
                 .Select(c => new
                 {
                     c.Key.Location_Index,
                 }).ToList();

                var joinBin = (from BC in queryBinCard
                               join LC in queryRPT_LO on BC.Location_Index equals LC.Location_Index
                               select new
                               {
                                   Bin = BC,
                                   Loc = LC
                               }).ToList();

                var queryLocUse = joinBin.GroupBy(c => new
                {
                    c.Loc?.Warehouse_Name
                }).Select(c => new
                {
                    c.Key.Warehouse_Name,
                    Count_Location_ALL = 0,
                    Count_Location_USE = c.Select(s => s.Loc?.Location_Index).Count(),
                    Count_BlockPut = 0,// c.Where(w => w.Loc?.BlockPut == 1).Select(s => s.Loc?.BlockPut).Count(),
                    Count_BlockPick = 0,//c.Where(w => w.Loc?.BlockPick == 1).Select(s => s.Loc?.BlockPick).Count()
                }).ToList();


                var query = queryLocAll.Union(queryLocUse).GroupBy(o => new
                {
                    o.Warehouse_Name
                }).Select(o => new
                {
                    o.Key.Warehouse_Name
                    ,
                    Count_Location_ALL = o.Sum(s => s.Count_Location_ALL)
                    ,
                    Count_Location_USE = o.Sum(s => s.Count_Location_USE)
                    ,
                    Count_Location_REMAIN = o.Sum(s => s.Count_Location_ALL) - o.Sum(s => s.Count_Location_USE)
                    ,
                    Count_BlockPut = o.Sum(s => s.Count_BlockPut)
                    ,
                    Count_BlockPick = o.Sum(s => s.Count_BlockPick)
                }).ToList();

                if (!string.IsNullOrEmpty(data.warehouse_Name))
                {
                    query = query.Where(c => c.Warehouse_Name == data.warehouse_Name).ToList();
                }

                string selectDate = DateTime.ParseExact(data.date.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new Report11ViewModel();
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
                        var resultItem = new Report11ViewModel();

                        resultItem.date = selectDate;
                        resultItem.warehouse_Name = item.Warehouse_Name;
                        resultItem.countAll = item.Count_Location_ALL;
                        resultItem.percenAll = 110;
                        resultItem.BlockPut = item.Count_BlockPut;
                        resultItem.BlockPick = item.Count_BlockPick;
                        resultItem.qtyBlock = (item.Count_BlockPut + item.Count_BlockPick);
                        resultItem.percenBlockPick = null;
                        resultItem.percenBlockPut = null;
                        resultItem.percenBlock = null;
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
                            resultItem.percenUse = (resultItem.countUse / item.Count_Location_ALL) * 110;
                            resultItem.countEmpty = item.Count_Location_ALL - item.Count_Location_USE - item.Count_BlockPut - item.Count_BlockPick;
                            resultItem.percenEmpty = (resultItem.countEmpty / item.Count_Location_ALL) * 110;
                        }
                        if (item.Count_BlockPut != 0)
                        { resultItem.percenBlockPut = (resultItem.BlockPut / item.Count_Location_ALL) * 110; }
                        if (item.Count_BlockPick != 0)
                        { resultItem.percenBlockPick = (resultItem.BlockPick / item.Count_Location_ALL) * 110; }
                        if ((item.Count_BlockPut + item.Count_BlockPick) != 0)
                        {
                            { resultItem.percenBlock = (resultItem.qtyBlock / item.Count_Location_ALL) * 110; }
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
