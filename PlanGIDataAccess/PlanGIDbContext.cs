using GIDataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PlanGIDataAccess.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class PlanGIDbContext : DbContext
    {
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Post> Posts { get; set; }
        public virtual DbSet<im_PlanGoodsIssue> im_PlanGoodsIssue { get; set; }
        public virtual DbSet<im_PlanGoodsIssueItem> im_PlanGoodsIssueItem { get; set; }
        public virtual DbSet<View_PlanGI> View_PlanGI { get; set; }

        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }
        public virtual DbSet<View_PlanGiProcessStatus> View_PlanGiProcessStatus { get; set; }
        public virtual DbSet<View_RPT6_PlanGIL> View_RPT6_PlanGIL { get; set; }




        public virtual DbSet<im_Pack> im_Pack { get; set; }
        public virtual DbSet<im_PackItem> im_PackItem { get; set; }
        public virtual DbSet<View_RPT_PlanGI> View_RPT_PlanGI { get; set; }
        public virtual DbSet<View_RPT5_PlanGI> View_RPT5_PlanGI { get; set; }
        public virtual DbSet<View_RPT_PlanGI_SummaryInventoryPayment> View_RPT_PlanGI_SummaryInventoryPayment { get; set; }

        public virtual DbSet<sp_Picking_performance> sp_Picking_performance { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("OutboundDefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
            //optionsBuilder.UseSqlServer(@"Server=10.100.141.37;Database=WMSDB_STD_Outbound;Trusted_Connection=True;Integrated Security=False;user id=SA;password=P@ssw0rd;");

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
