using Microsoft.EntityFrameworkCore;
using NewsAndMedia.Model.Data;

namespace NewsAndMedia.Infrastructure
{
    public class NewsAndMediaDbContext: DbContext
    {
        public NewsAndMediaDbContext()
        {

        }

        public NewsAndMediaDbContext(DbContextOptions<NewsAndMediaDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Author>(e =>
            {
                e.HasKey(entity => entity.Id);
                
                e.HasIndex(entity => entity.Name)
                .IsUnique();                
            });

            modelBuilder.Entity<Article>(e =>
            {
                e.HasKey(entity => entity.Id);

                e.HasIndex(entity => entity.Title)
                    .IsUnique();

                e.HasMany(entity => entity.Author)
                    .WithMany(entity => entity.Articles);

                e.HasOne(entity => entity.Site)
                    .WithMany(entity => entity.Articles)
                    .HasForeignKey(entity => entity.SiteId );
            });

            modelBuilder.Entity<Site>(e =>
            {
                e.HasKey(entity => entity.Id);
            });

            modelBuilder.Entity<Image>(e =>
            {
                e.HasKey(entity => entity.Id);
                e.HasOne(entity => entity.Author)
                    .WithOne(entity => entity.Image)
                    .HasForeignKey<Author>(entity => entity.ImageId);
            });
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Site> Sites { get; set; }
    }
}
