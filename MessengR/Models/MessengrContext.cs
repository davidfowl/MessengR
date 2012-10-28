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
            //modelBuilder.Entity<Message>()
            //    .HasRequired(c => c.Contact)
            //    .WithMany()
            //    .HasForeignKey(c => c.ContactId)
            //    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Message>()
            //    .HasRequired(c => c.Initiator)
            //    .WithMany()
            //    .HasForeignKey(c => c.InitiatorId)
            //    .WillCascadeOnDelete(false);
                
            modelBuilder.Entity<Message>().Ignore(msg => msg.IsMine);
            modelBuilder.Entity<Message>().Ignore(msg => msg.Initiator);
            modelBuilder.Entity<Message>().Ignore(msg => msg.Contact);
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Message> Messages { get; set; }
        //public DbSet<User> Users { get; set; }
    }

    //public class MessengrContextInitializer : DropCreateDatabaseIfModelChanges<MessengrContext>
    //{

    //}
}