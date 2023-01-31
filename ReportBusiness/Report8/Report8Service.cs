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
using MasterDataBusiness.ViewModels;
using System.IO;

namespace ReportBusiness.Report8
{
    public class Report8Service
    {

        #region report8
        public string printReport8(Report8ViewModelV2 data, string rootPath = "")
        {

            var GT_DB = new TransferDbContext();
            var BB_DB = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            try
            {
                var queryGT = GT_DB.View_Report_GoodsTransfer.AsQueryable();
                var result = new List<Report8ViewModelV2>();
                if (!string.IsNullOrEmpty(data.goodsTransfer_No))
                {
                    queryGT = queryGT.Where(c => c.GoodsTransfer_No.Contains(data.goodsTransfer_No));
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGT = queryGT.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    queryGT = queryGT.Where(c => c.Tag_No.Contains(data.tag_No));
                }
                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    queryGT = queryGT.Where(c => c.Location_Name.Contains(data.location_Name));
                }
                //if (!string.IsNullOrEmpty(data.Owner_Id))
                //{
                //    queryGT = queryGT.Where(c => c.Owner_Id.Contains(data.Owner_Id));
                //}
                if (!string.IsNullOrEmpty(data.goodsTransfer_date) && !string.IsNullOrEmpty(data.goodsTransfer_date_To))
                {
                    var dateStart = data.goodsTransfer_date.toBetweenDate();
                    var dateEnd = data.goodsTransfer_date_To.toBetweenDate();
                    queryGT = queryGT.Where(c => c.GoodsTransfer_Date >= dateStart.start && c.GoodsTransfer_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsTransfer_date))
                {
                    var goodsTransfer_date_From = data.goodsTransfer_date.toBetweenDate();
                    queryGT = queryGT.Where(c => c.GoodsTransfer_Date >= goodsTransfer_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsTransfer_date_To))
                {
                    var goodsTransfer_date_To = data.goodsTransfer_date_To.toBetweenDate();
                    queryGT = queryGT.Where(c => c.GoodsTransfer_Date <= goodsTransfer_date_To.start);
                }

                if (!string.IsNullOrEmpty(data.documentType_Index.ToString()))
                {
                    queryGT = queryGT.Where(c => c.DocumentType_Index == data.documentType_Index);
                }

                if (!string.IsNullOrEmpty(data.erp_Location))
                {
                    queryGT = queryGT.Where(c => c.ERP_Location == data.erp_Location);
                }

                if (!string.IsNullOrEmpty(data.locationType_Index.ToString()))
                {
                    queryGT = queryGT.Where(c => c.LocationType_Index == data.locationType_Index);
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGT = queryGT.Where(c => c.Create_By == data.create_By);
                }

                var query = queryGT.OrderBy(x => x.Create_Date).ToList();


                string startDate = data.goodsTransfer_date;
                string GTDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsTransfer_date_To;
                string GTDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new Report8ViewModelV2();

                    resultItem.qty = 0;
                    resultItem.goodsTransfer_date = GTDateStart;
                    resultItem.goodsTransfer_date_To = GTDateEnd;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        string date = item.GoodsTransfer_Date.toString();
                        string GoodTransferDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report8ViewModelV2();
                        //resultItem.rowNum = item.RowNum;
                        resultItem.goodsTransfer_Index = item.GoodsTransfer_Index;
                        resultItem.goodsTransfer_No = item.GoodsTransfer_No;
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.create_Date = item.Create_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.goodsTransfer_Date = item.GoodsTransfer_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.goodsTransfer_date_To = GTDateEnd;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.qty = item.Qty;
                        resultItem.unit = item.Unit;
                        resultItem.totalQty = item.TotalQty;
                        resultItem.subUnit = item.SubUnit;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.itemStatus_Name_To = item.ItemStatus_Name_To;
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.location_Index_To = item.Location_Index_To;
                        resultItem.location_Name_To = item.Location_Name_To;
                        resultItem.erp_Location = item.ERP_Location;
                        resultItem.erp_Location_To = item.ERP_Location_To;
                        resultItem.document_Status = item.Document_Status;
                        resultItem.documentStatus_Name = item.DocumentStatus_Name;
                        resultItem.item_Document_Status = item.Item_Document_Status;
                        resultItem.goodsReceiveItemLocation_Index = item.GoodsReceiveItemLocation_Index;
                        resultItem.goodsTransferItem_Index = item.GoodsTransferItem_Index;
                        resultItem.update_By = item.Update_By;
                        resultItem.create_By = item.Create_By;
                        resultItem.sumQty = item.SumQty;
                        resultItem.sumTotalQty = item.SumTotalQty;
                        resultItem.goodsTransfer_date = GTDateStart;
                        resultItem.goodsTransfer_date_To = GTDateEnd;


                        result.Add(resultItem);
                    }

                }
                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report8\\Report8.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("Report8");
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

        //public string printReport8(Report8ViewModel data, string rootPath = "")
        //{

        //    var GT_DB = new TransferDbContext();
        //    var BB_DB = new BinbalanceDbContext();

        //    var culture = new System.Globalization.CultureInfo("en-US");
        //    String State = "Start";
        //    String msglog = "";
        //    var olog = new logtxt();

        //    try
        //    {
        //        var queryGT = GT_DB.View_GT.AsQueryable();
        //        var result = new List<Report8ViewModel>();
        //        if (!string.IsNullOrEmpty(data.goodsTransfer_No))
        //        {
        //            queryGT = queryGT.Where(c => c.GoodsTransfer_No.Contains(data.goodsTransfer_No));
        //        }
        //        if (!string.IsNullOrEmpty(data.product_Id))
        //        {
        //            queryGT = queryGT.Where(c => c.Product_Id.Contains(data.product_Id));
        //        }
        //        if (!string.IsNullOrEmpty(data.tag_No))
        //        {
        //            queryGT = queryGT.Where(c => c.Tag_No.Contains(data.tag_No));
        //        }
        //        if (!string.IsNullOrEmpty(data.location_Id))
        //        {
        //            queryGT = queryGT.Where(c => c.Location_Id.Contains(data.location_Id));
        //        }
        //        if (!string.IsNullOrEmpty(data.Owner_Id))
        //        {
        //            queryGT = queryGT.Where(c => c.Owner_Id.Contains(data.Owner_Id));
        //        }
        //        if (!string.IsNullOrEmpty(data.goodsTransfer_date) && !string.IsNullOrEmpty(data.goodsTransfer_date_To))
        //        {
        //            var dateStart = data.goodsTransfer_date.toBetweenDate();
        //            var dateEnd = data.goodsTransfer_date_To.toBetweenDate();
        //            queryGT = queryGT.Where(c => c.GoodsTransfer_Date >= dateStart.start && c.GoodsTransfer_Date <= dateEnd.end);
        //        }
        //        else if (!string.IsNullOrEmpty(data.goodsTransfer_date))
        //        {
        //            var goodsTransfer_date_From = data.goodsTransfer_date.toBetweenDate();
        //            queryGT = queryGT.Where(c => c.GoodsTransfer_Date >= goodsTransfer_date_From.start);
        //        }
        //        else if (!string.IsNullOrEmpty(data.goodsTransfer_date_To))
        //        {
        //            var goodsTransfer_date_To = data.goodsTransfer_date_To.toBetweenDate();
        //            queryGT = queryGT.Where(c => c.GoodsTransfer_Date <= goodsTransfer_date_To.start);
        //        }

        //        var query = queryGT.OrderBy(x => x.GoodsTransfer_Date).ToList();


        //        string startDate = data.goodsTransfer_date;
        //        string GTDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
        //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //        string endDate = data.goodsTransfer_date_To;
        //        string GTDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
        //        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //        if (query.Count == 0)
        //        {
        //            var resultItem = new Report8ViewModel();

        //            resultItem.qty = 0;
        //            resultItem.goodsTransfer_date = GTDateStart;
        //            resultItem.goodsTransfer_date_To = GTDateEnd;
        //            result.Add(resultItem);
        //        }
        //        else
        //        {
        //            foreach (var item in query)
        //            {
        //                string date = item.GoodsTransfer_Date.toString();
        //                string GoodTransferDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
        //                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

        //                var resultItem = new Report8ViewModel();
        //                resultItem.goodsTransfer_No = item.GoodsTransfer_No;
        //                resultItem.tag_No = item.Tag_No;
        //                resultItem.Owner_Id = item.Owner_Id;
        //                resultItem.Owner_Name = item.Owner_Name;
        //                resultItem.location_Id = item.Location_Id;
        //                resultItem.location_Name = item.Location_Name;
        //                resultItem.location_Id_To = item.Location_Id_To;
        //                resultItem.location_Name_To = item.Location_Name_To;
        //                resultItem.product_Id = item.Product_Id;
        //                resultItem.product_Name = item.Product_Name;
        //                resultItem.binbalance_Qty = !string.IsNullOrEmpty(item.Binbalance_Qty) ? item.Binbalance_Qty.sParse<decimal>() : 0;
        //                resultItem.qty = item.Qty;
        //                resultItem.productConversion_Id = item.ProductConversion_Id;
        //                resultItem.productConversion_Name = item.ProductConversion_Name;
        //                resultItem.goodsTransfer_Date = item.GoodsTransfer_Date == null ? "" : item.GoodsTransfer_Date.Value.ToString("dd/MM/yyyy");
        //                resultItem.goodsReceive_Date = BB_DB.wm_BinBalance.FirstOrDefault(c => c.Owner_Index == item.Owner_Index && c.Product_Index == item.Product_Index && c.TagItem_Index == item.TagItem_Index)?.GoodsReceive_Date.ToString("dd/MM/yyyy");
        //                resultItem.Update_By = item.Update_By;



        //                resultItem.create_Date = DateTime.Now.ToString("dd/MM/yyyy", culture);
        //                resultItem.goodsTransfer_date = GTDateStart;
        //                resultItem.goodsTransfer_date_To = GTDateEnd;


        //                result.Add(resultItem);
        //            }

        //        }
        //        rootPath = rootPath.Replace("\\ReportAPI", "");
        //        //var reportPath = rootPath + "\\ReportBusiness\\Report8\\Report8.rdlc";
        //        var reportPath = rootPath + new AppSettingConfig().GetUrl("Report8");
        //        LocalReport report = new LocalReport(reportPath);
        //        report.AddDataSource("DataSet1", result);

        //        System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //        string fileName = "";
        //        string fullPath = "";
        //        fileName = "tmpReport" + DateTime.Now.ToString("yyyyMMddHHmmss");

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



        #region autoSearchTagNo
        public List<Report8ViewModel> autoSearchTagNo(Report8ViewModel data)
        {
            try
            {

                using (var context = new TransferDbContext())
                {


                    var query = context.View_GT.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Tag_No.Contains(data.key));

                    }

                    var items = new List<Report8ViewModel>();

                    var result = query.Select(c => new { c.Tag_No }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new Report8ViewModel
                        {

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

        #region autoSearchlocation
        public List<Report8ViewModel> autoSearchLocation(Report8ViewModel data)
        {
            try
            {

                using (var context = new TransferDbContext())
                {


                    var query = context.View_GT.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Location_Name.Contains(data.key));

                    }

                    var items = new List<Report8ViewModel>();

                   var result = query.Select(c => new { c.Location_Name }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new Report8ViewModel
                        {

                            name = item.Location_Name,

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

                using (var context = new TransferDbContext())
                {


                    var query = context.View_GT.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Owner_Id.Contains(data.key)
                        || c.Owner_Name.Contains(data.key));

                    }

                    var items = new List<ItemListViewModel>();

                    var result = query.Select(c => new { c.Owner_Index,c.Owner_Id,c.Owner_Name }).Distinct().Take(10).ToList();

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

        public string ExportExcel(Report8ViewModelV2 data, string rootPath = "")
        {

            var GT_DB = new TransferDbContext();
            var BB_DB = new BinbalanceDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<Report8ViewModelV2>();
            rootPath = rootPath.Replace("\\ReportAPI", "");
            var reportPath = rootPath + new AppSettingConfig().GetUrl("Report8");

            try
            {

                var queryGT = GT_DB.View_Report_GoodsTransfer.AsQueryable();
                //var result = new List<Report8ViewModelV2>();
                if (!string.IsNullOrEmpty(data.goodsTransfer_No))
                {
                    queryGT = queryGT.Where(c => c.GoodsTransfer_No.Contains(data.goodsTransfer_No));
                }
                if (!string.IsNullOrEmpty(data.product_Id))
                {
                    queryGT = queryGT.Where(c => c.Product_Id.Contains(data.product_Id));
                }
                if (!string.IsNullOrEmpty(data.tag_No))
                {
                    queryGT = queryGT.Where(c => c.Tag_No.Contains(data.tag_No));
                }
                if (!string.IsNullOrEmpty(data.location_Name))
                {
                    queryGT = queryGT.Where(c => c.Location_Name.Contains(data.location_Name));
                }
                //if (!string.IsNullOrEmpty(data.Owner_Id))
                //{
                //    queryGT = queryGT.Where(c => c.Owner_Id.Contains(data.Owner_Id));
                //}
                if (!string.IsNullOrEmpty(data.goodsTransfer_date) && !string.IsNullOrEmpty(data.goodsTransfer_date_To))
                {
                    var dateStart = data.goodsTransfer_date.toBetweenDate();
                    var dateEnd = data.goodsTransfer_date_To.toBetweenDate();
                    queryGT = queryGT.Where(c => c.GoodsTransfer_Date >= dateStart.start && c.GoodsTransfer_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.goodsTransfer_date))
                {
                    var goodsTransfer_date_From = data.goodsTransfer_date.toBetweenDate();
                    queryGT = queryGT.Where(c => c.GoodsTransfer_Date >= goodsTransfer_date_From.start);
                }
                else if (!string.IsNullOrEmpty(data.goodsTransfer_date_To))
                {
                    var goodsTransfer_date_To = data.goodsTransfer_date_To.toBetweenDate();
                    queryGT = queryGT.Where(c => c.GoodsTransfer_Date <= goodsTransfer_date_To.start);
                }

                if (!string.IsNullOrEmpty(data.documentType_Index.ToString()))
                {
                    queryGT = queryGT.Where(c => c.DocumentType_Index == data.documentType_Index);
                }

                if (!string.IsNullOrEmpty(data.erp_Location))
                {
                    queryGT = queryGT.Where(c => c.ERP_Location == data.erp_Location);
                }

                if (!string.IsNullOrEmpty(data.locationType_Index.ToString()))
                {
                    queryGT = queryGT.Where(c => c.LocationType_Index == data.locationType_Index);
                }

                if (!string.IsNullOrEmpty(data.create_By))
                {
                    queryGT = queryGT.Where(c => c.Create_By == data.create_By);
                }

                var query = queryGT.OrderBy(x => x.Create_Date).ToList();


                string startDate = data.goodsTransfer_date;
                string GTDateStart = DateTime.ParseExact(startDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                string endDate = data.goodsTransfer_date_To;
                string GTDateEnd = DateTime.ParseExact(endDate.Substring(0, 8), "yyyyMMdd",
                System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                if (query.Count == 0)
                {
                    var resultItem = new Report8ViewModelV2();

                    resultItem.qty = 0;
                    resultItem.goodsTransfer_date = GTDateStart;
                    resultItem.goodsTransfer_date_To = GTDateEnd;
                    result.Add(resultItem);
                }
                else
                {
                    foreach (var item in query)
                    {
                        string date = item.GoodsTransfer_Date.toString();
                        string GoodTransferDate = DateTime.ParseExact(date.Substring(0, 8), "yyyyMMdd",
                        System.Globalization.CultureInfo.InvariantCulture).ToString("dd/MM/yyyy", culture);

                        var resultItem = new Report8ViewModelV2();
                        //resultItem.rowNum = item.RowNum;
                        resultItem.goodsTransfer_Index = item.GoodsTransfer_Index;
                        resultItem.goodsTransfer_No = item.GoodsTransfer_No;
                        resultItem.documentType_Index = item.DocumentType_Index;
                        resultItem.documentType_Name = item.DocumentType_Name;
                        resultItem.create_Date = item.Create_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.goodsTransfer_Date = item.GoodsTransfer_Date.Value.ToString("dd/MM/yyyy");
                        resultItem.goodsTransfer_date_To = GTDateEnd;
                        resultItem.tag_No = item.Tag_No;
                        resultItem.product_Index = item.Product_Index;
                        resultItem.product_Id = item.Product_Id;
                        resultItem.product_Name = item.Product_Name;
                        resultItem.product_Lot = item.Product_Lot;
                        resultItem.qty = item.Qty;
                        resultItem.unit = item.Unit;
                        resultItem.totalQty = item.TotalQty;
                        resultItem.subUnit = item.SubUnit;
                        resultItem.itemStatus_Name = item.ItemStatus_Name;
                        resultItem.itemStatus_Name_To = item.ItemStatus_Name_To;
                        resultItem.location_Index = item.Location_Index;
                        resultItem.location_Name = item.Location_Name;
                        resultItem.location_Index_To = item.Location_Index_To;
                        resultItem.location_Name_To = item.Location_Name_To;
                        resultItem.erp_Location = item.ERP_Location;
                        resultItem.erp_Location_To = item.ERP_Location_To;
                        resultItem.document_Status = item.Document_Status;
                        resultItem.documentStatus_Name = item.DocumentStatus_Name;
                        resultItem.item_Document_Status = item.Item_Document_Status;
                        resultItem.goodsReceiveItemLocation_Index = item.GoodsReceiveItemLocation_Index;
                        resultItem.goodsTransferItem_Index = item.GoodsTransferItem_Index;
                        resultItem.update_By = item.Update_By;
                        resultItem.create_By = item.Create_By;
                        resultItem.sumQty = item.SumQty;
                        resultItem.sumTotalQty = item.SumTotalQty;
                        resultItem.goodsTransfer_date = GTDateStart;
                        resultItem.goodsTransfer_date_To = GTDateEnd;


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

        #region autoSearchSloc
        public List<Report8ViewModel> autoSearchSloc(Report8ViewModel data)
        {
            try
            {

                using (var context = new TransferDbContext())
                {


                    var query = context.IM_GoodsTransferItem.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Erp_Location.Contains(data.key));

                    }

                    var items = new List<Report8ViewModel>();

                    var result = query.Select(c => new { c.Erp_Location }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new Report8ViewModel
                        {

                            name = item.Erp_Location,

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
        #region autoCreateBy
        public List<Report8ViewModel> autoCreateBy(Report8ViewModel data)
        {
            try
            {

                using (var context = new TransferDbContext())
                {


                    var query = context.IM_GoodsTransfer.AsQueryable();

                    if (!string.IsNullOrEmpty(data.key) && data.key != "-")
                    {
                        query = query.Where(c => c.Create_By.Contains(data.key));

                    }

                    var items = new List<Report8ViewModel>();

                    var result = query.Select(c => new { c.Create_By }).Distinct().Take(10).ToList();

                    foreach (var item in result)
                    {
                        var resultItem = new Report8ViewModel
                        {

                            name = item.Create_By,

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
