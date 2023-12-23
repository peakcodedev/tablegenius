﻿// <auto-generated />
using System;
using TableGenius.Api.Repo.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TableGenius.Api.Web.Migrations
{
    [DbContext(typeof(RepositoryContext))]
    partial class RepositoryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TableGenius.Api.Entities.Admin.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<bool>("AllowSocialMedia")
                        .HasColumnType("boolean");

                    b.Property<string>("ApprenticeshipYear")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<bool>("Confirmed")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Mail")
                        .HasColumnType("text");

                    b.Property<string>("Mobile")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("Profession")
                        .HasColumnType("text");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.Property<int>("ZipCode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Company.Company", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Url")
                        .HasColumnType("text");

                    b.Property<int>("ZipCode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Company");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Company.Employee", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CompanyId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Employee");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Course.Course", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Answer")
                        .HasColumnType("text");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Course");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.General.Profession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<int>("ExternalId")
                        .HasColumnType("integer");

                    b.Property<string>("FemaleTitle")
                        .HasColumnType("text");

                    b.Property<string>("MaleTitle")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.HasKey("Id");

                    b.ToTable("Profession");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.Project", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<bool>("AllowSocialMedia")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("File")
                        .HasColumnType("text");

                    b.Property<string>("InternalProjectNumber")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("PriceJury")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ProjectBoothId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("ProjectRatingId")
                        .HasColumnType("uuid");

                    b.Property<string>("SupervisorMail")
                        .HasColumnType("text");

                    b.Property<string>("SupervisorName")
                        .HasColumnType("text");

                    b.Property<string>("SupervisorPhone")
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .HasColumnType("text");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<string>("VideoUrl")
                        .HasColumnType("text");

                    b.Property<int>("Year")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Project");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectBooth", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Comments")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<bool>("Electrcity230")
                        .HasColumnType("boolean");

                    b.Property<bool>("Electrcity400")
                        .HasColumnType("boolean");

                    b.Property<bool>("Internet")
                        .HasColumnType("boolean");

                    b.Property<double>("Length")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("ProjectBoothCustomLayout")
                        .HasColumnType("text");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<double>("Wide")
                        .HasColumnType("double precision");

                    b.Property<long>("XCoordinate")
                        .HasColumnType("bigint");

                    b.Property<long>("YCoordinate")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("ProjectBooth");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectCollaboration", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ProjectCollaboration");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectExpert", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Address")
                        .HasColumnType("text");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("City")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("FirstName")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("LastName")
                        .HasColumnType("text");

                    b.Property<string>("Mobile")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<int>("ZipCode")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("ProjectExpert");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("Image")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid>("ProjectId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ProjectId");

                    b.ToTable("ProjectImage");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<DateTime>("End")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<Guid?>("ProjectExpertId")
                        .HasColumnType("uuid");

                    b.Property<Guid?>("ProjectId")
                        .HasColumnType("uuid");

                    b.Property<double>("Rating")
                        .HasColumnType("double precision");

                    b.Property<DateTime>("Start")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("Id");

                    b.HasIndex("ProjectExpertId");

                    b.HasIndex("ProjectId")
                        .IsUnique();

                    b.ToTable("ProjectRating");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectUserRating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<bool>("Deleted")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("boolean")
                        .HasDefaultValue(false);

                    b.Property<string>("First")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("timestamp without time zone")
                        .HasDefaultValueSql("NOW()");

                    b.Property<string>("Second")
                        .HasColumnType("text");

                    b.Property<string>("Third")
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("ProjectUserRating");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Company.Employee", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Company.Company", "Company")
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId");

                    b.HasOne("TableGenius.Api.Entities.Admin.User", "User")
                        .WithOne()
                        .HasForeignKey("TableGenius.Api.Entities.Company.Employee", "UserId");

                    b.Navigation("Company");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Course.Course", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Admin.User", "User")
                        .WithOne()
                        .HasForeignKey("TableGenius.Api.Entities.Course.Course", "UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectBooth", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Project.Project", "Project")
                        .WithOne("ProjectBooth")
                        .HasForeignKey("TableGenius.Api.Entities.Project.ProjectBooth", "ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectCollaboration", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Project.Project", "Project")
                        .WithMany("ProjectCollaborations")
                        .HasForeignKey("ProjectId");

                    b.HasOne("TableGenius.Api.Entities.Admin.User", "User")
                        .WithOne()
                        .HasForeignKey("TableGenius.Api.Entities.Project.ProjectCollaboration", "UserId");

                    b.Navigation("Project");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectImage", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Project.Project", "Project")
                        .WithMany("ProjectImages")
                        .HasForeignKey("ProjectId");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectRating", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Project.ProjectExpert", "ProjectExpert")
                        .WithMany("ProjectRatings")
                        .HasForeignKey("ProjectExpertId");

                    b.HasOne("TableGenius.Api.Entities.Project.Project", "Project")
                        .WithOne("ProjectRating")
                        .HasForeignKey("TableGenius.Api.Entities.Project.ProjectRating", "ProjectId");

                    b.Navigation("Project");

                    b.Navigation("ProjectExpert");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectUserRating", b =>
                {
                    b.HasOne("TableGenius.Api.Entities.Admin.User", "User")
                        .WithOne("ProjectUserRating")
                        .HasForeignKey("TableGenius.Api.Entities.Project.ProjectUserRating", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Admin.User", b =>
                {
                    b.Navigation("ProjectUserRating");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Company.Company", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.Project", b =>
                {
                    b.Navigation("ProjectBooth");

                    b.Navigation("ProjectCollaborations");

                    b.Navigation("ProjectImages");

                    b.Navigation("ProjectRating");
                });

            modelBuilder.Entity("TableGenius.Api.Entities.Project.ProjectExpert", b =>
                {
                    b.Navigation("ProjectRatings");
                });
#pragma warning restore 612, 618
        }
    }
}
