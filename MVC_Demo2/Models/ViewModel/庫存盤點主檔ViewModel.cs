using MVC_Demo2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MVC_Demo2.Models.ViewModel
{
    [NotMapped]
    public class 庫存盤點主檔ViewModel : 庫存盤點主檔
    {
        public 庫存盤點主檔ViewModel() : base()
        {
            // 預設值（視系統需求可調整）
            盤點狀態 = "N";         // 尚未完成
            庫存異動狀態 = "0";     // 尚未異動
            是否註記刪除 = false;
            修改日期時間 = DateTime.Now;
        }

        // 畫面顯示名稱欄位（以下是示意，你可依系統實際需求調整）

        [NotMapped]
        [DisplayName("倉庫名稱")]
        [StringLength(50)]
        public string 倉庫名稱 { get; set; }

        [NotMapped]
        [DisplayName("盤點種類名稱")]
        [StringLength(20)]
        public string 盤點種類名稱 { get; set; }

        [NotMapped]
        [DisplayName("災害別名稱")]
        [StringLength(20)]
        public string 災害別名稱 { get; set; }

        [NotMapped]
        [DisplayName("進銷存組織名稱")]
        [StringLength(50)]
        public string 進銷存組織名稱 { get; set; }

        [NotMapped]
        [DisplayName("單據別名稱")]
        [StringLength(20)]
        public string 單據別名稱 { get; set; }

        [NotMapped]
        [DisplayName("核准人姓名")]
        [StringLength(20)]
        public string 核准人姓名 { get; set; }

        [NotMapped]
        [DisplayName("盤點人姓名")]
        [StringLength(20)]
        public string 盤點人姓名 { get; set; }
    }
}
