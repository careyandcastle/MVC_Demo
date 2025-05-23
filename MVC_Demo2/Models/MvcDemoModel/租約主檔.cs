using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 租約主檔
    {
        public 租約主檔()
        {
            租約保險檔 = new HashSet<租約保險檔>();
            租約明細檔 = new HashSet<租約明細檔>();
            租約水電檔 = new HashSet<租約水電檔>();
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
        [StringLength(5)]
        public string 案號 { get; set; }
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
        [Column(TypeName = "date")]
        public DateTime? 租約終止日期 { get; set; }
        [StringLength(2)]
        public string 租約終止原因編號 { get; set; }
        public int 累計租期月數 { get; set; }
        public int 累計續約次數 { get; set; }
        [StringLength(20)]
        public string 懸記帳分戶 { get; set; }
        [StringLength(200)]
        public string 備註 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 契約年租金含稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 契約年租金未稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 浮動年租金未稅 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 固定年租金未稅 { get; set; }
        public int 本年期月數 { get; set; }
        public int 下年期月數 { get; set; }
        [StringLength(50)]
        public string 房屋地址 { get; set; }
        [StringLength(50)]
        public string 房地坐落 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 承租建物總面積 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 承租土地總面積 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [ForeignKey(nameof(保證金種類編號))]
        [InverseProperty(nameof(保證金種類檔.租約主檔))]
        public virtual 保證金種類檔 保證金種類編號Navigation { get; set; }
        [ForeignKey("事業,單位,部門,分部,承租人編號")]
        [InverseProperty("租約主檔")]
        public virtual 承租人檔 承租人檔 { get; set; }
        [ForeignKey(nameof(租約終止原因編號))]
        [InverseProperty(nameof(終止原因檔.租約主檔))]
        public virtual 終止原因檔 租約終止原因編號Navigation { get; set; }
        [ForeignKey(nameof(租賃方式編號))]
        [InverseProperty(nameof(租賃方式檔.租約主檔))]
        public virtual 租賃方式檔 租賃方式編號Navigation { get; set; }
        [InverseProperty("租約主檔")]
        public virtual ICollection<租約保險檔> 租約保險檔 { get; set; }
        [InverseProperty("租約主檔")]
        public virtual ICollection<租約明細檔> 租約明細檔 { get; set; }
        [InverseProperty("租約主檔")]
        public virtual ICollection<租約水電檔> 租約水電檔 { get; set; }
    }
}
