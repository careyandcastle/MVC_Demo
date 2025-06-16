using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 庫存異動狀態
    {
        public 庫存異動狀態()
        {
            庫存盤點主檔 = new HashSet<庫存盤點主檔>();
        }

        [Key]
        [Column("庫存異動狀態")]
        [StringLength(1)]
        public string 庫存異動狀態1 { get; set; }
        [Required]
        [StringLength(20)]
        public string 庫存異動狀態名稱 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("庫存異動狀態Navigation")]
        public virtual ICollection<庫存盤點主檔> 庫存盤點主檔 { get; set; }
    }
}
