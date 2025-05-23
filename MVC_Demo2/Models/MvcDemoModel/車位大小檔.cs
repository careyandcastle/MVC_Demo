using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    [Keyless]
    public partial class 車位大小檔
    {
        [Required]
        [StringLength(2)]
        public string 車位大小編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 車位大小 { get; set; }
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 修改時間 { get; set; }
    }
}
