using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 土地資料檔
    {
        [Key]
        [StringLength(1)]
        public string 地類別 { get; set; }
        [Key]
        [StringLength(1)]
        public string 縣市 { get; set; }
        [Key]
        [StringLength(2)]
        public string 鄉鎮 { get; set; }
        [Key]
        [StringLength(4)]
        public string 段號 { get; set; }
        [Key]
        [StringLength(4)]
        public string 母號 { get; set; }
        [Key]
        [StringLength(4)]
        public string 子號 { get; set; }
        [Key]
        [StringLength(3)]
        public string 分號 { get; set; }
        [Column(TypeName = "decimal(9, 2)")]
        public decimal 面積 { get; set; }
        [Required]
        [StringLength(2)]
        public string 使用事業 { get; set; }
        [Required]
        [StringLength(2)]
        public string 使用單位 { get; set; }
        [Required]
        [StringLength(2)]
        public string 使用部門 { get; set; }
        [Required]
        [StringLength(2)]
        public string 使用分部 { get; set; }
        [StringLength(10)]
        public string 土地出租案號 { get; set; }
        public int? 申報地價年 { get; set; }
        [Column(TypeName = "decimal(8, 1)")]
        public decimal 申報地價 { get; set; }
        [Required]
        [StringLength(3)]
        public string 用途別 { get; set; }
        [Required]
        [StringLength(30)]
        public string 用途別名稱 { get; set; }
        [Required]
        [StringLength(10)]
        public string 使用分區 { get; set; }
        [Required]
        [StringLength(10)]
        public string 使用地類 { get; set; }
        [Required]
        [StringLength(1)]
        public string 建物編號 { get; set; }
        [Required]
        [StringLength(10)]
        public string 縣市名稱 { get; set; }
        [Required]
        [StringLength(10)]
        public string 鄉鎮名稱 { get; set; }
        [Required]
        [StringLength(26)]
        public string 段號名稱 { get; set; }
        [StringLength(2)]
        public string 物業事業 { get; set; }
        [StringLength(2)]
        public string 物業單位 { get; set; }
        [StringLength(2)]
        public string 物業部門 { get; set; }
        [StringLength(2)]
        public string 物業分部 { get; set; }
        [StringLength(3)]
        public string 物業建物編號 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
