﻿using Microsoft.EntityFrameworkCore;
using NingaApp.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace NingaApp.Data
{
    public class NingaContext : DbContext
    {
        public DbSet<Ninga> Ningas { get; set; }
        public DbSet<Quote> Quotes { get; set; }
        public DbSet<Battle> Battles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Server=DESKTOP-B1VG1L9\\SQLEXPRESS;Database=NingaDB;Trusted_Connection=True;");
        }

        // Configure bridge entity -NingaBattle- relationship 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NingaBattle>()
                .HasKey(s => new { s.NingaId, s.BattleId });
        }
    }
}
