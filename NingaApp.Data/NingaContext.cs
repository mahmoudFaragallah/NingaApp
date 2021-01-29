using Microsoft.EntityFrameworkCore;
using NingaApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NingaApp.Data
{
    public class NingaContext : DbContext
    {
        public NingaContext(DbContextOptions<NingaContext> options) 
            : base(options)
        {

        }
        public DbSet<Ninga> Ningas { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }
    }
}
