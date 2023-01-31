using GRDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DataAccess
{
    public class temp_GRDbContext : DbContext
    {
        public DbSet<IM_PlanGoodsReceive> IM_PlanGoodsReceive { get; set; }
        public DbSet<IM_PlanGoodsReceiveItem> IM_PlanGoodsReceiveItems { get; set; }
        public DbSet<IM_GoodsReceive> IM_GoodsReceive { get; set; }
        public DbSet<IM_GoodsReceiveItem> IM_GoodsReceiveItem { get; set; }
        public DbSet<WM_Tag> WM_Tag { get; set; }
        public DbSet<View_ProductDetail> View_ProductDetail { get; set; }
        public virtual DbSet<IM_GoodsReceiveItemLocation> IM_GoodsReceiveItemLocation { get; set; }
        //public virtual DbSet<WM_BinBalance> wm_BinBalance { get; set; }
        //public virtual DbSet<WM_BinCard> wm_BinCard { get; set; }
        public virtual DbSet<WM_TagItem> wm_TagItem { get; set; }
        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }
        public virtual DbSet<View_GoodsReceivePending> View_GoodsReceivePending { get; set; }
        public virtual DbSet<View_RPT_GRIL> View_RPT_GRIL { get; set; }
        public virtual DbSet<View_GoodssReceiveItem_RPT_6> View_GoodssReceiveItem_RPT_6 { get; set; }
        public virtual DbSet<View_RPT_GRTI> View_RPT_GRTI { get; set; }
        public virtual DbSet<View_CheckCloseDocument> View_CheckCloseDocuments { get; set; }
        public DbSet<View_PlanGoodsReceiveItem> View_PlanGoodsReceiveItem { get; set; }
        public DbSet<View_PlanGrProcessStatus> View_PlanGrProcessStatus { get; set; }
        public DbSet<View_GoodsReceiveItemAlloCate> View_GoodsReceiveItemAlloCate { get; set; }

        public DbSet<View_CheckDocumentStatus> View_CheckDocumentStatus { get; set; }

        public DbSet<View_GrProcessStatus> View_GrProcessStatus { get; set; }

        public DbSet<View_GetPlanGoodsReceiveItem> View_GetPlanGoodsReceiveItem { get; set; }

        public DbSet<View_GetTagItem> View_GetTagItem { get; set; }
        public DbSet<TagItemPutawaySKU> TagItemPutawaySKU { get; set; }

        public DbSet<View_GetScanProductDetail> View_GetScanProductDetail { get; set; }
        public DbSet<Get_PlanGoodsIssueItemPopup> Get_PlanGoodsIssueItemPopup { get; set; }

        public DbSet<View_ReturnReceive> View_ReturnReceive { get; set; }
        public DbSet<im_GoodsIssueItemLocation> IM_GoodsIssueItemLocation { get; set; }

        public DbSet<View_CheckPlanGR> View_CheckPlanGR { get; set; }

        public DbSet<View_GoodsReceiveWithTag> View_GoodsReceiveWithTag { get; set; }
        public virtual DbSet<View_RPT_GR_SummaryMaterialsStock> View_RPT_GR_SummaryMaterialsStock { get; set; }
        public virtual DbSet<View_GoodsReceive> View_GoodsReceive { get; set; }

        public virtual DbSet<View_TagitemSugesstion> View_TagitemSugesstion { get; set; }
        public virtual DbSet<View_RPT_GR> View_RPT_GR { get; set; }
        public virtual DbSet<View_RPT_GRV2> View_RPT_GRV2 { get; set; }
        public virtual DbSet<View_RPT_GR_GRI_GRIL> View_RPT_GR_GRI_GRIL { get; set; }
        
        public virtual DbSet<View_RPT5_GRI> View_RPT5_GRI { get; set; }

        public virtual DbSet<View_RPT_GRI_SummaryInventoryPayment> View_RPT_GRI_SummaryInventoryPayment { get; set; }

        public virtual DbSet<View_PrintOutRetrun> View_PrintOutRetrun { get; set; }
        public virtual DbSet<View_GoodsReceive_Finish_Good_BOM> View_GoodsReceive_Finish_Good_BOM { get; set; }
        public virtual DbSet<View_Report1_GR> View_Report1_GR { get; set; }
        public virtual DbSet<sp_Inventory_Accuracy> sp_Inventory_Accuracy { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("temp_InboundDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
            //optionsBuilder.UseSqlServer(@"Server=192.168.1.11\MSSQL2017;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");
            //optionsBuilder.UseSqlServer(@"Server=10.0.177.33\SQLEXPRESS;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=cfrffmusr;password=ffmusr@cfr;");
            //optionsBuilder.UseSqlServer(@"Server=10.100.141.37;Database=WMSDB_STD_Inbound;Trusted_Connection=True;Integrated Security=False;user id=SA;password=P@ssw0rd;");
        }
    }

    

   
}
