using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 商品檔
    {
        public 商品檔()
        {
            租約明細檔 = new HashSet<租約明細檔>();
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
        public string 商品編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 商品名稱 { get; set; }
        [StringLength(4)]
        public string 資產科目 { get; set; }
        [StringLength(5)]
        public string 資產編號 { get; set; }
        [Required]
        [StringLength(2)]
        public string 商品類別編號 { get; set; }
        [Required]
        [StringLength(3)]
        public string 建物編號 { get; set; }
        [StringLength(10)]
        public string 樓層 { get; set; }
        [StringLength(10)]
        public string 區位 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? 面積 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 含稅單價 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? 未稅單價 { get; set; }
        public bool 是否共用 { get; set; }
        [StringLength(5)]
        public string 案號 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [ForeignKey(nameof(商品類別編號))]
        [InverseProperty(nameof(商品類別檔.商品檔))]
        public virtual 商品類別檔 商品類別編號Navigation { get; set; }
        [ForeignKey("事業,單位,部門,分部,建物編號")]
        [InverseProperty("商品檔")]
        public virtual 建物主檔 建物主檔 { get; set; }
        [InverseProperty("商品檔")]
        public virtual ICollection<租約明細檔> 租約明細檔 { get; set; }
    }
}
