using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 水電分表使用度數檔
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
        public int 發生年 { get; set; }
        [Key]
        public int 發生月 { get; set; }
        [Key]
        [StringLength(20)]
        public string 總表號 { get; set; }
        [Key]
        public int 分表號 { get; set; }
        [StringLength(5)]
        public string 案號 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 期初度數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 期末度數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 本期度數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 每度單價 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 發生金額 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 分攤比例 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 被分攤的總金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 本期金額 { get; set; }
        [Required]
        [StringLength(2)]
        public string 計量表種類編號 { get; set; }
        [Required]
        [StringLength(2)]
        public string 分攤方式編號 { get; set; }
        [Required]
        [StringLength(10)]
        public string 資料產生人員 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 資料產生時間 { get; set; }
        public bool 已產生應收帳款 { get; set; }
        [StringLength(10)]
        public string 產生應收帳款人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 產生應收帳款時間 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
