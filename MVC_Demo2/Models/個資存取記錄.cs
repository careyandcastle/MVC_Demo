using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 個資存取記錄
    {
        [Key]
        public int Log編號 { get; set; }
        [Required]
        [StringLength(40)]
        public string 來源IP { get; set; }
        [Required]
        [StringLength(5)]
        public string 查詢者員編 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 查詢日期時間 { get; set; }
        [Required]
        [StringLength(40)]
        public string 功能項目名稱 { get; set; }
        [Required]
        public byte[] 查詢結果 { get; set; }
    }
}
