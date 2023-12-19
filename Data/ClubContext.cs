using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto_ClubDeportes.Models;

    public class ClubContext : IdentityDbContext
    {
        public ClubContext (DbContextOptions<ClubContext> options)
            : base(options)
        {
        }
        public DbSet<Proyecto_ClubDeportes.Models.Partner> Partner { get; set; } = default!;

        public DbSet<Proyecto_ClubDeportes.Models.Sport> Sport { get; set; } = default!;

        public DbSet<Proyecto_ClubDeportes.Models.IncomeRecord> IncomeRecord { get; set; } = default!;
        
        public DbSet<Proyecto_ClubDeportes.Models.Membership> Membership { get; set; } = default!;
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Partner>()
                .HasMany(p=> p.Sports)
                .WithMany(p=> p.Partners)
                .UsingEntity("Practice");

            modelBuilder.Entity<Partner>()
                .HasMany(p => p.IncomeRecords)
                .WithOne(i => i.Partner)
                .HasForeignKey(i => i.PartnerId);

            modelBuilder.Entity<IncomeRecord>()
                .HasMany(i => i.Sports)
                .WithMany(s => s.IncomeRecords)
                .UsingEntity("RecordSport");

            base.OnModelCreating(modelBuilder);
        } 
    }
