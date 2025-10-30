using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Models;

namespace LearnerCenter.API.Data.Seeders
{
    public static class BasicDataSeeder
    {
        public static void SeedBasicData(ModelBuilder modelBuilder)
        {
            // Create fixed GUIDs for consistent seeding (15 campuses total)
            var campus1Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890");
            var campus2Id = Guid.Parse("b2c3d4e5-f6a7-8901-bcde-f23456789012");
            var campus3Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234");
            var campus4Id = Guid.Parse("d4e5f6a7-b8c9-0123-def0-456789012345");
            var campus5Id = Guid.Parse("e5f6a7b8-c9d0-1234-ef01-567890123456");
            var campus6Id = Guid.Parse("f6a7b8c9-d0e1-2345-f012-678901234567");
            var campus7Id = Guid.Parse("a7b8c9d0-e1f2-3456-0123-789012345678");
            var campus8Id = Guid.Parse("b8c9d0e1-f2a3-4567-1234-890123456789");
            var campus9Id = Guid.Parse("c9d0e1f2-a3b4-5678-2345-901234567890");
            var campus10Id = Guid.Parse("d0e1f2a3-b4c5-6789-3456-012345678901");
            var campus11Id = Guid.Parse("e1f2a3b4-c5d6-7890-4567-123456789012");
            var campus12Id = Guid.Parse("f2a3b4c5-d6e7-8901-5678-234567890123");
            var campus13Id = Guid.Parse("a3b4c5d6-e7f8-9012-6789-345678901234");
            var campus14Id = Guid.Parse("b4c5d6e7-f8a9-0123-7890-456789012345");
            var campus15Id = Guid.Parse("c5d6e7f8-a9b0-1234-8901-567890123456");

            // Seed Campuses
            modelBuilder.Entity<Campus>().HasData(
                new Campus
                {
                    CampusId = campus1Id,
                    CampusName = "State University Main Campus",
                    CampusCode = "SU-MAIN",
                    Address = "123 University Ave",
                    City = "Springfield",
                    State = "IL",
                    ZipCode = "62701",
                    PhoneNumber = "(555) 123-4567",
                    Email = "info@stateuniv.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus2Id,
                    CampusName = "Community College Downtown",
                    CampusCode = "CC-DOWN",
                    Address = "456 College St",
                    City = "Springfield",
                    State = "IL",
                    ZipCode = "62702",
                    PhoneNumber = "(555) 234-5678",
                    Email = "admissions@ccdowntown.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus3Id,
                    CampusName = "Technical Institute North",
                    CampusCode = "TI-NORTH",
                    Address = "789 Tech Blvd",
                    City = "Rockford",
                    State = "IL",
                    ZipCode = "61101",
                    PhoneNumber = "(555) 345-6789",
                    Email = "contact@technorth.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus4Id,
                    CampusName = "Liberal Arts College",
                    CampusCode = "LAC-MAIN",
                    Address = "321 Arts Way",
                    City = "Peoria",
                    State = "IL",
                    ZipCode = "61602",
                    PhoneNumber = "(555) 456-7890",
                    Email = "info@liberalarts.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus5Id,
                    CampusName = "Business School Central",
                    CampusCode = "BSC-CENT",
                    Address = "654 Commerce Dr",
                    City = "Chicago",
                    State = "IL",
                    ZipCode = "60601",
                    PhoneNumber = "(555) 567-8901",
                    Email = "admissions@businesscentral.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                // Additional campuses across different states
                new Campus
                {
                    CampusId = campus6Id,
                    CampusName = "Sunshine Community College",
                    CampusCode = "SCC-MAIN",
                    Address = "1200 Ocean Drive",
                    City = "Miami",
                    State = "FL",
                    ZipCode = "33139",
                    PhoneNumber = "(305) 123-4567",
                    Email = "info@sunshinecc.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus7Id,
                    CampusName = "Mountain View Technical Institute",
                    CampusCode = "MVTI-CO",
                    Address = "850 Alpine Way",
                    City = "Denver",
                    State = "CO",
                    ZipCode = "80202",
                    PhoneNumber = "(303) 234-5678",
                    Email = "admissions@mvti.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus8Id,
                    CampusName = "Lone Star University",
                    CampusCode = "LSU-TX",
                    Address = "2500 Rodeo Boulevard",
                    City = "Houston",
                    State = "TX",
                    ZipCode = "77002",
                    PhoneNumber = "(713) 345-6789",
                    Email = "info@lonestaruniv.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus9Id,
                    CampusName = "Golden Gate College",
                    CampusCode = "GGC-SF",
                    Address = "777 Market Street",
                    City = "San Francisco",
                    State = "CA",
                    ZipCode = "94103",
                    PhoneNumber = "(415) 456-7890",
                    Email = "contact@goldengatecc.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus10Id,
                    CampusName = "Empire State Institute",
                    CampusCode = "ESI-NYC",
                    Address = "350 Fifth Avenue",
                    City = "New York",
                    State = "NY",
                    ZipCode = "10118",
                    PhoneNumber = "(212) 567-8901",
                    Email = "info@empireinst.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus11Id,
                    CampusName = "Desert Innovation University",
                    CampusCode = "DIU-AZ",
                    Address = "1500 Cactus Road",
                    City = "Phoenix",
                    State = "AZ",
                    ZipCode = "85001",
                    PhoneNumber = "(602) 678-9012",
                    Email = "admissions@desertinnovation.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus12Id,
                    CampusName = "Great Lakes Technical College",
                    CampusCode = "GLTC-MI",
                    Address = "900 Harbor Drive",
                    City = "Detroit",
                    State = "MI",
                    ZipCode = "48201",
                    PhoneNumber = "(313) 789-0123",
                    Email = "info@greatlakestech.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus13Id,
                    CampusName = "Emerald City University",
                    CampusCode = "ECU-WA",
                    Address = "1400 Pine Street",
                    City = "Seattle",
                    State = "WA",
                    ZipCode = "98101",
                    PhoneNumber = "(206) 890-1234",
                    Email = "contact@emeraldcity.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus14Id,
                    CampusName = "Blue Ridge Community College",
                    CampusCode = "BRCC-VA",
                    Address = "625 Mountain View Lane",
                    City = "Richmond",
                    State = "VA",
                    ZipCode = "23219",
                    PhoneNumber = "(804) 901-2345",
                    Email = "info@blueridgecc.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                },
                new Campus
                {
                    CampusId = campus15Id,
                    CampusName = "Peach State Institute",
                    CampusCode = "PSI-GA",
                    Address = "1100 Peachtree Street",
                    City = "Atlanta",
                    State = "GA",
                    ZipCode = "30309",
                    PhoneNumber = "(404) 012-3456",
                    Email = "admissions@peachstate.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                }
            );

            // Seed Terms
            modelBuilder.Entity<Term>().HasData(
                new Term
                {
                    TermId = Guid.Parse("11111111-aaaa-bbbb-cccc-111111111111"),
                    TermName = "Fall 2024",
                    TermCode = "F24",
                    StartDate = new DateTime(2024, 8, 15),
                    EndDate = new DateTime(2024, 12, 15),
                    RegistrationStartDate = new DateTime(2024, 7, 1),
                    RegistrationEndDate = new DateTime(2024, 8, 10),
                    CreatedDate = new DateTime(2024, 6, 1)
                },
                new Term
                {
                    TermId = Guid.Parse("22222222-bbbb-cccc-dddd-222222222222"),
                    TermName = "Spring 2025",
                    TermCode = "S25",
                    StartDate = new DateTime(2025, 1, 15),
                    EndDate = new DateTime(2025, 5, 15),
                    RegistrationStartDate = new DateTime(2024, 11, 1),
                    RegistrationEndDate = new DateTime(2025, 1, 10),
                    CreatedDate = new DateTime(2024, 6, 1)
                },
                new Term
                {
                    TermId = Guid.Parse("33333333-cccc-dddd-eeee-333333333333"),
                    TermName = "Summer 2025",
                    TermCode = "SU25",
                    StartDate = new DateTime(2025, 6, 1),
                    EndDate = new DateTime(2025, 8, 15),
                    RegistrationStartDate = new DateTime(2025, 4, 1),
                    RegistrationEndDate = new DateTime(2025, 5, 25),
                    CreatedDate = new DateTime(2024, 6, 1)
                },
                new Term
                {
                    TermId = Guid.Parse("44444444-dddd-eeee-ffff-444444444444"),
                    TermName = "Fall 2025",
                    TermCode = "F25",
                    StartDate = new DateTime(2025, 8, 15),
                    EndDate = new DateTime(2025, 12, 15),
                    RegistrationStartDate = new DateTime(2025, 7, 1),
                    RegistrationEndDate = new DateTime(2025, 8, 10),
                    CreatedDate = new DateTime(2024, 6, 1)
                }
            );

            // Seed Enrollment Programs
            modelBuilder.Entity<Enrollment>().HasData(
                // State University Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("f6a7b8c9-d0e1-2345-f012-678901234567"),
                    CampusId = campus1Id,
                    ProgramName = "Computer Science Degree",
                    Degree = "Bachelor of Science",
                    Description = "Comprehensive computer science program covering programming, algorithms, and software engineering",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("a7b8c9d0-e1f2-3456-0123-789012345678"),
                    CampusId = campus1Id,
                    ProgramName = "Engineering Program",
                    Degree = "Bachelor of Engineering",
                    Description = "Engineering fundamentals with specializations available",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("b8c9d0e1-f2a3-4567-1234-890123456789"),
                    CampusId = campus1Id,
                    ProgramName = "Pre-Medicine Track",
                    Degree = "Bachelor of Science",
                    Description = "Pre-medical preparation with required science courses",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Community College Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("c9d0e1f2-a3b4-5678-2345-901234567890"),
                    CampusId = campus2Id,
                    ProgramName = "Business Administration",
                    Degree = "Associate of Applied Science",
                    Description = "Practical business skills and management fundamentals",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("d0e1f2a3-b4c5-6789-3456-012345678901"),
                    CampusId = campus2Id,
                    ProgramName = "Nursing Program",
                    Degree = "Associate of Science in Nursing",
                    Description = "Registered nurse preparation program",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("e1f2a3b4-c5d6-7890-4567-123456789012"),
                    CampusId = campus2Id,
                    ProgramName = "General Education Transfer",
                    Degree = "Associate of Arts",
                    Description = "Liberal arts foundation for university transfer",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Technical Institute Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("f2a3b4c5-d6e7-8901-5678-234567890123"),
                    CampusId = campus3Id,
                    ProgramName = "Automotive Technology",
                    Degree = "Certificate",
                    Description = "Automotive repair and maintenance certification",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("a3b4c5d6-e7f8-9012-6789-345678901234"),
                    CampusId = campus3Id,
                    ProgramName = "Electrical Technology",
                    Degree = "Associate of Applied Science",
                    Description = "Industrial and residential electrical systems",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("b4c5d6e7-f8a9-0123-7890-456789012345"),
                    CampusId = campus3Id,
                    ProgramName = "Welding Certification",
                    Degree = "Certificate",
                    Description = "Professional welding techniques and safety",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Liberal Arts Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("c5d6e7f8-a9b0-1234-8901-567890123456"),
                    CampusId = campus4Id,
                    ProgramName = "Fine Arts Degree",
                    Degree = "Bachelor of Fine Arts",
                    Description = "Creative arts with studio and theory components",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("d6e7f8a9-b0c1-2345-9012-678901234567"),
                    CampusId = campus4Id,
                    ProgramName = "Literature and Writing",
                    Degree = "Bachelor of Arts",
                    Description = "English literature and creative writing program",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Business School Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("e7f8a9b0-c1d2-3456-0123-789012345678"),
                    CampusId = campus5Id,
                    ProgramName = "MBA Program",
                    Degree = "Master of Business Administration",
                    Description = "Advanced business management and leadership",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("f8a9b0c1-d2e3-4567-1234-890123456789"),
                    CampusId = campus5Id,
                    ProgramName = "Accounting Degree",
                    Degree = "Bachelor of Science",
                    Description = "Financial accounting and business finance",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // New enrollment programs for additional campuses (15 new programs)
                
                // Sunshine Community College (Florida) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("a9b8c7d6-e5f4-3210-9876-543210fedcba"),
                    CampusId = campus6Id,
                    ProgramName = "Marine Biology",
                    Degree = "Associate of Science",
                    Description = "Study of ocean ecosystems and marine life",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("b8c7d6e5-f4a3-2109-8765-4321fedcba09"),
                    CampusId = campus6Id,
                    ProgramName = "Hospitality Management",
                    Degree = "Bachelor of Science",
                    Description = "Tourism and hotel management program",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Mountain View Technical Institute (Colorado) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("c7d6e5f4-a3b2-1098-7654-321fedcba098"),
                    CampusId = campus7Id,
                    ProgramName = "Renewable Energy Technology",
                    Degree = "Associate of Applied Science",
                    Description = "Solar and wind energy systems technology",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("d6e5f4a3-b210-9876-5432-1fedcba09876"),
                    CampusId = campus7Id,
                    ProgramName = "Mountain Rescue Operations",
                    Degree = "Certificate",
                    Description = "Emergency response in mountainous terrain",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Lone Star University (Texas) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("e5f4a3b2-1098-7654-3210-fedcba098765"),
                    CampusId = campus8Id,
                    ProgramName = "Petroleum Engineering",
                    Degree = "Bachelor of Science",
                    Description = "Oil and gas extraction technology",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("f4a3b210-9876-5432-10fe-dcba09876543"),
                    CampusId = campus8Id,
                    ProgramName = "Ranch Management",
                    Degree = "Associate of Applied Science",
                    Description = "Livestock and agricultural operations",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Golden Gate College (California) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("a3b21098-7654-3210-fedc-ba0987654321"),
                    CampusId = campus9Id,
                    ProgramName = "Software Development",
                    Degree = "Bachelor of Science",
                    Description = "Modern software engineering and development",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("b2109876-5432-10fe-dcba-098765432109"),
                    CampusId = campus9Id,
                    ProgramName = "Digital Media Arts",
                    Degree = "Bachelor of Fine Arts",
                    Description = "Digital design and multimedia production",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Empire State Institute (New York) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("10987654-3210-fedc-ba09-876543210987"),
                    CampusId = campus10Id,
                    ProgramName = "Finance and Banking",
                    Degree = "Bachelor of Science",
                    Description = "Financial markets and investment banking",
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("09876543-210f-edcb-a098-765432109876"),
                    CampusId = campus10Id,
                    ProgramName = "Fashion Design",
                    Degree = "Associate of Applied Arts",
                    Description = "Contemporary fashion design and merchandising",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Desert Innovation University (Arizona) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("87654321-0fed-cba0-9876-543210987654"),
                    CampusId = campus11Id,
                    ProgramName = "Cybersecurity",
                    Degree = "Bachelor of Science",
                    Description = "Information security and ethical hacking",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Great Lakes Technical College (Michigan) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("76543210-fedc-ba09-8765-432109876543"),
                    CampusId = campus12Id,
                    ProgramName = "Automotive Engineering",
                    Degree = "Bachelor of Science",
                    Description = "Vehicle design and manufacturing technology",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Emerald City University (Washington) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("65432109-edcb-a098-7654-321098765432"),
                    CampusId = campus13Id,
                    ProgramName = "Environmental Science",
                    Degree = "Bachelor of Science",
                    Description = "Ecology and environmental conservation",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Blue Ridge Community College (Virginia) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("54321098-dcba-0987-6543-210987654321"),
                    CampusId = campus14Id,
                    ProgramName = "Health Sciences",
                    Degree = "Associate of Science",
                    Description = "Pre-health and medical assistant preparation",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Peach State Institute (Georgia) Programs
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("43210987-cba0-9876-5432-109876543210"),
                    CampusId = campus15Id,
                    ProgramName = "Agricultural Technology",
                    Degree = "Associate of Applied Science",
                    Description = "Modern farming and crop management",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Additional Programs for expanded course offerings
                // Culinary Arts Program (Sunshine Community College)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("1a2b3c4d-5e6f-7890-abcd-ef1234567891"),
                    CampusId = campus6Id,
                    ProgramName = "Culinary Arts",
                    Degree = "Associate of Applied Science",
                    Description = "Professional culinary techniques and food service management",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Digital Marketing Program (Golden Gate College)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("2b3c4d5e-6f70-8901-bcde-f12345678902"),
                    CampusId = campus9Id,
                    ProgramName = "Digital Marketing",
                    Degree = "Bachelor of Business Administration",
                    Description = "Online marketing strategies and social media management",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Physical Therapy Assistant Program (Blue Ridge Community College)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("3c4d5e6f-7080-9012-cdef-123456789013"),
                    CampusId = campus14Id,
                    ProgramName = "Physical Therapy Assistant",
                    Degree = "Associate of Applied Science",
                    Description = "Physical therapy support and rehabilitation techniques",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Web Development Program (Golden Gate College)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("4d5e6f70-8090-1234-def0-234567890124"),
                    CampusId = campus9Id,
                    ProgramName = "Web Development",
                    Degree = "Associate of Applied Science",
                    Description = "Modern web technologies and full-stack development",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Dental Hygiene Program (Blue Ridge Community College)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("5e6f7080-90a0-2345-ef01-345678901235"),
                    CampusId = campus14Id,
                    ProgramName = "Dental Hygiene",
                    Degree = "Associate of Applied Science",
                    Description = "Oral health care and preventive dental services",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Veterinary Technology Program (Great Lakes Technical College)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("6f708090-a0b0-3456-f012-456789012346"),
                    CampusId = campus12Id,
                    ProgramName = "Veterinary Technology",
                    Degree = "Associate of Applied Science",
                    Description = "Animal care and veterinary assistant training",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Welding Technology Program (Mountain View Technical Institute)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("708090a0-b0c0-4567-0123-567890123457"),
                    CampusId = campus7Id,
                    ProgramName = "Welding Technology",
                    Degree = "Certificate",
                    Description = "Advanced welding techniques and metallurgy",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Graphic Design Program (Empire State Institute)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("8090a0b0-c0d0-5678-1234-678901234568"),
                    CampusId = campus10Id,
                    ProgramName = "Graphic Design",
                    Degree = "Associate of Applied Arts",
                    Description = "Visual communication and digital design",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Criminal Justice Program (Empire State Institute)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("90a0b0c0-d0e0-6789-2345-789012345679"),
                    CampusId = campus10Id,
                    ProgramName = "Criminal Justice",
                    Degree = "Bachelor of Science",
                    Description = "Law enforcement and criminal investigation",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Early Childhood Education Program (Peach State Institute)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("a0b0c0d0-e0f0-789a-3456-890123456780"),
                    CampusId = campus15Id,
                    ProgramName = "Early Childhood Education",
                    Degree = "Associate of Arts",
                    Description = "Child development and early learning education",
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // HVAC Technology Program (Mountain View Technical Institute)
                new Enrollment
                {
                    EnrollmentId = Guid.Parse("b0c0d0e0-f0a1-89ab-4567-901234567891"),
                    CampusId = campus7Id,
                    ProgramName = "HVAC Technology",
                    Degree = "Associate of Applied Science",
                    Description = "Heating, ventilation, and air conditioning systems",
                    CreatedDate = new DateTime(2024, 1, 15)
                }
            );

            // Seed Courses
            modelBuilder.Entity<Course>().HasData(
                // Computer Science Program Courses
                new Course
                {
                    CourseId = Guid.Parse("aa111111-bbbb-cccc-dddd-eeeeeeeeeeee"),
                    CourseCode = "CS101",
                    CourseName = "Introduction to Programming",
                    CreditHours = 4,
                    Description = "Basic programming concepts and problem solving",
                    EnrollmentId = Guid.Parse("f6a7b8c9-d0e1-2345-f012-678901234567"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("bb222222-cccc-dddd-eeee-ffffffffffff"),
                    CourseCode = "CS102",
                    CourseName = "Data Structures",
                    CreditHours = 4,
                    Description = "Fundamental data structures and algorithms",
                    EnrollmentId = Guid.Parse("f6a7b8c9-d0e1-2345-f012-678901234567"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("cc333333-dddd-eeee-ffff-000000000000"),
                    CourseCode = "MATH150",
                    CourseName = "Calculus I",
                    CreditHours = 4,
                    Description = "Differential calculus and applications",
                    EnrollmentId = Guid.Parse("f6a7b8c9-d0e1-2345-f012-678901234567"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Engineering Program Courses
                new Course
                {
                    CourseId = Guid.Parse("dd444444-eeee-ffff-0000-111111111111"),
                    CourseCode = "ENGR101",
                    CourseName = "Engineering Fundamentals",
                    CreditHours = 4,
                    Description = "Introduction to engineering principles",
                    EnrollmentId = Guid.Parse("a7b8c9d0-e1f2-3456-0123-789012345678"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("ee555555-ffff-0000-1111-222222222222"),
                    CourseCode = "PHYS210",
                    CourseName = "Physics for Engineers I",
                    CreditHours = 4,
                    Description = "Mechanics and thermodynamics",
                    EnrollmentId = Guid.Parse("a7b8c9d0-e1f2-3456-0123-789012345678"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Pre-Medicine Courses
                new Course
                {
                    CourseId = Guid.Parse("ff666666-0000-1111-2222-333333333333"),
                    CourseCode = "BIO120",
                    CourseName = "General Biology I",
                    CreditHours = 4,
                    Description = "Cell biology and genetics",
                    EnrollmentId = Guid.Parse("b8c9d0e1-f2a3-4567-1234-890123456789"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("00777777-1111-2222-3333-444444444444"),
                    CourseCode = "CHEM130",
                    CourseName = "General Chemistry I",
                    CreditHours = 4,
                    Description = "Basic chemical principles",
                    EnrollmentId = Guid.Parse("b8c9d0e1-f2a3-4567-1234-890123456789"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Business Administration Courses
                new Course
                {
                    CourseId = Guid.Parse("11888888-2222-3333-4444-555555555555"),
                    CourseCode = "BUS101",
                    CourseName = "Introduction to Business",
                    CreditHours = 3,
                    Description = "Business fundamentals",
                    EnrollmentId = Guid.Parse("c9d0e1f2-a3b4-5678-2345-901234567890"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("22999999-3333-4444-5555-666666666666"),
                    CourseCode = "ACC110",
                    CourseName = "Basic Accounting",
                    CreditHours = 4,
                    Description = "Accounting principles",
                    EnrollmentId = Guid.Parse("c9d0e1f2-a3b4-5678-2345-901234567890"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Nursing Program Courses
                new Course
                {
                    CourseId = Guid.Parse("33aaaaaa-4444-5555-6666-777777777777"),
                    CourseCode = "NUR100",
                    CourseName = "Introduction to Nursing",
                    CreditHours = 3,
                    Description = "Basic nursing concepts",
                    EnrollmentId = Guid.Parse("d0e1f2a3-b4c5-6789-3456-012345678901"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("44bbbbbb-5555-6666-7777-888888888888"),
                    CourseCode = "BIO201",
                    CourseName = "Anatomy & Physiology I",
                    CreditHours = 4,
                    Description = "Human body systems",
                    EnrollmentId = Guid.Parse("d0e1f2a3-b4c5-6789-3456-012345678901"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Automotive Technology Courses
                new Course
                {
                    CourseId = Guid.Parse("55cccccc-6666-7777-8888-999999999999"),
                    CourseCode = "AUTO120",
                    CourseName = "Automotive Basics",
                    CreditHours = 5,
                    Description = "Introduction to automotive systems",
                    EnrollmentId = Guid.Parse("f2a3b4c5-d6e7-8901-5678-234567890123"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("66dddddd-7777-8888-9999-aaaaaaaaaaaa"),
                    CourseCode = "AUTO220",
                    CourseName = "Engine Repair",
                    CreditHours = 5,
                    Description = "Engine diagnosis and repair",
                    EnrollmentId = Guid.Parse("f2a3b4c5-d6e7-8901-5678-234567890123"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Fine Arts Courses
                new Course
                {
                    CourseId = Guid.Parse("77eeeeee-8888-9999-aaaa-bbbbbbbbbbbb"),
                    CourseCode = "ART110",
                    CourseName = "Drawing Fundamentals",
                    CreditHours = 3,
                    Description = "Basic drawing techniques",
                    EnrollmentId = Guid.Parse("c5d6e7f8-a9b0-1234-8901-567890123456"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("88ffffff-9999-aaaa-bbbb-cccccccccccc"),
                    CourseCode = "ART210",
                    CourseName = "Painting I",
                    CreditHours = 3,
                    Description = "Introduction to painting",
                    EnrollmentId = Guid.Parse("c5d6e7f8-a9b0-1234-8901-567890123456"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // MBA Courses
                new Course
                {
                    CourseId = Guid.Parse("99000000-aaaa-bbbb-cccc-dddddddddddd"),
                    CourseCode = "MBA600",
                    CourseName = "Strategic Management",
                    CreditHours = 3,
                    Description = "Advanced business strategy",
                    EnrollmentId = Guid.Parse("e7f8a9b0-c1d2-3456-0123-789012345678"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("aa111111-bbbb-cccc-dddd-eeeeeeeeeeef"),
                    CourseCode = "MBA610",
                    CourseName = "Financial Management",
                    CreditHours = 3,
                    Description = "Corporate finance principles",
                    EnrollmentId = Guid.Parse("e7f8a9b0-c1d2-3456-0123-789012345678"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Medical Assistant Program Courses (Health Sciences - Blue Ridge Community College)
                new Course
                {
                    CourseId = Guid.Parse("1a2b3c4d-5e6f-7890-abcd-ef1234567890"),
                    CourseCode = "MA101",
                    CourseName = "Medical Terminology",
                    CreditHours = 3,
                    Description = "Medical language and terminology",
                    EnrollmentId = Guid.Parse("54321098-dcba-0987-6543-210987654321"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("2b3c4d5e-6f70-8901-bcde-f12345678901"),
                    CourseCode = "MA201",
                    CourseName = "Clinical Procedures",
                    CreditHours = 4,
                    Description = "Basic clinical procedures for medical assistants",
                    EnrollmentId = Guid.Parse("54321098-dcba-0987-6543-210987654321"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Cybersecurity Program Courses (Desert Innovation University)
                new Course
                {
                    CourseId = Guid.Parse("3c4d5e6f-7080-9012-cdef-123456789012"),
                    CourseCode = "CYB101",
                    CourseName = "Introduction to Cybersecurity",
                    CreditHours = 3,
                    Description = "Fundamentals of cybersecurity",
                    EnrollmentId = Guid.Parse("87654321-0fed-cba0-9876-543210987654"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("4d5e6f70-8090-1234-def0-234567890123"),
                    CourseCode = "CYB301",
                    CourseName = "Network Security",
                    CreditHours = 4,
                    Description = "Network security protocols and defense",
                    EnrollmentId = Guid.Parse("87654321-0fed-cba0-9876-543210987654"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Culinary Arts Courses
                new Course
                {
                    CourseId = Guid.Parse("5e6f7080-90a0-2345-ef01-345678901234"),
                    CourseCode = "CUL110",
                    CourseName = "Culinary Fundamentals",
                    CreditHours = 4,
                    Description = "Basic cooking techniques and food safety",
                    EnrollmentId = Guid.Parse("1a2b3c4d-5e6f-7890-abcd-ef1234567891"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("6f708090-a0b0-3456-f012-456789012345"),
                    CourseCode = "CUL220",
                    CourseName = "Advanced Culinary Techniques",
                    CreditHours = 5,
                    Description = "Professional cooking methods",
                    EnrollmentId = Guid.Parse("1a2b3c4d-5e6f-7890-abcd-ef1234567891"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Digital Marketing Courses
                new Course
                {
                    CourseId = Guid.Parse("708090a0-b0c0-4567-0123-567890123456"),
                    CourseCode = "MKTG150",
                    CourseName = "Digital Marketing Fundamentals",
                    CreditHours = 3,
                    Description = "Introduction to digital marketing strategies",
                    EnrollmentId = Guid.Parse("2b3c4d5e-6f70-8901-bcde-f12345678902"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("8090a0b0-c0d0-5678-1234-678901234567"),
                    CourseCode = "MKTG350",
                    CourseName = "Social Media Marketing",
                    CreditHours = 3,
                    Description = "Social media strategy and analytics",
                    EnrollmentId = Guid.Parse("2b3c4d5e-6f70-8901-bcde-f12345678902"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Physical Therapy Assistant Courses
                new Course
                {
                    CourseId = Guid.Parse("90a0b0c0-d0e0-6789-2345-789012345678"),
                    CourseCode = "PTA101",
                    CourseName = "Introduction to Physical Therapy",
                    CreditHours = 3,
                    Description = "Foundations of physical therapy practice",
                    EnrollmentId = Guid.Parse("3c4d5e6f-7080-9012-cdef-123456789013"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("a0b0c0d0-e0f0-789a-3456-890123456789"),
                    CourseCode = "PTA201",
                    CourseName = "Therapeutic Exercise",
                    CreditHours = 4,
                    Description = "Exercise principles and techniques",
                    EnrollmentId = Guid.Parse("3c4d5e6f-7080-9012-cdef-123456789013"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Web Development Courses
                new Course
                {
                    CourseId = Guid.Parse("b0c0d0e0-f0a1-89ab-4567-901234567890"),
                    CourseCode = "WEB101",
                    CourseName = "HTML & CSS Fundamentals",
                    CreditHours = 3,
                    Description = "Web development basics",
                    EnrollmentId = Guid.Parse("4d5e6f70-8090-1234-def0-234567890124"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("c0d0e0f0-a1b2-9abc-5678-012345678901"),
                    CourseCode = "WEB301",
                    CourseName = "JavaScript Programming",
                    CreditHours = 4,
                    Description = "Client-side and server-side JavaScript",
                    EnrollmentId = Guid.Parse("4d5e6f70-8090-1234-def0-234567890124"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Dental Hygiene Courses
                new Course
                {
                    CourseId = Guid.Parse("d0e0f0a1-b2c3-abcd-6789-123456789012"),
                    CourseCode = "DH101",
                    CourseName = "Oral Anatomy",
                    CreditHours = 3,
                    Description = "Structure and function of oral cavity",
                    EnrollmentId = Guid.Parse("5e6f7080-90a0-2345-ef01-345678901235"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("e0f0a1b2-c3d4-bcde-789a-234567890123"),
                    CourseCode = "DH201",
                    CourseName = "Periodontal Therapy",
                    CreditHours = 4,
                    Description = "Treatment of gum disease",
                    EnrollmentId = Guid.Parse("5e6f7080-90a0-2345-ef01-345678901235"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Veterinary Technology Courses
                new Course
                {
                    CourseId = Guid.Parse("f0a1b2c3-d4e5-cdef-89ab-345678901234"),
                    CourseCode = "VET101",
                    CourseName = "Animal Anatomy & Physiology",
                    CreditHours = 4,
                    Description = "Animal body systems and functions",
                    EnrollmentId = Guid.Parse("6f708090-a0b0-3456-f012-456789012346"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("a1b2c3d4-e5f6-def0-9abc-456789012345"),
                    CourseCode = "VET201",
                    CourseName = "Veterinary Pharmacology",
                    CreditHours = 3,
                    Description = "Animal medications and dosages",
                    EnrollmentId = Guid.Parse("6f708090-a0b0-3456-f012-456789012346"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Welding Technology Courses
                new Course
                {
                    CourseId = Guid.Parse("b2c3d4e5-f6a7-ef01-abcd-567890123456"),
                    CourseCode = "WELD101",
                    CourseName = "Introduction to Welding",
                    CreditHours = 4,
                    Description = "Basic welding processes and safety",
                    EnrollmentId = Guid.Parse("708090a0-b0c0-4567-0123-567890123457"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("c3d4e5f6-a7b8-f012-bcde-678901234567"),
                    CourseCode = "WELD301",
                    CourseName = "Advanced Welding Techniques",
                    CreditHours = 5,
                    Description = "Specialized welding methods",
                    EnrollmentId = Guid.Parse("708090a0-b0c0-4567-0123-567890123457"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Graphic Design Courses
                new Course
                {
                    CourseId = Guid.Parse("d4e5f6a7-b8c9-0123-cdef-789012345678"),
                    CourseCode = "GD101",
                    CourseName = "Design Fundamentals",
                    CreditHours = 3,
                    Description = "Basic principles of graphic design",
                    EnrollmentId = Guid.Parse("8090a0b0-c0d0-5678-1234-678901234568"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("e5f6a7b8-c9d0-1234-def0-890123456789"),
                    CourseCode = "GD301",
                    CourseName = "Digital Design Software",
                    CreditHours = 4,
                    Description = "Adobe Creative Suite mastery",
                    EnrollmentId = Guid.Parse("8090a0b0-c0d0-5678-1234-678901234568"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Criminal Justice Courses
                new Course
                {
                    CourseId = Guid.Parse("f6a7b8c9-d0e1-2345-ef01-901234567890"),
                    CourseCode = "CJ101",
                    CourseName = "Introduction to Criminal Justice",
                    CreditHours = 3,
                    Description = "Overview of the criminal justice system",
                    EnrollmentId = Guid.Parse("90a0b0c0-d0e0-6789-2345-789012345679"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("a7b8c9d0-e1f2-3456-f012-012345678901"),
                    CourseCode = "CJ301",
                    CourseName = "Criminal Investigation",
                    CreditHours = 4,
                    Description = "Investigation techniques and procedures",
                    EnrollmentId = Guid.Parse("90a0b0c0-d0e0-6789-2345-789012345679"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Early Childhood Education Courses
                new Course
                {
                    CourseId = Guid.Parse("b8c9d0e1-f2a3-4567-0123-123456789012"),
                    CourseCode = "ECE101",
                    CourseName = "Child Development",
                    CreditHours = 3,
                    Description = "Physical, cognitive, and social development",
                    EnrollmentId = Guid.Parse("a0b0c0d0-e0f0-789a-3456-890123456780"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("c9d0e1f2-a3b4-5678-1234-234567890123"),
                    CourseCode = "ECE201",
                    CourseName = "Curriculum Planning",
                    CreditHours = 4,
                    Description = "Age-appropriate curriculum development",
                    EnrollmentId = Guid.Parse("a0b0c0d0-e0f0-789a-3456-890123456780"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // HVAC Technology Courses
                new Course
                {
                    CourseId = Guid.Parse("d0e1f2a3-b4c5-6789-2345-345678901234"),
                    CourseCode = "HVAC101",
                    CourseName = "HVAC Fundamentals",
                    CreditHours = 4,
                    Description = "Heating, ventilation, and air conditioning basics",
                    EnrollmentId = Guid.Parse("b0c0d0e0-f0a1-89ab-4567-901234567891"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("e1f2a3b4-c5d6-789a-3456-456789012345"),
                    CourseCode = "HVAC301",
                    CourseName = "Refrigeration Systems",
                    CreditHours = 5,
                    Description = "Commercial and residential refrigeration",
                    EnrollmentId = Guid.Parse("b0c0d0e0-f0a1-89ab-4567-901234567891"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },

                // Fashion Design Courses (using existing Fashion Design program at Empire State Institute)
                new Course
                {
                    CourseId = Guid.Parse("f2a3b4c5-d6e7-89ab-4567-567890123456"),
                    CourseCode = "FASH101",
                    CourseName = "Fashion Illustration",
                    CreditHours = 3,
                    Description = "Fashion drawing and design techniques",
                    EnrollmentId = Guid.Parse("09876543-210f-edcb-a098-765432109876"),
                    CreatedDate = new DateTime(2024, 1, 15)
                },
                new Course
                {
                    CourseId = Guid.Parse("a3b4c5d6-e7f8-9abc-5678-678901234567"),
                    CourseCode = "FASH301",
                    CourseName = "Pattern Making",
                    CreditHours = 4,
                    Description = "Creating patterns for garment construction",
                    EnrollmentId = Guid.Parse("09876543-210f-edcb-a098-765432109876"),
                    CreatedDate = new DateTime(2024, 1, 15)
                }
            );
        }
    }
}