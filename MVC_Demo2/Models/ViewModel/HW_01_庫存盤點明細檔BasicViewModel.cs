using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點明細檔BasicViewModel
    {
        public string 進銷存組織 { get; set; }
        public string 單據別 { get; set; }
        public DateTime 日期 { get; set; }
        public decimal 流水號 { get; set; }
        public decimal 項次 { get; set; }
        public string 商品編號 { get; set; }
        public decimal 庫存數量 { get; set; }
        public decimal 盤點數量 { get; set; }
        //public string 修改人 { get; set; }
        //public DateTime 修改日期時間 { get; set; }
        public virtual 庫存盤點主檔 庫存盤點主檔 { get; set; }
    }
}
