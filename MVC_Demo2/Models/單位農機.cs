using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 單位農機
    {
        public 單位農機()
        {
            農機故障修理記錄 = new HashSet<農機故障修理記錄>();
        }

        [Key]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Key]
        [StringLength(2)]
        public string 農機機種 { get; set; }
        [Key]
        [StringLength(4)]
        public string 牌照號碼 { get; set; }
        public bool 是否除役 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("單位農機")]
        public virtual ICollection<農機故障修理記錄> 農機故障修理記錄 { get; set; }
    }
}
