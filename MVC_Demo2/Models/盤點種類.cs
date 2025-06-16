using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 盤點種類
    {
        public 盤點種類()
        {
            庫存盤點主檔 = new HashSet<庫存盤點主檔>();
        }

        [Key]
        [Column("盤點種類")]
        [StringLength(1)]
        public string 盤點種類1 { get; set; }
        [Required]
        [StringLength(20)]
        public string 盤點種類名稱 { get; set; }
        [Required]
        [StringLength(2)]
        public string 盤盈收付項目 { get; set; }
        [Required]
        [StringLength(2)]
        public string 盤虧收付項目 { get; set; }
        public bool 是否災害盤點 { get; set; }
        public bool 是否停用 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("盤點種類Navigation")]
        public virtual ICollection<庫存盤點主檔> 庫存盤點主檔 { get; set; }
    }
}
