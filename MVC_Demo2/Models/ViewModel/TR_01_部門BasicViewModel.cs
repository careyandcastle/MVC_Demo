﻿using MVC_Demo2.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Demo2.Models.ViewModel
{
    public class TR_01_部門BasicViewModel
    {

        //簡單來說，就是將部門.cs的內容貼過來，並且拿掉attribute(包含stringLength、column、required)，再拿掉修改人與修改日期

        [Key]
        //[StringLength(2)]
        public string 單位 { get; set; }
        [Key]
        //[Column("部門")]
        //[StringLength(2)]
        [CTRequired]
        [CTStringLength(2 ,MinimumLength = 2)]
        public string 部門 { get; set; }
        //public string 部門1 { get; set; }  //部門1要拿掉，要與部門做區分
        //部門.cs也有部門，MVC為了不混淆，將此處的"部門"改成"部門1"

        //[Required]
        //[StringLength(12)]
        public string 部門名稱 { get; set; }
        public bool 組織狀態 { get; set; }

        //[Required]
        //[StringLength(5)]
        //public string 修改人 { get; set; }

        //[Column(TypeName = "datetime")]
        //public DateTime 修改日期時間 { get; set; }

        //修改人與修改日期都拿掉(因為user不應該碰到他們)
    }
}
