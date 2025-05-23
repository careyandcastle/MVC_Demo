using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 保險別檔
    {
        public 保險別檔()
        {
            租約保險檔 = new HashSet<租約保險檔>();
        }

        [Key]
        [StringLength(2)]
        public string 保險別編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 保險別 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [InverseProperty("保險別編號Navigation")]
        public virtual ICollection<租約保險檔> 租約保險檔 { get; set; }
    }
}
