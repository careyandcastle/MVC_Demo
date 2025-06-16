using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 倉庫基本檔
    {
        public 倉庫基本檔()
        {
            庫存日檔 = new HashSet<庫存日檔>();
            庫存盤點主檔 = new HashSet<庫存盤點主檔>();
        }

        [Key]
        [StringLength(8)]
        public string 倉庫組織 { get; set; }
        [Key]
        [StringLength(8)]
        public string 倉庫代號 { get; set; }
        [Required]
        [StringLength(30)]
        public string 倉庫名稱 { get; set; }
        [Required]
        [StringLength(12)]
        public string 倉庫簡稱 { get; set; }
        [Required]
        [StringLength(2)]
        public string FA列帳事業 { get; set; }
        [Required]
        [StringLength(2)]
        public string FA列帳單位 { get; set; }
        [StringLength(2)]
        public string FA列帳部門 { get; set; }
        [StringLength(2)]
        public string FA列帳分部 { get; set; }
        [Required]
        [StringLength(2)]
        public string 倉庫性質 { get; set; }
        public bool 是否急單配送庫 { get; set; }
        [StringLength(5)]
        public string 管理人員 { get; set; }
        [Required]
        public byte[] 聯絡電話 { get; set; }
        [Required]
        [StringLength(6)]
        public string 郵遞區號 { get; set; }
        [StringLength(1)]
        public string 縣市 { get; set; }
        [StringLength(2)]
        public string 鄉鎮 { get; set; }
        [Required]
        [StringLength(60)]
        public string 地址 { get; set; }
        public bool 是否允許負庫存銷售 { get; set; }
        public bool 是否允許負庫存出貨 { get; set; }
        public bool 是否暫停入庫 { get; set; }
        public bool 是否暫停出庫 { get; set; }
        public bool 是否裁撤 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey(nameof(倉庫組織))]
        [InverseProperty(nameof(進銷存組織.倉庫基本檔))]
        public virtual 進銷存組織 倉庫組織Navigation { get; set; }
        [InverseProperty("倉庫")]
        public virtual ICollection<庫存日檔> 庫存日檔 { get; set; }
        [InverseProperty("倉庫基本檔")]
        public virtual ICollection<庫存盤點主檔> 庫存盤點主檔 { get; set; }
    }
}
