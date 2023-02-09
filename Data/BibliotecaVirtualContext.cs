using LibreriaVirtualApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriaVirtualApi.Data
{
    public partial class BibliotecaVirtualContext : DbContext
    {
        public BibliotecaVirtualContext()
        {
        }

        public BibliotecaVirtualContext(DbContextOptions<BibliotecaVirtualContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Book> Books { get; set; } = null!;
        public virtual DbSet<SalesDetail> SalesDetails { get; set; } = null!;
        public virtual DbSet<SalesHistory> SalesHistories { get; set; } = null!;
        public virtual DbSet<ShoppingCar> ShoppingCars { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>(entity =>
            {
                entity.Property(e => e.Author)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Edition)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Books)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Books__UserId__398D8EEE");
            });

            modelBuilder.Entity<SalesDetail>(entity =>
            {
                entity.HasKey(e => e.SaleDetail)
                    .HasName("PK__SalesDet__AB5D2D5D21B2838C");

                entity.ToTable("SalesDetail");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.SalesDetails)
                    .HasForeignKey(d => d.BookId)
                    .HasConstraintName("FK__SalesDeta__BookI__440B1D61");

                entity.HasOne(d => d.Sale)
                    .WithMany(p => p.SalesDetails)
                    .HasForeignKey(d => d.SaleId)
                    .HasConstraintName("FK__SalesDeta__SaleI__4316F928");
            });

            modelBuilder.Entity<SalesHistory>(entity =>
            {
                entity.HasKey(e => e.SaleId)
                    .HasName("PK__SalesHis__1EE3C3FFDD7BE0D8");

                entity.ToTable("SalesHistory");

                entity.Property(e => e.Total).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.SalesHistories)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__SalesHist__Custo__403A8C7D");
            });

            modelBuilder.Entity<ShoppingCar>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.BookId })
                    .HasName("PK__Shopping__7456C06C541C8FB4");

                entity.ToTable("ShoppingCar");

                entity.HasOne(d => d.Book)
                    .WithMany(p => p.ShoppingCars)
                    .HasForeignKey(d => d.BookId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShoppingC__BookI__3D5E1FD2");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ShoppingCars)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ShoppingC__UserI__3C69FB99");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
