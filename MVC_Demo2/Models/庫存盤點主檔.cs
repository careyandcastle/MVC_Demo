using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 庫存盤點主檔
    {
        public 庫存盤點主檔()
        {
            庫存盤點明細 = new HashSet<庫存盤點明細>();
        }

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
        [Required]
        [StringLength(8)]
        public string 倉庫代號 { get; set; }
        [Required]
        [StringLength(1)]
        public string 盤點種類 { get; set; }
        [StringLength(2)]
        public string 災害別 { get; set; }
        [Required]
        [StringLength(5)]
        public string 盤點人 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 盤點日期 { get; set; }
        [Required]
        [StringLength(50)]
        public string 備註 { get; set; }
        //[Required]
        //[StringLength(1)]
        //public string 盤點狀態 { get; set; }
        [StringLength(5)]
        public string 核准人 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 核准日期 { get; set; }
        [Required]
        [StringLength(1)]
        public string 庫存異動狀態 { get; set; }
        public bool 是否註記刪除 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey("進銷存組織,倉庫代號")]
        [InverseProperty("庫存盤點主檔")]
        public virtual 倉庫基本檔 倉庫基本檔 { get; set; }
        [ForeignKey(nameof(單據別))]
        [InverseProperty("庫存盤點主檔")]
        public virtual 單據別 單據別Navigation { get; set; }
        [ForeignKey(nameof(庫存異動狀態))]
        [InverseProperty("庫存盤點主檔")]
        public virtual 庫存異動狀態 庫存異動狀態Navigation { get; set; }
        [ForeignKey(nameof(災害別))]
        [InverseProperty("庫存盤點主檔")]
        public virtual 災害別 災害別Navigation { get; set; }
        [ForeignKey(nameof(盤點種類))]
        [InverseProperty("庫存盤點主檔")]
        public virtual 盤點種類 盤點種類Navigation { get; set; }
        [ForeignKey(nameof(進銷存組織))]
        [InverseProperty("庫存盤點主檔")]
        public virtual 進銷存組織 進銷存組織Navigation { get; set; }
        [InverseProperty("庫存盤點主檔")]
        public virtual ICollection<庫存盤點明細> 庫存盤點明細 { get; set; }
    }
}
