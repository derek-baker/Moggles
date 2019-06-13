﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Moggles.Data.Migrations
{
    [DbContext(typeof(TogglesContext))]
    [Migration("20190531082344_AddedIsPermanentFlag")]
    partial class AddedIsPermanentFlag
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Moggles.Domain.Application", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AppName")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("AppName");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("Moggles.Domain.DeployEnvironment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationId");

                    b.Property<bool>("DefaultToggleValue");

                    b.Property<string>("EnvName")
                        .HasMaxLength(50);

                    b.Property<int>("SortOrder")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(500);

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("EnvName");

                    b.ToTable("DeployEnvironments");
                });

            modelBuilder.Entity("Moggles.Domain.FeatureToggle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApplicationId");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.Property<bool>("IsPermanent");

                    b.Property<string>("Notes")
                        .HasMaxLength(500);

                    b.Property<string>("ToggleName")
                        .HasMaxLength(80);

                    b.Property<bool>("UserAccepted");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("ToggleName");

                    b.ToTable("FeatureToggles");
                });

            modelBuilder.Entity("Moggles.Domain.FeatureToggleStatus", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Enabled");

                    b.Property<int>("EnvironmentId");

                    b.Property<int>("FeatureToggleId");

                    b.Property<DateTime?>("FirstTimeDeployDate");

                    b.Property<bool>("IsDeployed");

                    b.Property<DateTime?>("LastDeployStatusUpdate");

                    b.Property<DateTime>("LastUpdated")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("GETUTCDATE()");

                    b.HasKey("Id");

                    b.HasIndex("EnvironmentId");

                    b.HasIndex("FeatureToggleId");

                    b.ToTable("FeatureToggleStatuses");
                });

            modelBuilder.Entity("Moggles.Domain.DeployEnvironment", b =>
                {
                    b.HasOne("Moggles.Domain.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Moggles.Domain.FeatureToggle", b =>
                {
                    b.HasOne("Moggles.Domain.Application", "Application")
                        .WithMany()
                        .HasForeignKey("ApplicationId")
                        .OnDelete(DeleteBehavior.Restrict);
                });

            modelBuilder.Entity("Moggles.Domain.FeatureToggleStatus", b =>
                {
                    b.HasOne("Moggles.Domain.DeployEnvironment", "Environment")
                        .WithMany()
                        .HasForeignKey("EnvironmentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Moggles.Domain.FeatureToggle", "FeatureToggle")
                        .WithMany("FeatureToggleStatuses")
                        .HasForeignKey("FeatureToggleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
