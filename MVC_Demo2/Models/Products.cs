﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    [Index(nameof(Cancelled), Name = "IX_Products")]
    public partial class Products
    {
        public Products()
        {
            BusinessDetail = new HashSet<BusinessDetail>();
            StoreProducts = new HashSet<StoreProducts>();
        }

        [Key]
        [StringLength(2)]
        public string ProductID { get; set; }
        [Required]
        [StringLength(2)]
        public string ProductType { get; set; }
        [Required]
        [StringLength(20)]
        public string ProductName { get; set; }
        [Column(TypeName = "decimal(18, 0)")]
        public decimal Price { get; set; }
        public bool Cancelled { get; set; }
        [Required]
        [StringLength(5)]
        public string UPD_USR { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime UPD_DATE { get; set; }

        [ForeignKey(nameof(ProductType))]
        [InverseProperty(nameof(ProductTypes.Products))]
        public virtual ProductTypes ProductTypeNavigation { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<BusinessDetail> BusinessDetail { get; set; }
        [InverseProperty("Product")]
        public virtual ICollection<StoreProducts> StoreProducts { get; set; }
    }
}
