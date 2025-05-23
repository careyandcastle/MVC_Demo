using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 固定資產主檔
    {
        [Key]
        [StringLength(4)]
        public string 資產科目 { get; set; }
        [Key]
        [StringLength(5)]
        public string 資產編號 { get; set; }
        [Key]
        [StringLength(2)]
        public string 外附件編號 { get; set; }
        [Key]
        [StringLength(2)]
        public string 修配編號 { get; set; }
        [Key]
        public int 起用年 { get; set; }
        [Key]
        public int 起用月 { get; set; }
        [Required]
        [StringLength(2)]
        public string 事業 { get; set; }
        [Required]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Required]
        [StringLength(2)]
        public string 存放部門 { get; set; }
        [Required]
        [StringLength(2)]
        public string 存放分部 { get; set; }
        [Required]
        [StringLength(1)]
        public string 營業別 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 帳面原值 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 每月折舊 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 累計折舊 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 殘值 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 重估增值 { get; set; }
        public int 耐用年限 { get; set; }
        [Required]
        [StringLength(3)]
        public string 歸屬科目 { get; set; }
        public bool 文化資產 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 殘值每月折舊 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 帳面累損_IFRS前 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 帳面累損_IFRS後 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 重估增值_累計減損 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 減損每月折舊_IFRS前 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 減損每月折舊_IFRS後 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 減損累折_IFRS前 { get; set; }
        [Column(TypeName = "decimal(13, 0)")]
        public decimal 減損累折_IFRS後 { get; set; }
        [Required]
        [StringLength(2)]
        public string 不提折舊 { get; set; }
        [Column(TypeName = "decimal(7, 2)")]
        public decimal 數量 { get; set; }
        public int 列帳年 { get; set; }
        public int 列帳月 { get; set; }
        public bool 逾齡代號 { get; set; }
        public string 規範 { get; set; }
        [Required]
        [StringLength(5)]
        public string 資產保管人 { get; set; }
        [Required]
        [StringLength(2)]
        public string 最近一次異動項目 { get; set; }
        [Required]
        [StringLength(1)]
        public string 註銷號 { get; set; }
        [Column(TypeName = "decimal(10, 0)")]
        public decimal 逾齡後續用月數 { get; set; }
        [Column(TypeName = "decimal(10, 0)")]
        public decimal 預估最近一年提列殘值折舊 { get; set; }
        [Column(TypeName = "decimal(10, 0)")]
        public decimal 最近一年已提列殘值折舊 { get; set; }
    }
}
