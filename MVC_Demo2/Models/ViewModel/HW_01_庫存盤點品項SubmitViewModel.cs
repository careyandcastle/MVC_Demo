using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Models.ViewModel
{
    public class HW_01_庫存盤點品項SubmitViewModel
    {
        public string 進銷存組織 { get; set; }
        public string 單據別 { get; set; }
        public DateTime 日期 { get; set; }
        public int 流水號 { get; set; }
        public string 倉庫代號 { get; set; }
        public List<HW_01_庫存盤點品項InputViewModel> 選項清單 { get; set; }
    }
}
