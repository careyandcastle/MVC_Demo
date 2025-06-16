using System;
using System.ComponentModel.DataAnnotations;
using MVC_Demo2.Attributes;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點明細檔BasicViewModel
    {
        [Key]
        public string 進銷存組織 { get; set; }

        [Key]
        public string 單據別 { get; set; }

        [Key]
        public DateTime 日期 { get; set; }

        [Key]
        public int 流水號 { get; set; }

        [Key]
        public int 項次 { get; set; }

        [CTRequired]
        public string 商品編號 { get; set; }

        public decimal? 庫存數量 { get; set; }
        public decimal? 盤點數量 { get; set; }
    }
}
