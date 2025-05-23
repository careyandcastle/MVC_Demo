using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 租金應攤列檔
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
        [Column(TypeName = "date")]
        public DateTime 本租期起始日期 { get; set; }
        public int 租期月數 { get; set; }
        public int 計租間隔月數 { get; set; }
        public int 本年期月數 { get; set; }
        public int 下年期月數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 本年期應攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 本年期己攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 本年期未攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 下年期應攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 下年期己攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 下年期未攤列金額 { get; set; }
        public int 應攤列月數 { get; set; }
        public int 未攤列月數 { get; set; }
        public int? 傳票流水號 { get; set; }
        public int? 傳票批數 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 傳票日期 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
