using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    [Index(nameof(總表號), Name = "UQ__水電總表檔__603291107957753F", IsUnique = true)]
    public partial class 水電總表檔
    {
        public 水電總表檔()
        {
            水電分表檔 = new HashSet<水電分表檔>();
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
        [StringLength(20)]
        public string 總表號 { get; set; }
        [Required]
        [StringLength(2)]
        public string 計量表種類編號 { get; set; }
        [Required]
        [StringLength(2)]
        public string 分攤方式編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 量測對象 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 期末度數 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [ForeignKey(nameof(分攤方式編號))]
        [InverseProperty(nameof(水電分攤方式檔.水電總表檔))]
        public virtual 水電分攤方式檔 分攤方式編號Navigation { get; set; }
        [ForeignKey(nameof(計量表種類編號))]
        [InverseProperty(nameof(計量表種類檔.水電總表檔))]
        public virtual 計量表種類檔 計量表種類編號Navigation { get; set; }
        [InverseProperty("水電總表檔")]
        public virtual ICollection<水電分表檔> 水電分表檔 { get; set; }
    }
}
