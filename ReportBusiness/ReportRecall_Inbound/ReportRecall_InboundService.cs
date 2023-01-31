using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportRecall_Inbound
{
    public class ReportRecall_InboundService
    {

        #region printReport
        public dynamic printReportRecallInbound(ReportRecall_InboundRequestModel data, string rootPath = "" )
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportRecall_InboundExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var convert_Type_query = new List<MasterDataDataAccess.Models.View_ReportRecall_Inbound_Excel>();
                var query = convert_Type_query.AsQueryable();
                var room_Name = "";
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.View_ReportRecall_Inbound_Excel.AsQueryable();
                    room_Name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.View_ReportRecall_Inbound_Excel.AsQueryable();
                    room_Name = "Freeze";
                }
                
                if (data.advanceSearch == true)
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        query = query.Where(c => c.Product_Id == data.materialNo);
                    }

                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                    }

                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        query = query.Where(c => c.PO_No == data.po_No);
                    }

                    if (!string.IsNullOrEmpty(data.NoASN)) //รอเปลี่ยน c. =>
                    {
                        
                        query = query.Where(c => c.No_ASN == data.NoASN);
                    }


                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        query = query.Where(c => c.GoodsReceive_No == data.goodsReceive_No);
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
                    }
                    if (!string.IsNullOrEmpty(data.date_mfg) && !string.IsNullOrEmpty(data.date_mfg_to))
                    {
                        var dateStart = data.date_mfg.toBetweenDate();
                        var dateEnd = data.date_mfg_to.toBetweenDate();
                        query = query.Where(c => c.MFG_Date >= dateStart.start && c.MFG_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.date_exp) && !string.IsNullOrEmpty(data.date_exp_to))
                    {
                        var dateStart = data.date_exp.toBetweenDate();
                        var dateEnd = data.date_exp_to.toBetweenDate();
                        query = query.Where(c => c.EXP_Date >= dateStart.start && c.EXP_Date <= dateEnd.end);

                    }

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GR.Contains(data.batch_lot));
                    }
                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
                    }
                    // Match
                    //รอเปลี่ยน c.Match_Name
                    if (!string.IsNullOrEmpty(data.Match))
                    {
                        query = query.Where(c => c.Match_Name.Contains(data.Match));
                    }

                }
                else
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        query = query.Where(c => c.Product_Id == data.materialNo);
                    }
                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                    }
                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        query = query.Where(c => (c.PO_No) == data.po_No);
                    }
                    if (!string.IsNullOrEmpty(data.NoASN)) //รอเปลี่ยน c. =>
                    {
                        query = query.Where(c => (c.No_ASN) == data.NoASN);
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        query = query.Where(c => c.GoodsReceive_No == data.goodsReceive_No);
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
                    }

                }

                var resultquery = query.OrderBy(o=>o.Tag_No).ToList();
                //Orderby 
                //Tag_No
                //GoodsIssue_No
                //TruckLoad_No

                if (resultquery.Count == 0)
                {
                            
                }
                else
                {
                    int num = 0;
                    foreach (var item in query)
                    {
                        var resultItem = new ReportRecall_InboundExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.Vendor_Id = item.Vendor_Id;
                        resultItem.Vendor_Name = item.Vendor_Name;
                        resultItem.PO_No = item.PO_No;
                        resultItem.NoASN = item.No_ASN; //ASN
                        resultItem.GoodsReceive_No = item.GoodsReceive_No;
                        resultItem.GoodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Gr_Qty = item.GR_QTY; //GR Qty
                        resultItem.Gr_Unit = item.GR_unit; //GR unit
                        resultItem.Sale_BUQty = item.Sale_BUQty;
                        resultItem.Sale_BUConversion = item.Sale_BUConversion;
                        resultItem.Sale_SUQty = item.Sale_SUQty;
                        resultItem.Sale_SUConversion = item.Sale_SUConversion;
                        resultItem.MFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.EXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Product_Lot_GR = item.Product_Lot_GR; //Bacht/lot GR?
                        resultItem.ERP_Location = item.ERP_Location;
                        resultItem.Match = item.Match_Name; //Match
                        resultItem.Billing_Date = item.Billing_Date != null ? item.Billing_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Billing_Matdoc_GR = item.Billing_Matdoc_GR;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRecall_Inbound_Export");
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
                //olog.logging("Report", ex.Message);
                throw ex;
            }
        }
        #endregion


        #region ExportExcel
        public dynamic ExportExcelRecallInbound(ReportRecall_InboundRequestModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportRecall_InboundExModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                var convert_Type_query = new List<MasterDataDataAccess.Models.View_ReportRecall_Inbound_Excel>();
                var query = convert_Type_query.AsQueryable();
                var room_Name = "";
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.View_ReportRecall_Inbound_Excel.AsQueryable();
                    room_Name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.View_ReportRecall_Inbound_Excel.AsQueryable();
                    room_Name = "Freeze";
                }
                //var query = Master_DBContext.View_ReportRecall_Inbound_Excel.AsQueryable();

                if (data.advanceSearch == true)
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {

                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator, count, StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));

                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                    }

                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        //query = query.Where(c => c.Vendor_Id.Contains(data.vendorId));
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                    }

                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] poNo_list = data.po_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => poNo_list.Contains(c.PO_No));
                        //query = query.Where(c => c.PO_No.Contains(data.po_No));
                    }

                    if (!string.IsNullOrEmpty(data.NoASN)) //รอเปลี่ยน c. =>
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] asn_list = data.NoASN.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => asn_list.Contains(c.No_ASN));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }


                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] grNo_list = data.goodsReceive_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => grNo_list.Contains(c.GoodsReceive_No));
                        //query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
                    }
                    if (!string.IsNullOrEmpty(data.date_mfg) && !string.IsNullOrEmpty(data.date_mfg_to))
                    {
                        var dateStart = data.date_mfg.toBetweenDate();
                        var dateEnd = data.date_mfg_to.toBetweenDate();
                        query = query.Where(c => c.MFG_Date >= dateStart.start && c.MFG_Date <= dateEnd.end);
                    }
                    if (!string.IsNullOrEmpty(data.date_exp) && !string.IsNullOrEmpty(data.date_exp_to))
                    {
                        var dateStart = data.date_exp.toBetweenDate();
                        var dateEnd = data.date_exp_to.toBetweenDate();
                        query = query.Where(c => c.EXP_Date >= dateStart.start && c.EXP_Date <= dateEnd.end);

                    }

                    //เช็ตแถวสาม
                    if (!string.IsNullOrEmpty(data.batch_lot))
                    {
                        query = query.Where(c => c.Product_Lot_GR.Contains(data.batch_lot));
                    }
                    if (!string.IsNullOrEmpty(data.sloc))
                    {
                        query = query.Where(c => c.ERP_Location.Contains(data.sloc));
                    }
                    // Match
                    //รอเปลี่ยน c.Match_Name
                    if (!string.IsNullOrEmpty(data.Match))
                    {
                        query = query.Where(c => c.Match_Name.Contains(data.Match));
                    }

                }
                else
                {
                    //เช็ตแถวแรก
                    if (!string.IsNullOrEmpty(data.materialNo))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] materNo_list = data.materialNo.Split(spearator, count, StringSplitOptions.None);
                        //var multi_mater = string.arr_material.Where(c => arr_material.Contains(c.Product_Id));

                        query = query.Where(c => materNo_list.Contains(c.Product_Id));
                        //query = query.Where(c => c.Product_Id.Contains(data.materialNo));
                    }
                    if (!string.IsNullOrEmpty(data.vendorId))   //
                    {
                        query = query.Where(c => c.Vendor_Id.Contains(data.vendorId)
                                                 || c.Vendor_Name.Contains(data.vendorId));
                        //query = query.Where(c => c.Vendor_Id.Contains(data.vendorId));
                    }
                    if (!string.IsNullOrEmpty(data.po_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] poNo_list = data.po_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => poNo_list.Contains(c.PO_No));
                        //query = query.Where(c => c.PO_No.Contains(data.po_No));
                    }
                    if (!string.IsNullOrEmpty(data.NoASN)) //รอเปลี่ยน c. =>
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] asn_list = data.NoASN.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => asn_list.Contains(c.No_ASN));
                        //query = query.Where(c => c.Tag_No.Contains(data.NoTag));
                    }

                    //เช็ตแถวสอง
                    if (!string.IsNullOrEmpty(data.goodsReceive_No))
                    {
                        char[] spearator = { ',', ' ' };
                        Int32 count = 1000;
                        String[] grNo_list = data.goodsReceive_No.Split(spearator, count, StringSplitOptions.None);
                        query = query.Where(c => grNo_list.Contains(c.GoodsReceive_No));
                        //query = query.Where(c => c.GoodsReceive_No.Contains(data.goodsReceive_No));
                    }
                    if (!string.IsNullOrEmpty(data.date_GR) && !string.IsNullOrEmpty(data.date_GR_to))
                    {
                        var dateStart = data.date_GR.toBetweenDate();
                        var dateEnd = data.date_GR_to.toBetweenDate();
                        query = query.Where(c => c.GoodsReceive_Date >= dateStart.start && c.GoodsReceive_Date <= dateEnd.end); //
                    }

                }

                var resultquery = query.OrderBy(o => o.Tag_No).ToList();
                //Orderby 
                //Tag_No
                //GoodsIssue_No
                //TruckLoad_No

                if (resultquery.Count == 0)
                {

                }
                else
                {
                    int num = 0;
                    foreach (var item in query)
                    {
                        var resultItem = new ReportRecall_InboundExModel();

                        resultItem.rowNo = num + 1;
                        resultItem.Date_now_form = DateTime.Now.ToString("dd/MM/yyyy  hh:mm:ss ") + " น.";
                        resultItem.Tag_No = item.Tag_No;
                        resultItem.Vendor_Id = item.Vendor_Id;
                        resultItem.Vendor_Name = item.Vendor_Name;
                        resultItem.PO_No = item.PO_No;
                        resultItem.NoASN = item.No_ASN; //ASN
                        resultItem.GoodsReceive_No = item.GoodsReceive_No;
                        resultItem.GoodsReceive_Date = item.GoodsReceive_Date != null ? item.GoodsReceive_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Product_Id = item.Product_Id;
                        resultItem.Product_Name = item.Product_Name;
                        resultItem.Gr_Qty = item.GR_QTY; //GR Qty
                        resultItem.Gr_Unit = item.GR_unit; //GR unit
                        resultItem.Sale_BUQty = item.Sale_BUQty;
                        resultItem.Sale_BUConversion = item.Sale_BUConversion;
                        resultItem.Sale_SUQty = item.Sale_SUQty;
                        resultItem.Sale_SUConversion = item.Sale_SUConversion;
                        resultItem.MFG_Date = item.MFG_Date != null ? item.MFG_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.EXP_Date = item.EXP_Date != null ? item.EXP_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Product_Lot_GR = item.Product_Lot_GR; //Bacht/lot GR?
                        resultItem.ERP_Location = item.ERP_Location;
                        resultItem.Match = item.Match_Name; //Match
                        resultItem.Billing_Date = item.Billing_Date != null ? item.Billing_Date.Value.ToString("dd/MM/yyyy") : "";
                        resultItem.Billing_Matdoc_GR = item.Billing_Matdoc_GR;
                        resultItem.ambientRoom = room_Name;
                        result.Add(resultItem);
                        num++;

                    }
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportRecall_Inbound_Export");

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
        #endregion
    }
}