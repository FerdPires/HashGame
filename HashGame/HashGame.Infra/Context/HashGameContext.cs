using HashGame.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HashGame.Infra.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Game> Games { get; set; }
        public DbSet<Movements> Movements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>().ToTable("Games");
            modelBuilder.Entity<Game>().HasKey(x => x.Id);
            modelBuilder.Entity<Game>().Property(x => x.first_player).IsRequired();
            modelBuilder.Entity<Game>().Property(x => x.next_player).IsRequired();
            modelBuilder.Entity<Game>().Property(x => x.status_game).IsRequired();
            modelBuilder.Entity<Game>().Property(x => x.winner_game);

            modelBuilder.Entity<Movements>().ToTable("Movements");
            modelBuilder.Entity<Movements>().HasKey(x => x.Id);
            modelBuilder.Entity<Movements>().Property(x => x.id_game).IsRequired();
            modelBuilder.Entity<Movements>().Property(x => x.player).IsRequired();
            modelBuilder.Entity<Movements>().Property(x => x.pos_x).IsRequired();
            modelBuilder.Entity<Movements>().Property(x => x.pos_y).IsRequired();
        }

    }
}