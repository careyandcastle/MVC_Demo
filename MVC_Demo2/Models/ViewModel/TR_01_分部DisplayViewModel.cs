using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TscLibCore.Attribute;

namespace MVC_Demo2.Models.ViewModel
{
    public class TR_01_分部DisplayViewModel
    {
        [Key]
        //[StringLength(2)]
        [HiddenForView]
        public string 單位 { get; set; }
        [DisplayName("單位")]
        //[HiddenForView]
        public string 單位顯示 { get; set; }
        [Key]
        //[StringLength(2)]
        [HiddenForView]
        public string 部門 { get; set; }
        [DisplayName("部門")]
        public string 部門顯示 { get; set; }
        [Key]
        //[Column("分部")]
        //[StringLength(2)]
        [HiddenForView]
        public string 分部 { get; set; }
        //public string 分部代號 { get; set; }
        //public string 分部1 { get; set; }
        //[Required]
        //[StringLength(12)]
        //[DisplayName("分部")]
        [HiddenForView]
        public string 分部名稱 { get; set; }
        [DisplayName("分部")]
        public string 分部顯示 { get; set; }

        [HiddenForView]
        public bool 組織狀態 { get; set; }
        [DisplayName("組織狀態")]
        public string 組織狀態顯示 { get; set; }
        //[Required]
        //[StringLength(5)]
        [HiddenForView]
        public string 修改人 { get; set; }
        [DisplayName("修改人")]
        public string 修改人顯示 { get; set; }
        //[Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }
    }
}
