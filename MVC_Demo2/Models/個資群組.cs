using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 個資群組
    {
        public 個資群組()
        {
            個資群組成員 = new HashSet<個資群組成員>();
            個資群組權限 = new HashSet<個資群組權限>();
        }

        [Key]
        [StringLength(50)]
        public string 個資群組名稱 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [InverseProperty("個資群組名稱Navigation")]
        public virtual ICollection<個資群組成員> 個資群組成員 { get; set; }
        [InverseProperty("個資群組名稱Navigation")]
        public virtual ICollection<個資群組權限> 個資群組權限 { get; set; }
    }
}
