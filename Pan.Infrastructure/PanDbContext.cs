using Microsoft.EntityFrameworkCore;
using Pan.Core;

namespace Pan.Infrastructure
{
    public class PanDbContext : DbContext
    {
        public PanDbContext(DbContextOptions<PanDbContext> options) : base(options)
        {
        }

        public DbSet<Word> Words => Set<Word>();
        public DbSet<WordForm> WordForms => Set<WordForm>();
        public DbSet<Text> Texts => Set<Text>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Unicode support explicitly
            modelBuilder.Entity<Word>(entity =>
            {
                entity.Property(w => w.GreekLemma)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(true);

                entity.Property(w => w.DefinitionEn)
                    .IsUnicode(true);

                entity.Property(w => w.Notes)
                    .IsUnicode(true);

                entity.HasMany(w => w.WordForms)
                    .WithOne(wf => wf.Word)
                    .HasForeignKey(wf => wf.WordId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<WordForm>(entity =>
            {
                entity.Property(wf => wf.Form)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(true);

                entity.Property(wf => wf.Morphology)
                    .HasMaxLength(200)
                    .IsUnicode(true);

                // Add index for fast lookups
                entity.HasIndex(wf => wf.Form);
            });

            modelBuilder.Entity<Text>(entity =>
            {
                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(200)
                    .IsUnicode(true);

                entity.Property(t => t.GreekContent)
                    .IsRequired()
                    .IsUnicode(true);

                entity.Property(t => t.EnglishTranslation)
                    .IsUnicode(true);
            });
        }
    }
}
