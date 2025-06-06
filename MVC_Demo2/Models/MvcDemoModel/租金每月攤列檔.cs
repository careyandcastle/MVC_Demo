﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 租金每月攤列檔
    {
        [Key]
        [StringLength(2)]
        public string 事業 { get; set; }
        [Key]
        [StringLength(2)]
        public string 單位 { get; set; }
        [Key]
        [StringLength(2)]
        public string 部門 { get; set; }
        [Key]
        [StringLength(2)]
        public string 分部 { get; set; }
        [Key]
        [StringLength(5)]
        public string 案號 { get; set; }
        [Key]
        public int 計租年 { get; set; }
        [Key]
        public int 計租月 { get; set; }
        [Key]
        public int 攤列年 { get; set; }
        [Key]
        public int 攤列月 { get; set; }
        public int 攤列前期尚未攤列月數 { get; set; }
        public int 攤列後尚未攤列月數 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 攤列前尚未攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 本月攤列金額 { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal 攤列後尚未攤列金額 { get; set; }
        public int? 傳票流水號 { get; set; }
        public int? 傳票批數 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 傳票日期 { get; set; }
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? 修改時間 { get; set; }
    }
}
