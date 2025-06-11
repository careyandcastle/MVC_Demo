using MVC_Demo2.Attributes;
using MVC_Demo2.Models.MvcDemoModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Models.ViewModel
{
    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public class 租約主檔ViewModel : 租約主檔
    {
        public 租約主檔ViewModel() : base()
        {
            租期月數 = 12;
            續約次數限制 = 1;
            計租間隔月數 = 1;
            每期繳款期限日 = 5;
            履約保證金 = 0;
            每期租金含稅 = 0;
        }

        [NotMapped]
        [DisplayName("承租人")]
        [CTStringLength(20)]
        public string 承租人 { get; set; }

        [NotMapped]
        [DisplayName("租賃方式")]
        [StringLength(10)]
        public string 租賃方式 { get; set; }

        [NotMapped]
        [StringLength(10)]
        [DisplayName("保證金種類")]
        public string 保證金種類 { get; set; }

        [NotMapped]
        [StringLength(10)]
        [DisplayName("租約終止原因")]
        public string 租約終止原因 { get; set; }
    }
}