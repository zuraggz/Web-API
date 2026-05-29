using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    //გასარჩევი
    public class ApplicationDbContext :IdentityDbContext<AppUser>
    {
        // ამ კოდით მშობელ კლასს ვაწვდით პარამეტრებს.
        public ApplicationDbContext(DbContextOptions dbContextOptions) 
            :base(dbContextOptions)
        {
            
        }

        // Telling EF what Tables should be made.
        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
        base.OnModelCreating(modelBuilder);


        // დრო რო აცტომატურად აიღოს
        modelBuilder.Entity<Comment>()
            .Property(c => c.CreatedOn)
            .HasDefaultValueSql("GETDATE()");


        List<IdentityRole> roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = "32d8bc93-3af4-4b8c-bcb4-9ec9d56151d5",
                    Name = "Admin",
                    NormalizedName = "ADMIN",
                    ConcurrencyStamp = "8b46b674-1de1-430d-9ea9-762074865e46"
                },
                new IdentityRole
                {
                    Id = "77306733-78f5-4cff-b8b4-b1924259c0da",
                    Name = "User",
                    NormalizedName = "USER",
                    ConcurrencyStamp = "7037fdc9-b46c-41f8-9826-ab4ed19cd38c"
                },
            };
            modelBuilder.Entity<IdentityRole>().HasData(roles);
    }
}
}
