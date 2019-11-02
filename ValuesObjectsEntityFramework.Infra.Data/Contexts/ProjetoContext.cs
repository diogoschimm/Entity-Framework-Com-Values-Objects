using Flunt.Notifications;
using Microsoft.EntityFrameworkCore;
using System;
using ValuesObjectsEntityFramework.Domain.Entidades;
using ValuesObjectsEntityFramework.Infra.Data.Mappings;

namespace ValuesObjectsEntityFramework.Infra.Data
{
    public class ProjetoContext: DbContext
    {
        public ProjetoContext(DbContextOptions<ProjetoContext> options): base(options)
        {
        }

        public DbSet<Cliente> Cliente { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.Ignore<Notification>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
