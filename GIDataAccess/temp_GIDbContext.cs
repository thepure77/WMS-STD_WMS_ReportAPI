using GIDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class temp_GIDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }

        public DbSet<im_GoodsIssue> IM_GoodsIssue { get; set; }
        public DbSet<im_GoodsIssueItem> IM_GoodsIssueItem { get; set; }
        public DbSet<im_GoodsIssueItemLocation> IM_GoodsIssueItemLocation { get; set; }
        public DbSet<im_PlanGoodsIssue> IM_PlanGoodsIssue { get; set; }
        public DbSet<im_PlanGoodsIssueItem> IM_PlanGoodsIssueItem { get; set; }
        public DbSet<im_Pack> IM_Pack { get; set; }
        public DbSet<im_PackItem> IM_PackItem { get; set; }
        public DbSet<im_Task> IM_Task { get; set; }
        public DbSet<im_TaskItem> IM_TaskItem { get; set; }
        public DbSet<ms_DocumentType> MS_DocumentType { get; set; }
        public DbSet<ms_DocumentTypeNumber> MS_DocumentTypeNumber { get; set; }
        public DbSet<Sy_AutoNumber> Sy_AutoNumber { get; set; }
        public DbSet<sy_Process> Sy_Process { get; set; }
        public DbSet<sy_ProcessStatus> Sy_ProcessStatus { get; set; }
        public DbSet<wm_TagOut> WM_TagOut { get; set; }
        public DbSet<wm_TagOutItem> WM_TagOutItem { get; set; }
        public DbSet<wm_TagOutPick> WM_TagOutPick { get; set; }

        public DbSet<__overall_round> __overall_round { get; set; }
        public DbSet<__overall_round_pick> __overall_round_pick { get; set; }
        public DbSet<__overall_route> __overall_route { get; set; }
        public DbSet<__overall_status> __overall_status { get; set; }
        public DbSet<__overall_status_Express> __overall_status_Express { get; set; }
        public DbSet<__overall_zone_pick> __overall_zone_pick { get; set; }
        public DbSet<__dashboard> __dashboard { get; set; }

        public virtual DbSet<View_GoodsIssue> View_GoodsIssue { get; set; }

        public DbSet<View_PLANWAVEV> View_PLANWAVEV { get; set; }

        public DbSet<View_PLANWAVEbyPLANGI> View_PLANWAVEbyPLANGI { get; set; }
        public DbSet<View_PLANWAVEbyPLANGIV2> View_PLANWAVEbyPLANGIV2 { get; set; }
        public DbSet<View_Task> View_Task { get; set; }
        public DbSet<View_RPT18_Task> View_RPT18_Task { get; set; }
        public DbSet<View_TaskInsertBinCard> View_TaskInsertBinCard { get; set; }
        public DbSet<View_RPT6_GIL> View_RPT6_GIL { get; set; }
        public DbSet<View_RPT_GI> View_RPT_GI { get; set; }
        public DbSet<View_RPT5_GI> View_RPT5_GI { get; set; }
        public DbSet<View_RPT_GI_SummaryInventoryPayment> View_RPT_GI_SummaryInventoryPayment { get; set; }
        public DbSet<View_RPT_SummaryInventoryPayment> View_RPT_SummaryInventoryPayment { get; set; }
        public DbSet<TB_Summary_Shipping> TB_Summary_Shipping { get; set; }
        public DbSet<TB_RPT_picking_Performance> TB_RPT_picking_Performance { get; set; }
        public DbSet<sp_payment> sp_payment { get; set; }

        public virtual DbSet<im_TruckLoad> im_TruckLoad { get; set; }
        public virtual DbSet<View_Trace_Loading> View_Trace_Loading { get; set; }
        public virtual DbSet<View_Trace_picking> View_Trace_picking { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("temp_OutboundDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
            //optionsBuilder.UseSqlServer(@"Server=10.100.141.37;Database=WMSDB_STD_Outbound;Trusted_Connection=True;Integrated Security=False;user id=SA;password=P@ssw0rd;");

            //optionsBuilder.UseSqlServer(@"Server=kascoit.ddns.net,22017;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=sa;password=K@sc0db12345;");

            //optionsBuilder.UseSqlServer(@"Server=10.0.177.33\SQLEXPRESS;Database=WMSDB;Trusted_Connection=True;Integrated Security=False;user id=cfrffmusr;password=ffmusr@cfr;");
        }
    }

    public class temp_Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class temp_Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
