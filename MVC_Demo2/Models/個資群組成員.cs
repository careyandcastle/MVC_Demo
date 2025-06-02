using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 個資群組成員
    {
        [Key]
        [StringLength(50)]
        public string 個資群組名稱 { get; set; }
        [Key]
        [StringLength(5)]
        public string 員工編號 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey(nameof(個資群組名稱))]
        [InverseProperty(nameof(個資群組.個資群組成員))]
        public virtual 個資群組 個資群組名稱Navigation { get; set; }
    }
}
