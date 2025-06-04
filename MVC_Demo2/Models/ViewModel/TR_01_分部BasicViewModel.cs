using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TscLibCore.Attribute;

namespace MVC_Demo2.Models.ViewModel
{
    public class TR_01_分部BasicViewModel
    {
        [Key]
        //[StringLength(2)]
        [Required]
        //[HiddenForView]
        public string 單位 { get; set; }
        [Key]
        //[StringLength(2)]
        [Required]
        //[HiddenForView]
        public string 部門 { get; set; }
        [Key]
        //[Column("分部")]
        //[StringLength(2)]
        [Required]
        [HiddenForView]
        public string 分部 { get; set; }
        //public string 分部1 { get; set; }
        //[Required]
        //[StringLength(12)]
        [Required]
        [DisplayName("分部名稱")]
        public string 分部名稱 { get; set; }
        [HiddenForView]
        public bool 組織狀態 { get; set; }
        //[Required]
        //[StringLength(5)]
        //public string 修改人 { get; set; }
        //[Column(TypeName = "datetime")]
        //public DateTime 修改日期時間 { get; set; }
    }
}
