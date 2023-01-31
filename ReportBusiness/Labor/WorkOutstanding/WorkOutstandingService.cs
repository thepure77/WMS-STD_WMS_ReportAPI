using BinbalanceBusiness;
using Common.Utils;
using ReportBusiness.ReportPerformance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ReportBusiness.Labor.WorkOutstanding.SearchDetailModel;

namespace ReportBusiness.Labor.WorkOutstanding
{
    public class WorkOutstandingService
    {
        #region filter
        public actionResultViewModel filter(SearchDetailModel model)
        {
            try
            {
                //var query = db.im_MatDoc.AsQueryable();
                DateTime startdate = DateTime.Now.toString().toBetweenDate().start;
                DateTime enddate = DateTime.Now.toString().toBetweenDate().end;

                //#region Basic

                //if (!string.IsNullOrEmpty(model.key))
                //{
                //    query = query.Where(c => c.MatDoc_No.Contains(model.key)
                //                        //|| c.Owner_Name.Contains(model.key)
                //                        //|| c.Create_By.Contains(model.key)
                //                        //|| c.DocumentRef_No1.Contains(model.key)
                //                        //|| c.DocumentType_Name.Contains(model.key)
                //                        );
                //}


                //if (!string.IsNullOrEmpty(model.planGoodsReceive_date) && !string.IsNullOrEmpty(model.planGoodsReceive_date_To))
                //{
                //    var dateStart = model.planGoodsReceive_date.toBetweenDate();
                //    var dateEnd = model.planGoodsReceive_date_To.toBetweenDate();
                //    query = query.Where(c => c.MatDoc_Date >= dateStart.start && c.MatDoc_Date <= dateEnd.end);
                //    startdate = dateStart.start;
                //    enddate = dateEnd.end;
                //}
                //else if (!string.IsNullOrEmpty(model.planGoodsReceive_date))
                //{
                //    var planGoodsReceive_date_From = model.planGoodsReceive_date.toBetweenDate();
                //    query = query.Where(c => c.MatDoc_Date >= planGoodsReceive_date_From.start);
                //    startdate = planGoodsReceive_date_From.start;
                //    enddate = planGoodsReceive_date_From.end;
                //}
                //else if (!string.IsNullOrEmpty(model.planGoodsReceive_date_To))
                //{
                //    var planGoodsReceive_date_To = model.planGoodsReceive_date_To.toBetweenDate();
                //    query = query.Where(c => c.MatDoc_Date <= planGoodsReceive_date_To.start);
                //    startdate = planGoodsReceive_date_To.start;
                //    enddate = planGoodsReceive_date_To.end;
                //}

                //if (!string.IsNullOrEmpty(model.product_Lot))
                //{
                //    var filterLot = db.im_MatDocItem.Where(c => c.Product_Lot.Contains(model.product_Lot) && query.Select(s => s.MatDoc_Index).Contains(c.MatDoc_Index)
                //    && c.Create_Date >= startdate && c.Create_Date <= enddate).Select(s => new { s.MatDoc_Index }).GroupBy(g => g.MatDoc_Index).ToList();
                //    query = query.Where(c => filterLot.Select(s => s.Key).Contains(c.MatDoc_Index));
                //}
                //#endregion

                //var Item = new List<im_MatDoc>();
                var query = new List<ReportPerformanceViewModel>();
                query.Add(new ReportPerformanceViewModel()
                {
                    Create_Date = "20/12/2021",
                    User_Id = "001",
                    User_Name = "Admin",
                    First_Name = "Admin",
                    Last_Name = "Admi",
                    Menu_Name = "จัดการสินค้า ขาออก",
                    Sub_Menu_Name = "ใบขอโอนสินค้า",
                    UDF_2 = "20/12/2021",
                    UDF_3 = "14:00"
                });
                query.Add(new ReportPerformanceViewModel()
                {
                    Create_Date = "20/12/2021",
                    User_Id = "001",
                    User_Name = "Admin",
                    First_Name = "Admin",
                    Last_Name = "Admi",
                    Menu_Name = "จัดการสินค้า ขาออก",
                    Sub_Menu_Name = "ใบขอโอนสินค้า",
                    UDF_2 = "21/12/2021",
                    UDF_3 = "10:00"
                });

                var Item = query.ToList();


                var countAll = Item.Count;
                var count1 = Item.Count;

                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    Item = Item.Skip(((model.CurrentPage - 1) * model.PerPage)).ToList();
                }

                if (model.PerPage != 0)
                {
                    Item = Item.Take(model.PerPage).ToList();
                }
                else
                {
                    Item = Item.Take(50).ToList();
                }

                String Statue = "";
                var result = new List<SearchDetailModel>();

                foreach (var item in Item)
                {
                    var resultItem = new SearchDetailModel();
                    resultItem.first_Name = item.First_Name;
                    result.Add(resultItem);
                }

                var count = 0;
                count = countAll;

                var actionResultWO = new actionResultViewModel();
                actionResultWO.itemsWO = result.ToList();
                actionResultWO.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };
                actionResultWO.total_Tab1 = count1;
                actionResultWO.countAll = countAll;

                return actionResultWO;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
