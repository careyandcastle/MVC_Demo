using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 車位資料檔
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
        public int 樓層 { get; set; }
        [Key]
        public int 車位編號 { get; set; }
        [StringLength(2)]
        public string 車位大小編號 { get; set; }
        [StringLength(2)]
        public string 車位種類編號 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal? 每月租金 { get; set; }
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 修改時間 { get; set; }
    }
}
