using BinBalanceBusiness;
using AspNetCore.Reporting;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Business.Library;
using PlanGRBusiness.Libs;
using Microsoft.EntityFrameworkCore;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using ReportBusiness.Report3;
using System.IO;
using MasterDataBusiness.ViewModels;

namespace ReportBusiness.Report3
{
    public class Report3Service
    {

        #region report3
        public string printReport3(Report3ViewModel data, string rootPath = "")
        {

            var GR_DB = new GRDbContext();
            var M_DB = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryGR = GR_DB.View_RPT_GRIL.AsQueryable();
                var result = new List<Report3ViewModel>();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    queryGR = queryGR.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    queryGR = queryGR.Where(c => c.Tag_No.Contains(data.tag_No));
                }
                if (!string.IsNullOrEmpty(data.po_no))
                {
                    queryGR = queryGR.Where(c => c.PO_No.Contains(data.po_no));
                }

                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date <= goodsReceive_date_To.start);
                }
                if (!string.IsNullOrEmpty(data.documentType_Index))
                {
                    queryGR = queryGR.Where(c => c.DocumentType_Index == Guid.Parse(data.documentType_Index));
                }

                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date_To;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //var query = queryGR.OrderBy(x => x.GoodsReceive_Date).ToList();
                var query = (from GR in queryGR.ToList()
                             join PRDCAT in M_DB.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).Join(M_DB.MS_ProductConversion.Where(c => c.IsActive == 1 && c.IsDelete == 0),
                             PRD => PRD.ProductConversion_Index,
                             PRDCAT => PRDCAT.ProductConversion_Index,
                             (PRD, PRDCAT) => new
                             {
                                 PRD.Product_Index,
                                 PRD.Product_Id,
                                 PRD.ProductCategory_Index,
                                 PRD.ProductCategory_Id,
                                 PRD.ProductCategory_Name,
                                 PRD.Ref_No3,
                                 PRD.Qty_Per_Tag,
                                 PRDCAT.ProductConversion_Index,
                                 PRDCAT.ProductConversion_Id,
                                 PRDCAT.ProductConversion_Name,
                                 PRDCAT.Ref_No1,
                                 PRDCAT.Ref_No2,
                                 PRDCAT.ProductConversion_Height,
                                 PRDCAT.ProductConversion_Weight,
                                 PRDCAT_Ref_No3 = PRDCAT.Ref_No3
                             }).ToList() on GR.Product_Index equals PRDCAT.Product_Index into PDC
                             from PRDCAT in PDC.DefaultIfEmpty()
                             join LO in M_DB.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0).Select(s => new { s.Location_Index, s.Location_Id, s.Location_Name, s.LocationVol_Height }) on GR.PutawayLocation_Index equals LO.Location_Index into LOC
                             from LO in LOC.DefaultIfEmpty()
                             select new
                             {
                                 GR.GoodsReceive_Index,
                                 GR.GoodsReceiveItem_Index,
                                 GR.GoodsReceiveItemLocation_Index,
                                 GR.Owner_Index,
                                 GR.Owner_Id,
                                 GR.Owner_Name,
                                 GR.GoodsReceive_No,
                                 GR.Tag_Index,
                                 GR.TagItem_Index,
                                 GR.Tag_No,
                                 PRDCAT?.ProductCategory_Id,
                                 PRDCAT?.ProductCategory_Name,
                                 PRDCAT?.Ref_No3,
                                 GR.PutawayLocation_Index,
                                 GR.PutawayLocation_Id,
                                 GR.PutawayLocation_Name,
                                 GR.Product_Index,
                                 GR.Product_Id,
                                 GR.Product_Name,
                                 GR.Qty,
                                 GR.ProductConversion_Index,
                                 GR.ProductConversion_Id,
                                 GR.ProductConversion_Name,
                                 Pack_Size = PRDCAT?.PRDCAT_Ref_No3,
                                 TixHi = PRDCAT?.Ref_No1 + 'x' + PRDCAT?.Ref_No2,
                                 PRDCAT?.ProductConversion_Height,
                                 LO?.LocationVol_Height,
                                 PRDCAT?.ProductConversion_Weight,
                                 PRDCAT?.Qty_Per_Tag,
                                 GR.GoodsReceive_Date,
                                 GR.Putaway_Date,
                                 GR.ItemStatus_Index,
                                 GR.ItemStatus_Id,
                                 GR.ItemStatus_Name,
                                 GR.Putaway_By,
                                 GR.PO_No
                             }
                             ).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report3ViewModel();

                    resultItem.qty = 0;
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in query)
                    {
                        string date = item.GoodsReceive_Date.toString();
                        string GoodReceiveDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);



                        var resultItem = new Report3ViewModel(); ;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.productCategory_Name = item.ProductCategory_Id;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.putawayLocation_Id = item.PutawayLocation_Id;
                        resultItem.putawayLocation_Name = item.PutawayLocation_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = item.Qty;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.pack_Size = item.Pack_Size;
                        resultItem.TixHi = item.TixHi;
                        resultItem.productConversion_Height = item.ProductConversion_Height;
                        resultItem.locationVol_Height = item.LocationVol_Height;
                        resultItem.productConversion_Weight = item.ProductConversion_Weight;
                        resultItem.qty_Per_Tag = item.Qty_Per_Tag;
                        resultItem.goodsReceive_date = item.GoodsReceive_Date == null ? "": item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.putaway_Date = item.Putaway_Date == null ? "" : item.Putaway_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.putaway_Time = item.Putaway_Date == null ? "" : item.Putaway_Date.Value.ToString("HH:mm:ss");
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.putaway_By = item.Putaway_By;
                        resultItem.goodsReceive_Date = GoodReceiveDate;
                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.PO_No = item.PO_No;


                        result.Add(resultItem);
                    }
                    result.ToList();
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report3\\Report3.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report3");
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

        public string ExportExcel(Report3ViewModel data, string rootPath = "")
        {
            var GR_DB = new GRDbContext();
            var M_DB = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report3ViewModel>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report3");

            try
            {



                var queryGR = GR_DB.View_RPT_GRIL.AsQueryable();
                if (!string.IsNullOrEmpty(data.goodsReceive_No))
                {
                    queryGR = queryGR.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGR = queryGR.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    queryGR = queryGR.Where(c => c.Tag_No.Contains(data.tag_No));
                }

                if (!string.IsNullOrEmpty(data.goodsReceive_date) && !string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var dateStart = data.goodsReceive_date.toBetweenDate();
                    var dateEnd = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date))
                {
                    var goodsReceive_date_From = data.goodsReceive_date.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date >= goodsReceive_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsReceive_date_To))
                {
                    var goodsReceive_date_To = data.goodsReceive_date_To.toBetweenDate();
                    queryGR = queryGR.Where(c => c.GoodsReceive_Date <= goodsReceive_date_To.start);
                }

                string startDate = data.goodsReceive_date;
                string GRDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsReceive_date_To;
                string GRDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                //var query = queryGR.OrderBy(x => x.GoodsReceive_Date).ToList();
                var query = (from GR in queryGR.ToList()
                             join PRDCAT in M_DB.MS_Product.Where(c => c.IsActive == 1 && c.IsDelete == 0).Join(M_DB.MS_ProductConversion.Where(c => c.IsActive == 1 && c.IsDelete == 0),
                             PRD => PRD.Product_Id,
                             PRDCAT => PRDCAT.Product_Id,
                             (PRD, PRDCAT) => new
                             {
                                 PRD.Product_Index,
                                 PRD.Product_Id,
                                 PRD.ProductCategory_Index,
                                 PRD.ProductCategory_Id,
                                 PRD.ProductCategory_Name,
                                 PRD.Ref_No3,
                                 PRD.Qty_Per_Tag,
                                 PRDCAT.ProductConversion_Index,
                                 PRDCAT.ProductConversion_Id,
                                 PRDCAT.ProductConversion_Name,
                                 PRDCAT.Ref_No1,
                                 PRDCAT.Ref_No2,
                                 PRDCAT.ProductConversion_Height,
                                 PRDCAT.ProductConversion_Weight,
                                 PRDCAT_Ref_No3 = PRDCAT.Ref_No3
                             }).ToList() on GR.ProductConversion_Index equals PRDCAT.ProductConversion_Index into PDC
                             from PRDCAT in PDC.DefaultIfEmpty()
                             join LO in M_DB.MS_Location.Where(c => c.IsActive == 1 && c.IsDelete == 0).Select(s => new { s.Location_Index, s.Location_Id, s.Location_Name, s.LocationVol_Height }) on GR.PutawayLocation_Index equals LO.Location_Index into LOC
                             from LO in LOC.DefaultIfEmpty()
                             select new
                             {
                                 GR.GoodsReceive_Index,
                                 GR.GoodsReceiveItem_Index,
                                 GR.GoodsReceiveItemLocation_Index,
                                 GR.Owner_Index,
                                 GR.Owner_Id,
                                 GR.Owner_Name,
                                 GR.GoodsReceive_No,
                                 GR.Tag_Index,
                                 GR.TagItem_Index,
                                 GR.Tag_No,
                                 PRDCAT?.ProductCategory_Id,
                                 PRDCAT?.ProductCategory_Name,
                                 PRDCAT?.Ref_No3,
                                 GR.PutawayLocation_Index,
                                 GR.PutawayLocation_Id,
                                 GR.PutawayLocation_Name,
                                 GR.Product_Index,
                                 GR.Product_Id,
                                 GR.Product_Name,
                                 GR.Qty,
                                 GR.ProductConversion_Index,
                                 GR.ProductConversion_Id,
                                 GR.ProductConversion_Name,
                                 Pack_Size = PRDCAT?.PRDCAT_Ref_No3,
                                 TixHi = PRDCAT?.Ref_No1 + 'x' + PRDCAT?.Ref_No2,
                                 PRDCAT?.ProductConversion_Height,
                                 LO?.LocationVol_Height,
                                 PRDCAT?.ProductConversion_Weight,
                                 PRDCAT?.Qty_Per_Tag,
                                 GR.GoodsReceive_Date,
                                 GR.Putaway_Date,
                                 GR.ItemStatus_Index,
                                 GR.ItemStatus_Id,
                                 GR.ItemStatus_Name,
                                 GR.Putaway_By,
                                 GR.PO_No
                             }
                             ).ToList();

                if (query.Count == 0)
                {
                    var resultItem = new Report3ViewModel();

                    resultItem.qty = 0;
                    resultItem.goodsReceive_date = GRDateStart;
                    resultItem.goodsReceive_date_To = GRDateEnd;
                    result.Add(resultItem);
                }
                else
                {

                    foreach (var item in query)
                    {
                        string date = item.GoodsReceive_Date.toString();
                        string GoodReceiveDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);



                        var resultItem = new Report3ViewModel(); ;
                        resultItem.owner_Id = item.Owner_Id;
                        resultItem.owner_Name = item.Owner_Name;
                        resultItem.goodsReceive_No = item.GoodsReceive_No;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.productCategory_Name = item.ProductCategory_Id;
                        resultItem.ref_No3 = item.Ref_No3;
                        resultItem.putawayLocation_Id = item.PutawayLocation_Id;
                        resultItem.putawayLocation_Name = item.PutawayLocation_Name;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.qty = item.Qty;
                        resultItem.productConversion_Id = item.ProductConversion_Id;
                        resultItem.productConversion_Name = item.ProductConversion_Name;
                        resultItem.pack_Size = item.Pack_Size;
                        resultItem.TixHi = item.TixHi;
                        resultItem.productConversion_Height = item.ProductConversion_Height;
                        resultItem.locationVol_Height = item.LocationVol_Height;
                        resultItem.productConversion_Weight = item.ProductConversion_Weight;
                        resultItem.qty_Per_Tag = item.Qty_Per_Tag;
                        resultItem.goodsReceive_date = item.GoodsReceive_Date == null ? "" : item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.putaway_Date = item.Putaway_Date == null ? "" : item.Putaway_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.putaway_Time = item.Putaway_Date == null ? "" : item.Putaway_Date.Value.ToString("HH:mm:ss");
                        resultItem.itemStatus_Id = item.ItemStatus_Id;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.putaway_By = item.Putaway_By;
                        resultItem.goodsReceive_date = GRDateStart;
                        resultItem.goodsReceive_date_To = GRDateEnd;
                        resultItem.PO_No = item.PO_No;


                        result.Add(resultItem);
                    }
                    result.ToList();
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

        #region autoSearchTag
        public List<ItemListViewModel> autoSearchTag(ItemListViewModel data)
        {
            try
            {

                using (var context = new GRDbContext())
                {


                    var query = context.View_RPT_GRIL.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Tag_No.Contains(data.key));
                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Tag_Index, c.Tag_No }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new ItemListViewModel
                        {
                            index = item.Tag_Index,
                            name = item.Tag_No,
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
