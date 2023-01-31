using BinbalanceBusiness;
using Common.Utils;
using ReportBusiness.ReportPerformance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static ReportBusiness.Labor.NonStandardTransactions.SearchDetailModel;

namespace ReportBusiness.Labor.NonStandardTransactions
{
    public class NonStandardTransactionsService
    {
        #region filter_WO
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

                //var Item = new List<im_MatDoc>();
                var query = new List<ReportPerformanceViewModel>();
                query.Add(new ReportPerformanceViewModel()
                {
                    Create_Date = "20/12/2021",
                    User_Id = "001",
                    User_Name = "Admin",
                    First_Name = "Admin",
                    Last_Name = "Admi",
                    Menu_Name = "ระบบจัดการภายในคลังพัสดุ",
                    Sub_Menu_Index = Guid.Parse("C6F77105-4E5C-479D-8989-B90B3C5A058B"),
                    Sub_Menu_Name = "การโอนย้ายสินค้า",
                    Operations = "UPDATE Transfer No : TF-2021120015",
                    Ref_Document_No = "TF-2021120015",
                    UDF_2 = "20/12/2021",
                    UDF_3 = "14:00",
                });
                query.Add(new ReportPerformanceViewModel()
                {
                    Create_Date = "20/12/2021",
                    User_Id = "001",
                    User_Name = "Admin",
                    First_Name = "Admin",
                    Last_Name = "Admi",
                    Menu_Name = "ระบบจัดการภายในคลังพัสดุ",
                    Sub_Menu_Index = Guid.Parse("C6F77105-4E5C-479D-8989-B90B3C5A058B"),
                    Sub_Menu_Name = "การโอนย้ายสินค้า",
                    Operations = "UPDATE Transfer No : TF-2021120016",
                    Ref_Document_No = "TF-2021120016",
                    UDF_2 = "21/12/2021",
                    UDF_3 = "10:00"
                });

                //var Item = query.ToList();
                //var countAll = Item.Count;

                //if (model.CurrentPage != 0 && model.PerPage != 0)
                //{
                //    Item = Item.Skip(((model.CurrentPage - 1) * model.PerPage)).ToList();
                //}

                //if (model.PerPage != 0)
                //{
                //    Item = Item.Take(model.PerPage).ToList();
                //}
                //else
                //{
                //    Item = Item.Take(50).ToList();
                //}
                var groupItem = query.GroupBy(G => new { G.Create_Date, G.User_Id,G.First_Name,G.Last_Name, G.Sub_Menu_Index }).
                    Select(s => new { s.Key.Create_Date, s.Key.User_Id, s.Key.First_Name, s.Key.Last_Name, s.Key.Sub_Menu_Index }).ToList();
                var countAll = groupItem.Count;
                if (model.CurrentPage != 0 && model.PerPage != 0)
                {
                    groupItem = groupItem.Skip(((model.CurrentPage - 1) * model.PerPage)).ToList();
                }

                if (model.PerPage != 0)
                {
                    groupItem = groupItem.Take(model.PerPage).ToList();
                }
                else
                {
                    groupItem = groupItem.Take(50).ToList();
                }

                String Statue = "";
                var result = new List<SearchDetailModel>();

                //foreach (var item in Item)
                //{
                //    var resultItem = new SearchDetailModel();
                //    resultItem.first_Name = item.First_Name;
                //    result.Add(resultItem);
                //}
                foreach (var item in groupItem)
                {
                    var resultItem = new SearchDetailModel();
                    var itemList = query.Where(c => c.Create_Date == item.Create_Date && c.User_Id == item.User_Id && c.Sub_Menu_Index == item.Sub_Menu_Index).ToList();
                    foreach(var items in itemList)
                    {
                        resultItem.menu_Name = items.Menu_Name;
                        if (!string.IsNullOrEmpty(resultItem.operations))
                        {
                            resultItem.operations += ",";
                        }
                        resultItem.operations += items.Operations;
                        if (!string.IsNullOrEmpty(resultItem.ref_Document_No))
                        {
                            resultItem.ref_Document_No += ",";
                        }
                        resultItem.ref_Document_No += items.Ref_Document_No;
                    }
                    resultItem.user_Id = item.User_Id;
                    resultItem.first_Name = item.First_Name;
                    resultItem.last_Name = item.Last_Name;
                    resultItem.create_Date = item.Create_Date;
                    result.Add(resultItem);
                }

                var count = 0;
                count = countAll;

                var actionResultWO = new actionResultViewModel();
                actionResultWO.itemsNonStandard = result.ToList();
                actionResultWO.pagination = new Pagination() { TotalRow = count, CurrentPage = model.CurrentPage, PerPage = model.PerPage, };
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
