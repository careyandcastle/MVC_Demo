using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 部門
    {
        [Key]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Key]
        [Column("部門")]
        [StringLength(2)]
        public string 部門1 { get; set; }
        [Required]
        [StringLength(12)]
        public string 部門名稱 { get; set; }
        public bool 組織狀態 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }
    }
}
