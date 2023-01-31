
using GIDataAccess.Models;
using TransferDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using TransferDataAccess.Models;
using MasterDataDataAccess.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace DataAccess
{
    public class TransferDbContext : DbContext
    {


        public virtual DbSet<WM_BinBalance> wm_BinBalance { get; set; }
        public virtual DbSet<WM_BinBalance2> wm_BinBalance2 { get; set; }
        public virtual DbSet<ms_ProductConversionBarcode> ms_ProductConversionBarcode { get; set; }

        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }
        public virtual DbSet<im_TransferStockAdjustment> im_TransferStockAdjustment { get; set; }
        public virtual DbSet<im_TransferStockAdjustmentItem> im_TransferStockAdjustmentItem { get; set; }
        public DbSet<IM_GoodsReceive> IM_GoodsReceives { get; set; }
        public DbSet<IM_GoodsReceiveItem> IM_GoodsReceiveItems { get; set; }
        public virtual DbSet<WM_Tag> wm_Tag { get; set; }
        public virtual DbSet<IM_GoodsReceiveItemLocation> IM_GoodsReceiveItemLocation { get; set; }
        public DbSet<im_GoodsIssue> IM_GoodsIssue { get; set; }
        public DbSet<im_GoodsIssueItem> IM_GoodsIssueItem { get; set; }
        public DbSet<im_GoodsIssueItemLocation> IM_GoodsIssueItemLocation { get; set; }

        public virtual DbSet<WM_TagItem> wm_TagItem { get; set; }

        public DbSet<MS_Warehouse> MS_Warehouse { get; set; }
        public DbSet<MS_Owner> MS_Owner { get; set; }
        public virtual DbSet<wm_BinCard> wm_BinCard { get; set; }
        public DbSet<wm_TagOutPick> wm_TagOutPick { get; set; }
        public DbSet<wm_TagOut> wm_TagOut { get; set; }
        public DbSet<wm_TagOutItem> wm_TagOutItem { get; set; }
        public virtual DbSet<View_BinBalance> View_BinBalance { get; set; }
        public virtual DbSet<View_CartonList> View_CartonList { get; set; }
        public virtual DbSet<ms_ReasonCode> ms_ReasonCode { get; set; }
        public virtual DbSet<View_SumQtyBinbalance> View_SumQtyBinbalance { get; set; }
        public virtual DbSet<View_SumQtyBinbalanceLPN> View_SumQtyBinbalanceLPN { get; set; }
        public virtual DbSet<View_SumQtyLocation> View_SumQtyLocation { get; set; }
        public virtual DbSet<View_SumQtyLpnNo> View_SumQtyLpnNo { get; set; }
        public virtual DbSet<View_SumQtyCarton> View_SumQtyCarton { get; set; }
        public virtual DbSet<View_GT> View_GT { get; set; }
        public virtual DbSet<View_SumQtyBarcodeLocation> View_SumQtyBarcodeLocation { get; set; }
        public virtual DbSet<View_TranferCarton> View_TranferCarton { get; set; }
        
        public DbSet<MS_Product> MS_Product { get; set; }
        public DbSet<MS_DocumentType> MS_DocumentType { get; set; }

        public virtual DbSet<WM_BinCardReserve> WM_BinCardReserve { get; set; }

        public DbSet<IM_GoodsTransfer> IM_GoodsTransfer { get; set; }

        public DbSet<IM_GoodsTransferItem> IM_GoodsTransferItem { get; set; }
        public DbSet<im_TaskList> im_TaskList { get; set; }
        public DbSet<im_TaskListItem> im_TaskListItem { get; set; }
        public DbSet<MS_ProductConversion> ms_ProductConversion { get; set; }
        public DbSet<MS_Location> MS_Location { get; set; }

        public DbSet<View_PlanCarton> View_PlanCarton { get; set; }
        public virtual DbSet<View_SumTranferCarton> View_SumTranferCarton { get; set; }
        public virtual DbSet<View_TranferCartonV2> View_TranferCartonV2 { get; set; }
        public virtual DbSet<Get_CartonRelocation> Get_CartonRelocation { get; set; }
        public virtual DbSet<sp_Get_CartonShortwave> sp_Get_CartonShortwave { get; set; }
        public virtual DbSet<sp_Get_CheckCartonShortwave> sp_Get_CheckCartonShortwave { get; set; }


        public virtual DbSet<View_GTProcessStatus> View_GTProcessStatus { get; set; }
        public virtual DbSet<im_BOM> im_BOM { get; set; }
        public virtual DbSet<View_Report_GoodsTransfer> View_Report_GoodsTransfer { get; set; }
        public virtual DbSet<sp_Trace_replenishment> sp_Trace_replenishment { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("TransferDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
            //optionsBuilder.UseSqlServer(@"Server=10.0.177.33\SQLEXPRESS;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=cfrffmusr;password=ffmusr@cfr;");

            //optionsBuilder.UseSqlServer(@"Server=kascoit.ddns.net,22017;Database=WMSDB_QA;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");
        }
    }
}
