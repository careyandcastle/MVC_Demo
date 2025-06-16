using MVC_Demo2.Models;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo2.Models.ViewModel
{
    [NotMapped]
    public class 庫存盤點明細ViewModel : 庫存盤點明細
    {
        public 庫存盤點明細ViewModel() : base()
        {
            // 預設值設定（如有需要）
            盤點數量 = 0;
            庫存數量 = 0;
        }

        [NotMapped]
        [DisplayName("商品名稱")]
        [StringLength(50)]
        public string 商品名稱 { get; set; }

        [NotMapped]
        [DisplayName("單位")]
        [StringLength(10)]
        public string 商品單位 { get; set; }

        [NotMapped]
        [DisplayName("倉庫名稱")]
        [StringLength(50)]
        public string 倉庫名稱 { get; set; }

        [NotMapped]
        [DisplayName("差異數量")]
        public decimal 差異數量
        {
            get { return 盤點數量 - 庫存數量; }
        }

        [NotMapped]
        [DisplayName("備註")]
        [StringLength(100)]
        public string 備註 { get; set; }
    }
}
