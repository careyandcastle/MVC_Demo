using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 建物土地檔
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
        [StringLength(3)]
        public string 建物編號 { get; set; }
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
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 面積 { get; set; }
        [StringLength(20)]
        public string 使用分區 { get; set; }
        [StringLength(20)]
        public string 使用地類 { get; set; }
        public int? 申報地價年 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? 申報地價 { get; set; }
        [StringLength(20)]
        public string 縣市名稱 { get; set; }
        [StringLength(20)]
        public string 鄉鎮名稱 { get; set; }
        [StringLength(20)]
        public string 段號名稱 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [ForeignKey("事業,單位,部門,分部,建物編號")]
        [InverseProperty("建物土地檔")]
        public virtual 建物主檔 建物主檔 { get; set; }
    }
}
