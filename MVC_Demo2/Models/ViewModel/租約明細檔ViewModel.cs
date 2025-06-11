using MVC_Demo2.Attributes;
using MVC_Demo2.Models.MvcDemoModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Models
{
    [NotMapped]
    public class 租約明細檔ViewModel : 租約明細檔
    {
        [NotMapped]
        [DisplayName("類別編號")]
        [CTStringLength(20)]
        public string 商品類別編號 { get; set; }

        [NotMapped]
        [DisplayName("建物編號")]
        [StringLength(20)]
        public string 建物編號 { get; set; }


        [NotMapped]
        [StringLength(20)]
        public string 商品類別 { get; set; }

        [NotMapped]
        [StringLength(20)]
        public string 商品名稱 { get; set; }

        public DateTime 租約起始日期 { get; set; }
        public DateTime? 租約終止日期 { get; set; }
        public string 租賃方式 { get; set; }
        public string 租賃用途 { get; set; }
        public string 案名 { get; set; }

    }
}