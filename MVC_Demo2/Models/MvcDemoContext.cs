using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MVC_Demo2.Models.MvcDemoModel;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class MvcDemoContext : DbContext
    {
        public MvcDemoContext()
        {
        }

        public MvcDemoContext(DbContextOptions<MvcDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<事業商品類別檔> 事業商品類別檔 { get; set; }
        public virtual DbSet<作業別檔> 作業別檔 { get; set; }
        public virtual DbSet<保證金種類檔> 保證金種類檔 { get; set; }
        public virtual DbSet<保險別檔> 保險別檔 { get; set; }
        public virtual DbSet<商品檔> 商品檔 { get; set; }
        public virtual DbSet<商品類別檔> 商品類別檔 { get; set; }
        public virtual DbSet<固定資產主檔> 固定資產主檔 { get; set; }
        public virtual DbSet<土地資料檔> 土地資料檔 { get; set; }
        public virtual DbSet<建物主檔> 建物主檔 { get; set; }
        public virtual DbSet<建物土地檔> 建物土地檔 { get; set; }
        public virtual DbSet<建物資產編號檔> 建物資產編號檔 { get; set; }
        public virtual DbSet<承租人檔> 承租人檔 { get; set; }
        public virtual DbSet<控制檔> 控制檔 { get; set; }
        public virtual DbSet<收款主檔> 收款主檔 { get; set; }
        public virtual DbSet<收款明細檔> 收款明細檔 { get; set; }
        public virtual DbSet<檔案存放> 檔案存放 { get; set; }
        public virtual DbSet<檔案類型> 檔案類型 { get; set; }
        public virtual DbSet<水電分攤方式檔> 水電分攤方式檔 { get; set; }
        public virtual DbSet<水電分表使用度數檔> 水電分表使用度數檔 { get; set; }
        public virtual DbSet<水電分表檔> 水電分表檔 { get; set; }
        public virtual DbSet<水電總表檔> 水電總表檔 { get; set; }
        public virtual DbSet<水電表單價檔> 水電表單價檔 { get; set; }
        public virtual DbSet<水電費計算檔> 水電費計算檔 { get; set; }
        public virtual DbSet<租約主檔> 租約主檔 { get; set; }
        public virtual DbSet<租約主檔log> 租約主檔log { get; set; }
        public virtual DbSet<租約保險檔> 租約保險檔 { get; set; }
        public virtual DbSet<租約明細檔> 租約明細檔 { get; set; }
        public virtual DbSet<租約水電檔> 租約水電檔 { get; set; }
        public virtual DbSet<租賃住宅資料> 租賃住宅資料 { get; set; }
        public virtual DbSet<租賃方式檔> 租賃方式檔 { get; set; }
        public virtual DbSet<租賃用途檔> 租賃用途檔 { get; set; }
        public virtual DbSet<租金應攤列檔> 租金應攤列檔 { get; set; }
        public virtual DbSet<租金每月攤列檔> 租金每月攤列檔 { get; set; }
        public virtual DbSet<租金計算檔> 租金計算檔 { get; set; }
        public virtual DbSet<稅別檔> 稅別檔 { get; set; }
        public virtual DbSet<系統參數檔> 系統參數檔 { get; set; }
        public virtual DbSet<終止原因檔> 終止原因檔 { get; set; }
        public virtual DbSet<表種類檔> 表種類檔 { get; set; }
        public virtual DbSet<計量表種類檔> 計量表種類檔 { get; set; }
        public virtual DbSet<身分別檔> 身分別檔 { get; set; }
        public virtual DbSet<車位大小檔> 車位大小檔 { get; set; }
        public virtual DbSet<車位種類檔> 車位種類檔 { get; set; }
        public virtual DbSet<車位資料檔> 車位資料檔 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<事業商品類別檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.商品類別編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品類別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人).IsFixedLength(true);

                entity.HasOne(d => d.商品類別編號Navigation)
                    .WithMany(p => p.事業商品類別檔)
                    .HasForeignKey(d => d.商品類別編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_事業商品類別檔_商品類別檔");
            });

            modelBuilder.Entity<作業別檔>(entity =>
            {
                entity.Property(e => e.作業別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<保證金種類檔>(entity =>
            {
                entity.Property(e => e.保證金種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改時間).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<保險別檔>(entity =>
            {
                entity.Property(e => e.保險別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<商品檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.商品編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品類別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.建物編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產科目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.商品類別編號Navigation)
                    .WithMany(p => p.商品檔)
                    .HasForeignKey(d => d.商品類別編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_商品檔_商品類別檔");

                entity.HasOne(d => d.建物主檔)
                    .WithMany(p => p.商品檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.建物編號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_商品檔_建物主檔");
            });

            modelBuilder.Entity<商品類別檔>(entity =>
            {
                entity.Property(e => e.商品類別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.作業別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.稅別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.稅別編號Navigation)
                    .WithMany(p => p.商品類別檔)
                    .HasForeignKey(d => d.稅別編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_商品類別檔_稅別檔");
            });

            modelBuilder.Entity<固定資產主檔>(entity =>
            {
                entity.HasKey(e => new { e.資產科目, e.資產編號, e.外附件編號, e.修配編號, e.起用年, e.起用月 });

                entity.Property(e => e.資產科目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.外附件編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修配編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.不提折舊)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.存放分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.存放部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.最近一次異動項目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.歸屬科目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.營業別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.註銷號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產保管人)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<土地資料檔>(entity =>
            {
                entity.HasKey(e => new { e.地類別, e.縣市, e.鄉鎮, e.段號, e.母號, e.子號, e.分號 });

                entity.Property(e => e.地類別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.縣市)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.鄉鎮)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.段號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.母號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.子號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.使用事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.使用分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.使用單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.使用部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改人)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.土地出租案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.建物編號).IsUnicode(false);

                entity.Property(e => e.物業事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.物業分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.物業單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.物業建物編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.物業部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.用途別)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<建物主檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.建物編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.建物編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.地址).IsFixedLength(true);

                entity.Property(e => e.資產科目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<建物土地檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.建物編號, e.地類別, e.縣市, e.鄉鎮, e.段號, e.母號, e.子號, e.分號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.建物編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.地類別)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.縣市)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.鄉鎮)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.段號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.母號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.子號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.建物主檔)
                    .WithMany(p => p.建物土地檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.建物編號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_建物土地檔_建物主檔");
            });

            modelBuilder.Entity<建物資產編號檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.建物編號, e.資產科目, e.資產編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.建物編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產科目)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資產編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.建物主檔)
                    .WithMany(p => p.建物資產編號檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.建物編號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_建物資產編號檔_建物主檔");
            });

            modelBuilder.Entity<承租人檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.承租人編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.承租人編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改時間).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.身分別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.身分別編號Navigation)
                    .WithMany(p => p.承租人檔)
                    .HasForeignKey(d => d.身分別編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_承租人檔_身分別檔");
            });

            modelBuilder.Entity<控制檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<收款主檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.收款記錄流水號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.發票號碼).IsUnicode(false);
            });

            modelBuilder.Entity<收款明細檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.收款記錄流水號, e.計租年, e.計租月, e.商品編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.收款主檔)
                    .WithMany(p => p.收款明細檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.收款記錄流水號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_收款明細檔_收款主檔");
            });

            modelBuilder.Entity<檔案存放>(entity =>
            {
                entity.HasKey(e => new { e.ID, e.事業, e.單位, e.部門, e.分部 })
                    .HasName("PK_檔案管理檔");

                entity.Property(e => e.ID).ValueGeneratedOnAdd();

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.開放範圍)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('00')")
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<檔案類型>(entity =>
            {
                entity.Property(e => e.副檔名).IsUnicode(false);

                entity.Property(e => e.MimeType).IsUnicode(false);
            });

            modelBuilder.Entity<水電分攤方式檔>(entity =>
            {
                entity.Property(e => e.分攤方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<水電分表使用度數檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.發生年, e.發生月, e.總表號, e.分表號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.總表號).IsUnicode(false);

                entity.Property(e => e.分攤方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.計量表種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.資料產生人員).IsFixedLength(true);
            });

            modelBuilder.Entity<水電分表檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.總表號, e.分表號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.總表號).IsUnicode(false);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.水電總表檔)
                    .WithMany(p => p.水電分表檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.總表號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_水電分表檔_水電總表檔");
            });

            modelBuilder.Entity<水電總表檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.總表號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.總表號).IsUnicode(false);

                entity.Property(e => e.分攤方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.計量表種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.分攤方式編號Navigation)
                    .WithMany(p => p.水電總表檔)
                    .HasForeignKey(d => d.分攤方式編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_水電總表檔_水電分攤方式檔");

                entity.HasOne(d => d.計量表種類編號Navigation)
                    .WithMany(p => p.水電總表檔)
                    .HasForeignKey(d => d.計量表種類編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_水電總表檔_計量表種類檔");
            });

            modelBuilder.Entity<水電表單價檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.計量表種類編號 })
                    .HasName("PK_計量表每度單價檔");

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.計量表種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<水電費計算檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.計租年, e.計租月, e.總表號, e.分表號, e.案號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.總表號).IsUnicode(false);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.作業別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分攤方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.稅別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.計量表種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<租約主檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.案號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.保證金種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.承租人編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.租約終止原因編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.租賃方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.保證金種類編號Navigation)
                    .WithMany(p => p.租約主檔)
                    .HasForeignKey(d => d.保證金種類編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約主檔_保證金種類檔");

                entity.HasOne(d => d.租約終止原因編號Navigation)
                    .WithMany(p => p.租約主檔)
                    .HasForeignKey(d => d.租約終止原因編號)
                    .HasConstraintName("FK_租約主檔_終止原因檔");

                entity.HasOne(d => d.租賃方式編號Navigation)
                    .WithMany(p => p.租約主檔)
                    .HasForeignKey(d => d.租賃方式編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約主檔_租賃方式檔");

                entity.HasOne(d => d.承租人檔)
                    .WithMany(p => p.租約主檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.承租人編號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約主檔_承租人檔");
            });

            modelBuilder.Entity<租約主檔log>(entity =>
            {
                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.保證金種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.承租人編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.租約終止原因編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.租賃方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<租約保險檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.案號, e.保險別編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.保險別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.保險別編號Navigation)
                    .WithMany(p => p.租約保險檔)
                    .HasForeignKey(d => d.保險別編號)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約保險檔_保險別檔");

                entity.HasOne(d => d.租約主檔)
                    .WithMany(p => p.租約保險檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.案號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約保險檔_租約主檔");
            });

            modelBuilder.Entity<租約明細檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.案號, e.商品編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.商品檔)
                    .WithMany(p => p.租約明細檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.商品編號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約明細檔_商品檔");

                entity.HasOne(d => d.租約主檔)
                    .WithMany(p => p.租約明細檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.案號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約明細檔_租約主檔");
            });

            modelBuilder.Entity<租約水電檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.案號, e.總表號, e.分表號 })
                    .HasName("PK_租約水電");

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.總表號).IsUnicode(false);

                entity.Property(e => e.分攤方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.HasOne(d => d.租約主檔)
                    .WithMany(p => p.租約水電檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.案號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約水電_租約主檔");

                entity.HasOne(d => d.水電分表檔)
                    .WithMany(p => p.租約水電檔)
                    .HasForeignKey(d => new { d.事業, d.單位, d.部門, d.分部, d.總表號, d.分表號 })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_租約水電檔_水電分表檔");
            });

            modelBuilder.Entity<租賃住宅資料>(entity =>
            {
                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<租賃方式檔>(entity =>
            {
                entity.Property(e => e.租賃方式編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<租金應攤列檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.計租年, e.計租月, e.案號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<租金每月攤列檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.案號, e.計租年, e.計租月, e.攤列年, e.攤列月 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<租金計算檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.案號, e.計租年, e.計租月, e.商品編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.案號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.作業別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.商品名稱).HasDefaultValueSql("((0))");

                entity.Property(e => e.稅別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<稅別檔>(entity =>
            {
                entity.Property(e => e.稅別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<系統參數檔>(entity =>
            {
                entity.Property(e => e.參數型態).IsUnicode(false);
            });

            modelBuilder.Entity<終止原因檔>(entity =>
            {
                entity.Property(e => e.租約終止原因編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改時間).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<表種類檔>(entity =>
            {
                entity.Property(e => e.種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<計量表種類檔>(entity =>
            {
                entity.HasKey(e => e.計量表種類編號)
                    .HasName("PK_計量表種類");

                entity.Property(e => e.計量表種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<身分別檔>(entity =>
            {
                entity.Property(e => e.身分別編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.修改時間).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<車位大小檔>(entity =>
            {
                entity.Property(e => e.車位大小編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<車位種類檔>(entity =>
            {
                entity.Property(e => e.車位種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<車位資料檔>(entity =>
            {
                entity.HasKey(e => new { e.事業, e.單位, e.部門, e.分部, e.建物編號, e.樓層, e.車位編號 });

                entity.Property(e => e.事業)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.單位)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.部門)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.分部)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.建物編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.車位大小編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);

                entity.Property(e => e.車位種類編號)
                    .IsUnicode(false)
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
