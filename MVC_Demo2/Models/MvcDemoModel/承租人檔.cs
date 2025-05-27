using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models.MvcDemoModel
{
    public partial class 承租人檔
    {
        public 承租人檔()
        {
            租約主檔 = new HashSet<租約主檔>();
        }

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
        public string 承租人編號 { get; set; }
        //[Required]
        public byte[] 承租人 { get; set; }
        //public byte[] 承租人姓名 { get; set; }
        //承租人VM，繼承"承租人檔"
        //新增資料夾ViewModel於Models底下，承租人VM.cs
        //新增類別，稱為 D:\每日資料\20250523_工作日\MVC\MVC_Demo2\MVC_Demo2\Models\ViewModel\承租人VM.cs
        [Required]
        [StringLength(2)]
        public string 身分別編號 { get; set; }
        //[Required]
        public byte[] 統一編號 { get; set; }
        public byte[] 電話 { get; set; }
        public byte[] 行動電話 { get; set; }
        public byte[] 傳真 { get; set; }
        public byte[] eMail { get; set; }
        public byte[] 地址 { get; set; }
        public byte[] 發票寄送地址 { get; set; }
        public byte[] 銀行帳號 { get; set; }
        public byte[] 備註 { get; set; }
        public byte[] 發票載具 { get; set; }
        public bool 刪除註記 { get; set; }
        //[Required]
        [StringLength(10)]
        public string 修改人 { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime 修改時間 { get; set; }

        [ForeignKey(nameof(身分別編號))]
        [InverseProperty(nameof(身分別檔.承租人檔))]
        public virtual 身分別檔 身分別編號Navigation { get; set; }
        [InverseProperty("承租人檔")]
        public virtual ICollection<租約主檔> 租約主檔 { get; set; }
    }
}
