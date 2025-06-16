using System;
using System.ComponentModel.DataAnnotations;
using MVC_Demo2.Attributes;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點主檔BasicViewModel
    {
        [Key]
        public string 進銷存組織 { get; set; }

        [CTRequired]
        [CTStringLength(3, MinimumLength = 3)]
        public string 單據別 { get; set; }

        public DateTime 日期 { get; set; }

        public int 流水號 { get; set; }

        [CTRequired]
        public string 倉庫代號 { get; set; }

        [CTRequired]
        public string 盤點種類 { get; set; }

        public string 災害別 { get; set; }

        [CTRequired]
        public string 盤點人 { get; set; }

        public DateTime? 盤點日期 { get; set; }

        public string 備註 { get; set; }
    }
}
