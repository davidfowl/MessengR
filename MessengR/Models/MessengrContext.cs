using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MessengR.Models
{
    public class MessengrContext : DbContext
    {
        public MessengrContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().Ignore(msg => msg.IsMine);
            modelBuilder.Entity<Message>().Ignore(msg => msg.Initiator);
            modelBuilder.Entity<Message>().Ignore(msg => msg.Contact);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Message> Messages { get; set; }
    }
}