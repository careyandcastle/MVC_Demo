using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 租約主檔log
    {
        [Key]
        public int SN { get; set; }
        [Required]
        [StringLength(2)]
        public string 事業 { get; set; }
        [Required]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Required]
        [StringLength(2)]
        public string 部門 { get; set; }
        [Required]
        [StringLength(2)]
        public string 分部 { get; set; }
        [Required]
        [StringLength(5)]
        public string 案號 { get; set; }
        [Required]
        [StringLength(50)]
        public string 案名 { get; set; }
        [Required]
        [StringLength(5)]
        public string 承租人編號 { get; set; }
        [Required]
        [StringLength(2)]
        public string 租賃方式編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 租賃用途 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 簽約日期 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 租約起始日期 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 本租期起始日期 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 本租期終止日期 { get; set; }
        public int 租期月數 { get; set; }
        public int 續約次數限制 { get; set; }
        public int 累計租期限制 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 開始計算租金日期 { get; set; }
        public int 計租間隔月數 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 下次計租日期 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 每期租金含稅 { get; set; }
        public int 每期繳款期限日 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 履約保證金 { get; set; }
        [Required]
        [StringLength(2)]
        public string 保證金種類編號 { get; set; }
        [StringLength(20)]
        public string 保證金票證號 { get; set; }
        [StringLength(20)]
        public string 火險保單號碼 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 火險保額 { get; set; }
        [StringLength(20)]
        public string 責任險保單號碼 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 責任險保額 { get; set; }
        [Column(TypeName = "date")]
        public DateTime? 租約終止日期 { get; set; }
        [StringLength(2)]
        public string 租約終止原因編號 { get; set; }
        public int 累計租期月數 { get; set; }
        public int 累計續約次數 { get; set; }
        [StringLength(20)]
        public string 懸記帳分戶 { get; set; }
        [StringLength(50)]
        public string 房屋地址 { get; set; }
        [StringLength(50)]
        public string 房地坐落 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 承租建物總面積 { get; set; }
        [StringLength(200)]
        public string 備註 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
