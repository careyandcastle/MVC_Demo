using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TscLibCore.BaseObject;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class TRDBContext : BaseDbContext
    {
        public TRDBContext()
        {
        }

        public TRDBContext(DbContextOptions<TRDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Business> Business { get; set; }
        public virtual DbSet<BusinessDetail> BusinessDetail { get; set; }
        public virtual DbSet<ClassStudents> ClassStudents { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<ProductTypes> ProductTypes { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<StoreProducts> StoreProducts { get; set; }
        public virtual DbSet<SystemParameters> SystemParameters { get; set; }
        public virtual DbSet<事業> 事業 { get; set; }
        public virtual DbSet<修改人> 修改人 { get; set; }
        public virtual DbSet<個資名稱> 個資名稱 { get; set; }
        public virtual DbSet<個資名稱異動> 個資名稱異動 { get; set; }
        public virtual DbSet<個資存取記錄> 個資存取記錄 { get; set; }
        public virtual DbSet<個資群組> 個資群組 { get; set; }
        public virtual DbSet<個資群組成員> 個資群組成員 { get; set; }
        public virtual DbSet<個資群組權限> 個資群組權限 { get; set; }
        public virtual DbSet<分部> 分部 { get; set; }
        public virtual DbSet<單位> 單位 { get; set; }
        public virtual DbSet<單位農機> 單位農機 { get; set; }
        public virtual DbSet<數量折扣主檔> 數量折扣主檔 { get; set; }
        public virtual DbSet<數量折扣明細> 數量折扣明細 { get; set; }
        public virtual DbSet<數量折扣明細異動記錄> 數量折扣明細異動記錄 { get; set; }
        public virtual DbSet<農機具修理方式> 農機具修理方式 { get; set; }
        public virtual DbSet<農機故障修理記錄> 農機故障修理記錄 { get; set; }
        public virtual DbSet<農機故障部位> 農機故障部位 { get; set; }
        public virtual DbSet<農機機種> 農機機種 { get; set; }
        public virtual DbSet<部門> 部門 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Business>(entity =>
            {
                entity.HasKey(e => new { e.BusinessMonth, e.BusinessID });

                entity.Property(e => e.CustomerID)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.DepartmentNo)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UPD_USR)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Business)
                    .HasForeignKey(d => d.CustomerID)
                    .HasConstraintName("FK_Business_Customers");
            });

            modelBuilder.Entity<BusinessDetail>(entity =>
            {
                entity.HasKey(e => new { e.BusinessMonth, e.BusinessID, e.ProductID });

                entity.Property(e => e.ProductID)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UPD_USR)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.BusinessDetail)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessDetail_Products");

                entity.HasOne(d => d.Business)
                    .WithMany(p => p.BusinessDetail)
                    .HasForeignKey(d => new { d.BusinessMonth, d.BusinessID })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_BusinessDetail_Business");
            });

            modelBuilder.Entity<ClassStudents>(entity =>
            {
                entity.HasKey(e => e.EmployeeNo)
                    .HasName("PK__ClassStu__7AD0F1B6CE816617");

                entity.Property(e => e.EmployeeNo)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.Gender)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.Property(e => e.CustomerID)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.AlwaysEncryptName).UseCollation("Chinese_Taiwan_Stroke_BIN2");

                entity.Property(e => e.UPD_USR)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<ProductTypes>(entity =>
            {
                entity.Property(e => e.ProductType)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UPD_USR)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.Property(e => e.ProductID)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ProductType)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UPD_USR)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.ProductTypeNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductType)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_ProductTypes");
            });

            modelBuilder.Entity<StoreProducts>(entity =>
            {
                entity.HasKey(e => new { e.DepartmentNo, e.ProductID })
                    .HasName("PK_ProductStat");

                entity.Property(e => e.DepartmentNo)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.ProductID)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.UPD_USR)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.StoreProducts)
                    .HasForeignKey(d => d.ProductID)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductStat_Products");
            });

            modelBuilder.Entity<SystemParameters>(entity =>
            {
                entity.Property(e => e.UPD_USER).IsFixedLength(true);
            });

            modelBuilder.Entity<事業>(entity =>
            {
                entity.Property(e => e.事業1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<修改人>(entity =>
            {
                entity.Property(e => e.修改人1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<個資名稱>(entity =>
            {
                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<個資名稱異動>(entity =>
            {
                entity.HasKey(e => e.Log編號)
                    .HasName("PK_個資名稱異動記錄");

                entity.Property(e => e.原修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.新修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<個資存取記錄>(entity =>
            {
                entity.Property(e => e.來源IP).IsUnicode(false);

                entity.Property(e => e.查詢者員編)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<個資群組>(entity =>
            {
                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<個資群組成員>(entity =>
            {
                entity.HasKey(e => new { e.個資群組名稱, e.員工編號 });

                entity.Property(e => e.員工編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.個資群組名稱Navigation)
                    .WithMany(p => p.個資群組成員)
                    .HasForeignKey(d => d.個資群組名稱)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_個資群組成員_個資群組");
            });

            modelBuilder.Entity<個資群組權限>(entity =>
            {
                entity.HasKey(e => new { e.個資群組名稱, e.個資名稱 });

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.個資名稱Navigation)
                    .WithMany(p => p.個資群組權限)
                    .HasForeignKey(d => d.個資名稱)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_個資群組權限_個資名稱");

                entity.HasOne(d => d.個資群組名稱Navigation)
                    .WithMany(p => p.個資群組權限)
                    .HasForeignKey(d => d.個資群組名稱)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_個資群組權限_個資群組");
            });

            modelBuilder.Entity<分部>(entity =>
            {
                entity.HasKey(e => new { e.單位, e.部門, e.分部1 });

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.部門Navigation)
                    .WithMany(p => p.分部)
                    .HasForeignKey(d => new { d.單位, d.部門 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_分部_部門");
            });

            modelBuilder.Entity<單位>(entity =>
            {
                entity.Property(e => e.單位1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<單位農機>(entity =>
            {
                entity.HasKey(e => new { e.單位, e.農機機種, e.牌照號碼 });

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.農機機種)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.牌照號碼)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<數量折扣主檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.數量折扣代號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.事業Navigation)
                    .WithMany(p => p.數量折扣主檔)
                    .HasForeignKey(d => d.事業)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_數量折扣主檔_事業");
            });

            modelBuilder.Entity<數量折扣明細>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.數量折扣代號, e.折扣順序 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.數量折扣主檔)
                    .WithMany(p => p.數量折扣明細)
                    .HasForeignKey(d => new { d.事業, d.數量折扣代號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_數量折扣明細_數量折扣主檔");
            });

            modelBuilder.Entity<數量折扣明細異動記錄>(entity =>
            {
                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<農機具修理方式>(entity =>
            {
                entity.Property(e => e.修理方式)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<農機故障修理記錄>(entity =>
            {
                entity.HasKey(e => new { e.單位, e.農機機種, e.牌照號碼, e.故障日期, e.故障部位, e.流水號 });

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.農機機種)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.牌照號碼)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.故障部位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修理方式)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.修理方式Navigation)
                    .WithMany(p => p.農機故障修理記錄)
                    .HasForeignKey(d => d.修理方式)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_農機故障修理_農機具修理方式");

                entity.HasOne(d => d.故障部位Navigation)
                    .WithMany(p => p.農機故障修理記錄)
                    .HasForeignKey(d => d.故障部位)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_農機故障修理_農機故障部位");

                entity.HasOne(d => d.單位農機)
                    .WithMany(p => p.農機故障修理記錄)
                    .HasForeignKey(d => new { d.單位, d.農機機種, d.牌照號碼 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_農機故障修理記錄_單位農機");
            });

            modelBuilder.Entity<農機故障部位>(entity =>
            {
                entity.Property(e => e.故障部位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<農機機種>(entity =>
            {
                entity.Property(e => e.農機機種1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<部門>(entity =>
            {
                entity.HasKey(e => new { e.單位, e.部門1 });

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.單位Navigation)
                    .WithMany(p => p.部門)
                    .HasForeignKey(d => d.單位)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_部門_單位");
            });

            OnModelCreatingPartial(modelBuilder);
        }


        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
