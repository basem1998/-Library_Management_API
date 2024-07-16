using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Models
{
    public class LibraryDb:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Patron> Patrons { get; set; }

        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }
        public LibraryDb(DbContextOptions<LibraryDb> options):base(options) 
        {
           
        }
        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Book>(entity =>
            {
                entity.HasKey(b => b.ID);
                entity.Property(b => b.Title).IsRequired().HasMaxLength(255);
                entity.Property(b => b.Author).IsRequired().HasMaxLength(255);
                entity.Property(b => b.PublicationYear)
                    .IsRequired()
                    .HasDefaultValue(0);
                entity.Property(b => b.ISBN)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false);
            });

           
            modelBuilder.Entity<Patron>(entity =>
            {
                entity.HasKey(p => p.ID);
                entity.Property(p => p.Name).IsRequired().HasMaxLength(255);
                entity.Property(p => p.ContactInformation).IsRequired().HasMaxLength(255);
            });

            
            modelBuilder.Entity<BorrowingRecord>(entity =>
            {
                entity.HasKey(br => br.ID);
                entity.Property(br => br.BorrowingDate).IsRequired();
                entity.HasOne(br => br.Book)
                      .WithMany()
                      .HasForeignKey(br => br.BookID)
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(br => br.Patron)
                      .WithMany()
                      .HasForeignKey(br => br.PatronID)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }

    }
}
