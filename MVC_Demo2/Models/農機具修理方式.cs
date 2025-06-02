using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 農機具修理方式
    {
        public 農機具修理方式()
        {
            農機故障修理記錄 = new HashSet<農機故障修理記錄>();
        }

        [Key]
        [StringLength(1)]
        public string 修理方式 { get; set; }
        [Required]
        [StringLength(20)]
        public string 修理方式名稱 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("修理方式Navigation")]
        public virtual ICollection<農機故障修理記錄> 農機故障修理記錄 { get; set; }
    }
}
