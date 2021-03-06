﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Shop;

namespace Shop.Migrations
{
    [DbContext(typeof(CommentDbContent))]
    [Migration("20210221050120_a1")]
    partial class a1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Shop.CommentTypes.CommentType", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4")
                        .HasMaxLength(36);

                    b.Property<string>("Code")
                        .HasColumnName("code")
                        .HasColumnType("varchar(10) CHARACTER SET utf8mb4")
                        .HasMaxLength(10);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creation_time")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnName("deletion_time")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnName("last_modification_time")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .HasColumnName("name")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("varchar(1) CHARACTER SET utf8mb4")
                        .HasMaxLength(1);

                    b.HasKey("Id");

                    b.ToTable("t_comment_type");
                });

            modelBuilder.Entity("Shop.Comments.Comment", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnName("id")
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4")
                        .HasMaxLength(36);

                    b.Property<string>("Account")
                        .HasColumnName("account")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("Content")
                        .HasColumnName("content")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnName("creation_time")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("DeletionTime")
                        .HasColumnName("deletion_time")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("is_deleted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("LastModificationTime")
                        .HasColumnName("last_modification_time")
                        .HasColumnType("datetime");

                    b.Property<string>("Nickname")
                        .HasColumnName("nickname")
                        .HasColumnType("varchar(20) CHARACTER SET utf8mb4")
                        .HasMaxLength(20);

                    b.Property<string>("OrderDetailID")
                        .HasColumnName("order_detail_id")
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4")
                        .HasMaxLength(36);

                    b.Property<string>("OrderID")
                        .HasColumnName("order_id")
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4")
                        .HasMaxLength(36);

                    b.Property<string>("ProductID")
                        .HasColumnName("product_id")
                        .HasColumnType("varchar(36) CHARACTER SET utf8mb4")
                        .HasMaxLength(36);

                    b.Property<string>("Reply")
                        .HasColumnName("reply")
                        .HasColumnType("varchar(500) CHARACTER SET utf8mb4")
                        .HasMaxLength(500);

                    b.Property<int>("Star")
                        .HasColumnName("star")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnName("status")
                        .HasColumnType("varchar(1) CHARACTER SET utf8mb4")
                        .HasMaxLength(1);

                    b.HasKey("Id");

                    b.ToTable("t_comment");
                });
#pragma warning restore 612, 618
        }
    }
}
