﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TestTaskGFL.Models.Contexts;

#nullable disable

namespace TestTaskGFL.Migrations
{
    [DbContext(typeof(FoldersContext))]
    [Migration("20230705182915_defaultData")]
    partial class defaultData
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.19")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("TestTaskGFL.Models.Folder", b =>
                {
                    b.Property<int>("FolderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FolderId"), 1L, 1);

                    b.Property<string>("FolderName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ParentFolderId")
                        .HasColumnType("int");

                    b.HasKey("FolderId");

                    b.HasIndex("ParentFolderId");

                    b.ToTable("Folders");

                    b.HasData(
                        new
                        {
                            FolderId = 1,
                            FolderName = "Creating Digital Images"
                        },
                        new
                        {
                            FolderId = 2,
                            FolderName = "Resources",
                            ParentFolderId = 1
                        },
                        new
                        {
                            FolderId = 3,
                            FolderName = "Evidence",
                            ParentFolderId = 1
                        },
                        new
                        {
                            FolderId = 4,
                            FolderName = "Graphic Products",
                            ParentFolderId = 1
                        },
                        new
                        {
                            FolderId = 5,
                            FolderName = "Primary Sources",
                            ParentFolderId = 2
                        },
                        new
                        {
                            FolderId = 6,
                            FolderName = "Secondary Sources",
                            ParentFolderId = 2
                        },
                        new
                        {
                            FolderId = 7,
                            FolderName = "Process",
                            ParentFolderId = 4
                        },
                        new
                        {
                            FolderId = 8,
                            FolderName = "Final Product",
                            ParentFolderId = 4
                        });
                });

            modelBuilder.Entity("TestTaskGFL.Models.Folder", b =>
                {
                    b.HasOne("TestTaskGFL.Models.Folder", "ParentFolder")
                        .WithMany("ChildFolderes")
                        .HasForeignKey("ParentFolderId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("ParentFolder");
                });

            modelBuilder.Entity("TestTaskGFL.Models.Folder", b =>
                {
                    b.Navigation("ChildFolderes");
                });
#pragma warning restore 612, 618
        }
    }
}