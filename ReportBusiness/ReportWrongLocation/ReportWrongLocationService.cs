using BinbalanceBusiness;
using DataAccess;
using MasterDataDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ReportBusiness.ReportWrongLocation
{
    public class ReportWrongLocationService
    {
        private BinbalanceDbContext db;

        public ReportWrongLocationService()
        {
            db = new BinbalanceDbContext();
        }

        public ReportWrongLocationService(BinbalanceDbContext db)
        {
            this.db = db;
        }

        #region filter
        //filter
        public ReportWrongLocationModelAct filterViewWrongLocation(ReportWrongLocationModel data)
        {
            var result = new List<ReportWrongLocationModel>();
            try
            {
                var query = db.View_ReportWrongLocation.AsQueryable();

                //if (!string.IsNullOrEmpty(data.key))
                //{
                //    query = query.Where(c => c.Product_Id.Contains(data.key)
                //                        || c.Product_Name.Contains(data.key)
                //                        || c.Location_Name.Contains(data.key));
                //}

                var Item = query.ToList();
                var TotalRow = query.ToList();

                if (data.CurrentPage != 0 && data.PerPage != 0)
                {
                    query = query.Skip(((data.CurrentPage - 1) * data.PerPage));
                }

                if (data.PerPage != 0)
                {
                    query = query.Take(data.PerPage);
                }

                foreach (var item in Item)
                {
                    var resultItem = new ReportWrongLocationModel();
                    resultItem.product_Id = item.Product_Id;
                    resultItem.product_Name = item.Product_Name;
                    resultItem.location_Name = item.Location_Name;
                    resultItem.tag_No = item.Tag_No;
                    resultItem.create_By = item.Create_By;
                    resultItem.create_Date = item.Create_Date != null ? item.Create_Date.Value.ToString("dd/MM/yyyy") : "";
                    result.Add(resultItem);
                }


                var count = TotalRow.Count;

                var ReportWrongLocationModelAct = new ReportWrongLocationModelAct();
                ReportWrongLocationModelAct.itemsview = result.ToList();
                ReportWrongLocationModelAct.pagination = new Pagination() { TotalRow = count, CurrentPage = data.CurrentPage, PerPage = data.PerPage, Key = data.key };

                return ReportWrongLocationModelAct;

            }

            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
