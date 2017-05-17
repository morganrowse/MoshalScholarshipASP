using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using MoshalScholarship.Data;

namespace MoshalScholarship.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Administrator", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Administrator");
                });

            modelBuilder.Entity("MoshalScholarship.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("DateOfBirth");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MobileNumber");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserId");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<string>("UserType");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Attendance", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int>("EventId");

                    b.Property<string>("ToUserId");

                    b.Property<string>("UserId");

                    b.HasKey("ID");

                    b.HasIndex("EventId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Attendance");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Buddy", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FromUserId");

                    b.Property<string>("ToUserId");

                    b.HasKey("ID");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Buddy");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Event", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("EventType");

                    b.Property<string>("Location");

                    b.HasKey("ID");

                    b.ToTable("Event");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Mentee", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("EndedAt");

                    b.Property<string>("MentorId");

                    b.Property<DateTime>("StartAt");

                    b.Property<string>("StudentId");

                    b.HasKey("ID");

                    b.HasIndex("MentorId");

                    b.HasIndex("StudentId");

                    b.ToTable("Mentee");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Mentor", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<string>("AddressLine4");

                    b.Property<string>("CurrentCompany");

                    b.Property<string>("CurrentJob");

                    b.Property<string>("Degree");

                    b.Property<string>("Faculty");

                    b.Property<string>("IdentityNumber");

                    b.Property<string>("Institution");

                    b.Property<string>("PassportNumber");

                    b.Property<string>("StudentCount");

                    b.Property<string>("UserId");

                    b.Property<string>("WorkLocation");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Mentor");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Message", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Body");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<string>("FromUserId");

                    b.Property<string>("ToUserId");

                    b.HasKey("ID");

                    b.HasIndex("FromUserId");

                    b.HasIndex("ToUserId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Student", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressLine1");

                    b.Property<string>("AddressLine2");

                    b.Property<string>("AddressLine3");

                    b.Property<string>("AddressLine4");

                    b.Property<string>("Degree");

                    b.Property<string>("Faculty");

                    b.Property<string>("IdentityNumber");

                    b.Property<string>("Institution");

                    b.Property<string>("Nationality");

                    b.Property<string>("PassportNumber");

                    b.Property<string>("PreferredCompany");

                    b.Property<string>("PreferredJob");

                    b.Property<string>("StudentCount");

                    b.Property<string>("UserId");

                    b.Property<string>("WorkLocation");

                    b.HasKey("ID");

                    b.HasIndex("UserId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoshalScholarship.Models.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("MoshalScholarship.Models.Administrator", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Attendance", b =>
                {
                    b.HasOne("MoshalScholarship.Models.Event", "Event")
                        .WithMany()
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Buddy", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Mentee", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "Mentor")
                        .WithMany()
                        .HasForeignKey("MentorId");

                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Mentor", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Message", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "FromUser")
                        .WithMany()
                        .HasForeignKey("FromUserId");

                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "ToUser")
                        .WithMany()
                        .HasForeignKey("ToUserId");
                });

            modelBuilder.Entity("MoshalScholarship.Models.Student", b =>
                {
                    b.HasOne("MoshalScholarship.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
