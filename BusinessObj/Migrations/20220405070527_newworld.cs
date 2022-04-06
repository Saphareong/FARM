using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObj.Migrations
{
    public partial class newworld : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApplicationTypes",
                columns: table => new
                {
                    ApplicationTypeID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ApplicationTypeName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationTypes", x => x.ApplicationTypeID);
                });

            migrationBuilder.CreateTable(
                name: "ExamTypes",
                columns: table => new
                {
                    ExamTypeID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ExamTypeName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExamTypes", x => x.ExamTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Majors",
                columns: table => new
                {
                    MajorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MajorName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Majors", x => x.MajorCode);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    RoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.RoomID);
                });

            migrationBuilder.CreateTable(
                name: "Slots",
                columns: table => new
                {
                    SlotID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slots", x => x.SlotID);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    SubjectID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    SubjectName = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    SlotRequire = table.Column<int>(type: "int", nullable: false),
                    MajorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.SubjectID);
                    table.ForeignKey(
                        name: "FK_Subjects_Majors_MajorCode",
                        column: x => x.MajorCode,
                        principalTable: "Majors",
                        principalColumn: "MajorCode");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AccountName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Ower = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    ClassID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    SubjectID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.ClassID);
                    table.ForeignKey(
                        name: "FK_Classes_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateTable(
                name: "FinalExams",
                columns: table => new
                {
                    ExamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExamDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExamTime = table.Column<int>(type: "int", nullable: false),
                    RoomID = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    ExamTypeID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    SubjectID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinalExams", x => x.ExamID);
                    table.ForeignKey(
                        name: "FK_FinalExams_ExamTypes_ExamTypeID",
                        column: x => x.ExamTypeID,
                        principalTable: "ExamTypes",
                        principalColumn: "ExamTypeID");
                    table.ForeignKey(
                        name: "FK_FinalExams_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID");
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicationContent = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ApplicationStatus = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    ApplicationTypeID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CreateDay = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK_Applications_ApplicationTypes_ApplicationTypeID",
                        column: x => x.ApplicationTypeID,
                        principalTable: "ApplicationTypes",
                        principalColumn: "ApplicationTypeID");
                    table.ForeignKey(
                        name: "FK_Applications_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Users",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "UserMajors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    MajorCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMajors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMajors_Majors_MajorCode",
                        column: x => x.MajorCode,
                        principalTable: "Majors",
                        principalColumn: "MajorCode",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMajors_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Users",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    ScoreID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quiz1 = table.Column<double>(type: "float", nullable: false),
                    Quiz2 = table.Column<double>(type: "float", nullable: false),
                    Lab1 = table.Column<double>(type: "float", nullable: false),
                    Lab2 = table.Column<double>(type: "float", nullable: false),
                    Lab3 = table.Column<double>(type: "float", nullable: false),
                    Assignment = table.Column<double>(type: "float", nullable: false),
                    PE = table.Column<double>(type: "float", nullable: false),
                    FE = table.Column<double>(type: "float", nullable: false),
                    Total = table.Column<double>(type: "float", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: true),
                    SubjectID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    ClassID = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    AccountId = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.ScoreID);
                    table.ForeignKey(
                        name: "FK_Scores_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Users",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TimeTables",
                columns: table => new
                {
                    TimeTableID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClassID = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    AccountId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    DateStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeTables", x => x.TimeTableID);
                    table.ForeignKey(
                        name: "FK_TimeTables_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID");
                    table.ForeignKey(
                        name: "FK_TimeTables_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Users",
                        principalColumn: "AccountId");
                });

            migrationBuilder.CreateTable(
                name: "UserClasses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    SubjectID = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    ClassID = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClasses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClasses_Classes_ClassID",
                        column: x => x.ClassID,
                        principalTable: "Classes",
                        principalColumn: "ClassID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClasses_Subjects_SubjectID",
                        column: x => x.SubjectID,
                        principalTable: "Subjects",
                        principalColumn: "SubjectID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserClasses_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Users",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoomDetails",
                columns: table => new
                {
                    RoomDetailID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    TimeTableID = table.Column<int>(type: "int", nullable: false),
                    SlotCurrentDay = table.Column<int>(type: "int", nullable: false),
                    DateBusy = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlotTotal = table.Column<int>(type: "int", nullable: false),
                    AttendanceAction = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TakeAttendanceTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomDetails", x => x.RoomDetailID);
                    table.ForeignKey(
                        name: "FK_RoomDetails_Rooms_RoomID",
                        column: x => x.RoomID,
                        principalTable: "Rooms",
                        principalColumn: "RoomID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomDetails_TimeTables_TimeTableID",
                        column: x => x.TimeTableID,
                        principalTable: "TimeTables",
                        principalColumn: "TimeTableID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    AttendanceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountId = table.Column<string>(type: "nvarchar(10)", nullable: true),
                    SlotTotal = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomDetailID = table.Column<int>(type: "int", nullable: false),
                    UserClassID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.AttendanceID);
                    table.ForeignKey(
                        name: "FK_Attendances_RoomDetails_RoomDetailID",
                        column: x => x.RoomDetailID,
                        principalTable: "RoomDetails",
                        principalColumn: "RoomDetailID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_UserClasses_UserClassID",
                        column: x => x.UserClassID,
                        principalTable: "UserClasses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Users_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Users",
                        principalColumn: "AccountId");
                });

            migrationBuilder.InsertData(
                table: "ApplicationTypes",
                columns: new[] { "ApplicationTypeID", "ApplicationTypeName" },
                values: new object[,]
                {
                    { "AT1", "Đề nghị miễn điểm danh" },
                    { "AT2", "Đề nghị cấp bảng điểm quá trình" },
                    { "AT3", "Đề nghị chuyển đổi tín chỉ" },
                    { "AT4", "Đề nghị phúc tra" },
                    { "AT5", "Đăng kí thi cải thiện điểm" },
                    { "AT6", "Đề nghị chuyển ngành" },
                    { "AT7", "Đề nghị chuyển cơ sở" },
                    { "AT8", "Đề nghị rút tiền" }
                });

            migrationBuilder.InsertData(
                table: "Majors",
                columns: new[] { "MajorCode", "MajorName" },
                values: new object[,]
                {
                    { "AI", "Artificial Intelligence" },
                    { "DM", "Digital Marketing" },
                    { "EL", "English Language" },
                    { "GD", "Graphic Design" },
                    { "HM", "Hospitality Management" },
                    { "IA", "Information Assurance" },
                    { "IB", "International Business" },
                    { "JL", "Japanese Language" },
                    { "MC", "Multimedia Communication" },
                    { "SE", "Software Engineer" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName" },
                values: new object[,]
                {
                    { "AD", "Admin" },
                    { "PA", "Parent" },
                    { "ST", "Student" },
                    { "TE", "Teacher" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "AccountId", "AccountName", "Email", "Ower", "Password", "Phone", "RoleId", "Status" },
                values: new object[] { "AD01", "Admin", "admin@gmail.com", "Admin", "aA@123", "0398462788", "AD", "ON" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "AccountId", "AccountName", "Email", "Ower", "Password", "Phone", "RoleId", "Status" },
                values: new object[] { "ST01", "Student", "student@gmail.com", "Student Test", "aA@123", "0398462788", "ST", "ON" });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_AccountId",
                table: "Applications",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicationTypeID",
                table: "Applications",
                column: "ApplicationTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_AccountId",
                table: "Attendances",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_RoomDetailID",
                table: "Attendances",
                column: "RoomDetailID");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_UserClassID",
                table: "Attendances",
                column: "UserClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SubjectID",
                table: "Classes",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_FinalExams_ExamTypeID",
                table: "FinalExams",
                column: "ExamTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_FinalExams_SubjectID",
                table: "FinalExams",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDetails_RoomID",
                table: "RoomDetails",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_RoomDetails_TimeTableID",
                table: "RoomDetails",
                column: "TimeTableID");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_AccountId",
                table: "Scores",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_ClassID",
                table: "Scores",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_SubjectID",
                table: "Scores",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_MajorCode",
                table: "Subjects",
                column: "MajorCode");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_AccountId",
                table: "TimeTables",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_TimeTables_ClassID",
                table: "TimeTables",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_UserClasses_AccountId",
                table: "UserClasses",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClasses_ClassID",
                table: "UserClasses",
                column: "ClassID");

            migrationBuilder.CreateIndex(
                name: "IX_UserClasses_SubjectID",
                table: "UserClasses",
                column: "SubjectID");

            migrationBuilder.CreateIndex(
                name: "IX_UserMajors_AccountId",
                table: "UserMajors",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMajors_MajorCode",
                table: "UserMajors",
                column: "MajorCode");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "FinalExams");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Slots");

            migrationBuilder.DropTable(
                name: "UserMajors");

            migrationBuilder.DropTable(
                name: "ApplicationTypes");

            migrationBuilder.DropTable(
                name: "RoomDetails");

            migrationBuilder.DropTable(
                name: "UserClasses");

            migrationBuilder.DropTable(
                name: "ExamTypes");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "TimeTables");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Majors");
        }
    }
}
