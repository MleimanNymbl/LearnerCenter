using Microsoft.EntityFrameworkCore;
using LearnerCenter.API.Models;

namespace LearnerCenter.API.Data.Seeders
{
    public static class BasicDataSeeder
    {
        private static readonly Random _random = new Random(42); // Fixed seed for consistent data

        public static void SeedBasicData(ModelBuilder modelBuilder)
        {
            var campuses = GenerateCampuses();
            var enrollments = GenerateEnrollments(campuses);
            var courses = GenerateCourses(enrollments);

            modelBuilder.Entity<Campus>().HasData(campuses);
            modelBuilder.Entity<Enrollment>().HasData(enrollments);
            modelBuilder.Entity<Course>().HasData(courses);
        }

        private static List<Campus> GenerateCampuses()
        {
            var campusTemplates = new[]
            {
                new { Name = "State University Main Campus", Code = "SU-MAIN", City = "Springfield", State = "IL", Zip = "62701" },
                new { Name = "Community College Downtown", Code = "CC-DOWN", City = "Springfield", State = "IL", Zip = "62702" },
                new { Name = "Technical Institute North", Code = "TI-NORTH", City = "Rockford", State = "IL", Zip = "61101" },
                new { Name = "Liberal Arts College", Code = "LAC-MAIN", City = "Peoria", State = "IL", Zip = "61602" },
                new { Name = "Business School Central", Code = "BSC-CENT", City = "Chicago", State = "IL", Zip = "60601" },
                new { Name = "Sunshine Community College", Code = "SCC-MAIN", City = "Miami", State = "FL", Zip = "33101" },
                new { Name = "Mountain View Technical Institute", Code = "MVTI-MAIN", City = "Denver", State = "CO", Zip = "80201" },
                new { Name = "Lone Star University", Code = "LSU-MAIN", City = "Austin", State = "TX", Zip = "73301" },
                new { Name = "Golden Gate College", Code = "GGC-MAIN", City = "San Francisco", State = "CA", Zip = "94101" },
                new { Name = "Empire State Institute", Code = "ESI-MAIN", City = "New York", State = "NY", Zip = "10001" },
                new { Name = "Desert Innovation University", Code = "DIU-MAIN", City = "Phoenix", State = "AZ", Zip = "85001" },
                new { Name = "Great Lakes Technical College", Code = "GLTC-MAIN", City = "Detroit", State = "MI", Zip = "48201" },
                new { Name = "Emerald City University", Code = "ECU-MAIN", City = "Seattle", State = "WA", Zip = "98101" },
                new { Name = "Blue Ridge Community College", Code = "BRCC-MAIN", City = "Richmond", State = "VA", Zip = "23218" },
                new { Name = "Peach State Institute", Code = "PSI-MAIN", City = "Atlanta", State = "GA", Zip = "30301" }
            };

            var campuses = new List<Campus>();
            for (int i = 0; i < campusTemplates.Length; i++)
            {
                var template = campusTemplates[i];
                campuses.Add(new Campus
                {
                    CampusId = GenerateGuid(i + 1, "campus"),
                    CampusName = template.Name,
                    CampusCode = template.Code,
                    Address = $"{100 + i * 10} {GetRandomStreetName()} {GetRandomStreetType()}",
                    City = template.City,
                    State = template.State,
                    ZipCode = template.Zip,
                    PhoneNumber = $"({555 + i})-{123 + i:000}-{4567 + i:0000}",
                    Email = $"info@{template.Code.ToLower().Replace("-", "")}.edu",
                    CreatedDate = new DateTime(2024, 1, 1)
                });
            }
            return campuses;
        }

        private static List<Enrollment> GenerateEnrollments(List<Campus> campuses)
        {
            var programTemplates = new[]
            {
                new { Name = "Computer Science", Degree = "Bachelor of Science", Description = "Comprehensive computer science program covering programming, algorithms, and software engineering" },
                new { Name = "Engineering", Degree = "Bachelor of Engineering", Description = "Engineering fundamentals with multiple specializations available" },
                new { Name = "Business Administration", Degree = "Bachelor of Business Administration", Description = "Comprehensive business management and leadership program" },
                new { Name = "Nursing", Degree = "Associate of Science in Nursing", Description = "Registered nurse preparation program with clinical experience" },
                new { Name = "Cybersecurity", Degree = "Bachelor of Science", Description = "Information security and ethical hacking program" },
                new { Name = "Digital Marketing", Degree = "Associate of Applied Science", Description = "Modern marketing strategies and social media management" },
                new { Name = "Automotive Technology", Degree = "Associate of Applied Science", Description = "Automotive repair and maintenance certification program" },
                new { Name = "Culinary Arts", Degree = "Associate of Applied Science", Description = "Professional culinary techniques and food service management" },
                new { Name = "Graphic Design", Degree = "Bachelor of Fine Arts", Description = "Visual communication and digital design program" },
                new { Name = "Web Development", Degree = "Associate of Applied Science", Description = "Modern web technologies and full-stack development" },
                new { Name = "Criminal Justice", Degree = "Bachelor of Science", Description = "Law enforcement and criminal investigation program" },
                new { Name = "Early Childhood Education", Degree = "Associate of Arts", Description = "Child development and early learning education" },
                new { Name = "HVAC Technology", Degree = "Associate of Applied Science", Description = "Heating, ventilation, and air conditioning systems" },
                new { Name = "Physical Therapy Assistant", Degree = "Associate of Applied Science", Description = "Physical therapy support and rehabilitation techniques" },
                new { Name = "Veterinary Technology", Degree = "Associate of Applied Science", Description = "Animal care and veterinary assistant training" },
                new { Name = "Welding Technology", Degree = "Certificate", Description = "Advanced welding techniques and metallurgy" },
                new { Name = "Dental Hygiene", Degree = "Associate of Applied Science", Description = "Oral health care and preventive dental services" },
                new { Name = "Fashion Design", Degree = "Associate of Applied Arts", Description = "Contemporary fashion design and merchandising" },
                new { Name = "Environmental Science", Degree = "Bachelor of Science", Description = "Ecology and environmental conservation program" },
                new { Name = "Finance", Degree = "Bachelor of Science", Description = "Financial markets and investment analysis" },
                new { Name = "Marine Biology", Degree = "Associate of Science", Description = "Study of ocean ecosystems and marine life" },
                new { Name = "Renewable Energy Technology", Degree = "Associate of Applied Science", Description = "Solar and wind energy systems technology" },
                new { Name = "Petroleum Engineering", Degree = "Bachelor of Science", Description = "Oil and gas extraction technology" },
                new { Name = "Software Development", Degree = "Bachelor of Science", Description = "Modern software engineering and development practices" },
                new { Name = "Agricultural Technology", Degree = "Associate of Applied Science", Description = "Modern farming and crop management techniques" }
            };

            var enrollments = new List<Enrollment>();
            int enrollmentIndex = 0;

            // Distribute programs across campuses (2-3 programs per campus)
            foreach (var campus in campuses)
            {
                int programsPerCampus = _random.Next(2, 4); // 2-3 programs per campus
                for (int i = 0; i < programsPerCampus && enrollmentIndex < programTemplates.Length; i++)
                {
                    var template = programTemplates[enrollmentIndex];
                    enrollments.Add(new Enrollment
                    {
                        EnrollmentId = GenerateGuid(enrollmentIndex + 1, "enrollment"),
                        CampusId = campus.CampusId,
                        ProgramName = template.Name,
                        Degree = template.Degree,
                        Description = template.Description,
                        CreatedDate = new DateTime(2024, 1, 15)
                    });
                    enrollmentIndex++;
                }
            }

            return enrollments;
        }

        private static List<Course> GenerateCourses(List<Enrollment> enrollments)
        {
            var courseTemplates = new Dictionary<string, string[]>
            {
                ["Computer Science"] = new[] { "CS101:Introduction to Programming:4", "CS102:Data Structures:4", "CS201:Algorithms:4", "CS301:Software Engineering:4", "CS401:Database Systems:3", "CS501:Web Development:4", "MATH150:Calculus I:4", "MATH250:Discrete Mathematics:3" },
                ["Engineering"] = new[] { "ENGR101:Engineering Fundamentals:4", "PHYS210:Physics for Engineers I:4", "MATH170:Calculus for Engineers:4", "CHEM110:General Chemistry:4", "ENGR201:Statics:3", "ENGR301:Thermodynamics:4", "ENGR401:Design Project:3", "COMM101:Technical Communication:3" },
                ["Business Administration"] = new[] { "BUS101:Introduction to Business:3", "ACC110:Principles of Accounting:4", "ECON101:Microeconomics:3", "MGMT201:Management Principles:3", "MKT101:Marketing Fundamentals:3", "FIN201:Business Finance:4", "BUS301:Business Ethics:3", "STAT201:Business Statistics:3" },
                ["Nursing"] = new[] { "NUR100:Introduction to Nursing:3", "BIO201:Anatomy & Physiology I:4", "BIO202:Anatomy & Physiology II:4", "NUR201:Pharmacology:3", "NUR301:Clinical Nursing I:5", "NUR302:Clinical Nursing II:5", "PSY101:Introduction to Psychology:3", "NUR401:Nursing Leadership:3" },
                ["Cybersecurity"] = new[] { "CYB101:Introduction to Cybersecurity:3", "CYB201:Network Security:4", "CYB301:Ethical Hacking:4", "CYB401:Digital Forensics:4", "CS101:Programming Basics:3", "NET201:Network Administration:4", "CYB501:Incident Response:3", "LAW301:Cyber Law:3" },
                ["Digital Marketing"] = new[] { "MKT150:Digital Marketing Fundamentals:3", "MKT250:Social Media Marketing:3", "MKT350:Content Marketing:3", "WEB101:Web Analytics:3", "GD101:Graphic Design Basics:3", "MKT450:Email Marketing:3", "BUS201:Consumer Behavior:3", "MKT501:Marketing Strategy:4" },
                ["Automotive Technology"] = new[] { "AUTO101:Automotive Fundamentals:4", "AUTO201:Engine Systems:5", "AUTO301:Transmission Systems:4", "AUTO401:Electrical Systems:4", "AUTO501:Brake Systems:4", "AUTO601:Suspension Systems:4", "SHOP101:Tool Safety:2", "AUTO701:Diagnostic Technology:4" },
                ["Culinary Arts"] = new[] { "CUL101:Culinary Fundamentals:4", "CUL201:Baking & Pastry:4", "CUL301:Food Safety & Sanitation:3", "CUL401:Menu Planning:3", "CUL501:International Cuisine:4", "BUS201:Restaurant Management:3", "NUT101:Nutrition Basics:3", "CUL601:Advanced Techniques:5" },
                ["Graphic Design"] = new[] { "GD101:Design Fundamentals:3", "GD201:Typography:3", "GD301:Digital Design Software:4", "GD401:Brand Identity:4", "ART101:Drawing Basics:3", "GD501:Web Design:4", "GD601:Portfolio Development:3", "BUS301:Design Business:3" },
                ["Web Development"] = new[] { "WEB101:HTML & CSS Fundamentals:3", "WEB201:JavaScript Programming:4", "WEB301:Backend Development:4", "WEB401:Database Integration:4", "WEB501:Framework Development:4", "WEB601:Mobile Development:4", "CS201:Version Control:2", "WEB701:Deployment & DevOps:4" }
            };

            // Generic course templates for programs not in the dictionary
            var genericCourses = new[] {
                "101:Program Fundamentals:3", "102:Basic Principles:4", "201:Intermediate Concepts:4",
                "202:Advanced Theory:4", "301:Practical Applications:4", "302:Specialized Techniques:4",
                "401:Capstone Project:4", "402:Professional Practice:3"
            };

            var courses = new List<Course>();
            int courseIndex = 1;

            foreach (var enrollment in enrollments)
            {
                string[] courseList;
                
                // Try to get specific courses for the program, otherwise use generic ones
                if (courseTemplates.ContainsKey(enrollment.ProgramName))
                {
                    courseList = courseTemplates[enrollment.ProgramName];
                }
                else
                {
                    // Generate generic courses with program prefix
                    var programPrefix = GetProgramPrefix(enrollment.ProgramName);
                    courseList = genericCourses.Select(course => $"{programPrefix}{course}").ToArray();
                }

                // Ensure exactly 8 courses per enrollment
                for (int i = 0; i < 8; i++)
                {
                    var courseInfo = courseList[i % courseList.Length].Split(':');
                    var courseCode = courseInfo[0];
                    var courseName = courseInfo[1];
                    var creditHours = int.Parse(courseInfo[2]);

                    // If we're repeating courses, add roman numerals
                    if (i >= courseList.Length)
                    {
                        var romanNumeral = GetRomanNumeral((i / courseList.Length) + 1);
                        courseName += $" {romanNumeral}";
                        courseCode += romanNumeral.Replace(" ", "");
                    }

                    courses.Add(new Course
                    {
                        CourseId = GenerateGuid(courseIndex, "course"),
                        CourseCode = courseCode,
                        CourseName = courseName,
                        CreditHours = creditHours,
                        Description = GenerateCourseDescription(courseName, enrollment.ProgramName),
                        EnrollmentId = enrollment.EnrollmentId,
                        CreatedDate = new DateTime(2024, 1, 15)
                    });
                    courseIndex++;
                }
            }

            return courses;
        }

        private static Guid GenerateGuid(int index, string type)
        {
            // Generate consistent GUIDs based on type and index
            var guidBytes = new byte[16];
            var indexBytes = BitConverter.GetBytes(index);
            var typeBytes = System.Text.Encoding.UTF8.GetBytes(type.PadRight(8).Substring(0, 8));
            
            Array.Copy(indexBytes, 0, guidBytes, 0, 4);
            Array.Copy(typeBytes, 0, guidBytes, 4, 8);
            Array.Copy(indexBytes, 0, guidBytes, 12, 4);
            
            return new Guid(guidBytes);
        }

        private static string GetRandomStreetName()
        {
            var streetNames = new[] { "University", "College", "Academic", "Scholar", "Campus", "Education", "Learning", "Knowledge", "Wisdom", "Discovery" };
            return streetNames[_random.Next(streetNames.Length)];
        }

        private static string GetRandomStreetType()
        {
            var streetTypes = new[] { "Ave", "St", "Blvd", "Dr", "Way", "Rd", "Ct", "Pl" };
            return streetTypes[_random.Next(streetTypes.Length)];
        }

        private static string GetProgramPrefix(string programName)
        {
            // Generate a 3-4 letter prefix from program name
            var words = programName.Split(' ');
            if (words.Length == 1)
            {
                return words[0].Substring(0, Math.Min(4, words[0].Length)).ToUpper();
            }
            else if (words.Length == 2)
            {
                return (words[0].Substring(0, Math.Min(2, words[0].Length)) + 
                       words[1].Substring(0, Math.Min(2, words[1].Length))).ToUpper();
            }
            else
            {
                return (words[0][0].ToString() + words[1][0].ToString() + words[2][0].ToString()).ToUpper();
            }
        }

        private static string GetRomanNumeral(int number)
        {
            return number switch
            {
                1 => "I",
                2 => "II", 
                3 => "III",
                4 => "IV",
                5 => "V",
                _ => number.ToString()
            };
        }

        private static string GenerateCourseDescription(string courseName, string programName)
        {
            var descriptions = new[]
            {
                $"Comprehensive study of {courseName.ToLower()} in {programName.ToLower()}",
                $"Advanced concepts and practical applications in {courseName.ToLower()}",
                $"Fundamental principles and theory of {courseName.ToLower()}",
                $"Hands-on experience with {courseName.ToLower()} techniques",
                $"Professional-level {courseName.ToLower()} skills development",
                $"Industry-standard {courseName.ToLower()} practices and procedures"
            };
            
            return descriptions[_random.Next(descriptions.Length)];
        }
    }
}