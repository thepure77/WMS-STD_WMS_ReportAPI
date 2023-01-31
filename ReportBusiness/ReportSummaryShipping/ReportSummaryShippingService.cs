using AspNetCore.Reporting;
using BinBalanceBusiness;
using Business.Library;
using Common.Utils;
using DataAccess;
using PlanGRBusiness.Libs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportSummaryShipping
{
    public class ReportSummaryShippingService
    {
        #region printReportSummaryShipping
        public dynamic printReportSummaryShipping(ReportSummaryShippingViewModel data, string rootPath = "")
        {

            var GI_DBContext = new GIDbContext();
            //var M_DBContext = new MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();

            var result = new List<ReportSummaryShippingViewModel>();

            try
            {
                var querySS = GI_DBContext.TB_Summary_Shipping.AsQueryable();

                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    querySS = querySS.Where(c => c.TruckLoad_No.Contains(data.TruckLoad_No));
                }

                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    querySS = querySS.Where(c => c.PlanGoodsIssue_No.Contains(data.PlanGoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    querySS = querySS.Where(c => c.GoodsIssue_No.Contains(data.GoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.ShipTo_Id))
                {
                    querySS = querySS.Where(c => c.ShipTo_Id.Contains(data.ShipTo_Id));
                }

                if (!string.IsNullOrEmpty(data.Due_Date) && !string.IsNullOrEmpty(data.Due_Date_To))
                {
                    var dateStart = data.Due_Date.toBetweenDate().start;
                    var dateEnd = data.Due_Date_To.toBetweenDate().end;

                    querySS = querySS.Where(c => c.Expect_Delivery_Date >= dateStart && c.Expect_Delivery_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.Due_Date))
                {
                    var dateStart = data.Due_Date.toBetweenDate().start;
                    var dateEnd = data.Due_Date.toBetweenDate().end;

                    querySS = querySS.Where(c => c.Expect_Delivery_Date >= dateStart && c.Expect_Delivery_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.Due_Date_To))
                {
                    var dateStart = data.Due_Date_To.toBetweenDate().start;
                    var dateEnd = data.Due_Date_To.toBetweenDate().end;

                    querySS = querySS.Where(c => c.Expect_Delivery_Date >= dateStart && c.Expect_Delivery_Date <= dateEnd);
                }

                if (!string.IsNullOrEmpty(data.Appoint_Date) && !string.IsNullOrEmpty(data.Appoint_Date_To))
                {
                    var dateStart = data.Appoint_Date.toBetweenDate();
                    var dateEnd = data.Appoint_Date_To.toBetweenDate();
                    querySS = querySS.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.Appoint_Date))
                {
                    var dateStart = data.Appoint_Date.toBetweenDate();
                    var dateEnd = data.Appoint_Date.toBetweenDate();

                    querySS = querySS.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.Appoint_Date_To))
                {
                    var dateStart = data.Appoint_Date_To.toBetweenDate();
                    var dateEnd = data.Appoint_Date_To.toBetweenDate();

                    querySS = querySS.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end);
                }

                var query = querySS.ToList();

                foreach (var item in query)
                {
                    var resultItem = new ReportSummaryShippingViewModel();
                    resultItem.Expect_Delivery_Date = item.Expect_Delivery_Date == null ? "" : item.Expect_Delivery_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.Appointment_Date = item.Appointment_Date == null ? "" : item.Appointment_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.Appointment_Time = item.Appointment_Time;
                    resultItem.TruckLoad_No = item.TruckLoad_No;
                    resultItem.Appointment_Id = item.Appointment_Id;
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.GoodsIssue_No = item.GoodsIssue_No;
                    resultItem.GoodsIssue_Date = item.GoodsIssue_Date == null ? "" : item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                    resultItem.Bill_No = item.Bill_No;
                    resultItem.Matdoc = item.Matdoc;
                    resultItem.ShipTo_Id = item.ShipTo_Id;
                    resultItem.ShipTo_Name = item.ShipTo_Name;
                    resultItem.Province = item.Province;
                    resultItem.BranchCode = item.BranchCode;
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.Order_Qty = item.Order_Qty;
                    resultItem.Order_UNIT = item.Order_UNIT;
                    resultItem.SU_Order_QTY = item.SU_Order_QTY;
                    resultItem.SU_Unit = item.SU_Unit;
                    resultItem.BU_Order_Qty = item.BU_Order_Qty;
                    resultItem.BU_Unit = item.BU_Unit;
                    resultItem.CBM = item.CBM;
                    resultItem.Document_Remark = item.Document_Remark;
                    resultItem.DocumentRef_No3 = item.DocumentRef_No3;
                    resultItem.VehicleCompany_Name = item.VehicleCompany_Name;
                    resultItem.VehicleType_Name = item.VehicleType_Name;
                    resultItem.Vehicle_Registration = item.Vehicle_Registration;

                    result.Add(resultItem);
                }


                //result.Add(new ReportSummaryShippingViewModel() { Expect_Delivery_Date = "20/12/2021", Appointment_Date = "20/12/2021", Appointment_Time = "13:00-15:00", TruckLoad_No = "T00001", PlanGoodsIssue_No = "DO001", Order_Qty = 10 });
                //result.Add(new ReportSummaryShippingViewModel() { Expect_Delivery_Date = "20/12/2021", Appointment_Date = "20/12/2021", Appointment_Time = "13:00-15:00", TruckLoad_No = "T00001", PlanGoodsIssue_No = "DO002", Order_Qty = 30 });
                //result.Add(new ReportSummaryShippingViewModel() { Expect_Delivery_Date = "20/12/2021", Appointment_Date = "20/12/2021", Appointment_Time = "13:00-15:00", TruckLoad_No = "T00002", PlanGoodsIssue_No = "DO003", Order_Qty = 25 });


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryShipping");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportSummaryShipping" + DateTime.Now.ToString("yyyyMMddHHmmss");

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

        public string ExportExcel(ReportSummaryShippingViewModel data, string rootPath = "")
        {

            var GI_DBContext = new GIDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportSummaryShippingViewModel>();

            try
            {
                var querySS = GI_DBContext.TB_Summary_Shipping.AsQueryable();

                if (!string.IsNullOrEmpty(data.TruckLoad_No))
                {
                    querySS = querySS.Where(c => c.TruckLoad_No.Contains(data.TruckLoad_No));
                }

                if (!string.IsNullOrEmpty(data.PlanGoodsIssue_No))
                {
                    querySS = querySS.Where(c => c.PlanGoodsIssue_No.Contains(data.PlanGoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.GoodsIssue_No))
                {
                    querySS = querySS.Where(c => c.GoodsIssue_No.Contains(data.GoodsIssue_No));
                }

                if (!string.IsNullOrEmpty(data.ShipTo_Id))
                {
                    querySS = querySS.Where(c => c.ShipTo_Id.Contains(data.ShipTo_Id));
                }

                if (!string.IsNullOrEmpty(data.Due_Date) && !string.IsNullOrEmpty(data.Due_Date_To))
                {
                    var dateStart = data.Due_Date.toBetweenDate().start;
                    var dateEnd = data.Due_Date_To.toBetweenDate().end;

                    querySS = querySS.Where(c => c.Expect_Delivery_Date >= dateStart && c.Expect_Delivery_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.Due_Date))
                {
                    var dateStart = data.Due_Date.toBetweenDate().start;
                    var dateEnd = data.Due_Date.toBetweenDate().end;

                    querySS = querySS.Where(c => c.Expect_Delivery_Date >= dateStart && c.Expect_Delivery_Date <= dateEnd);
                }
                else if (!string.IsNullOrEmpty(data.Due_Date_To))
                {
                    var dateStart = data.Due_Date_To.toBetweenDate().start;
                    var dateEnd = data.Due_Date_To.toBetweenDate().end;

                    querySS = querySS.Where(c => c.Expect_Delivery_Date >= dateStart && c.Expect_Delivery_Date <= dateEnd);
                }

                if (!string.IsNullOrEmpty(data.Appoint_Date) && !string.IsNullOrEmpty(data.Appoint_Date_To))
                {
                    var dateStart = data.Appoint_Date.toBetweenDate();
                    var dateEnd = data.Appoint_Date_To.toBetweenDate();
                    querySS = querySS.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.Appoint_Date))
                {
                    var dateStart = data.Appoint_Date.toBetweenDate();
                    var dateEnd = data.Appoint_Date.toBetweenDate();

                    querySS = querySS.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end);
                }
                else if (!string.IsNullOrEmpty(data.Appoint_Date_To))
                {
                    var dateStart = data.Appoint_Date_To.toBetweenDate();
                    var dateEnd = data.Appoint_Date_To.toBetweenDate();

                    querySS = querySS.Where(c => c.Appointment_Date >= dateStart.start && c.Appointment_Date <= dateEnd.end);
                }

                var query = querySS.ToList();

                foreach (var item in query)
                {
                    var resultItem = new ReportSummaryShippingViewModel();
                    resultItem.Expect_Delivery_Date = item.Expect_Delivery_Date == null ? "" : item.Expect_Delivery_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.Appointment_Date = item.Appointment_Date == null ? "" : item.Appointment_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.Appointment_Time = item.Appointment_Time;
                    resultItem.TruckLoad_No = item.TruckLoad_No;
                    resultItem.Appointment_Id = item.Appointment_Id;
                    resultItem.Dock_Name = item.Dock_Name;
                    resultItem.GoodsIssue_No = item.GoodsIssue_No;
                    resultItem.GoodsIssue_Date = item.GoodsIssue_Date == null ? "" : item.GoodsIssue_Date.Value.ToString("dd/MM/yyyy");
                    resultItem.PlanGoodsIssue_No = item.PlanGoodsIssue_No;
                    resultItem.Bill_No = item.Bill_No;
                    resultItem.Matdoc = item.Matdoc;
                    resultItem.ShipTo_Id = item.ShipTo_Id;
                    resultItem.ShipTo_Name = item.ShipTo_Name;
                    resultItem.Province = item.Province;
                    resultItem.BranchCode = item.BranchCode;
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.Order_Qty = item.Order_Qty;
                    resultItem.Order_UNIT = item.Order_UNIT;
                    resultItem.SU_Order_QTY = item.SU_Order_QTY;
                    resultItem.SU_Unit = item.SU_Unit;
                    resultItem.BU_Order_Qty = item.BU_Order_Qty;
                    resultItem.BU_Unit = item.BU_Unit;
                    resultItem.CBM = item.CBM;
                    resultItem.Document_Remark = item.Document_Remark;
                    resultItem.DocumentRef_No3 = item.DocumentRef_No3;
                    resultItem.VehicleCompany_Name = item.VehicleCompany_Name;
                    resultItem.VehicleType_Name = item.VehicleType_Name;
                    resultItem.Vehicle_Registration = item.Vehicle_Registration;

                    result.Add(resultItem);
                }




                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSummaryShipping");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportSummaryShipping" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Excel);
                //fullPath = saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".xls", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".xls", rootPath);
                return saveLocation;


                //var saveLocation = rootPath + fullPath;
                ////File.Delete(saveLocation);
                ////ExcelRefresh(reportPath);
                //return saveLocation;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //public string saveReport(byte[] file, string name, string rootPath)
        //{
        //    var saveLocation = PhysicalPath(name, rootPath);
        //    FileStream fs = new FileStream(saveLocation, FileMode.Create);
        //    BinaryWriter bw = new BinaryWriter(fs);
        //    try
        //    {
        //        try
        //        {
        //            bw.Write(file);
        //        }
        //        finally
        //        {
        //            fs.Close();
        //            bw.Close();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    return VirtualPath(name);
        //}

        //public string PhysicalPath(string name, string rootPath)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    var path = rootPath + vPath;
        //    if (!System.IO.Directory.Exists(path))
        //    {
        //        System.IO.Directory.CreateDirectory(path);
        //    }

        //    var saveLocation = System.IO.Path.Combine(path, filename);
        //    return saveLocation;
        //}
        //public string VirtualPath(string name)
        //{
        //    var filename = name;
        //    var vPath = ReportPath;
        //    vPath = vPath.Replace("~", "");
        //    return vPath + filename;
        //}
        //private string ReportPath
        //{
        //    get
        //    {
        //        var url = "\\ReportGenerator\\";
        //        return url;
        //    }
        //}
    }
}
