using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 農機機種
    {
        [Key]
        [Column("農機機種")]
        [StringLength(2)]
        public string 農機機種1 { get; set; }
        [Required]
        [StringLength(20)]
        public string 農機機種名稱 { get; set; }
        public bool 是否甘蔗採收機 { get; set; }
        public bool 是否除役 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }
    }
}
