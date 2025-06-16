using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 災害別
    {
        public 災害別()
        {
            庫存盤點主檔 = new HashSet<庫存盤點主檔>();
        }

        [Key]
        [Column("災害別")]
        [StringLength(2)]
        public string 災害別1 { get; set; }
        [Required]
        [StringLength(20)]
        public string 災害別名稱 { get; set; }
        public bool 是否停用 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("災害別Navigation")]
        public virtual ICollection<庫存盤點主檔> 庫存盤點主檔 { get; set; }
    }
}
