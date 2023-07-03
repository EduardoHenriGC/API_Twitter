using API_TESTE.Controllers;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using API_TESTE.Models;

namespace API_TESTE.Models.Context
{
    public class MeuContexto : DbContext
    {
        public MeuContexto(DbContextOptions<MeuContexto> options) : base(options)
        {
        }

        // Defina as DbSet para cada entidade que deseja mapear para tabelas
   
        public DbSet<Tweet> Tweet { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Like> Like { get; set; }

        public DbSet<Comment> Comment { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tweet>()
                .HasOne(t => t.User)
                .WithMany(u => u.Tweets)
                .HasForeignKey(t => t.UserId);
        }


        

    }
}