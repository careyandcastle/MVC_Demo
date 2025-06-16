using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 庫存盤點明細
    {
        [Key]
        [StringLength(8)]
        public string 進銷存組織 { get; set; }
        [Key]
        [StringLength(3)]
        public string 單據別 { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime 日期 { get; set; }
        [Key]
        [Column(TypeName = "decimal(4, 0)")]
        public decimal 流水號 { get; set; }
        [Key]
        [Column(TypeName = "decimal(4, 0)")]
        public decimal 項次 { get; set; }
        [Required]
        [StringLength(13)]
        public string 商品編號 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 庫存數量 { get; set; }
        [Column(TypeName = "decimal(12, 4)")]
        public decimal 盤點數量 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey("進銷存組織,單據別,日期,流水號")]
        [InverseProperty("庫存盤點明細")]
        public virtual 庫存盤點主檔 庫存盤點主檔 { get; set; }
    }
}
