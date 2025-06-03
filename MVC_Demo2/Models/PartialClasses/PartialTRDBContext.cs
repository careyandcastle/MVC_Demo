using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.DependencyInjection;
using MVC_Demo2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TscLibCore.BaseObject;

namespace MVC_Demo2.Models
{
    public partial class TRDBContext : BaseDbContext
    {
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            CustomSqlFunctions.Register(modelBuilder);
        }

        
    }
}
