using AppConfig;
using Models.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace Infrastructure
{
    public class ExchangeContext : DbContext
    {
        private AppConfiguration _appConfiguration = new AppConfiguration();
        public DbSet<Exchange> Exchanges { get; set; }
        public DbSet<ExchangePair> ExchangePairs { get; set; }
        public DbSet<Currency> Currencies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = _appConfiguration.ConnectionString;
            builder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder.Entity<Currency>(x => {
                x.ToTable("TB_CURRENCIES", "dbo");
                x.HasKey(y => y.Id).HasName("ID");
                x.Property(y => y.Symbol).HasColumnName("SYMBOL");
                x.Property(y => y.Name).HasColumnName("NAME");
                x.Property(y => y.NameId).HasColumnName("NAME_ID");
            }
            );

            builder.Entity<Exchange>(x => {
                    x.ToTable("TB_EXCHANGES", "dbo");
                    x.HasKey(y => y.Id).HasName("ID");
                    x.Property(y => y.Name).HasColumnName("NAME");
                    x.Property(y => y.NameId).HasColumnName("NAME_ID");
                    x.Property(y => y.Url).HasColumnName("URL");
                    x.Property(y => y.VolumeUsd).HasColumnName("VOLUME_USD");
                    x.Property(y => y.ActivePairs).HasColumnName("ACTIVE_PAIRS");
                    x.Property(y => y.Country).HasColumnName("COUNTRY");
                    x.HasMany(y => y.ExchangePairs).WithOne(y => y.Exchange);
                }
            );

            builder.Entity<ExchangePair>(x => {
                    x.ToTable("TB_EXCHANGE_PAIRS");
                    x.HasKey(y => y.Id).HasName("ID");
                    x.Property(y => y.IdExchange).HasColumnName("ID_EXCHANGE");
                    x.Property(y => y.Base).HasColumnName("BASE");
                    x.Property(y => y.Quote).HasColumnName("QUOTE");
                    x.Property(y => y.Volume).HasColumnName("VOLUME");
                    x.Property(y => y.Price).HasColumnName("PRICE");
                    x.Property(y => y.PriceUsd).HasColumnName("PRICE_USD");
                    x.Property(y => y.Time).HasColumnName("TIME");
                    x.HasOne(y => y.Exchange).WithMany(y => y.ExchangePairs).HasForeignKey(y => y.IdExchange).OnDelete(DeleteBehavior.Cascade);
                }
            );
        }
    }
}
