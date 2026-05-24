using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDbContext :DbContext
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
}
    }
}