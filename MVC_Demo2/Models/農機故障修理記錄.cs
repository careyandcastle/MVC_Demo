using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 農機故障修理記錄
    {
        [Key]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Key]
        [StringLength(2)]
        public string 農機機種 { get; set; }
        [Key]
        [StringLength(4)]
        public string 牌照號碼 { get; set; }
        [Key]
        [Column(TypeName = "date")]
        public DateTime 故障日期 { get; set; }
        [Key]
        [StringLength(1)]
        public string 故障部位 { get; set; }
        [Key]
        [Column(TypeName = "decimal(4, 0)")]
        public decimal 流水號 { get; set; }
        [Required]
        [StringLength(1)]
        public string 修理方式 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 修復日期 { get; set; }
        [Column(TypeName = "decimal(8, 0)")]
        public decimal 修理金額 { get; set; }
        [Required]
        [StringLength(50)]
        public string 備註 { get; set; }
        public bool 是否註銷 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey(nameof(修理方式))]
        [InverseProperty(nameof(農機具修理方式.農機故障修理記錄))]
        public virtual 農機具修理方式 修理方式Navigation { get; set; }
        [ForeignKey("單位,農機機種,牌照號碼")]
        [InverseProperty("農機故障修理記錄")]
        public virtual 單位農機 單位農機 { get; set; }
        [ForeignKey(nameof(故障部位))]
        [InverseProperty(nameof(農機故障部位.農機故障修理記錄))]
        public virtual 農機故障部位 故障部位Navigation { get; set; }
    }
}
