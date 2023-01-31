using BinbalanceDataAccess.Models;
using BinBalanceDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class BinbalanceDbContext : DbContext
    {

        public virtual DbSet<wm_BinBalance> wm_BinBalance { get; set; }
        public virtual DbSet<wm_BinCard> wm_BinCard { get; set; }
        public virtual DbSet<wm_BinCardReserve> wm_BinCardReserve { get; set; }
        public virtual DbSet<View_WaveBinBalance> View_WaveBinBalance { get; set; }
        public virtual DbSet<View_RPT_BinBalance> View_RPT_BinBalance { get; set; }
        public virtual DbSet<View_RPT_BinBalance_SummaryMaterialsStock> View_RPT_BinBalance_SummaryMaterialsStock { get; set; }
        public virtual DbSet<View_RPT_BinBalance_SummaryInventoryPayment> View_RPT_BinBalance_SummaryInventoryPayment { get; set; }
        public virtual DbSet<View_RPT18_BinCard> View_RPT18_BinCard { get; set; }
        public virtual DbSet<View_RPT18_BinCard_Year> View_RPT18_BinCard_Year { get; set; }
        public virtual DbSet<View_RPT17_BinCard> View_RPT17_BinCard { get; set; }
        public virtual DbSet<View_RPT15_BinCard_UU> View_RPT15_BinCard_UU { get; set; }
        public virtual DbSet<View_RPT15_BinCard_QI> View_RPT15_BinCard_QI { get; set; }
        public virtual DbSet<View_RPT17_GR> View_RPT17_GR { get; set; }
        public virtual DbSet<View_RPT14_BinBalance> View_RPT14_BinBalance { get; set; }
        public virtual DbSet<View_RPT16_BinCard> View_RPT16_BinCard { get; set; }
        public virtual DbSet<View_RPT16_BinCard_MaxDate> View_RPT16_BinCard_MaxDate { get; set; }
        public virtual DbSet<View_RPT19_BinBalance> View_RPT19_BinBalance { get; set; }
        public virtual DbSet<View_RPT20_BinBalance> View_RPT20_BinBalance { get; set; }
        public virtual DbSet<View_RPT13_CycleCountDetail> View_RPT13_CycleCountDetail { get; set; }
        public virtual DbSet<View_RPT9_CycleCountDetail> View_RPT9_CycleCountDetail { get; set; }
        public virtual DbSet<View_RPT_InventoryStock> View_RPT_InventoryStock { get; set; }
        public virtual DbSet<View_RPT_StockAging> View_RPT_StockAging { get; set; }
        public virtual DbSet<View_RPT_VendorWarehouse> View_RPT_VendorWarehouse { get; set; }
        public virtual DbSet<View_ReportWrongLocation> View_ReportWrongLocation { get; set; }
        public virtual DbSet<sp_BinbalanceLocationType> sp_BinbalanceLocationType { get; set; }
        public virtual DbSet<wm_CycleCountDetail> wm_CycleCountDetail { get; set; }
        public virtual DbSet<sp_CycleCountSummary> sp_CycleCountSummary { get; set; }
        


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("BinBalanceDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
            //optionsBuilder.UseSqlServer(@"Server=10.100.141.37;Database=WMSDB_STD_Binbalance;Trusted_Connection=True;Integrated Security=False;user id=SA;password=P@ssw0rd;");


        }
    }

    public class Blog
    {
        public int BlogId { get; set; }
        public string Url { get; set; }
        public int Rating { get; set; }
        public List<Post> Posts { get; set; }
    }

    public class Post
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public int BlogId { get; set; }
        public Blog Blog { get; set; }
    }
}
