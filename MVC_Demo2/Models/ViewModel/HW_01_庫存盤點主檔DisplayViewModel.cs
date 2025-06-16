using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using TscLibCore.Attribute;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點主檔DisplayViewModel
    {
        [Key]
        [HiddenForView]
        public string 進銷存組織 { get; set; }

        [Key]
        [HiddenForView]
        public string 單據別名稱 { get; set; }

        [Key]
        public DateTime 日期 { get; set; }

        [Key]
        public decimal 流水號 { get; set; } 

        [DisplayName("倉庫")]
        public string 倉庫代號 { get; set; }

        public string 倉庫名稱 { get; set; }

        [DisplayName("盤點種類")]
        public string 盤點種類 { get; set; }

        public string 盤點種類名稱 { get; set; }

        [DisplayName("災害別")]
        public string 災害別 { get; set; }

        public string 災害別名稱 { get; set; }

        [DisplayName("盤點人")]
        public string 盤點人 { get; set; }

        public string 盤點人姓名 { get; set; }

        [DisplayName("盤點日期")]
        public DateTime? 盤點日期 { get; set; }

        [DisplayName("備註")]
        public string 備註 { get; set; }

        [HiddenForView]
        public string 庫存異動狀態 { get; set; }

        [DisplayName("庫存異動狀態")]
        public string 庫存異動狀態名稱 { get; set; }

        [HiddenForView]
        public bool 是否註記刪除 { get; set; }

        [DisplayName("是否註記刪除")]
        public string 是否註記刪除顯示 { get; set; }

        [DisplayName("修改人")]
        public string 修改人 { get; set; }

        [DisplayName("修改時間")]
        public DateTime? 修改時間 { get; set; }
    }
}
