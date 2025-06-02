using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace MVC_Demo2.Models
{
    public partial class ClassStudents
    {
        [Key]
        [StringLength(10)]
        public string EmployeeNo { get; set; }
        [Required]
        public byte[] Name { get; set; }
        [Required]
        [StringLength(1)]
        public string Gender { get; set; }
    }
}
