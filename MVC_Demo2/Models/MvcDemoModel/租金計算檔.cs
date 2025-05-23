using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 租金計算檔
    {
        [Key]
        [StringLength(2)]
        public string 事業 { get; set; }
        [Key]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Key]
        [StringLength(2)]
        public string 部門 { get; set; }
        [Key]
        [StringLength(2)]
        public string 分部 { get; set; }
        [Key]
        public int 計租年 { get; set; }
        [Key]
        public int 計租月 { get; set; }
        [Key]
        [StringLength(5)]
        public string 案號 { get; set; }
        [Key]
        [StringLength(5)]
        public string 商品編號 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 單價 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 數量 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 金額 { get; set; }
        [Column(TypeName = "decimal(3, 2)")]
        public decimal 折數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 調整金額 { get; set; }
        [StringLength(100)]
        public string 調整說明 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 計算金額 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 租金計算起始日期 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 租金計算終止日期 { get; set; }
        public int? 計租間隔月數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 應收含稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 應收未稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 實收含稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 實收未稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 尚欠含稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 尚欠未稅 { get; set; }
        public bool 已產生應收帳款 { get; set; }
        [StringLength(10)]
        public string 產生應收帳款人員 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 產生應收帳款時間 { get; set; }
        [Required]
        [StringLength(20)]
        public string 商品名稱 { get; set; }
        [Required]
        [StringLength(2)]
        public string 稅別編號 { get; set; }
        [Required]
        [StringLength(2)]
        public string 作業別編號 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 調整應收含稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 調整應收未稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 調整實收含稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 調整實收未稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? 調整應收尾差 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? 調整實收尾差 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
