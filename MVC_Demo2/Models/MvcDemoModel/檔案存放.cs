using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 檔案存放
    {
        [Key]
        public int ID { get; set; }
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
        [Required]
        [StringLength(256)]
        public string 檔案名稱 { get; set; }
        [StringLength(2000)]
        public string 檔案說明 { get; set; }
        [Required]
        public byte[] 檔案實體 { get; set; }
        public bool 啟用 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 啟用日期 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 停用日期 { get; set; }
        [StringLength(2)]
        public string 開放範圍 { get; set; }
        [Required]
        [StringLength(16)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
