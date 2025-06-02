using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 個資名稱異動
    {
        [Key]
        public int Log編號 { get; set; }
        [StringLength(50)]
        public string 原個資名稱 { get; set; }
        public bool? 原是否隱藏 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal? 原前端遮蔽字數 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal? 原中間遮蔽字數 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal? 原末端遮蔽字數 { get; set; }
        [StringLength(5)]
        public string 原修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 原修改日期時間 { get; set; }
        [StringLength(50)]
        public string 新個資名稱 { get; set; }
        public bool? 新是否隱藏 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal? 新前端遮蔽字數 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal? 新中間遮蔽字數 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal? 新末端遮蔽字數 { get; set; }
        [Required]
        [StringLength(5)]
        public string 新修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 新修改日期時間 { get; set; }
    }
}
