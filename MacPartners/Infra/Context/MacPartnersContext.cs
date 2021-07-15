using Flunt.Notifications;
using MacPartners.Domain.Models;
using MacPartners.Domain.Models.Entities;
using MacPartners.Domain.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MacPartners.Infra.Context
{
    public class MacPartnersContext : DbContext
    {
        public MacPartnersContext(DbContextOptions<MacPartnersContext> options) : base(options)
        {
        }

        public DbSet<Partner> Partners { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Notification>();
        }
    }
}
