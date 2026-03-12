using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Jobs.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(4000)", maxLength: 4000, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompanyName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Recruiter = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    RecruiterEmail = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    YearlySalary = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    JobId = table.Column<int>(type: "int", nullable: false),
                    DesiredSalary = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Closed = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    ClosingReason = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_AspNetUsers_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Applications_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(260)", maxLength: 260, nullable: false),
                    ContentType = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    SymDocumentType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ApplicationId = table.Column<int>(type: "int", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ApplicationId1 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documents_Applications_ApplicationId",
                        column: x => x.ApplicationId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Documents_Applications_ApplicationId1",
                        column: x => x.ApplicationId1,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "abccf311-276d-4b72-ac45-390017f36c8c", "83715b5c-ed78-43b9-a0ea-aaccd16b164b", "Admin", null },
                    { "e6da29bc-3c78-4134-9b6f-fd273fa2cc56", "204d473f-802a-4d08-ac56-5a3115a52f44", "Applicant", null }
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "CompanyName", "Description", "Location", "Recruiter", "RecruiterEmail", "StartDate", "Title", "YearlySalary" },
                values: new object[,]
                {
                    { 1, "TechNova Solutions", "We are looking for a senior backend developer experienced in .NET and distributed systems. The role involves designing scalable APIs, working with microservices architecture, and collaborating closely with frontend and DevOps teams. You will participate in architectural discussions, code reviews, and mentoring junior developers while helping shape the technical direction of the platform.", "Vienna, Austria", "Anna Berger", "anna.berger@technova.com", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Senior Backend Developer", 85000 },
                    { 2, "PixelForge", "Join our product engineering team to build modern web applications using React and TypeScript. The role focuses on implementing responsive UI components, optimizing performance, and collaborating with UX designers and backend developers. You will contribute to design systems and ensure accessibility and maintainability of the frontend codebase.", "Berlin, Germany", "Daniel Schmidt", "daniel.schmidt@pixelforge.io", new DateTime(2026, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Frontend Engineer", 72000 },
                    { 3, "CloudAxis", "We are seeking a DevOps engineer to help automate infrastructure and improve deployment pipelines. The role involves working with Kubernetes, CI/CD systems, and cloud platforms to ensure reliability and scalability. You will collaborate with development teams to streamline release cycles and monitor system performance.", "Munich, Germany", "Laura Weber", "laura.weber@cloudaxis.io", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "DevOps Engineer", 80000 },
                    { 4, "BridgeSoft", "As a full stack developer you will work across both backend and frontend systems, implementing new product features and improving existing services. The position involves working with .NET, REST APIs, relational databases, and modern JavaScript frameworks while collaborating with designers and product managers.", "Prague, Czech Republic", "Martin Novak", "martin.novak@bridgesoft.cz", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Full Stack Developer", 70000 },
                    { 5, "Alpine Digital", "We are hiring a software architect responsible for defining system architecture and technical standards across multiple development teams. The role requires deep expertise in distributed systems, cloud architecture, and design patterns. You will guide technical decisions and ensure maintainability, performance, and scalability.", "Zurich, Switzerland", "Sophie Keller", "sophie.keller@alpinedigital.ch", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Software Architect", 120000 },
                    { 6, "AppWorks", "Develop high quality mobile applications for iOS and Android platforms. You will work with cross platform frameworks and native integrations to build performant and user friendly applications. Collaboration with product managers and designers is key to delivering engaging user experiences.", "Budapest, Hungary", "Peter Horvath", "peter.horvath@appworks.hu", new DateTime(2026, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Mobile App Developer", 65000 },
                    { 7, "QualityFirst", "The QA automation engineer will design and maintain automated test suites for our web and backend services. Responsibilities include writing integration tests, maintaining testing frameworks, and collaborating with developers to ensure product quality throughout the development lifecycle.", "Warsaw, Poland", "Katarzyna Lewandowska", "k.lewandowska@qualityfirst.pl", new DateTime(2026, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "QA Automation Engineer", 60000 },
                    { 8, "DataBridge", "Design and maintain data pipelines that process and transform large volumes of data. The role involves working with ETL processes, distributed data systems, and cloud storage platforms. You will help ensure reliable data availability for analytics and machine learning workloads.", "Amsterdam, Netherlands", "Tom Van Dijk", "tom.vandijk@databridge.nl", new DateTime(2026, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Data Engineer", 78000 },
                    { 9, "Nordic AI Labs", "Work with data scientists and engineers to deploy machine learning models into production environments. Responsibilities include optimizing model performance, building scalable pipelines, and integrating predictive services into production systems.", "Stockholm, Sweden", "Erik Johansson", "erik.j@nordicai.se", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Machine Learning Engineer", 95000 },
                    { 10, "SecureNet", "Protect company systems and infrastructure by identifying vulnerabilities and implementing security controls. You will conduct security audits, monitor incidents, and collaborate with engineering teams to maintain compliance with modern security standards.", "Vienna, Austria", "Michael Gruber", "m.gruber@securenet.at", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cybersecurity Specialist", 88000 },
                    { 11, "BrightApps", "Lead the development of digital products by coordinating between engineering, design, and business stakeholders. You will define product roadmaps, gather requirements, and ensure the successful delivery of new features aligned with customer needs.", "London, UK", "Emily Carter", "emily.carter@brightapps.co.uk", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Product Manager", 90000 },
                    { 12, "DesignFlow", "Design intuitive and visually appealing interfaces for web and mobile products. The role involves user research, wireframing, prototyping, and collaborating with developers to translate design concepts into functional interfaces.", "Copenhagen, Denmark", "Lars Petersen", "lars.petersen@designflow.dk", new DateTime(2026, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "UI/UX Designer", 68000 },
                    { 13, "Nimbus Tech", "Help organizations migrate and operate workloads in cloud environments such as AWS or Azure. Responsibilities include architecture design, cost optimization, and implementing infrastructure as code for scalable deployments.", "Dublin, Ireland", "Sean Murphy", "sean.murphy@nimbustech.ie", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Cloud Solutions Engineer", 87000 },
                    { 14, "Arctic Games", "Join our creative development team to build immersive gameplay experiences. You will work with modern game engines, optimize performance, and collaborate with artists and designers to deliver engaging interactive worlds.", "Helsinki, Finland", "Janne Korhonen", "janne.k@arcticgames.fi", new DateTime(2026, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Game Developer", 70000 },
                    { 15, "DataCore", "Maintain and optimize relational databases supporting mission critical applications. Duties include monitoring performance, managing backups, tuning queries, and ensuring high availability and data integrity across environments.", "Bratislava, Slovakia", "Ivana Kovac", "ivana.kovac@datacore.sk", new DateTime(2026, 5, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Database Administrator", 72000 },
                    { 16, "OfficeTech", "Provide technical support to internal staff and maintain company IT infrastructure. Tasks include troubleshooting hardware and software issues, managing user accounts, and ensuring smooth daily operation of internal systems.", "Vienna, Austria", "Stefan Hofer", "stefan.hofer@officetech.at", new DateTime(2026, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "IT Support Engineer", 48000 },
                    { 17, "Insight Consulting", "Analyze business processes and translate requirements into technical specifications. You will work closely with stakeholders to improve operational efficiency and support the delivery of technology driven solutions.", "Frankfurt, Germany", "Julia Fischer", "julia.fischer@insightconsult.de", new DateTime(2026, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Business Analyst", 75000 },
                    { 18, "ScaleOps", "Ensure the reliability and scalability of large scale production systems. Responsibilities include monitoring infrastructure, responding to incidents, improving observability, and automating operational processes.", "Barcelona, Spain", "Carlos Ruiz", "c.ruiz@scaleops.es", new DateTime(2026, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Site Reliability Engineer", 86000 },
                    { 19, "MicroTech Systems", "Develop firmware and low level software for embedded hardware platforms. You will work with microcontrollers, communication protocols, and real time operating systems in collaboration with hardware engineering teams.", "Stuttgart, Germany", "Thomas Bauer", "t.bauer@microtech.de", new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Embedded Systems Engineer", 83000 },
                    { 20, "DocuCraft", "Create clear and comprehensive documentation for software products and developer APIs. You will collaborate with engineers and product managers to produce guides, tutorials, and technical references for users.", "Remote", "Rachel Evans", "rachel.evans@docucraft.io", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technical Writer", 62000 },
                    { 21, "ChainWave", "Build decentralized applications and smart contracts using modern blockchain frameworks. The role involves designing secure contract logic, integrating blockchain APIs, and maintaining distributed ledger infrastructure.", "Lisbon, Portugal", "Miguel Santos", "miguel.santos@chainwave.pt", new DateTime(2026, 7, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Blockchain Developer", 92000 },
                    { 22, "FutureMind Labs", "Conduct applied research in artificial intelligence and help transform research prototypes into scalable software solutions. You will collaborate with academic partners and internal engineering teams on innovative AI projects.", "Zurich, Switzerland", "Nina Roth", "nina.roth@futuremind.ch", new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AI Research Engineer", 110000 },
                    { 23, "NetSecure", "Design and maintain secure network infrastructure supporting enterprise services. Responsibilities include configuring routers and switches, monitoring traffic, and ensuring reliable connectivity across multiple sites.", "Hamburg, Germany", "Oliver Brandt", "oliver.brandt@netsecure.de", new DateTime(2026, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Network Engineer", 73000 },
                    { 24, "VisionXR", "Develop immersive augmented and virtual reality applications for enterprise and consumer use. The role includes working with 3D environments, interaction systems, and performance optimization for real time rendering.", "Paris, France", "Claire Dubois", "claire.dubois@visionxr.fr", new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "AR/VR Developer", 84000 },
                    { 25, "StratEdge", "Lead cross functional software projects from planning through delivery. You will coordinate teams, manage timelines, mitigate risks, and ensure successful implementation of complex technical initiatives.", "Vienna, Austria", "Patrick Leitner", "patrick.leitner@stratedge.at", new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Technical Project Manager", 91000 },
                    { 26, "NextGen Software", "Entry level role for developers eager to grow their skills in modern software engineering. You will work alongside experienced engineers to implement features, fix bugs, and learn best practices in professional development environments.", "Graz, Austria", "Lisa Maier", "lisa.maier@nextgen.at", new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Junior Software Developer", 45000 },
                    { 27, "EuroSystems", "Design and maintain complex enterprise systems that support critical business operations. Responsibilities include infrastructure planning, integration of services, and ensuring system stability and performance.", "Milan, Italy", "Marco Bianchi", "marco.bianchi@eurosystems.it", new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Systems Engineer", 77000 },
                    { 28, "ShopScale", "Develop scalable backend systems powering high traffic e-commerce platforms. You will implement payment integrations, optimize checkout flows, and ensure high availability during peak demand periods.", "Warsaw, Poland", "Adam Zielinski", "adam.zielinski@shopscale.pl", new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "E-commerce Platform Engineer", 74000 },
                    { 29, "MetricFlow", "Bridge the gap between data engineering and analytics by designing robust data models and transformation pipelines. The role focuses on making clean, well structured data available for business intelligence and reporting.", "Amsterdam, Netherlands", "Sanne Visser", "sanne.visser@metricflow.nl", new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Analytics Engineer", 79000 },
                    { 30, "NordPlatform", "Build and maintain internal developer platforms that improve engineering productivity. Responsibilities include creating automation tools, maintaining CI/CD pipelines, and ensuring consistent development environments across teams.", "Oslo, Norway", "Anders Larsen", "anders.larsen@nordplatform.no", new DateTime(2026, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Platform Engineer", 88000 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Applications_ApplicantId",
                table: "Applications",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_JobId",
                table: "Applications",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApplicationId",
                table: "Documents",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ApplicationId1",
                table: "Documents",
                column: "ApplicationId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Documents");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
