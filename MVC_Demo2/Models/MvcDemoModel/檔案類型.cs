using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 檔案類型
    {
        [Key]
        [StringLength(10)]
        public string 副檔名 { get; set; }
        [Required]
        [StringLength(400)]
        public string MimeType { get; set; }
        [Required]
        [StringLength(16)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }
    }
}
