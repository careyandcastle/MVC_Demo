using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 水電分攤方式檔
    {
        public 水電分攤方式檔()
        {
            水電總表檔 = new HashSet<水電總表檔>();
        }

        [Key]
        [StringLength(2)]
        public string 分攤方式編號 { get; set; }
        [Required]
        [StringLength(20)]
        public string 分攤方式 { get; set; }
        [Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [InverseProperty("分攤方式編號Navigation")]
        public virtual ICollection<水電總表檔> 水電總表檔 { get; set; }
    }
}
