using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ReportDataAccess.Models;
using System.Collections.Generic;
using System.IO;

namespace DataAccess
{
    public class ReportDbContext : DbContext
    {

        public virtual DbSet<GetValueByColumn> GetValueByColumn { get; set; }
        public virtual DbSet<im_GoodsIssue> im_GoodsIssue { get; set; }
        public virtual DbSet<im_GoodsIssueItem> im_GoodsIssueItem { get; set; }
        public virtual DbSet<im_GoodsIssueItemLocation> im_GoodsIssueItemLocation { get; set; }
        public virtual DbSet<im_GoodsReceive> im_GoodsReceive { get; set; }
        public virtual DbSet<im_GoodsReceiveItem> im_GoodsReceiveItem { get; set; }
        public virtual DbSet<im_GoodsReceiveItemLocation> im_GoodsReceiveItemLocation { get; set; }
        public virtual DbSet<im_PlanGoodsIssue> im_PlanGoodsIssue { get; set; }
        public virtual DbSet<im_PlanGoodsIssueItem> im_PlanGoodsIssueItem { get; set; }
        public virtual DbSet<im_PlanGoodsReceive> im_PlanGoodsReceive { get; set; }
        public virtual DbSet<im_PlanGoodsReceiveItem> im_PlanGoodsReceiveItem { get; set; }
             
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder();
                builder.AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false);

                var configuration = builder.Build();

                var connectionString = configuration.GetConnectionString("DefaultConnection").ToString();

                optionsBuilder.UseSqlServer(connectionString);
            }
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
