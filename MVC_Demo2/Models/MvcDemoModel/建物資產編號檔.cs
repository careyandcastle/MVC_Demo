using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 建物資產編號檔
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
        [StringLength(3)]
        public string 建物編號 { get; set; }
        [Key]
        [StringLength(4)]
        public string 資產科目 { get; set; }
        [Key]
        [StringLength(5)]
        public string 資產編號 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [ForeignKey("事業,單位,部門,分部,建物編號")]
        [InverseProperty("建物資產編號檔")]
        public virtual 建物主檔 建物主檔 { get; set; }
    }
}
