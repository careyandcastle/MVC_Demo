using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 系統參數檔
    {
        [Key]
        [StringLength(50)]
        public string 參數編號 { get; set; }
        [Required]
        [StringLength(50)]
        public string 參數型態 { get; set; }
        [Required]
        [StringLength(50)]
        public string 參數值 { get; set; }
        [StringLength(200)]
        public string 參數說明 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
