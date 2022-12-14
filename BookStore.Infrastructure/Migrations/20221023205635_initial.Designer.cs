// <auto-generated />
using System;
using BookStore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BookStore.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20221023205635_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BookStore.Infrastructure.Entities.BookEntity", b =>
                {
                    b.Property<int>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BookId"), 1L, 1);

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfCopies")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NumberOfRentedCopies")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("BookId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.LoanEntity", b =>
                {
                    b.Property<int>("LoanId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DueDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("RentedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DomainUserId")
                        .HasColumnType("int");

                    b.HasKey("LoanId");

                    b.HasIndex("BookId");

                    b.HasIndex("DomainUserId");

                    b.ToTable("Loans");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.LoanHistoryEntity", b =>
                {
                    b.Property<int>("LoanHistoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LoanHistoryId"), 1L, 1);

                    b.Property<int>("BookId")
                        .HasColumnType("int");

                    b.Property<DateTime>("RentedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ReturnedTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("DomainUserId")
                        .HasColumnType("int");

                    b.HasKey("LoanHistoryId");

                    b.HasIndex("BookId");

                    b.HasIndex("DomainUserId");

                    b.ToTable("LoanHistories");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.DomainUserEntity", b =>
                {
                    b.Property<int>("DomainUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DomainUserId"), 1L, 1);

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("datetime2");

                    b.Property<string>("EmailAdress")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("UpdatedTime")
                        .HasColumnType("datetime2");

                    b.HasKey("DomainUserId");

                    b.ToTable("DomainUsers");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.LoanEntity", b =>
                {
                    b.HasOne("BookStore.Infrastructure.Entities.BookEntity", "Book")
                        .WithMany("Loans")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Infrastructure.Entities.DomainUserEntity", "DomainUser")
                        .WithMany("Loans")
                        .HasForeignKey("DomainUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("DomainUser");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.LoanHistoryEntity", b =>
                {
                    b.HasOne("BookStore.Infrastructure.Entities.BookEntity", "Book")
                        .WithMany("LoanHistories")
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BookStore.Infrastructure.Entities.DomainUserEntity", "DomainUser")
                        .WithMany("LoanHistories")
                        .HasForeignKey("DomainUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("DomainUser");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.BookEntity", b =>
                {
                    b.Navigation("LoanHistories");

                    b.Navigation("Loans");
                });

            modelBuilder.Entity("BookStore.Infrastructure.Entities.DomainUserEntity", b =>
                {
                    b.Navigation("LoanHistories");

                    b.Navigation("Loans");
                });
#pragma warning restore 612, 618
        }
    }
}
