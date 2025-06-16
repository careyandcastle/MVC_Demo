using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TscLibCore.Attribute;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點明細檔DisplayViewModel
    {
        [Key]
        [HiddenForView]
        public string 進銷存組織 { get; set; }

        [Key]
        [HiddenForView]
        public string 單據別 { get; set; }

        [Key]
        public DateTime 日期 { get; set; }

        [Key]
        public decimal 流水號 { get; set; }

        [Key]
        public decimal 項次 { get; set; }

        [DisplayName("商品編號")]
        public string 商品編號 { get; set; }

        [DisplayName("商品名稱")]
        public string 商品名稱 { get; set; }

        [DisplayName("商品規格")]
        public string 商品規格 { get; set; }

        [DisplayName("單位")]
        public string 單位 { get; set; }

        [DisplayName("庫存結存量")]
        public decimal? 庫存數量 { get; set; }

        [DisplayName("盤點數量")]
        public decimal? 盤點數量 { get; set; }

        [DisplayName("盤差")]
        public decimal? 盤差 => 盤點數量 - 庫存數量;
    }
}
