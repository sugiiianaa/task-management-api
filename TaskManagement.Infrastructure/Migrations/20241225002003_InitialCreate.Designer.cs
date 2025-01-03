﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskManagement.Infrastructure.Persistence;

#nullable disable

namespace TaskManagement.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20241225002003_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskManagement.Domain.Entities.SubTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<bool>("IsCompleted")
                        .HasColumnType("boolean")
                        .HasColumnName("is_completed");

                    b.Property<Guid>("TaskId")
                        .HasColumnType("uuid")
                        .HasColumnName("task_id");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("TaskId");

                    b.ToTable("sub_tasks");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("email");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("name");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("password_hash");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("role");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("users");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Address")
                        .HasColumnType("text")
                        .HasColumnName("address");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("date_of_birth");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text")
                        .HasColumnName("phone_number");

                    b.HasKey("Id");

                    b.ToTable("user_profiles");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.UserTask", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Description")
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<DateTime>("ExpectedFinishDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("expected_finish_date");

                    b.Property<Guid>("TaskOwnerId")
                        .HasColumnType("uuid")
                        .HasColumnName("task_owner_id");

                    b.Property<int>("TaskStatus")
                        .HasColumnType("integer")
                        .HasColumnName("task_status");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("title");

                    b.HasKey("Id");

                    b.HasIndex("TaskOwnerId");

                    b.ToTable("user_tasks");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.SubTask", b =>
                {
                    b.HasOne("TaskManagement.Domain.Entities.UserTask", "Task")
                        .WithMany("Subtasks")
                        .HasForeignKey("TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_sub_tasks__user_tasks_task_id");

                    b.Navigation("Task");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.UserProfile", b =>
                {
                    b.HasOne("TaskManagement.Domain.Entities.User", "User")
                        .WithOne("Profile")
                        .HasForeignKey("TaskManagement.Domain.Entities.UserProfile", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_profiles_users_id");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.UserTask", b =>
                {
                    b.HasOne("TaskManagement.Domain.Entities.User", "TaskOwner")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskOwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("f_k_user_tasks_users_task_owner_id");

                    b.Navigation("TaskOwner");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.User", b =>
                {
                    b.Navigation("Profile");

                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("TaskManagement.Domain.Entities.UserTask", b =>
                {
                    b.Navigation("Subtasks");
                });
#pragma warning restore 612, 618
        }
    }
}
