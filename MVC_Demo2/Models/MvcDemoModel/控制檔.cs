using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 控制檔
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
        [Required]
        [StringLength(20)]
        public string 單位名稱 { get; set; }
        public int 控制年 { get; set; }
        public int 控制月 { get; set; }
        public int 承租人流水號 { get; set; }
        public int 案號流水號 { get; set; }
        public int 收款記錄流水號 { get; set; }
        public int 調整記錄流水號 { get; set; }
        public int 支出記錄流水號 { get; set; }
        public int 傳票流水號 { get; set; }
        public int 傳票批數 { get; set; }
        [StringLength(2)]
        public string 建物土地選取範圍 { get; set; }
        public int? 租約到期提醒月數 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
