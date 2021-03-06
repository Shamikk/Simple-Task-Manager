﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SimpleTaskManager.API.DAL;

namespace SimpleTaskManager.API.Migrations
{
    [DbContext(typeof(STMContext))]
    [Migration("20180920213703_Removed unecessary table, removed IsLocked from SimpleTask")]
    partial class RemovedunecessarytableremovedIsLockedfromSimpleTask
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SimpleTaskManager.API.Model.BLL.SimpleTask", b =>
                {
                    b.Property<string>("Name")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50);

                    b.Property<string>("CreatedBy");

                    b.Property<DateTime>("CreationDateTime");

                    b.Property<string>("LastUpdateBy");

                    b.Property<DateTime>("LastUpdateDateTime");

                    b.Property<int>("NumberOfUpdates");

                    b.Property<int>("Status");

                    b.HasKey("Name");

                    b.ToTable("SimpleTask");
                });
#pragma warning restore 612, 618
        }
    }
}
