using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBcontext : DbContext
    {
        public ApplicationDBcontext (DbContextOptions dbContextOptions):base(dbContextOptions)
        {

        }

        public DbSet<Stock> Stocks { get; set; }       
        public DbSet<Comment> comments { get; set; }
        
    }
}