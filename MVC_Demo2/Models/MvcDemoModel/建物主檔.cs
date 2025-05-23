using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 建物主檔
    {
        public 建物主檔()
        {
            商品檔 = new HashSet<商品檔>();
            建物土地檔 = new HashSet<建物土地檔>();
            建物資產編號檔 = new HashSet<建物資產編號檔>();
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
        [StringLength(3)]
        public string 建物編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 建物名稱 { get; set; }
        [StringLength(20)]
        public string 建號 { get; set; }
        [StringLength(50)]
        public string 土地坐落 { get; set; }
        [StringLength(50)]
        public string 地址 { get; set; }
        [Required]
        [StringLength(4)]
        public string 資產科目 { get; set; }
        [Required]
        [StringLength(5)]
        public string 資產編號 { get; set; }
        public int 分號筆數 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 土地面積 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 主建物總面積 { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal 附屬建物總面積 { get; set; }
        public int 地上樓層數 { get; set; }
        public int 地下樓層數 { get; set; }
        public int 汽車停車位數 { get; set; }
        public int 機車停車位數 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [InverseProperty("建物主檔")]
        public virtual ICollection<商品檔> 商品檔 { get; set; }
        [InverseProperty("建物主檔")]
        public virtual ICollection<建物土地檔> 建物土地檔 { get; set; }
        [InverseProperty("建物主檔")]
        public virtual ICollection<建物資產編號檔> 建物資產編號檔 { get; set; }
    }
}
