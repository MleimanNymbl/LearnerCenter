using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnerCenter.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialWithPostgreSQLDefaults : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    CampusId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampusName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    CampusCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.CampusId);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    TermId = table.Column<Guid>(type: "uuid", nullable: false),
                    TermName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    TermCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RegistrationStartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    RegistrationEndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.TermId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    CampusId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProgramName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Degree = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.EnrollmentId);
                    table.ForeignKey(
                        name: "FK_Enrollments_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "CampusId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<Guid>(type: "uuid", nullable: false),
                    CourseCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    CourseName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreditHours = table.Column<int>(type: "integer", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Enrollments_EnrollmentId",
                        column: x => x.EnrollmentId,
                        principalTable: "Enrollments",
                        principalColumn: "EnrollmentId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UserProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    EmergencyContactName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false, defaultValueSql: "NOW() AT TIME ZONE 'UTC'"),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UserProfileId);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Campuses",
                columns: new[] { "CampusId", "Address", "CampusCode", "CampusName", "City", "CreatedDate", "Email", "IsActive", "PhoneNumber", "State", "ZipCode" },
                values: new object[,]
                {
                    { new Guid("00000001-6163-706d-7573-202001000000"), "100 Learning St", "SU-MAIN", "State University Main Campus", "Springfield", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@sumain.edu", true, "(555)-123-4567", "IL", "62701" },
                    { new Guid("00000002-6163-706d-7573-202002000000"), "110 College Way", "CC-DOWN", "Community College Downtown", "Springfield", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@ccdown.edu", true, "(556)-124-4568", "IL", "62702" },
                    { new Guid("00000003-6163-706d-7573-202003000000"), "120 College Blvd", "TI-NORTH", "Technical Institute North", "Rockford", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@tinorth.edu", true, "(557)-125-4569", "IL", "61101" },
                    { new Guid("00000004-6163-706d-7573-202004000000"), "130 Knowledge Way", "LAC-MAIN", "Liberal Arts College", "Peoria", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@lacmain.edu", true, "(558)-126-4570", "IL", "61602" },
                    { new Guid("00000005-6163-706d-7573-202005000000"), "140 College Ct", "BSC-CENT", "Business School Central", "Chicago", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@bsccent.edu", true, "(559)-127-4571", "IL", "60601" },
                    { new Guid("00000006-6163-706d-7573-202006000000"), "150 Academic Blvd", "SCC-MAIN", "Sunshine Community College", "Miami", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@sccmain.edu", true, "(560)-128-4572", "FL", "33101" },
                    { new Guid("00000007-6163-706d-7573-202007000000"), "160 Education Blvd", "MVTI-MAIN", "Mountain View Technical Institute", "Denver", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@mvtimain.edu", true, "(561)-129-4573", "CO", "80201" },
                    { new Guid("00000008-6163-706d-7573-202008000000"), "170 Scholar Blvd", "LSU-MAIN", "Lone Star University", "Austin", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@lsumain.edu", true, "(562)-130-4574", "TX", "73301" },
                    { new Guid("00000009-6163-706d-7573-202009000000"), "180 Education Ave", "GGC-MAIN", "Golden Gate College", "San Francisco", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@ggcmain.edu", true, "(563)-131-4575", "CA", "94101" },
                    { new Guid("0000000a-6163-706d-7573-20200a000000"), "190 Wisdom Way", "ESI-MAIN", "Empire State Institute", "New York", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@esimain.edu", true, "(564)-132-4576", "NY", "10001" },
                    { new Guid("0000000b-6163-706d-7573-20200b000000"), "200 Scholar St", "DIU-MAIN", "Desert Innovation University", "Phoenix", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@diumain.edu", true, "(565)-133-4577", "AZ", "85001" },
                    { new Guid("0000000c-6163-706d-7573-20200c000000"), "210 University Rd", "GLTC-MAIN", "Great Lakes Technical College", "Detroit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@gltcmain.edu", true, "(566)-134-4578", "MI", "48201" },
                    { new Guid("0000000d-6163-706d-7573-20200d000000"), "220 Wisdom Way", "ECU-MAIN", "Emerald City University", "Seattle", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@ecumain.edu", true, "(567)-135-4579", "WA", "98101" },
                    { new Guid("0000000e-6163-706d-7573-20200e000000"), "230 University Rd", "BRCC-MAIN", "Blue Ridge Community College", "Richmond", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@brccmain.edu", true, "(568)-136-4580", "VA", "23218" },
                    { new Guid("0000000f-6163-706d-7573-20200f000000"), "240 College Pl", "PSI-MAIN", "Peach State Institute", "Atlanta", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "info@psimain.edu", true, "(569)-137-4581", "GA", "30301" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "CampusId", "CreatedDate", "Degree", "Description", "IsActive", "ProgramName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), new Guid("00000001-6163-706d-7573-202001000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Comprehensive computer science program covering programming, algorithms, and software engineering", true, "Computer Science", null },
                    { new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), new Guid("00000001-6163-706d-7573-202001000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Engineering", "Engineering fundamentals with multiple specializations available", true, "Engineering", null },
                    { new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), new Guid("00000001-6163-706d-7573-202001000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Business Administration", "Comprehensive business management and leadership program", true, "Business Administration", null },
                    { new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), new Guid("00000002-6163-706d-7573-202002000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Science in Nursing", "Registered nurse preparation program with clinical experience", true, "Nursing", null },
                    { new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), new Guid("00000002-6163-706d-7573-202002000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Information security and ethical hacking program", true, "Cybersecurity", null },
                    { new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), new Guid("00000002-6163-706d-7573-202002000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Modern marketing strategies and social media management", true, "Digital Marketing", null },
                    { new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), new Guid("00000003-6163-706d-7573-202003000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Automotive repair and maintenance certification program", true, "Automotive Technology", null },
                    { new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), new Guid("00000003-6163-706d-7573-202003000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Professional culinary techniques and food service management", true, "Culinary Arts", null },
                    { new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), new Guid("00000004-6163-706d-7573-202004000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Fine Arts", "Visual communication and digital design program", true, "Graphic Design", null },
                    { new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), new Guid("00000004-6163-706d-7573-202004000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Modern web technologies and full-stack development", true, "Web Development", null },
                    { new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), new Guid("00000005-6163-706d-7573-202005000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Law enforcement and criminal investigation program", true, "Criminal Justice", null },
                    { new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), new Guid("00000005-6163-706d-7573-202005000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Arts", "Child development and early learning education", true, "Early Childhood Education", null },
                    { new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), new Guid("00000006-6163-706d-7573-202006000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Heating, ventilation, and air conditioning systems", true, "HVAC Technology", null },
                    { new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), new Guid("00000006-6163-706d-7573-202006000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Physical therapy support and rehabilitation techniques", true, "Physical Therapy Assistant", null },
                    { new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), new Guid("00000006-6163-706d-7573-202006000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Animal care and veterinary assistant training", true, "Veterinary Technology", null },
                    { new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), new Guid("00000007-6163-706d-7573-202007000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Certificate", "Advanced welding techniques and metallurgy", true, "Welding Technology", null },
                    { new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), new Guid("00000007-6163-706d-7573-202007000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Oral health care and preventive dental services", true, "Dental Hygiene", null },
                    { new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), new Guid("00000007-6163-706d-7573-202007000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Arts", "Contemporary fashion design and merchandising", true, "Fashion Design", null },
                    { new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), new Guid("00000008-6163-706d-7573-202008000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Ecology and environmental conservation program", true, "Environmental Science", null },
                    { new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), new Guid("00000008-6163-706d-7573-202008000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Financial markets and investment analysis", true, "Finance", null },
                    { new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), new Guid("00000009-6163-706d-7573-202009000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Science", "Study of ocean ecosystems and marine life", true, "Marine Biology", null },
                    { new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), new Guid("00000009-6163-706d-7573-202009000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Solar and wind energy systems technology", true, "Renewable Energy Technology", null },
                    { new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), new Guid("00000009-6163-706d-7573-202009000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Oil and gas extraction technology", true, "Petroleum Engineering", null },
                    { new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), new Guid("0000000a-6163-706d-7573-20200a000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Bachelor of Science", "Modern software engineering and development practices", true, "Software Development", null },
                    { new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), new Guid("0000000a-6163-706d-7573-20200a000000"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Associate of Applied Science", "Modern farming and crop management techniques", true, "Agricultural Technology", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseCode", "CourseName", "CreatedDate", "CreditHours", "Description", "EnrollmentId", "IsActive" },
                values: new object[,]
                {
                    { new Guid("00000001-6f63-7275-7365-202001000000"), "CS101", "Introduction to Programming", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level introduction to programming skills development", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000002-6f63-7275-7365-202002000000"), "CS102", "Data Structures", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with data structures techniques", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000003-6f63-7275-7365-202003000000"), "CS201", "Algorithms", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of algorithms in computer science", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000004-6f63-7275-7365-202004000000"), "CS301", "Software Engineering", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in software engineering", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000005-6f63-7275-7365-202005000000"), "CS401", "Database Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level database systems skills development", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000006-6f63-7275-7365-202006000000"), "CS501", "Web Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in web development", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000007-6f63-7275-7365-202007000000"), "MATH150", "Calculus I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with calculus i techniques", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000008-6f63-7275-7365-202008000000"), "MATH250", "Discrete Mathematics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of discrete mathematics in computer science", new Guid("00000001-6e65-6f72-6c6c-6d6501000000"), true },
                    { new Guid("00000009-6f63-7275-7365-202009000000"), "ENGR101", "Engineering Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of engineering fundamentals in engineering", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("0000000a-6f63-7275-7365-20200a000000"), "PHYS210", "Physics for Engineers I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with physics for engineers i techniques", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("0000000b-6f63-7275-7365-20200b000000"), "MATH170", "Calculus for Engineers", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with calculus for engineers techniques", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("0000000c-6f63-7275-7365-20200c000000"), "CHEM110", "General Chemistry", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of general chemistry in engineering", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("0000000d-6f63-7275-7365-20200d000000"), "ENGR201", "Statics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of statics", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("0000000e-6f63-7275-7365-20200e000000"), "ENGR301", "Thermodynamics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level thermodynamics skills development", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("0000000f-6f63-7275-7365-20200f000000"), "ENGR401", "Design Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with design project techniques", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("00000010-6f63-7275-7365-202010000000"), "COMM101", "Technical Communication", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in technical communication", new Guid("00000002-6e65-6f72-6c6c-6d6502000000"), true },
                    { new Guid("00000011-6f63-7275-7365-202011000000"), "BUS101", "Introduction to Business", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of introduction to business in business administration", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000012-6f63-7275-7365-202012000000"), "ACC110", "Principles of Accounting", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of principles of accounting", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000013-6f63-7275-7365-202013000000"), "ECON101", "Microeconomics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in microeconomics", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000014-6f63-7275-7365-202014000000"), "MGMT201", "Management Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of management principles in business administration", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000015-6f63-7275-7365-202015000000"), "MKT101", "Marketing Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level marketing fundamentals skills development", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000016-6f63-7275-7365-202016000000"), "FIN201", "Business Finance", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of business finance in business administration", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000017-6f63-7275-7365-202017000000"), "BUS301", "Business Ethics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of business ethics", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000018-6f63-7275-7365-202018000000"), "STAT201", "Business Statistics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of business statistics in business administration", new Guid("00000003-6e65-6f72-6c6c-6d6503000000"), true },
                    { new Guid("00000019-6f63-7275-7365-202019000000"), "NUR100", "Introduction to Nursing", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level introduction to nursing skills development", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("0000001a-6f63-7275-7365-20201a000000"), "BIO201", "Anatomy & Physiology I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of anatomy & physiology i", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("0000001b-6f63-7275-7365-20201b000000"), "BIO202", "Anatomy & Physiology II", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with anatomy & physiology ii techniques", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("0000001c-6f63-7275-7365-20201c000000"), "NUR201", "Pharmacology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of pharmacology", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("0000001d-6f63-7275-7365-20201d000000"), "NUR301", "Clinical Nursing I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Comprehensive study of clinical nursing i in nursing", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("0000001e-6f63-7275-7365-20201e000000"), "NUR302", "Clinical Nursing II", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Professional-level clinical nursing ii skills development", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("0000001f-6f63-7275-7365-20201f000000"), "PSY101", "Introduction to Psychology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of introduction to psychology in nursing", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("00000020-6f63-7275-7365-202020000000"), "NUR401", "Nursing Leadership", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of nursing leadership in nursing", new Guid("00000004-6e65-6f72-6c6c-6d6504000000"), true },
                    { new Guid("00000021-6f63-7275-7365-202021000000"), "CYB101", "Introduction to Cybersecurity", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of introduction to cybersecurity in cybersecurity", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000022-6f63-7275-7365-202022000000"), "CYB201", "Network Security", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with network security techniques", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000023-6f63-7275-7365-202023000000"), "CYB301", "Ethical Hacking", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of ethical hacking in cybersecurity", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000024-6f63-7275-7365-202024000000"), "CYB401", "Digital Forensics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard digital forensics practices and procedures", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000025-6f63-7275-7365-202025000000"), "CS101", "Programming Basics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard programming basics practices and procedures", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000026-6f63-7275-7365-202026000000"), "NET201", "Network Administration", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of network administration", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000027-6f63-7275-7365-202027000000"), "CYB501", "Incident Response", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of incident response", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000028-6f63-7275-7365-202028000000"), "LAW301", "Cyber Law", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with cyber law techniques", new Guid("00000005-6e65-6f72-6c6c-6d6505000000"), true },
                    { new Guid("00000029-6f63-7275-7365-202029000000"), "MKT150", "Digital Marketing Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of digital marketing fundamentals in digital marketing", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("0000002a-6f63-7275-7365-20202a000000"), "MKT250", "Social Media Marketing", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of social media marketing", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("0000002b-6f63-7275-7365-20202b000000"), "MKT350", "Content Marketing", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of content marketing in digital marketing", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("0000002c-6f63-7275-7365-20202c000000"), "WEB101", "Web Analytics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of web analytics", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("0000002d-6f63-7275-7365-20202d000000"), "GD101", "Graphic Design Basics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level graphic design basics skills development", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("0000002e-6f63-7275-7365-20202e000000"), "MKT450", "Email Marketing", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with email marketing techniques", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("0000002f-6f63-7275-7365-20202f000000"), "BUS201", "Consumer Behavior", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of consumer behavior", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("00000030-6f63-7275-7365-202030000000"), "MKT501", "Marketing Strategy", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in marketing strategy", new Guid("00000006-6e65-6f72-6c6c-6d6506000000"), true },
                    { new Guid("00000031-6f63-7275-7365-202031000000"), "AUTO101", "Automotive Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard automotive fundamentals practices and procedures", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000032-6f63-7275-7365-202032000000"), "AUTO201", "Engine Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Hands-on experience with engine systems techniques", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000033-6f63-7275-7365-202033000000"), "AUTO301", "Transmission Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level transmission systems skills development", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000034-6f63-7275-7365-202034000000"), "AUTO401", "Electrical Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of electrical systems in automotive technology", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000035-6f63-7275-7365-202035000000"), "AUTO501", "Brake Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard brake systems practices and procedures", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000036-6f63-7275-7365-202036000000"), "AUTO601", "Suspension Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard suspension systems practices and procedures", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000037-6f63-7275-7365-202037000000"), "SHOP101", "Tool Safety", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Advanced concepts and practical applications in tool safety", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000038-6f63-7275-7365-202038000000"), "AUTO701", "Diagnostic Technology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level diagnostic technology skills development", new Guid("00000007-6e65-6f72-6c6c-6d6507000000"), true },
                    { new Guid("00000039-6f63-7275-7365-202039000000"), "CUL101", "Culinary Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of culinary fundamentals in culinary arts", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("0000003a-6f63-7275-7365-20203a000000"), "CUL201", "Baking & Pastry", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of baking & pastry in culinary arts", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("0000003b-6f63-7275-7365-20203b000000"), "CUL301", "Food Safety & Sanitation", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of food safety & sanitation", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("0000003c-6f63-7275-7365-20203c000000"), "CUL401", "Menu Planning", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in menu planning", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("0000003d-6f63-7275-7365-20203d000000"), "CUL501", "International Cuisine", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level international cuisine skills development", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("0000003e-6f63-7275-7365-20203e000000"), "BUS201", "Restaurant Management", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of restaurant management in culinary arts", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("0000003f-6f63-7275-7365-20203f000000"), "NUT101", "Nutrition Basics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of nutrition basics in culinary arts", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("00000040-6f63-7275-7365-202040000000"), "CUL601", "Advanced Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 5, "Advanced concepts and practical applications in advanced techniques", new Guid("00000008-6e65-6f72-6c6c-6d6508000000"), true },
                    { new Guid("00000041-6f63-7275-7365-202041000000"), "GD101", "Design Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with design fundamentals techniques", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000042-6f63-7275-7365-202042000000"), "GD201", "Typography", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with typography techniques", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000043-6f63-7275-7365-202043000000"), "GD301", "Digital Design Software", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard digital design software practices and procedures", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000044-6f63-7275-7365-202044000000"), "GD401", "Brand Identity", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level brand identity skills development", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000045-6f63-7275-7365-202045000000"), "ART101", "Drawing Basics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level drawing basics skills development", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000046-6f63-7275-7365-202046000000"), "GD501", "Web Design", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level web design skills development", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000047-6f63-7275-7365-202047000000"), "GD601", "Portfolio Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in portfolio development", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000048-6f63-7275-7365-202048000000"), "BUS301", "Design Business", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with design business techniques", new Guid("00000009-6e65-6f72-6c6c-6d6509000000"), true },
                    { new Guid("00000049-6f63-7275-7365-202049000000"), "WEB101", "HTML & CSS Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard html & css fundamentals practices and procedures", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("0000004a-6f63-7275-7365-20204a000000"), "WEB201", "JavaScript Programming", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with javascript programming techniques", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("0000004b-6f63-7275-7365-20204b000000"), "WEB301", "Backend Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of backend development in web development", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("0000004c-6f63-7275-7365-20204c000000"), "WEB401", "Database Integration", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of database integration", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("0000004d-6f63-7275-7365-20204d000000"), "WEB501", "Framework Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard framework development practices and procedures", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("0000004e-6f63-7275-7365-20204e000000"), "WEB601", "Mobile Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of mobile development in web development", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("0000004f-6f63-7275-7365-20204f000000"), "CS201", "Version Control", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 2, "Advanced concepts and practical applications in version control", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("00000050-6f63-7275-7365-202050000000"), "WEB701", "Deployment & DevOps", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in deployment & devops", new Guid("0000000a-6e65-6f72-6c6c-6d650a000000"), true },
                    { new Guid("00000051-6f63-7275-7365-202051000000"), "CRJU101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of program fundamentals in criminal justice", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000052-6f63-7275-7365-202052000000"), "CRJU102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in basic principles", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000053-6f63-7275-7365-202053000000"), "CRJU201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with intermediate concepts techniques", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000054-6f63-7275-7365-202054000000"), "CRJU202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of advanced theory", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000055-6f63-7275-7365-202055000000"), "CRJU301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of practical applications in criminal justice", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000056-6f63-7275-7365-202056000000"), "CRJU302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard specialized techniques practices and procedures", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000057-6f63-7275-7365-202057000000"), "CRJU401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of capstone project in criminal justice", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000058-6f63-7275-7365-202058000000"), "CRJU402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of professional practice in criminal justice", new Guid("0000000b-6e65-6f72-6c6c-6d650b000000"), true },
                    { new Guid("00000059-6f63-7275-7365-202059000000"), "ECE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in program fundamentals", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("0000005a-6f63-7275-7365-20205a000000"), "ECE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in basic principles", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("0000005b-6f63-7275-7365-20205b000000"), "ECE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level intermediate concepts skills development", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("0000005c-6f63-7275-7365-20205c000000"), "ECE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level advanced theory skills development", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("0000005d-6f63-7275-7365-20205d000000"), "ECE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of practical applications in early childhood education", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("0000005e-6f63-7275-7365-20205e000000"), "ECE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of specialized techniques in early childhood education", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("0000005f-6f63-7275-7365-20205f000000"), "ECE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard capstone project practices and procedures", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("00000060-6f63-7275-7365-202060000000"), "ECE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard professional practice practices and procedures", new Guid("0000000c-6e65-6f72-6c6c-6d650c000000"), true },
                    { new Guid("00000061-6f63-7275-7365-202061000000"), "HVTE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in program fundamentals", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000062-6f63-7275-7365-202062000000"), "HVTE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard basic principles practices and procedures", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000063-6f63-7275-7365-202063000000"), "HVTE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard intermediate concepts practices and procedures", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000064-6f63-7275-7365-202064000000"), "HVTE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in advanced theory", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000065-6f63-7275-7365-202065000000"), "HVTE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with practical applications techniques", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000066-6f63-7275-7365-202066000000"), "HVTE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with specialized techniques techniques", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000067-6f63-7275-7365-202067000000"), "HVTE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with capstone project techniques", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000068-6f63-7275-7365-202068000000"), "HVTE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in professional practice", new Guid("0000000d-6e65-6f72-6c6c-6d650d000000"), true },
                    { new Guid("00000069-6f63-7275-7365-202069000000"), "PTA101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in program fundamentals", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("0000006a-6f63-7275-7365-20206a000000"), "PTA102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in basic principles", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("0000006b-6f63-7275-7365-20206b000000"), "PTA201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of intermediate concepts in physical therapy assistant", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("0000006c-6f63-7275-7365-20206c000000"), "PTA202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in advanced theory", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("0000006d-6f63-7275-7365-20206d000000"), "PTA301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard practical applications practices and procedures", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("0000006e-6f63-7275-7365-20206e000000"), "PTA302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard specialized techniques practices and procedures", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("0000006f-6f63-7275-7365-20206f000000"), "PTA401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level capstone project skills development", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("00000070-6f63-7275-7365-202070000000"), "PTA402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Comprehensive study of professional practice in physical therapy assistant", new Guid("0000000e-6e65-6f72-6c6c-6d650e000000"), true },
                    { new Guid("00000071-6f63-7275-7365-202071000000"), "VETE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard program fundamentals practices and procedures", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000072-6f63-7275-7365-202072000000"), "VETE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in basic principles", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000073-6f63-7275-7365-202073000000"), "VETE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in intermediate concepts", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000074-6f63-7275-7365-202074000000"), "VETE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with advanced theory techniques", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000075-6f63-7275-7365-202075000000"), "VETE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with practical applications techniques", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000076-6f63-7275-7365-202076000000"), "VETE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level specialized techniques skills development", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000077-6f63-7275-7365-202077000000"), "VETE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of capstone project in veterinary technology", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000078-6f63-7275-7365-202078000000"), "VETE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with professional practice techniques", new Guid("0000000f-6e65-6f72-6c6c-6d650f000000"), true },
                    { new Guid("00000079-6f63-7275-7365-202079000000"), "WETE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of program fundamentals", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("0000007a-6f63-7275-7365-20207a000000"), "WETE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard basic principles practices and procedures", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("0000007b-6f63-7275-7365-20207b000000"), "WETE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of intermediate concepts", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("0000007c-6f63-7275-7365-20207c000000"), "WETE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of advanced theory", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("0000007d-6f63-7275-7365-20207d000000"), "WETE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard practical applications practices and procedures", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("0000007e-6f63-7275-7365-20207e000000"), "WETE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of specialized techniques", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("0000007f-6f63-7275-7365-20207f000000"), "WETE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with capstone project techniques", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("00000080-6f63-7275-7365-202080000000"), "WETE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard professional practice practices and procedures", new Guid("00000010-6e65-6f72-6c6c-6d6510000000"), true },
                    { new Guid("00000081-6f63-7275-7365-202081000000"), "DEHY101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level program fundamentals skills development", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000082-6f63-7275-7365-202082000000"), "DEHY102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of basic principles in dental hygiene", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000083-6f63-7275-7365-202083000000"), "DEHY201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of intermediate concepts in dental hygiene", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000084-6f63-7275-7365-202084000000"), "DEHY202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of advanced theory in dental hygiene", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000085-6f63-7275-7365-202085000000"), "DEHY301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in practical applications", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000086-6f63-7275-7365-202086000000"), "DEHY302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard specialized techniques practices and procedures", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000087-6f63-7275-7365-202087000000"), "DEHY401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level capstone project skills development", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000088-6f63-7275-7365-202088000000"), "DEHY402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of professional practice", new Guid("00000011-6e65-6f72-6c6c-6d6511000000"), true },
                    { new Guid("00000089-6f63-7275-7365-202089000000"), "FADE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with program fundamentals techniques", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("0000008a-6f63-7275-7365-20208a000000"), "FADE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of basic principles", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("0000008b-6f63-7275-7365-20208b000000"), "FADE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of intermediate concepts in fashion design", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("0000008c-6f63-7275-7365-20208c000000"), "FADE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard advanced theory practices and procedures", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("0000008d-6f63-7275-7365-20208d000000"), "FADE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard practical applications practices and procedures", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("0000008e-6f63-7275-7365-20208e000000"), "FADE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard specialized techniques practices and procedures", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("0000008f-6f63-7275-7365-20208f000000"), "FADE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in capstone project", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("00000090-6f63-7275-7365-202090000000"), "FADE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of professional practice", new Guid("00000012-6e65-6f72-6c6c-6d6512000000"), true },
                    { new Guid("00000091-6f63-7275-7365-202091000000"), "ENSC101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with program fundamentals techniques", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000092-6f63-7275-7365-202092000000"), "ENSC102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with basic principles techniques", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000093-6f63-7275-7365-202093000000"), "ENSC201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard intermediate concepts practices and procedures", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000094-6f63-7275-7365-202094000000"), "ENSC202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level advanced theory skills development", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000095-6f63-7275-7365-202095000000"), "ENSC301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level practical applications skills development", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000096-6f63-7275-7365-202096000000"), "ENSC302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in specialized techniques", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000097-6f63-7275-7365-202097000000"), "ENSC401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of capstone project", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000098-6f63-7275-7365-202098000000"), "ENSC402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with professional practice techniques", new Guid("00000013-6e65-6f72-6c6c-6d6513000000"), true },
                    { new Guid("00000099-6f63-7275-7365-202099000000"), "FINA101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level program fundamentals skills development", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("0000009a-6f63-7275-7365-20209a000000"), "FINA102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in basic principles", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("0000009b-6f63-7275-7365-20209b000000"), "FINA201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard intermediate concepts practices and procedures", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("0000009c-6f63-7275-7365-20209c000000"), "FINA202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with advanced theory techniques", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("0000009d-6f63-7275-7365-20209d000000"), "FINA301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in practical applications", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("0000009e-6f63-7275-7365-20209e000000"), "FINA302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in specialized techniques", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("0000009f-6f63-7275-7365-20209f000000"), "FINA401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in capstone project", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("000000a0-6f63-7275-7365-2020a0000000"), "FINA402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard professional practice practices and procedures", new Guid("00000014-6e65-6f72-6c6c-6d6514000000"), true },
                    { new Guid("000000a1-6f63-7275-7365-2020a1000000"), "MABI101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with program fundamentals techniques", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a2-6f63-7275-7365-2020a2000000"), "MABI102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of basic principles in marine biology", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a3-6f63-7275-7365-2020a3000000"), "MABI201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of intermediate concepts", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a4-6f63-7275-7365-2020a4000000"), "MABI202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level advanced theory skills development", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a5-6f63-7275-7365-2020a5000000"), "MABI301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard practical applications practices and procedures", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a6-6f63-7275-7365-2020a6000000"), "MABI302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level specialized techniques skills development", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a7-6f63-7275-7365-2020a7000000"), "MABI401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard capstone project practices and procedures", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a8-6f63-7275-7365-2020a8000000"), "MABI402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Industry-standard professional practice practices and procedures", new Guid("00000015-6e65-6f72-6c6c-6d6515000000"), true },
                    { new Guid("000000a9-6f63-7275-7365-2020a9000000"), "RET101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Fundamental principles and theory of program fundamentals", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000aa-6f63-7275-7365-2020aa000000"), "RET102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard basic principles practices and procedures", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000ab-6f63-7275-7365-2020ab000000"), "RET201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard intermediate concepts practices and procedures", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000ac-6f63-7275-7365-2020ac000000"), "RET202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of advanced theory in renewable energy technology", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000ad-6f63-7275-7365-2020ad000000"), "RET301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with practical applications techniques", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000ae-6f63-7275-7365-2020ae000000"), "RET302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in specialized techniques", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000af-6f63-7275-7365-2020af000000"), "RET401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with capstone project techniques", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000b0-6f63-7275-7365-2020b0000000"), "RET402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with professional practice techniques", new Guid("00000016-6e65-6f72-6c6c-6d6516000000"), true },
                    { new Guid("000000b1-6f63-7275-7365-2020b1000000"), "PEEN101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level program fundamentals skills development", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b2-6f63-7275-7365-2020b2000000"), "PEEN102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of basic principles in petroleum engineering", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b3-6f63-7275-7365-2020b3000000"), "PEEN201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level intermediate concepts skills development", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b4-6f63-7275-7365-2020b4000000"), "PEEN202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of advanced theory", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b5-6f63-7275-7365-2020b5000000"), "PEEN301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with practical applications techniques", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b6-6f63-7275-7365-2020b6000000"), "PEEN302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Professional-level specialized techniques skills development", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b7-6f63-7275-7365-2020b7000000"), "PEEN401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in capstone project", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b8-6f63-7275-7365-2020b8000000"), "PEEN402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Hands-on experience with professional practice techniques", new Guid("00000017-6e65-6f72-6c6c-6d6517000000"), true },
                    { new Guid("000000b9-6f63-7275-7365-2020b9000000"), "SODE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level program fundamentals skills development", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000ba-6f63-7275-7365-2020ba000000"), "SODE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Hands-on experience with basic principles techniques", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000bb-6f63-7275-7365-2020bb000000"), "SODE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in intermediate concepts", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000bc-6f63-7275-7365-2020bc000000"), "SODE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard advanced theory practices and procedures", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000bd-6f63-7275-7365-2020bd000000"), "SODE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of practical applications in software development", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000be-6f63-7275-7365-2020be000000"), "SODE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Comprehensive study of specialized techniques in software development", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000bf-6f63-7275-7365-2020bf000000"), "SODE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in capstone project", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000c0-6f63-7275-7365-2020c0000000"), "SODE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in professional practice", new Guid("00000018-6e65-6f72-6c6c-6d6518000000"), true },
                    { new Guid("000000c1-6f63-7275-7365-2020c1000000"), "AGTE101", "Program Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Advanced concepts and practical applications in program fundamentals", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c2-6f63-7275-7365-2020c2000000"), "AGTE102", "Basic Principles", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in basic principles", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c3-6f63-7275-7365-2020c3000000"), "AGTE201", "Intermediate Concepts", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Advanced concepts and practical applications in intermediate concepts", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c4-6f63-7275-7365-2020c4000000"), "AGTE202", "Advanced Theory", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Industry-standard advanced theory practices and procedures", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c5-6f63-7275-7365-2020c5000000"), "AGTE301", "Practical Applications", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of practical applications", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c6-6f63-7275-7365-2020c6000000"), "AGTE302", "Specialized Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of specialized techniques", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c7-6f63-7275-7365-2020c7000000"), "AGTE401", "Capstone Project", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, "Fundamental principles and theory of capstone project", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true },
                    { new Guid("000000c8-6f63-7275-7365-2020c8000000"), "AGTE402", "Professional Practice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Utc), 3, "Professional-level professional practice skills development", new Guid("00000019-6e65-6f72-6c6c-6d6519000000"), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_CampusCode",
                table: "Campuses",
                column: "CampusCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CourseCode_EnrollmentId",
                table: "Courses",
                columns: new[] { "CourseCode", "EnrollmentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_EnrollmentId",
                table: "Courses",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_CampusId",
                table: "Enrollments",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ProgramName_CampusId",
                table: "Enrollments",
                columns: new[] { "ProgramName", "CampusId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Terms_TermCode",
                table: "Terms",
                column: "TermCode",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_EnrollmentId",
                table: "Users",
                column: "EnrollmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Terms");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "Campuses");
        }
    }
}
