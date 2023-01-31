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
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportReconcileSap
{
    public class ReportReconcileSapService
    {
        #region printreportReconcileSap
        public dynamic printreportReconcileSap(ReportReconcileSapViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportReconcileSapViewModel>();

            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var BusinessUnit_Index = "";
                var Product_Id = "";
                var Plant = "";
                var Sap_Sloc = "";

                if (data.businessUnitList != null)
                {
                    BusinessUnit_Index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    Product_Id = data.Product_Id;
                }
                if (!string.IsNullOrEmpty(data.Plant))
                {
                    Plant = data.Plant;
                }
                if (!string.IsNullOrEmpty(data.Sap_Sloc))
                {
                    Sap_Sloc = data.Sap_Sloc;
                }


                var businessUnit_Index = new SqlParameter("@BusinessUnit_Index", BusinessUnit_Index);
                var product_Id = new SqlParameter("@Product_Id", Product_Id);
                var plant = new SqlParameter("@Plant", Plant);
                var sap_Sloc = new SqlParameter("@Sap_Sloc", Sap_Sloc);

                var query = new List<MasterDataDataAccess.Models.sp_rpt_16_Reconcile_Sap>();

                var ambientRoom_name = "";
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.sp_rpt_16_Reconcile_Sap.FromSql("sp_rpt_16_Reconcile_Sap @BusinessUnit_Index, @Product_Id, @Plant, @Sap_Sloc",
                        businessUnit_Index, product_Id, plant, sap_Sloc).ToList();
                    ambientRoom_name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.sp_rpt_16_Reconcile_Sap.FromSql("sp_rpt_16_Reconcile_Sap @BusinessUnit_Index, @Product_Id, @Plant, @Sap_Sloc",
                        businessUnit_Index, product_Id, plant, sap_Sloc).ToList();
                    ambientRoom_name = "Freeze";
                }

                int num = 0;
                foreach (var item in query)
                {
                    var resultItem = new ReportReconcileSapViewModel();
                    num = num + 1;
                    resultItem.Row_No = num;
                    resultItem.ambientRoom_name = ambientRoom_name;
                    resultItem.Warehouse_Type = item.Warehouse_Type;
                    resultItem.BusinessUnit_Index = item.BusinessUnit_Index.ToString();
                    resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                    resultItem.Product_Index = item.Product_Index.ToString();
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.Bu_Qty = item.Bu_Qty.ToString();
                    resultItem.Bu_Unit = item.Bu_Unit;
                    resultItem.Su_Qty = item.Su_Qty.ToString();
                    resultItem.Su_Ratio = item.Su_Ratio.ToString();
                    resultItem.Su_Unit = item.Su_Unit;
                    resultItem.Plant = item.Plant;
                    resultItem.Sap_Sloc = item.Sap_Sloc;
                    resultItem.Date = DateTime.Now.toString() == null ? "" : DateTime.Now.ToString("dd/MM/yyyy");

                    result.Add(resultItem);
                }


                rootPath = rootPath.Replace("\\ReportAPI", "");
                //var reportPath = rootPath + "\\ReportBusiness\\Report9\\Report9.rdlc";
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportReconcileSap");
                //var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportSpaceUtilization");
                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportReconcileSap" + DateTime.Now.ToString("yyyyMMddHHmmss");

                var renderedBytes = report.Execute(RenderType.Pdf);

                Utils objReport = new Utils();
                fullPath = objReport.saveReport(renderedBytes.MainStream, fileName + ".pdf", rootPath);
                var saveLocation = objReport.PhysicalPath(fileName + ".pdf", rootPath);
                return saveLocation;


            }
            catch (Exception ex)
            {
                //olog.logging("ReportKPI", ex.Message);
                throw ex;
            }
        }
        #endregion

        public string ExportExcel(ReportReconcileSapViewModel data, string rootPath = "")
        {
            var Master_DBContext = new MasterDataDbContext();
            var temp_Master_DBContext = new temp_MasterDataDbContext();

            var culture = new System.Globalization.CultureInfo("en-US");
            String State = "Start";
            String msglog = "";
            var olog = new logtxt();
            var result = new List<ReportReconcileSapViewModel>();
            try
            {
                Master_DBContext.Database.SetCommandTimeout(360);
                temp_Master_DBContext.Database.SetCommandTimeout(360);

                var BusinessUnit_Index = "";
                var Product_Id = "";
                var Plant = "";
                var Sap_Sloc = "";

                if (data.businessUnitList != null)
                {
                    BusinessUnit_Index = data.businessUnitList.BusinessUnit_Index.ToString();
                }
                if (!string.IsNullOrEmpty(data.Product_Id))
                {
                    Product_Id = data.Product_Id;
                }
                if (!string.IsNullOrEmpty(data.Plant))
                {
                    Plant = data.Plant;
                }
                if (!string.IsNullOrEmpty(data.Sap_Sloc))
                {
                    Sap_Sloc = data.Sap_Sloc;
                }


                var businessUnit_Index = new SqlParameter("@BusinessUnit_Index", BusinessUnit_Index);
                var product_Id = new SqlParameter("@Product_Id", Product_Id);
                var plant = new SqlParameter("@Plant", Plant);
                var sap_Sloc = new SqlParameter("@Sap_Sloc", Sap_Sloc);

                var query = new List<MasterDataDataAccess.Models.sp_rpt_16_Reconcile_Sap>();

                var ambientRoom_name = "";
                if (data.ambientRoom != "02")
                {
                    query = Master_DBContext.sp_rpt_16_Reconcile_Sap.FromSql("sp_rpt_16_Reconcile_Sap @BusinessUnit_Index, @Product_Id, @Plant, @Sap_Sloc",
                        businessUnit_Index, product_Id, plant, sap_Sloc).ToList();
                    ambientRoom_name = "Ambient";
                }
                else
                {
                    query = temp_Master_DBContext.sp_rpt_16_Reconcile_Sap.FromSql("sp_rpt_16_Reconcile_Sap @BusinessUnit_Index, @Product_Id, @Plant, @Sap_Sloc",
                        businessUnit_Index, product_Id, plant, sap_Sloc).ToList();
                    ambientRoom_name = "Freeze";
                }

                int num = 0;
                foreach (var item in query)
                {
                    var resultItem = new ReportReconcileSapViewModel();
                    num = num + 1;
                    resultItem.Row_No = num;
                    resultItem.ambientRoom_name = ambientRoom_name;
                    resultItem.Warehouse_Type = item.Warehouse_Type;
                    resultItem.BusinessUnit_Index = item.BusinessUnit_Index.ToString();
                    resultItem.BusinessUnit_Name = item.BusinessUnit_Name;
                    resultItem.Product_Index = item.Product_Index.ToString();
                    resultItem.Product_Id = item.Product_Id;
                    resultItem.Product_Name = item.Product_Name;
                    resultItem.Bu_Qty = item.Bu_Qty.ToString();
                    resultItem.Bu_Unit = item.Bu_Unit;
                    resultItem.Su_Qty = item.Su_Qty.ToString();
                    resultItem.Su_Ratio = item.Su_Ratio.ToString();
                    resultItem.Su_Unit = item.Su_Unit;
                    resultItem.Plant = item.Plant;
                    resultItem.Sap_Sloc = item.Sap_Sloc;
                    resultItem.Date = DateTime.Now.toString() == null ? "" : DateTime.Now.ToString("dd/MM/yyyy");

                    result.Add(resultItem);
                }

                rootPath = rootPath.Replace("\\ReportAPI", "");
                var reportPath = rootPath + new AppSettingConfig().GetUrl("ReportReconcileSap");

                LocalReport report = new LocalReport(reportPath);
                report.AddDataSource("DataSet1", result);

                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

                string fileName = "";
                string fullPath = "";
                fileName = "ReportReconcileSap";

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
