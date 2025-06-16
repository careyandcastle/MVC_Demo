using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 事業商品檔
    {
        [Key]
        [StringLength(2)]
        public string 事業 { get; set; }
        [Key]
        [StringLength(13)]
        public string 商品編號 { get; set; }
        [Required]
        [StringLength(30)]
        public string 商品名稱 { get; set; }
        [Required]
        [StringLength(10)]
        public string 商品簡稱 { get; set; }
        [Required]
        [StringLength(50)]
        public string 商品規格 { get; set; }
        [StringLength(2)]
        public string 生產事業 { get; set; }
        [StringLength(2)]
        public string 生產單位 { get; set; }
        [Required]
        [StringLength(12)]
        public string 目前供貨廠商 { get; set; }
        [Required]
        [StringLength(1)]
        public string 條碼種類 { get; set; }
        [Required]
        [StringLength(13)]
        public string 商品條碼 { get; set; }
        [StringLength(2)]
        public string 產品大類 { get; set; }
        [Required]
        [StringLength(2)]
        public string 商品大類 { get; set; }
        [Required]
        [StringLength(2)]
        public string 商品中類 { get; set; }
        [Required]
        [StringLength(2)]
        public string 商品小類 { get; set; }
        [Required]
        [StringLength(2)]
        public string 銷售商品單位 { get; set; }
        [Required]
        [StringLength(2)]
        public string 銷售標準單位 { get; set; }
        [StringLength(8)]
        public string 對應FA產品編號 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 標準單位折合率 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 事業回傳FA商品量 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal FA轉換後商品量 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 商品毛重 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 商品淨重 { get; set; }
        [Required]
        [StringLength(1)]
        public string 課稅別 { get; set; }
        [Required]
        [StringLength(1)]
        public string 商品區分 { get; set; }
        [Required]
        [StringLength(2)]
        public string 責任類別 { get; set; }
        [Required]
        [StringLength(2)]
        public string 產品保管別 { get; set; }
        public bool 是否冷凍冷藏商品 { get; set; }
        public bool 是否限總部銷售 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 建議含稅售價 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 未稅售價 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 含稅售價 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 搭贈門檻 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 搭贈數量 { get; set; }
        [StringLength(13)]
        public string 搭贈商品編號 { get; set; }
        [StringLength(12)]
        public string 數量折扣代號 { get; set; }
        [StringLength(2)]
        public string 總類 { get; set; }
        [StringLength(8)]
        public string 類別 { get; set; }
        public bool 是否進口商品 { get; set; }
        [Required]
        [StringLength(2)]
        public string 產地國家 { get; set; }
        public bool 是否串接產品追溯 { get; set; }
        [Required]
        [StringLength(40)]
        public string 產品追溯串接碼 { get; set; }
        public bool 是否暫停銷售 { get; set; }
        public bool 是否停用 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey(nameof(事業))]
        [InverseProperty("事業商品檔")]
        public virtual 事業 事業Navigation { get; set; }
    }
}
