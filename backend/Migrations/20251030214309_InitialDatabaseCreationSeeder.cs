using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LearnerCenter.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabaseCreationSeeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    CampusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CampusCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.CampusId);
                });

            migrationBuilder.CreateTable(
                name: "Terms",
                columns: table => new
                {
                    TermId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TermName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TermCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegistrationEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Terms", x => x.TermId);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CampusId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProgramName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Degree = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    CourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CourseCode = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CourseName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreditHours = table.Column<int>(type: "int", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
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
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastLoginDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    EnrollmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    City = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    State = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    EmergencyContactName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    EmergencyContactPhone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
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
                    { new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), "123 University Ave", "SU-MAIN", "State University Main Campus", "Springfield", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@stateuniv.edu", true, "(555) 123-4567", "IL", "62701" },
                    { new Guid("a3b4c5d6-e7f8-9012-6789-345678901234"), "1400 Pine Street", "ECU-WA", "Emerald City University", "Seattle", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@emeraldcity.edu", true, "(206) 890-1234", "WA", "98101" },
                    { new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), "850 Alpine Way", "MVTI-CO", "Mountain View Technical Institute", "Denver", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admissions@mvti.edu", true, "(303) 234-5678", "CO", "80202" },
                    { new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), "456 College St", "CC-DOWN", "Community College Downtown", "Springfield", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admissions@ccdowntown.edu", true, "(555) 234-5678", "IL", "62702" },
                    { new Guid("b4c5d6e7-f8a9-0123-7890-456789012345"), "625 Mountain View Lane", "BRCC-VA", "Blue Ridge Community College", "Richmond", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@blueridgecc.edu", true, "(804) 901-2345", "VA", "23219" },
                    { new Guid("b8c9d0e1-f2a3-4567-1234-890123456789"), "2500 Rodeo Boulevard", "LSU-TX", "Lone Star University", "Houston", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@lonestaruniv.edu", true, "(713) 345-6789", "TX", "77002" },
                    { new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), "789 Tech Blvd", "TI-NORTH", "Technical Institute North", "Rockford", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@technorth.edu", true, "(555) 345-6789", "IL", "61101" },
                    { new Guid("c5d6e7f8-a9b0-1234-8901-567890123456"), "1100 Peachtree Street", "PSI-GA", "Peach State Institute", "Atlanta", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admissions@peachstate.edu", true, "(404) 012-3456", "GA", "30309" },
                    { new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), "777 Market Street", "GGC-SF", "Golden Gate College", "San Francisco", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "contact@goldengatecc.edu", true, "(415) 456-7890", "CA", "94103" },
                    { new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), "350 Fifth Avenue", "ESI-NYC", "Empire State Institute", "New York", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@empireinst.edu", true, "(212) 567-8901", "NY", "10118" },
                    { new Guid("d4e5f6a7-b8c9-0123-def0-456789012345"), "321 Arts Way", "LAC-MAIN", "Liberal Arts College", "Peoria", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@liberalarts.edu", true, "(555) 456-7890", "IL", "61602" },
                    { new Guid("e1f2a3b4-c5d6-7890-4567-123456789012"), "1500 Cactus Road", "DIU-AZ", "Desert Innovation University", "Phoenix", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admissions@desertinnovation.edu", true, "(602) 678-9012", "AZ", "85001" },
                    { new Guid("e5f6a7b8-c9d0-1234-ef01-567890123456"), "654 Commerce Dr", "BSC-CENT", "Business School Central", "Chicago", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admissions@businesscentral.edu", true, "(555) 567-8901", "IL", "60601" },
                    { new Guid("f2a3b4c5-d6e7-8901-5678-234567890123"), "900 Harbor Drive", "GLTC-MI", "Great Lakes Technical College", "Detroit", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@greatlakestech.edu", true, "(313) 789-0123", "MI", "48201" },
                    { new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), "1200 Ocean Drive", "SCC-MAIN", "Sunshine Community College", "Miami", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "info@sunshinecc.edu", true, "(305) 123-4567", "FL", "33139" }
                });

            migrationBuilder.InsertData(
                table: "Terms",
                columns: new[] { "TermId", "CreatedDate", "EndDate", "IsActive", "RegistrationEndDate", "RegistrationStartDate", "StartDate", "TermCode", "TermName" },
                values: new object[,]
                {
                    { new Guid("11111111-aaaa-bbbb-cccc-111111111111"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2024, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "F24", "Fall 2024" },
                    { new Guid("22222222-bbbb-cccc-dddd-222222222222"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2024, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "S25", "Spring 2025" },
                    { new Guid("33333333-cccc-dddd-eeee-333333333333"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "SU25", "Summer 2025" },
                    { new Guid("44444444-dddd-eeee-ffff-444444444444"), new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "F25", "Fall 2025" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "EnrollmentId", "CampusId", "CreatedDate", "Degree", "Description", "IsActive", "ProgramName", "UpdatedDate" },
                values: new object[,]
                {
                    { new Guid("09876543-210f-edcb-a098-765432109876"), new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Arts", "Contemporary fashion design and merchandising", true, "Fashion Design", null },
                    { new Guid("10987654-3210-fedc-ba09-876543210987"), new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Financial markets and investment banking", true, "Finance and Banking", null },
                    { new Guid("1a2b3c4d-5e6f-7890-abcd-ef1234567891"), new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Professional culinary techniques and food service management", true, "Culinary Arts", null },
                    { new Guid("2b3c4d5e-6f70-8901-bcde-f12345678902"), new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Business Administration", "Online marketing strategies and social media management", true, "Digital Marketing", null },
                    { new Guid("3c4d5e6f-7080-9012-cdef-123456789013"), new Guid("b4c5d6e7-f8a9-0123-7890-456789012345"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Physical therapy support and rehabilitation techniques", true, "Physical Therapy Assistant", null },
                    { new Guid("43210987-cba0-9876-5432-109876543210"), new Guid("c5d6e7f8-a9b0-1234-8901-567890123456"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Modern farming and crop management", true, "Agricultural Technology", null },
                    { new Guid("4d5e6f70-8090-1234-def0-234567890124"), new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Modern web technologies and full-stack development", true, "Web Development", null },
                    { new Guid("54321098-dcba-0987-6543-210987654321"), new Guid("b4c5d6e7-f8a9-0123-7890-456789012345"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Science", "Pre-health and medical assistant preparation", true, "Health Sciences", null },
                    { new Guid("5e6f7080-90a0-2345-ef01-345678901235"), new Guid("b4c5d6e7-f8a9-0123-7890-456789012345"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Oral health care and preventive dental services", true, "Dental Hygiene", null },
                    { new Guid("65432109-edcb-a098-7654-321098765432"), new Guid("a3b4c5d6-e7f8-9012-6789-345678901234"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Ecology and environmental conservation", true, "Environmental Science", null },
                    { new Guid("6f708090-a0b0-3456-f012-456789012346"), new Guid("f2a3b4c5-d6e7-8901-5678-234567890123"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Animal care and veterinary assistant training", true, "Veterinary Technology", null },
                    { new Guid("708090a0-b0c0-4567-0123-567890123457"), new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Certificate", "Advanced welding techniques and metallurgy", true, "Welding Technology", null },
                    { new Guid("76543210-fedc-ba09-8765-432109876543"), new Guid("f2a3b4c5-d6e7-8901-5678-234567890123"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Vehicle design and manufacturing technology", true, "Automotive Engineering", null },
                    { new Guid("8090a0b0-c0d0-5678-1234-678901234568"), new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Arts", "Visual communication and digital design", true, "Graphic Design", null },
                    { new Guid("87654321-0fed-cba0-9876-543210987654"), new Guid("e1f2a3b4-c5d6-7890-4567-123456789012"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Information security and ethical hacking", true, "Cybersecurity", null },
                    { new Guid("90a0b0c0-d0e0-6789-2345-789012345679"), new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Law enforcement and criminal investigation", true, "Criminal Justice", null },
                    { new Guid("a0b0c0d0-e0f0-789a-3456-890123456780"), new Guid("c5d6e7f8-a9b0-1234-8901-567890123456"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Arts", "Child development and early learning education", true, "Early Childhood Education", null },
                    { new Guid("a3b21098-7654-3210-fedc-ba0987654321"), new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Modern software engineering and development", true, "Software Development", null },
                    { new Guid("a3b4c5d6-e7f8-9012-6789-345678901234"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Industrial and residential electrical systems", true, "Electrical Technology", null },
                    { new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Engineering", "Engineering fundamentals with specializations available", true, "Engineering Program", null },
                    { new Guid("a9b8c7d6-e5f4-3210-9876-543210fedcba"), new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Science", "Study of ocean ecosystems and marine life", true, "Marine Biology", null },
                    { new Guid("b0c0d0e0-f0a1-89ab-4567-901234567891"), new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Heating, ventilation, and air conditioning systems", true, "HVAC Technology", null },
                    { new Guid("b2109876-5432-10fe-dcba-098765432109"), new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Fine Arts", "Digital design and multimedia production", true, "Digital Media Arts", null },
                    { new Guid("b4c5d6e7-f8a9-0123-7890-456789012345"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Certificate", "Professional welding techniques and safety", true, "Welding Certification", null },
                    { new Guid("b8c7d6e5-f4a3-2109-8765-4321fedcba09"), new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Tourism and hotel management program", true, "Hospitality Management", null },
                    { new Guid("b8c9d0e1-f2a3-4567-1234-890123456789"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Pre-medical preparation with required science courses", true, "Pre-Medicine Track", null },
                    { new Guid("c5d6e7f8-a9b0-1234-8901-567890123456"), new Guid("d4e5f6a7-b8c9-0123-def0-456789012345"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Fine Arts", "Creative arts with studio and theory components", true, "Fine Arts Degree", null },
                    { new Guid("c7d6e5f4-a3b2-1098-7654-321fedcba098"), new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Solar and wind energy systems technology", true, "Renewable Energy Technology", null },
                    { new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Practical business skills and management fundamentals", true, "Business Administration", null },
                    { new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Science in Nursing", "Registered nurse preparation program", true, "Nursing Program", null },
                    { new Guid("d6e5f4a3-b210-9876-5432-1fedcba09876"), new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Certificate", "Emergency response in mountainous terrain", true, "Mountain Rescue Operations", null },
                    { new Guid("d6e7f8a9-b0c1-2345-9012-678901234567"), new Guid("d4e5f6a7-b8c9-0123-def0-456789012345"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Arts", "English literature and creative writing program", true, "Literature and Writing", null },
                    { new Guid("e1f2a3b4-c5d6-7890-4567-123456789012"), new Guid("b2c3d4e5-f6a7-8901-bcde-f23456789012"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Arts", "Liberal arts foundation for university transfer", true, "General Education Transfer", null },
                    { new Guid("e5f4a3b2-1098-7654-3210-fedcba098765"), new Guid("b8c9d0e1-f2a3-4567-1234-890123456789"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Oil and gas extraction technology", true, "Petroleum Engineering", null },
                    { new Guid("e7f8a9b0-c1d2-3456-0123-789012345678"), new Guid("e5f6a7b8-c9d0-1234-ef01-567890123456"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Master of Business Administration", "Advanced business management and leadership", true, "MBA Program", null },
                    { new Guid("f2a3b4c5-d6e7-8901-5678-234567890123"), new Guid("c3d4e5f6-a7b8-9012-cdef-345678901234"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Certificate", "Automotive repair and maintenance certification", true, "Automotive Technology", null },
                    { new Guid("f4a3b210-9876-5432-10fe-dcba09876543"), new Guid("b8c9d0e1-f2a3-4567-1234-890123456789"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Associate of Applied Science", "Livestock and agricultural operations", true, "Ranch Management", null },
                    { new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), new Guid("a1b2c3d4-e5f6-7890-abcd-ef1234567890"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Comprehensive computer science program covering programming, algorithms, and software engineering", true, "Computer Science Degree", null },
                    { new Guid("f8a9b0c1-d2e3-4567-1234-890123456789"), new Guid("e5f6a7b8-c9d0-1234-ef01-567890123456"), new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bachelor of Science", "Financial accounting and business finance", true, "Accounting Degree", null }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "CourseId", "CourseCode", "CourseName", "CreatedDate", "CreditHours", "Description", "EnrollmentId", "IsActive" },
                values: new object[,]
                {
                    { new Guid("00777777-1111-2222-3333-444444444444"), "CHEM130", "General Chemistry I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Basic chemical principles", new Guid("b8c9d0e1-f2a3-4567-1234-890123456789"), true },
                    { new Guid("11888888-2222-3333-4444-555555555555"), "BUS101", "Introduction to Business", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Business fundamentals", new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), true },
                    { new Guid("1a2b3c4d-5e6f-7890-abcd-ef1234567890"), "MA101", "Medical Terminology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Medical language and terminology", new Guid("54321098-dcba-0987-6543-210987654321"), true },
                    { new Guid("22999999-3333-4444-5555-666666666666"), "ACC110", "Basic Accounting", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Accounting principles", new Guid("c9d0e1f2-a3b4-5678-2345-901234567890"), true },
                    { new Guid("2b3c4d5e-6f70-8901-bcde-f12345678901"), "MA201", "Clinical Procedures", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Basic clinical procedures for medical assistants", new Guid("54321098-dcba-0987-6543-210987654321"), true },
                    { new Guid("33aaaaaa-4444-5555-6666-777777777777"), "NUR100", "Introduction to Nursing", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Basic nursing concepts", new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), true },
                    { new Guid("3c4d5e6f-7080-9012-cdef-123456789012"), "CYB101", "Introduction to Cybersecurity", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Fundamentals of cybersecurity", new Guid("87654321-0fed-cba0-9876-543210987654"), true },
                    { new Guid("44bbbbbb-5555-6666-7777-888888888888"), "BIO201", "Anatomy & Physiology I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Human body systems", new Guid("d0e1f2a3-b4c5-6789-3456-012345678901"), true },
                    { new Guid("4d5e6f70-8090-1234-def0-234567890123"), "CYB301", "Network Security", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Network security protocols and defense", new Guid("87654321-0fed-cba0-9876-543210987654"), true },
                    { new Guid("55cccccc-6666-7777-8888-999999999999"), "AUTO120", "Automotive Basics", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Introduction to automotive systems", new Guid("f2a3b4c5-d6e7-8901-5678-234567890123"), true },
                    { new Guid("5e6f7080-90a0-2345-ef01-345678901234"), "CUL110", "Culinary Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Basic cooking techniques and food safety", new Guid("1a2b3c4d-5e6f-7890-abcd-ef1234567891"), true },
                    { new Guid("66dddddd-7777-8888-9999-aaaaaaaaaaaa"), "AUTO220", "Engine Repair", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Engine diagnosis and repair", new Guid("f2a3b4c5-d6e7-8901-5678-234567890123"), true },
                    { new Guid("6f708090-a0b0-3456-f012-456789012345"), "CUL220", "Advanced Culinary Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Professional cooking methods", new Guid("1a2b3c4d-5e6f-7890-abcd-ef1234567891"), true },
                    { new Guid("708090a0-b0c0-4567-0123-567890123456"), "MKTG150", "Digital Marketing Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Introduction to digital marketing strategies", new Guid("2b3c4d5e-6f70-8901-bcde-f12345678902"), true },
                    { new Guid("77eeeeee-8888-9999-aaaa-bbbbbbbbbbbb"), "ART110", "Drawing Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Basic drawing techniques", new Guid("c5d6e7f8-a9b0-1234-8901-567890123456"), true },
                    { new Guid("8090a0b0-c0d0-5678-1234-678901234567"), "MKTG350", "Social Media Marketing", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Social media strategy and analytics", new Guid("2b3c4d5e-6f70-8901-bcde-f12345678902"), true },
                    { new Guid("88ffffff-9999-aaaa-bbbb-cccccccccccc"), "ART210", "Painting I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Introduction to painting", new Guid("c5d6e7f8-a9b0-1234-8901-567890123456"), true },
                    { new Guid("90a0b0c0-d0e0-6789-2345-789012345678"), "PTA101", "Introduction to Physical Therapy", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Foundations of physical therapy practice", new Guid("3c4d5e6f-7080-9012-cdef-123456789013"), true },
                    { new Guid("99000000-aaaa-bbbb-cccc-dddddddddddd"), "MBA600", "Strategic Management", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Advanced business strategy", new Guid("e7f8a9b0-c1d2-3456-0123-789012345678"), true },
                    { new Guid("a0b0c0d0-e0f0-789a-3456-890123456789"), "PTA201", "Therapeutic Exercise", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Exercise principles and techniques", new Guid("3c4d5e6f-7080-9012-cdef-123456789013"), true },
                    { new Guid("a1b2c3d4-e5f6-def0-9abc-456789012345"), "VET201", "Veterinary Pharmacology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Animal medications and dosages", new Guid("6f708090-a0b0-3456-f012-456789012346"), true },
                    { new Guid("a3b4c5d6-e7f8-9abc-5678-678901234567"), "FASH301", "Pattern Making", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Creating patterns for garment construction", new Guid("09876543-210f-edcb-a098-765432109876"), true },
                    { new Guid("a7b8c9d0-e1f2-3456-f012-012345678901"), "CJ301", "Criminal Investigation", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Investigation techniques and procedures", new Guid("90a0b0c0-d0e0-6789-2345-789012345679"), true },
                    { new Guid("aa111111-bbbb-cccc-dddd-eeeeeeeeeeee"), "CS101", "Introduction to Programming", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Basic programming concepts and problem solving", new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), true },
                    { new Guid("aa111111-bbbb-cccc-dddd-eeeeeeeeeeef"), "MBA610", "Financial Management", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Corporate finance principles", new Guid("e7f8a9b0-c1d2-3456-0123-789012345678"), true },
                    { new Guid("b0c0d0e0-f0a1-89ab-4567-901234567890"), "WEB101", "HTML & CSS Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Web development basics", new Guid("4d5e6f70-8090-1234-def0-234567890124"), true },
                    { new Guid("b2c3d4e5-f6a7-ef01-abcd-567890123456"), "WELD101", "Introduction to Welding", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Basic welding processes and safety", new Guid("708090a0-b0c0-4567-0123-567890123457"), true },
                    { new Guid("b8c9d0e1-f2a3-4567-0123-123456789012"), "ECE101", "Child Development", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Physical, cognitive, and social development", new Guid("a0b0c0d0-e0f0-789a-3456-890123456780"), true },
                    { new Guid("bb222222-cccc-dddd-eeee-ffffffffffff"), "CS102", "Data Structures", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Fundamental data structures and algorithms", new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), true },
                    { new Guid("c0d0e0f0-a1b2-9abc-5678-012345678901"), "WEB301", "JavaScript Programming", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Client-side and server-side JavaScript", new Guid("4d5e6f70-8090-1234-def0-234567890124"), true },
                    { new Guid("c3d4e5f6-a7b8-f012-bcde-678901234567"), "WELD301", "Advanced Welding Techniques", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Specialized welding methods", new Guid("708090a0-b0c0-4567-0123-567890123457"), true },
                    { new Guid("c9d0e1f2-a3b4-5678-1234-234567890123"), "ECE201", "Curriculum Planning", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Age-appropriate curriculum development", new Guid("a0b0c0d0-e0f0-789a-3456-890123456780"), true },
                    { new Guid("cc333333-dddd-eeee-ffff-000000000000"), "MATH150", "Calculus I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Differential calculus and applications", new Guid("f6a7b8c9-d0e1-2345-f012-678901234567"), true },
                    { new Guid("d0e0f0a1-b2c3-abcd-6789-123456789012"), "DH101", "Oral Anatomy", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Structure and function of oral cavity", new Guid("5e6f7080-90a0-2345-ef01-345678901235"), true },
                    { new Guid("d0e1f2a3-b4c5-6789-2345-345678901234"), "HVAC101", "HVAC Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Heating, ventilation, and air conditioning basics", new Guid("b0c0d0e0-f0a1-89ab-4567-901234567891"), true },
                    { new Guid("d4e5f6a7-b8c9-0123-cdef-789012345678"), "GD101", "Design Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Basic principles of graphic design", new Guid("8090a0b0-c0d0-5678-1234-678901234568"), true },
                    { new Guid("dd444444-eeee-ffff-0000-111111111111"), "ENGR101", "Engineering Fundamentals", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Introduction to engineering principles", new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), true },
                    { new Guid("e0f0a1b2-c3d4-bcde-789a-234567890123"), "DH201", "Periodontal Therapy", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Treatment of gum disease", new Guid("5e6f7080-90a0-2345-ef01-345678901235"), true },
                    { new Guid("e1f2a3b4-c5d6-789a-3456-456789012345"), "HVAC301", "Refrigeration Systems", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Commercial and residential refrigeration", new Guid("b0c0d0e0-f0a1-89ab-4567-901234567891"), true },
                    { new Guid("e5f6a7b8-c9d0-1234-def0-890123456789"), "GD301", "Digital Design Software", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Adobe Creative Suite mastery", new Guid("8090a0b0-c0d0-5678-1234-678901234568"), true },
                    { new Guid("ee555555-ffff-0000-1111-222222222222"), "PHYS210", "Physics for Engineers I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Mechanics and thermodynamics", new Guid("a7b8c9d0-e1f2-3456-0123-789012345678"), true },
                    { new Guid("f0a1b2c3-d4e5-cdef-89ab-345678901234"), "VET101", "Animal Anatomy & Physiology", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Animal body systems and functions", new Guid("6f708090-a0b0-3456-f012-456789012346"), true },
                    { new Guid("f2a3b4c5-d6e7-89ab-4567-567890123456"), "FASH101", "Fashion Illustration", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Fashion drawing and design techniques", new Guid("09876543-210f-edcb-a098-765432109876"), true },
                    { new Guid("f6a7b8c9-d0e1-2345-ef01-901234567890"), "CJ101", "Introduction to Criminal Justice", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Overview of the criminal justice system", new Guid("90a0b0c0-d0e0-6789-2345-789012345679"), true },
                    { new Guid("ff666666-0000-1111-2222-333333333333"), "BIO120", "General Biology I", new DateTime(2024, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Cell biology and genetics", new Guid("b8c9d0e1-f2a3-4567-1234-890123456789"), true }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Campuses_CampusCode",
                table: "Campuses",
                column: "CampusCode",
                unique: true,
                filter: "[CampusCode] IS NOT NULL");

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
