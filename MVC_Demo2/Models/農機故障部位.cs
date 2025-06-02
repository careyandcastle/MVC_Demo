using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 農機故障部位
    {
        public 農機故障部位()
        {
            農機故障修理記錄 = new HashSet<農機故障修理記錄>();
        }

        [Key]
        [StringLength(1)]
        public string 故障部位 { get; set; }
        [Required]
        [StringLength(20)]
        public string 故障部位名稱 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("故障部位Navigation")]
        public virtual ICollection<農機故障修理記錄> 農機故障修理記錄 { get; set; }
    }
}
