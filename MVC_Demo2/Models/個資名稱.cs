using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 個資名稱
    {
        public 個資名稱()
        {
            個資群組權限 = new HashSet<個資群組權限>();
        }

        [Key]
        [Column("個資名稱")]
        [StringLength(50)]
        public string 個資名稱1 { get; set; }
        public bool 是否隱藏 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal 前端遮蔽字數 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal 中間遮蔽字數 { get; set; }
        [Column(TypeName = "decimal(3, 0)")]
        public decimal 末端遮蔽字數 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("個資名稱Navigation")]
        public virtual ICollection<個資群組權限> 個資群組權限 { get; set; }
    }
}
