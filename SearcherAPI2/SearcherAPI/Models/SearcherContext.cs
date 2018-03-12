namespace SearcherAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class SearcherContext : DbContext
    {
        public SearcherContext()
            : base("name=DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<SearchWords> SearchWords { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Texts> Texts { get; set; }
        public virtual DbSet<MonitorLog> Logging { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MonitorLog>().HasKey(x => x.Id);

            modelBuilder.Entity<MonitorLog>().Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<SearchWords>().Property(p => p.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<Texts>().Property(p => p.id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            modelBuilder.Entity<MonitorLog>()
                .Property(f => f.BeginTime)
                .HasColumnType("datetime2")
                .HasPrecision(3);

            modelBuilder.Entity<MonitorLog>()
                .Property(f => f.EndTime)
                .HasColumnType("datetime2")
                .HasPrecision(3);

            modelBuilder.Entity<SearchWords>()
                .HasMany(e => e.Texts)
                .WithMany(e => e.SearchWords)
                .Map(m => m.ToTable("SearchWords_Texts"));
        }
    }
}
