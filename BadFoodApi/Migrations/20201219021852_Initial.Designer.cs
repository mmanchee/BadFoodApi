﻿// <auto-generated />
using BadFoodApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BadFoodApi.Migrations
{
    [DbContext(typeof(BadFoodApiContext))]
    [Migration("20201219021852_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("BadFoodApi.Models.Food", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Caffeine");

                    b.Property<string>("Category");

                    b.Property<string>("Description");

                    b.Property<int>("Egg");

                    b.Property<int>("FDCId");

                    b.Property<int>("FODMAP");

                    b.Property<int>("Fish");

                    b.Property<int>("Fructose");

                    b.Property<int>("Gluten");

                    b.Property<int>("Histamine");

                    b.Property<int>("Lactose");

                    b.Property<int>("Lectin");

                    b.Property<int>("Legume");

                    b.Property<string>("Name");

                    b.Property<int>("Nut");

                    b.Property<int>("Oxalte");

                    b.Property<int>("Salicylates");

                    b.Property<int>("Shellfish");

                    b.Property<int>("Soy");

                    b.Property<string>("SubCat");

                    b.Property<int>("Sulfites");

                    b.Property<int>("Tryamine");

                    b.HasKey("FoodId");

                    b.ToTable("Foods");
                });
#pragma warning restore 612, 618
        }
    }
}
