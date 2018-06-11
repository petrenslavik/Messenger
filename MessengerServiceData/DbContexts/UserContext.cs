using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerServiceData.Entities;

namespace MessengerServiceData.DbContexts
{
    public class UserContext:DbContext
    {
        public UserContext()
            :base("DbConnection")
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friendship> Friendships { get; set; }
        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>().HasRequired(x => x.Receiver).WithOptional().Map(x => x.MapKey("ReceiverId")).WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>().HasRequired(x=>x.Sender).WithOptional().Map(x=>x.MapKey("SenderId")).WillCascadeOnDelete(false);
            modelBuilder.Entity<Friendship>().HasRequired(x=>x.FirstUser).WithOptional().Map(x=>x.MapKey("FirstUserId")).WillCascadeOnDelete(false);
            modelBuilder.Entity<Friendship>().HasRequired(x => x.SecondUser).WithOptional().Map(x => x.MapKey("SecondUserId")).WillCascadeOnDelete(false);
            modelBuilder.Entity<Session>().HasRequired(x=>x.Holder).WithOptional().Map(x=>x.MapKey("UserId")).WillCascadeOnDelete(false);
            modelBuilder.Entity<Message>().HasKey(x => x.Id);
            modelBuilder.Entity<Friendship>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<Session>().HasKey(x => x.Id);
        }
    }
}
