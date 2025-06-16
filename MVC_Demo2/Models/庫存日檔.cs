using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 庫存日檔
    {
        [Key]
        [StringLength(8)]
        public string 倉庫組織 { get; set; }
        [Key]
        [StringLength(8)]
        public string 倉庫代號 { get; set; }
        [Key]
        [StringLength(13)]
        public string 商品編號 { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime 日期 { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal 昨日結存數量 { get; set; }
        [Column(TypeName = "decimal(12, 0)")]
        public decimal 昨日結存未稅金額 { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal 昨日結存寄庫數量 { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal 本日入庫數量 { get; set; }
        [Column(TypeName = "decimal(12, 0)")]
        public decimal 本日入庫未稅金額 { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal 本日出庫數量 { get; set; }
        [Column(TypeName = "decimal(12, 0)")]
        public decimal 本日出庫未稅金額 { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal 本日結存數量 { get; set; }
        [Column(TypeName = "decimal(12, 0)")]
        public decimal 本日結存未稅金額 { get; set; }
        [Column(TypeName = "decimal(13, 4)")]
        public decimal 本日結存寄庫數量 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey("倉庫組織,倉庫代號")]
        [InverseProperty(nameof(倉庫基本檔.庫存日檔))]
        public virtual 倉庫基本檔 倉庫 { get; set; }
    }
}
