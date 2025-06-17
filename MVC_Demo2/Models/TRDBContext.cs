using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVC_Demo2.Models.MvcDemoModel;
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
        public virtual DbSet<事業商品檔> 事業商品檔 { get; set; }
        public virtual DbSet<修改人> 修改人 { get; set; }
        public virtual DbSet<倉庫基本檔> 倉庫基本檔 { get; set; }
        public virtual DbSet<個資名稱> 個資名稱 { get; set; }
        public virtual DbSet<個資名稱異動> 個資名稱異動 { get; set; }
        public virtual DbSet<個資存取記錄> 個資存取記錄 { get; set; }
        public virtual DbSet<個資群組> 個資群組 { get; set; }
        public virtual DbSet<個資群組成員> 個資群組成員 { get; set; }
        public virtual DbSet<個資群組權限> 個資群組權限 { get; set; }
        public virtual DbSet<分部> 分部 { get; set; }
        public virtual DbSet<單位> 單位 { get; set; }
        public virtual DbSet<單位農機> 單位農機 { get; set; }
        public virtual DbSet<單據別> 單據別 { get; set; }
        public virtual DbSet<庫存日檔> 庫存日檔 { get; set; }
        public virtual DbSet<庫存異動狀態> 庫存異動狀態 { get; set; }
        public virtual DbSet<庫存盤點主檔> 庫存盤點主檔 { get; set; }
        public virtual DbSet<庫存盤點明細> 庫存盤點明細 { get; set; }
        public virtual DbSet<數量折扣主檔> 數量折扣主檔 { get; set; }
        public virtual DbSet<數量折扣明細> 數量折扣明細 { get; set; }
        public virtual DbSet<數量折扣明細異動記錄> 數量折扣明細異動記錄 { get; set; }
        public virtual DbSet<災害別> 災害別 { get; set; }
        public virtual DbSet<盤點種類> 盤點種類 { get; set; }
        public virtual DbSet<農機具修理方式> 農機具修理方式 { get; set; }
        public virtual DbSet<農機故障修理記錄> 農機故障修理記錄 { get; set; }
        public virtual DbSet<農機故障部位> 農機故障部位 { get; set; }
        public virtual DbSet<農機機種> 農機機種 { get; set; }
        public virtual DbSet<進銷存組織> 進銷存組織 { get; set; }
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

            modelBuilder.Entity<事業商品檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.商品編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品編號).IsUnicode(false);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品中類)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品區分)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品大類)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品小類)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品條碼).IsUnicode(false);

                entity.Property(e => e.對應FA產品編號).IsUnicode(false);

                entity.Property(e => e.搭贈商品編號).IsUnicode(false);

                entity.Property(e => e.條碼種類)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.生產事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.生產單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.產品保管別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.產品大類)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.產地國家)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.目前供貨廠商).IsUnicode(false);

                entity.Property(e => e.總類).IsUnicode(false);

                entity.Property(e => e.課稅別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.責任類別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.銷售商品單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.銷售標準單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.類別).IsUnicode(false);

                entity.HasOne(d => d.事業Navigation)
                    .WithMany(p => p.事業商品檔)
                    .HasForeignKey(d => d.事業)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_事業商品檔_事業");
            });

            modelBuilder.Entity<修改人>(entity =>
            {
                entity.Property(e => e.修改人1)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<倉庫基本檔>(entity =>
            {
                entity.HasKey(e => new { e.倉庫組織, e.倉庫代號 });

                entity.Property(e => e.倉庫組織).IsUnicode(false);

                entity.Property(e => e.倉庫代號).IsUnicode(false);

                entity.Property(e => e.FA列帳事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FA列帳分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FA列帳單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FA列帳部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.倉庫性質)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.管理人員)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.縣市)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.郵遞區號).IsUnicode(false);

                entity.Property(e => e.鄉鎮)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.倉庫組織Navigation)
                    .WithMany(p => p.倉庫基本檔)
                    .HasForeignKey(d => d.倉庫組織)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_倉庫基本檔_進銷存組織");
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

            modelBuilder.Entity<單據別>(entity =>
            {
                entity.Property(e => e.單據別1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單據別大類)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<庫存日檔>(entity =>
            {
                entity.HasKey(e => new { e.倉庫組織, e.倉庫代號, e.商品編號, e.日期 });

                entity.Property(e => e.倉庫組織).IsUnicode(false);

                entity.Property(e => e.倉庫代號).IsUnicode(false);

                entity.Property(e => e.商品編號).IsUnicode(false);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.倉庫)
                    .WithMany(p => p.庫存日檔)
                    .HasForeignKey(d => new { d.倉庫組織, d.倉庫代號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存日檔_倉庫基本檔");
            });

            modelBuilder.Entity<庫存異動狀態>(entity =>
            {
                entity.Property(e => e.庫存異動狀態1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<庫存盤點主檔>(entity =>
            {
                entity.HasKey(e => new { e.進銷存組織, e.單據別, e.日期, e.流水號 });

                entity.Property(e => e.進銷存組織).IsUnicode(false);

                entity.Property(e => e.單據別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.倉庫代號).IsUnicode(false);

                entity.Property(e => e.庫存異動狀態)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.核准人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.災害別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.盤點人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                //entity.Property(e => e.盤點狀態)
                //    .IsUnicode(false)
                //    .IsFixedLength(true);

                entity.Property(e => e.盤點種類)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.單據別Navigation)
                    .WithMany(p => p.庫存盤點主檔)
                    .HasForeignKey(d => d.單據別)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存盤點主檔_單據別");

                entity.HasOne(d => d.庫存異動狀態Navigation)
                    .WithMany(p => p.庫存盤點主檔)
                    .HasForeignKey(d => d.庫存異動狀態)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存盤點主檔_庫存異動狀態");

                entity.HasOne(d => d.災害別Navigation)
                    .WithMany(p => p.庫存盤點主檔)
                    .HasForeignKey(d => d.災害別)
                    .HasConstraintName("FK_庫存盤點主檔_災害別");

                entity.HasOne(d => d.盤點種類Navigation)
                    .WithMany(p => p.庫存盤點主檔)
                    .HasForeignKey(d => d.盤點種類)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存盤點主檔_盤點種類");

                entity.HasOne(d => d.進銷存組織Navigation)
                    .WithMany(p => p.庫存盤點主檔)
                    .HasForeignKey(d => d.進銷存組織)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存盤點主檔_進銷存組織");

                entity.HasOne(d => d.倉庫基本檔)
                    .WithMany(p => p.庫存盤點主檔)
                    .HasForeignKey(d => new { d.進銷存組織, d.倉庫代號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存盤點主檔_倉庫基本檔");
            });

            modelBuilder.Entity<庫存盤點明細>(entity =>
            {
                entity.HasKey(e => new { e.進銷存組織, e.單據別, e.日期, e.流水號, e.項次 });

                entity.Property(e => e.進銷存組織).IsUnicode(false);

                entity.Property(e => e.單據別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品編號).IsUnicode(false);

                entity.HasOne(d => d.庫存盤點主檔)
                    .WithMany(p => p.庫存盤點明細)
                    .HasForeignKey(d => new { d.進銷存組織, d.單據別, d.日期, d.流水號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_庫存盤點明細_庫存盤點主檔");
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

            modelBuilder.Entity<災害別>(entity =>
            {
                entity.Property(e => e.災害別1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<盤點種類>(entity =>
            {
                entity.Property(e => e.盤點種類1)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.盤盈收付項目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.盤虧收付項目)
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

            modelBuilder.Entity<進銷存組織>(entity =>
            {
                entity.Property(e => e.進銷存組織1).IsUnicode(false);

                entity.Property(e => e.FA列帳分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FA列帳單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.FA列帳部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.傳輸FA事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.傳輸FA單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.全員行銷生產部門代號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.列帳事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.列帳分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.列帳單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.列帳部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.物流商使用者帳號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.物流記錄轉出組織代號).IsUnicode(false);

                entity.Property(e => e.稅籍編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.統一編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.縣市)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.負責人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.郵遞區號).IsUnicode(false);

                entity.Property(e => e.鄉鎮)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.鎖定人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.傳輸FA事業Navigation)
                    .WithMany(p => p.進銷存組織)
                    .HasForeignKey(d => d.傳輸FA事業)
                    .HasConstraintName("FK_進銷存組織_事業(for轉出)");
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
