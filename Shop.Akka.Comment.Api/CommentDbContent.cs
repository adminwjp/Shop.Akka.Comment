using Microsoft.EntityFrameworkCore;
using Shop.Comments;
using Shop.CommentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility;

namespace Shop
{
    public class CommentDbContent: DbContext
    {
        public CommentDbContent( DbContextOptions<CommentDbContent> options):base(options)
        {

        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CommentType> CommentTypes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(it => it.Id);
                if (DbConfig.Flag == DbFlag.MySql)
                {
                    entity.Property(it => it.CreationTime).HasColumnName("creation_time").HasColumnType("datetime");
                    entity.Property(it => it.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");
                    entity.Property(it => it.DeletionTime).HasColumnName("deletion_time").HasColumnType("datetime");
                }
                entity.Property(it => it.IsDeleted).HasColumnName("is_deleted");
            });
            modelBuilder.Entity<CommentType>(entity =>
            {
                entity.HasKey(it => it.Id);
                if (DbConfig.Flag == DbFlag.MySql)
                {
                    entity.Property(it => it.CreationTime).HasColumnName("creation_time").HasColumnType("datetime");
                    entity.Property(it => it.LastModificationTime).HasColumnName("last_modification_time").HasColumnType("datetime");
                    entity.Property(it => it.DeletionTime).HasColumnName("deletion_time").HasColumnType("datetime");
                }
                entity.Property(it => it.IsDeleted).HasColumnName("is_deleted");
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
