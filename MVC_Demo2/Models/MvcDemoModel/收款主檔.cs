using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 收款主檔
    {
        public 收款主檔()
        {
            收款明細檔 = new HashSet<收款明細檔>();
        }

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
        public int 收款記錄流水號 { get; set; }
        [Required]
        [StringLength(5)]
        public string 案號 { get; set; }
        [StringLength(50)]
        public string 案名 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 收款日期 { get; set; }
        [Required]
        [StringLength(10)]
        public string 發票號碼 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 發票金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 未稅金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 稅額 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 代製傳票時間 { get; set; }
        public int? 代製傳票批數 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 刪除日期 { get; set; }
        public bool? 是否產生xml { get; set; }
        public bool? 是否作廢 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [InverseProperty("收款主檔")]
        public virtual ICollection<收款明細檔> 收款明細檔 { get; set; }
    }
}
