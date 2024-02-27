﻿// <auto-generated />

using ByteBookmarks.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ByteBookmarks.Infrastructure.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20240227001157_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.2");

            modelBuilder.Entity("ByteBookmarks.Core.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Bookmark", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsPasswordProtected")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Bookmarks");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.BookmarkCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookmarkId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookmarkId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BookmarkCategory");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.BookmarkTag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookmarkId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TagId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookmarkId");

                    b.HasIndex("TagId");

                    b.ToTable("BookmarkTag");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tag");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Bookmark", b =>
                {
                    b.HasOne("ByteBookmarks.Core.Entities.ApplicationUser", "User")
                        .WithMany("Bookmarks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.BookmarkCategory", b =>
                {
                    b.HasOne("ByteBookmarks.Core.Entities.Bookmark", "Bookmark")
                        .WithMany("BookmarkCategories")
                        .HasForeignKey("BookmarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ByteBookmarks.Core.Entities.Category", "Category")
                        .WithMany("BookmarkCategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bookmark");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.BookmarkTag", b =>
                {
                    b.HasOne("ByteBookmarks.Core.Entities.Bookmark", "Bookmark")
                        .WithMany("BookmarkTags")
                        .HasForeignKey("BookmarkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ByteBookmarks.Core.Entities.Tag", "Tag")
                        .WithMany("BookmarkTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Bookmark");

                    b.Navigation("Tag");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Category", b =>
                {
                    b.HasOne("ByteBookmarks.Core.Entities.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.ApplicationUser", b =>
                {
                    b.Navigation("Bookmarks");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Bookmark", b =>
                {
                    b.Navigation("BookmarkCategories");

                    b.Navigation("BookmarkTags");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Category", b =>
                {
                    b.Navigation("BookmarkCategories");
                });

            modelBuilder.Entity("ByteBookmarks.Core.Entities.Tag", b =>
                {
                    b.Navigation("BookmarkTags");
                });
#pragma warning restore 612, 618
        }
    }
}
