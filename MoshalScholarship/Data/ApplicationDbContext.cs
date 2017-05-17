using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MoshalScholarship.Models;

namespace MoshalScholarship.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<MoshalScholarship.Models.Mentor> Mentor { get; set; }

        public DbSet<MoshalScholarship.Models.Message> Message { get; set; }

        public DbSet<MoshalScholarship.Models.Buddy> Buddy { get; set; }

        public DbSet<MoshalScholarship.Models.Mentee> Mentee { get; set; }

        public DbSet<MoshalScholarship.Models.Administrator> Administrator { get; set; }

        public DbSet<MoshalScholarship.Models.Attendance> Attendance { get; set; }

        public DbSet<MoshalScholarship.Models.Event> Event { get; set; }

        public DbSet<MoshalScholarship.Models.Student> Student { get; set; }
    }
}
