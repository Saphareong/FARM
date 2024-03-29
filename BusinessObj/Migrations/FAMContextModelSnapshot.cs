﻿// <auto-generated />
using System;
using BusinessObj.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace BusinessObj.Migrations
{
    [DbContext(typeof(FAMContext))]
    partial class FAMContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("BusinessObj.Models.Application", b =>
                {
                    b.Property<int>("ApplicationID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationID"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ApplicationContent")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("ApplicationStatus")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("ApplicationTypeID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("CreateDay")
                        .HasColumnType("datetime2");

                    b.HasKey("ApplicationID");

                    b.HasIndex("AccountId");

                    b.HasIndex("ApplicationTypeID");

                    b.ToTable("Applications");
                });

            modelBuilder.Entity("BusinessObj.Models.ApplicationType", b =>
                {
                    b.Property<string>("ApplicationTypeID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ApplicationTypeName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ApplicationTypeID");

                    b.ToTable("ApplicationTypes");

                    b.HasData(
                        new
                        {
                            ApplicationTypeID = "AT1",
                            ApplicationTypeName = "Đề nghị miễn điểm danh"
                        },
                        new
                        {
                            ApplicationTypeID = "AT2",
                            ApplicationTypeName = "Đề nghị cấp bảng điểm quá trình"
                        },
                        new
                        {
                            ApplicationTypeID = "AT3",
                            ApplicationTypeName = "Đề nghị chuyển đổi tín chỉ"
                        },
                        new
                        {
                            ApplicationTypeID = "AT4",
                            ApplicationTypeName = "Đề nghị phúc tra"
                        },
                        new
                        {
                            ApplicationTypeID = "AT5",
                            ApplicationTypeName = "Đăng kí thi cải thiện điểm"
                        },
                        new
                        {
                            ApplicationTypeID = "AT6",
                            ApplicationTypeName = "Đề nghị chuyển ngành"
                        },
                        new
                        {
                            ApplicationTypeID = "AT7",
                            ApplicationTypeName = "Đề nghị chuyển cơ sở"
                        },
                        new
                        {
                            ApplicationTypeID = "AT8",
                            ApplicationTypeName = "Đề nghị rút tiền"
                        });
                });

            modelBuilder.Entity("BusinessObj.Models.Attendance", b =>
                {
                    b.Property<int>("AttendanceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AttendanceID"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("RoomDetailID")
                        .HasColumnType("int");

                    b.Property<int>("SlotTotal")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserClassID")
                        .HasColumnType("int");

                    b.HasKey("AttendanceID");

                    b.HasIndex("AccountId");

                    b.HasIndex("RoomDetailID");

                    b.HasIndex("UserClassID");

                    b.ToTable("Attendances");
                });

            modelBuilder.Entity("BusinessObj.Models.Class", b =>
                {
                    b.Property<string>("ClassID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Status")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("SubjectID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ClassID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("BusinessObj.Models.ExamType", b =>
                {
                    b.Property<string>("ExamTypeID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ExamTypeName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ExamTypeID");

                    b.ToTable("ExamTypes");
                });

            modelBuilder.Entity("BusinessObj.Models.FinalExam", b =>
                {
                    b.Property<int>("ExamID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ExamID"), 1L, 1);

                    b.Property<DateTime>("ExamDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExamTime")
                        .HasColumnType("int");

                    b.Property<string>("ExamTypeID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("RoomID")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<string>("SubjectID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ExamID");

                    b.HasIndex("ExamTypeID");

                    b.HasIndex("SubjectID");

                    b.ToTable("FinalExams");
                });

            modelBuilder.Entity("BusinessObj.Models.Major", b =>
                {
                    b.Property<string>("MajorCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MajorName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("MajorCode");

                    b.ToTable("Majors");

                    b.HasData(
                        new
                        {
                            MajorCode = "SE",
                            MajorName = "Software Engineer"
                        },
                        new
                        {
                            MajorCode = "AI",
                            MajorName = "Artificial Intelligence"
                        },
                        new
                        {
                            MajorCode = "MC",
                            MajorName = "Multimedia Communication"
                        },
                        new
                        {
                            MajorCode = "IB",
                            MajorName = "International Business"
                        },
                        new
                        {
                            MajorCode = "GD",
                            MajorName = "Graphic Design"
                        },
                        new
                        {
                            MajorCode = "EL",
                            MajorName = "English Language"
                        },
                        new
                        {
                            MajorCode = "JL",
                            MajorName = "Japanese Language"
                        },
                        new
                        {
                            MajorCode = "IA",
                            MajorName = "Information Assurance"
                        },
                        new
                        {
                            MajorCode = "HM",
                            MajorName = "Hospitality Management"
                        },
                        new
                        {
                            MajorCode = "DM",
                            MajorName = "Digital Marketing"
                        });
                });

            modelBuilder.Entity("BusinessObj.Models.Role", b =>
                {
                    b.Property<string>("RoleId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = "AD",
                            RoleName = "Admin"
                        },
                        new
                        {
                            RoleId = "ST",
                            RoleName = "Student"
                        },
                        new
                        {
                            RoleId = "TE",
                            RoleName = "Teacher"
                        },
                        new
                        {
                            RoleId = "PA",
                            RoleName = "Parent"
                        });
                });

            modelBuilder.Entity("BusinessObj.Models.Room", b =>
                {
                    b.Property<int>("RoomID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomID"), 1L, 1);

                    b.Property<string>("RoomName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoomID");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BusinessObj.Models.RoomDetail", b =>
                {
                    b.Property<int>("RoomDetailID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoomDetailID"), 1L, 1);

                    b.Property<string>("AttendanceAction")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateBusy")
                        .HasColumnType("datetime2");

                    b.Property<int>("RoomID")
                        .HasColumnType("int");

                    b.Property<int>("SlotCurrentDay")
                        .HasColumnType("int");

                    b.Property<int>("SlotTotal")
                        .HasColumnType("int");

                    b.Property<DateTime>("TakeAttendanceTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("TimeTableID")
                        .HasColumnType("int");

                    b.HasKey("RoomDetailID");

                    b.HasIndex("RoomID");

                    b.HasIndex("TimeTableID");

                    b.ToTable("RoomDetails");
                });

            modelBuilder.Entity("BusinessObj.Models.Score", b =>
                {
                    b.Property<int>("ScoreID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ScoreID"), 1L, 1);

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("Assignment")
                        .HasColumnType("float");

                    b.Property<string>("ClassID")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("FE")
                        .HasColumnType("float");

                    b.Property<double>("Lab1")
                        .HasColumnType("float");

                    b.Property<double>("Lab2")
                        .HasColumnType("float");

                    b.Property<double>("Lab3")
                        .HasColumnType("float");

                    b.Property<double>("PE")
                        .HasColumnType("float");

                    b.Property<double>("Quiz1")
                        .HasColumnType("float");

                    b.Property<double>("Quiz2")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<string>("SubjectID")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<double>("Total")
                        .HasColumnType("float");

                    b.HasKey("ScoreID");

                    b.HasIndex("AccountId");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectID");

                    b.ToTable("Scores");
                });

            modelBuilder.Entity("BusinessObj.Models.Slot", b =>
                {
                    b.Property<int>("SlotID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SlotID"), 1L, 1);

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("SlotID");

                    b.ToTable("Slots");
                });

            modelBuilder.Entity("BusinessObj.Models.Subject", b =>
                {
                    b.Property<string>("SubjectID")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MajorCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("SlotRequire")
                        .HasColumnType("int");

                    b.Property<string>("SubjectName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("nvarchar(70)");

                    b.HasKey("SubjectID");

                    b.HasIndex("MajorCode");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("BusinessObj.Models.TimeTable", b =>
                {
                    b.Property<int>("TimeTableID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TimeTableID"), 1L, 1);

                    b.Property<string>("AccountId")
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ClassID")
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.HasKey("TimeTableID");

                    b.HasIndex("AccountId");

                    b.HasIndex("ClassID");

                    b.ToTable("TimeTables");
                });

            modelBuilder.Entity("BusinessObj.Models.User", b =>
                {
                    b.Property<string>("AccountId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("AccountName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Ower")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("RoleId")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Status")
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("AccountId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            AccountId = "AD01",
                            AccountName = "Admin",
                            Email = "admin@gmail.com",
                            Ower = "Admin",
                            Password = "aA@123",
                            Phone = "0398462788",
                            RoleId = "AD",
                            Status = "ON"
                        },
                        new
                        {
                            AccountId = "ST01",
                            AccountName = "Student",
                            Email = "student@gmail.com",
                            Ower = "Student Test",
                            Password = "aA@123",
                            Phone = "0398462788",
                            RoleId = "ST",
                            Status = "ON"
                        });
                });

            modelBuilder.Entity("BusinessObj.Models.UserClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ClassID")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("SubjectID")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("ClassID");

                    b.HasIndex("SubjectID");

                    b.ToTable("UserClasses");
                });

            modelBuilder.Entity("BusinessObj.Models.UserMajor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("AccountId")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("MajorCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.HasIndex("AccountId");

                    b.HasIndex("MajorCode");

                    b.ToTable("UserMajors");
                });

            modelBuilder.Entity("BusinessObj.Models.Application", b =>
                {
                    b.HasOne("BusinessObj.Models.User", "User")
                        .WithMany("Applications")
                        .HasForeignKey("AccountId");

                    b.HasOne("BusinessObj.Models.ApplicationType", "ApplicationType")
                        .WithMany("ApplicationTypes")
                        .HasForeignKey("ApplicationTypeID");

                    b.Navigation("ApplicationType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObj.Models.Attendance", b =>
                {
                    b.HasOne("BusinessObj.Models.User", "User")
                        .WithMany("Attendances")
                        .HasForeignKey("AccountId");

                    b.HasOne("BusinessObj.Models.RoomDetail", "RoomDetail")
                        .WithMany("Attendances")
                        .HasForeignKey("RoomDetailID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.UserClass", "UserClass")
                        .WithMany()
                        .HasForeignKey("UserClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("RoomDetail");

                    b.Navigation("User");

                    b.Navigation("UserClass");
                });

            modelBuilder.Entity("BusinessObj.Models.Class", b =>
                {
                    b.HasOne("BusinessObj.Models.Subject", "Subject")
                        .WithMany("Classes")
                        .HasForeignKey("SubjectID");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("BusinessObj.Models.FinalExam", b =>
                {
                    b.HasOne("BusinessObj.Models.ExamType", "ExamType")
                        .WithMany("ExamTypes")
                        .HasForeignKey("ExamTypeID");

                    b.HasOne("BusinessObj.Models.Subject", "Subject")
                        .WithMany("FinalExams")
                        .HasForeignKey("SubjectID");

                    b.Navigation("ExamType");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("BusinessObj.Models.RoomDetail", b =>
                {
                    b.HasOne("BusinessObj.Models.Room", "Room")
                        .WithMany("RoomDetails")
                        .HasForeignKey("RoomID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.TimeTable", "TimeTable")
                        .WithMany("RoomDetails")
                        .HasForeignKey("TimeTableID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Room");

                    b.Navigation("TimeTable");
                });

            modelBuilder.Entity("BusinessObj.Models.Score", b =>
                {
                    b.HasOne("BusinessObj.Models.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.Subject", "Subject")
                        .WithMany("Scores")
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObj.Models.Subject", b =>
                {
                    b.HasOne("BusinessObj.Models.Major", "Major")
                        .WithMany("Subjects")
                        .HasForeignKey("MajorCode");

                    b.Navigation("Major");
                });

            modelBuilder.Entity("BusinessObj.Models.TimeTable", b =>
                {
                    b.HasOne("BusinessObj.Models.User", "User")
                        .WithMany("TimeTables")
                        .HasForeignKey("AccountId");

                    b.HasOne("BusinessObj.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID");

                    b.Navigation("Class");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObj.Models.User", b =>
                {
                    b.HasOne("BusinessObj.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("BusinessObj.Models.UserClass", b =>
                {
                    b.HasOne("BusinessObj.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.Class", "Class")
                        .WithMany()
                        .HasForeignKey("ClassID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.Subject", "Subject")
                        .WithMany()
                        .HasForeignKey("SubjectID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Class");

                    b.Navigation("Subject");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObj.Models.UserMajor", b =>
                {
                    b.HasOne("BusinessObj.Models.User", "User")
                        .WithMany("UserMajors")
                        .HasForeignKey("AccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BusinessObj.Models.Major", "Major")
                        .WithMany("UserMajors")
                        .HasForeignKey("MajorCode")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Major");

                    b.Navigation("User");
                });

            modelBuilder.Entity("BusinessObj.Models.ApplicationType", b =>
                {
                    b.Navigation("ApplicationTypes");
                });

            modelBuilder.Entity("BusinessObj.Models.ExamType", b =>
                {
                    b.Navigation("ExamTypes");
                });

            modelBuilder.Entity("BusinessObj.Models.Major", b =>
                {
                    b.Navigation("Subjects");

                    b.Navigation("UserMajors");
                });

            modelBuilder.Entity("BusinessObj.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("BusinessObj.Models.Room", b =>
                {
                    b.Navigation("RoomDetails");
                });

            modelBuilder.Entity("BusinessObj.Models.RoomDetail", b =>
                {
                    b.Navigation("Attendances");
                });

            modelBuilder.Entity("BusinessObj.Models.Subject", b =>
                {
                    b.Navigation("Classes");

                    b.Navigation("FinalExams");

                    b.Navigation("Scores");
                });

            modelBuilder.Entity("BusinessObj.Models.TimeTable", b =>
                {
                    b.Navigation("RoomDetails");
                });

            modelBuilder.Entity("BusinessObj.Models.User", b =>
                {
                    b.Navigation("Applications");

                    b.Navigation("Attendances");

                    b.Navigation("Scores");

                    b.Navigation("TimeTables");

                    b.Navigation("UserMajors");
                });
#pragma warning restore 612, 618
        }
    }
}
