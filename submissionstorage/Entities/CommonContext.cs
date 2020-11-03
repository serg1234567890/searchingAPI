using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using submissionstorage.Entities.Searching;

namespace submissionstorage.Entities
{
    public class CommonContext : IdentityDbContext<IdentityUser<Guid>, IdentityRole<Guid>, Guid>
    {
        private DbSet<Submission> Submissions { get; set; }
        private DbSet<Submission_type> Submission_types { get; set; }
        public CommonContext(DbContextOptions options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
