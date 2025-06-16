using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class 進銷存組織
    {
        public 進銷存組織()
        {
            倉庫基本檔 = new HashSet<倉庫基本檔>();
            庫存盤點主檔 = new HashSet<庫存盤點主檔>();
        }

        [Key]
        [Column("進銷存組織")]
        [StringLength(8)]
        public string 進銷存組織1 { get; set; }
        [Required]
        [StringLength(20)]
        public string 進銷存組織名稱 { get; set; }
        [Required]
        [StringLength(12)]
        public string 進銷存組織簡稱 { get; set; }
        [Required]
        [StringLength(2)]
        public string 列帳事業 { get; set; }
        [Required]
        [StringLength(2)]
        public string 列帳單位 { get; set; }
        [StringLength(2)]
        public string 列帳部門 { get; set; }
        [StringLength(2)]
        public string 列帳分部 { get; set; }
        [StringLength(2)]
        public string FA列帳單位 { get; set; }
        [StringLength(2)]
        public string FA列帳部門 { get; set; }
        [StringLength(2)]
        public string FA列帳分部 { get; set; }
        [StringLength(2)]
        public string 傳輸FA事業 { get; set; }
        [StringLength(2)]
        public string 傳輸FA單位 { get; set; }
        [Required]
        [StringLength(8)]
        public string 物流記錄轉出組織代號 { get; set; }
        [StringLength(1)]
        public string 全員行銷生產部門代號 { get; set; }
        [Column(TypeName = "date")]
        public DateTime 列帳日期 { get; set; }
        [Required]
        [StringLength(5)]
        public string 負責人 { get; set; }
        [Required]
        [StringLength(15)]
        public string 聯絡電話 { get; set; }
        [Required]
        [StringLength(15)]
        public string 服務專線 { get; set; }
        [StringLength(15)]
        public string 傳真號碼 { get; set; }
        [Required]
        [StringLength(6)]
        public string 郵遞區號 { get; set; }
        [Required]
        [StringLength(1)]
        public string 縣市 { get; set; }
        [Required]
        [StringLength(2)]
        public string 鄉鎮 { get; set; }
        [Required]
        [StringLength(30)]
        public string 地址 { get; set; }
        public bool 是否事業總部 { get; set; }
        public bool 是否物流組織 { get; set; }
        [StringLength(5)]
        public string 物流商使用者帳號 { get; set; }
        public bool 是否稅務申報組織 { get; set; }
        public bool 是否船期合約提領組織 { get; set; }
        public bool 是否畜殖飼料工場 { get; set; }
        public bool 是否列印發票 { get; set; }
        public bool 是否列印提貨單正本 { get; set; }
        public bool 是否列印提貨單副本 { get; set; }
        public bool 是否列印提貨單留抵聯 { get; set; }
        public bool 是否列印庫存異動單 { get; set; }
        public bool 是否列印送貨單 { get; set; }
        public bool 是否列印收款單 { get; set; }
        public bool 是否列印出門證 { get; set; }
        public bool 是否鎖定 { get; set; }
        [Required]
        [StringLength(5)]
        public string 鎖定人 { get; set; }
        [Required]
        [StringLength(20)]
        public string 程式編號 { get; set; }
        [StringLength(30)]
        public string 營業人名稱 { get; set; }
        [StringLength(8)]
        public string 統一編號 { get; set; }
        [StringLength(9)]
        public string 稅籍編號 { get; set; }
        public bool 是否總公司配號 { get; set; }
        public bool 組織狀態 { get; set; }
        [Required]
        [StringLength(5)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改日期時間 { get; set; }

        [ForeignKey(nameof(傳輸FA事業))]
        [InverseProperty(nameof(事業.進銷存組織))]
        public virtual 事業 傳輸FA事業Navigation { get; set; }
        [InverseProperty("倉庫組織Navigation")]
        public virtual ICollection<倉庫基本檔> 倉庫基本檔 { get; set; }
        [InverseProperty("進銷存組織Navigation")]
        public virtual ICollection<庫存盤點主檔> 庫存盤點主檔 { get; set; }
    }
}
