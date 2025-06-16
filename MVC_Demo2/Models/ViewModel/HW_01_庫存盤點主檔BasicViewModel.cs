using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點主檔BasicViewModel
    {
        public string 進銷存組織 { get; set; }
        public string 單據別 { get; set; }
        public DateTime 日期 { get; set; }
        public decimal 流水號 { get; set; }
        public string 倉庫代號 { get; set; }
        public string 盤點種類 { get; set; }
        public string 災害別 { get; set; }
        public string 盤點人 { get; set; }
        public DateTime 盤點日期 { get; set; }
        public string 備註 { get; set; }
        public string 盤點狀態 { get; set; }
        public string 核准人 { get; set; }
        public DateTime? 核准日期 { get; set; }
        public string 庫存異動狀態 { get; set; }
        public bool 是否註記刪除 { get; set; }
        //public string 修改人 { get; set; }
        //public DateTime 修改日期時間 { get; set; }
        public virtual 倉庫基本檔 倉庫基本檔 { get; set; }
        public virtual 單據別 單據別Navigation { get; set; }
        public virtual 庫存異動狀態 庫存異動狀態Navigation { get; set; }
        public virtual 災害別 災害別Navigation { get; set; }
        public virtual 盤點種類 盤點種類Navigation { get; set; }
        public virtual 進銷存組織 進銷存組織Navigation { get; set; }
        public virtual ICollection<庫存盤點明細> 庫存盤點明細 { get; set; }
    }
}
