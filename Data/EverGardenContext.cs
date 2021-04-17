using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EverGarden.Models;

namespace EverGarden.Data
{
    public class EverGardenContext : DbContext
    {
        public EverGardenContext(DbContextOptions<EverGardenContext> options) : base(options)
        { }

        public DbSet<Plant> Plant { get; set; } 
    }
}
