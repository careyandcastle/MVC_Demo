using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 單據別
    {
        public 單據別()
        {
            庫存盤點主檔 = new HashSet<庫存盤點主檔>();
        }

        [Key]
        [Column("單據別")]
        [StringLength(3)]
        public string 單據別1 { get; set; }
        [Required]
        [StringLength(20)]
        public string 單據別名稱 { get; set; }
        [Required]
        [StringLength(1)]
        public string 單據別大類 { get; set; }
        [Required]
        [StringLength(2)]
        public string 銷售單據別簡稱 { get; set; }
        public bool 是否物流配送單據別 { get; set; }
        public bool 是否流向追蹤單據別 { get; set; }
        public bool 是否調整額度單據別 { get; set; }
        [Required]
        [StringLength(20)]
        public string 對應資料表 { get; set; }
        public bool 是否停用 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("單據別Navigation")]
        public virtual ICollection<庫存盤點主檔> 庫存盤點主檔 { get; set; }
    }
}
