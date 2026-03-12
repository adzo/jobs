using Jobs.Api.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jobs.Api.Data;

public class JobsDbContext : IdentityDbContext<AppUser>
{
    public JobsDbContext(DbContextOptions<JobsDbContext> options) : base(options)
    {
    }

    public DbSet<Job> Jobs { get; set; }
    public DbSet<Document> Documents { get; set; }
    public DbSet<Application> Applications { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>()
            .HasData(
                new IdentityRole("Admin"),
                new IdentityRole("Applicant")
            );

        // ── AppUser ───────────────────────────────────────────────────────────────
        builder.Entity<AppUser>(u =>
        {
            u.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            u.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(100);

            u.Property(x => x.Birthdate)
                .IsRequired();
        });

        // ── Job ───────────────────────────────────────────────────────────────────
        builder.Entity<Job>(j =>
        {
            j.HasKey(x => x.Id);

            j.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(200);

            j.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(4000);

            j.Property(x => x.Location)
                .IsRequired()
                .HasMaxLength(200);

            j.Property(x => x.StartDate)
                .IsRequired();

            j.Property(x => x.CompanyName)
                .IsRequired()
                .HasMaxLength(200);

            j.Property(x => x.Recruiter)
                .IsRequired()
                .HasMaxLength(200);

            j.Property(x => x.RecruiterEmail)
                .IsRequired()
                .HasMaxLength(200);

            j.Property(x => x.YearlySalary)
                .IsRequired();
        });

        // ── Application ───────────────────────────────────────────────────────────
        builder.Entity<Application>(a =>
        {
            a.HasKey(x => x.Id);

            a.Property(x => x.DesiredSalary)
                .IsRequired();

            a.Property(x => x.Closed)
                .IsRequired()
                .HasDefaultValue(false);

            a.Property(x => x.ClosingReason)
                .HasMaxLength(1000)
                .HasDefaultValue(string.Empty);

            // Application → Job (restrict: don't delete a job that has applications)
            a.HasOne(x => x.Job)
                .WithMany()
                .HasForeignKey(x => x.JobId)
                .OnDelete(DeleteBehavior.Restrict);

            // Application → AppUser (restrict: don't delete a user that has applications)
            a.HasOne(x => x.Applicant)
                .WithMany()
                .HasForeignKey(x => x.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        // ── Document ──────────────────────────────────────────────────────────────
        builder.Entity<Document>(d =>
        {
            d.HasKey(x => x.Id);

            d.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(260);

            d.Property(x => x.ContentType)
                .IsRequired()
                .HasMaxLength(100);

            d.Property(x => x.FilePath)
                .IsRequired()
                .HasMaxLength(500);

            d.Property(x => x.SymDocumentType)
                .IsRequired()
                .HasConversion<string>() // stores "Resume" / "CoverLetter" / "Other"
                .HasMaxLength(50);

            // Document → Application (cascade: deleting an application removes its documents)
            d.HasOne<Application>()
                .WithMany(x => x.Documents)
                .HasForeignKey(x => x.ApplicationId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        builder.Entity<Job>().HasData(
            new Job
            {
                Id = 1,
                Title = "Senior Backend Developer",
                Description =
                    "We are looking for a senior backend developer experienced in .NET and distributed systems. The role involves designing scalable APIs, working with microservices architecture, and collaborating closely with frontend and DevOps teams. You will participate in architectural discussions, code reviews, and mentoring junior developers while helping shape the technical direction of the platform.",
                Location = "Vienna, Austria",
                StartDate = new DateTime(2026, 5, 1),
                CompanyName = "TechNova Solutions",
                Recruiter = "Anna Berger",
                RecruiterEmail = "anna.berger@technova.com",
                YearlySalary = 85000
            },
            new Job
            {
                Id = 2,
                Title = "Frontend Engineer",
                Description =
                    "Join our product engineering team to build modern web applications using React and TypeScript. The role focuses on implementing responsive UI components, optimizing performance, and collaborating with UX designers and backend developers. You will contribute to design systems and ensure accessibility and maintainability of the frontend codebase.",
                Location = "Berlin, Germany",
                StartDate = new DateTime(2026, 5, 15),
                CompanyName = "PixelForge",
                Recruiter = "Daniel Schmidt",
                RecruiterEmail = "daniel.schmidt@pixelforge.io",
                YearlySalary = 72000
            },
            new Job
            {
                Id = 3,
                Title = "DevOps Engineer",
                Description =
                    "We are seeking a DevOps engineer to help automate infrastructure and improve deployment pipelines. The role involves working with Kubernetes, CI/CD systems, and cloud platforms to ensure reliability and scalability. You will collaborate with development teams to streamline release cycles and monitor system performance.",
                Location = "Munich, Germany",
                StartDate = new DateTime(2026, 6, 1),
                CompanyName = "CloudAxis",
                Recruiter = "Laura Weber",
                RecruiterEmail = "laura.weber@cloudaxis.io",
                YearlySalary = 80000
            },
            new Job
            {
                Id = 4,
                Title = "Full Stack Developer",
                Description =
                    "As a full stack developer you will work across both backend and frontend systems, implementing new product features and improving existing services. The position involves working with .NET, REST APIs, relational databases, and modern JavaScript frameworks while collaborating with designers and product managers.",
                Location = "Prague, Czech Republic",
                StartDate = new DateTime(2026, 6, 10),
                CompanyName = "BridgeSoft",
                Recruiter = "Martin Novak",
                RecruiterEmail = "martin.novak@bridgesoft.cz",
                YearlySalary = 70000
            },
            new Job
            {
                Id = 5,
                Title = "Software Architect",
                Description =
                    "We are hiring a software architect responsible for defining system architecture and technical standards across multiple development teams. The role requires deep expertise in distributed systems, cloud architecture, and design patterns. You will guide technical decisions and ensure maintainability, performance, and scalability.",
                Location = "Zurich, Switzerland",
                StartDate = new DateTime(2026, 7, 1),
                CompanyName = "Alpine Digital",
                Recruiter = "Sophie Keller",
                RecruiterEmail = "sophie.keller@alpinedigital.ch",
                YearlySalary = 120000
            },
            new Job
            {
                Id = 6,
                Title = "Mobile App Developer",
                Description =
                    "Develop high quality mobile applications for iOS and Android platforms. You will work with cross platform frameworks and native integrations to build performant and user friendly applications. Collaboration with product managers and designers is key to delivering engaging user experiences.",
                Location = "Budapest, Hungary",
                StartDate = new DateTime(2026, 5, 20),
                CompanyName = "AppWorks",
                Recruiter = "Peter Horvath",
                RecruiterEmail = "peter.horvath@appworks.hu",
                YearlySalary = 65000
            },
            new Job
            {
                Id = 7,
                Title = "QA Automation Engineer",
                Description =
                    "The QA automation engineer will design and maintain automated test suites for our web and backend services. Responsibilities include writing integration tests, maintaining testing frameworks, and collaborating with developers to ensure product quality throughout the development lifecycle.",
                Location = "Warsaw, Poland",
                StartDate = new DateTime(2026, 5, 25),
                CompanyName = "QualityFirst",
                Recruiter = "Katarzyna Lewandowska",
                RecruiterEmail = "k.lewandowska@qualityfirst.pl",
                YearlySalary = 60000
            },
            new Job
            {
                Id = 8,
                Title = "Data Engineer",
                Description =
                    "Design and maintain data pipelines that process and transform large volumes of data. The role involves working with ETL processes, distributed data systems, and cloud storage platforms. You will help ensure reliable data availability for analytics and machine learning workloads.",
                Location = "Amsterdam, Netherlands",
                StartDate = new DateTime(2026, 6, 5),
                CompanyName = "DataBridge",
                Recruiter = "Tom Van Dijk",
                RecruiterEmail = "tom.vandijk@databridge.nl",
                YearlySalary = 78000
            },
            new Job
            {
                Id = 9,
                Title = "Machine Learning Engineer",
                Description =
                    "Work with data scientists and engineers to deploy machine learning models into production environments. Responsibilities include optimizing model performance, building scalable pipelines, and integrating predictive services into production systems.",
                Location = "Stockholm, Sweden",
                StartDate = new DateTime(2026, 7, 10),
                CompanyName = "Nordic AI Labs",
                Recruiter = "Erik Johansson",
                RecruiterEmail = "erik.j@nordicai.se",
                YearlySalary = 95000
            },
            new Job
            {
                Id = 10,
                Title = "Cybersecurity Specialist",
                Description =
                    "Protect company systems and infrastructure by identifying vulnerabilities and implementing security controls. You will conduct security audits, monitor incidents, and collaborate with engineering teams to maintain compliance with modern security standards.",
                Location = "Vienna, Austria",
                StartDate = new DateTime(2026, 6, 15),
                CompanyName = "SecureNet",
                Recruiter = "Michael Gruber",
                RecruiterEmail = "m.gruber@securenet.at",
                YearlySalary = 88000
            },
            new Job
            {
                Id = 11,
                Title = "Product Manager",
                Description =
                    "Lead the development of digital products by coordinating between engineering, design, and business stakeholders. You will define product roadmaps, gather requirements, and ensure the successful delivery of new features aligned with customer needs.",
                Location = "London, UK",
                StartDate = new DateTime(2026, 6, 1),
                CompanyName = "BrightApps",
                Recruiter = "Emily Carter",
                RecruiterEmail = "emily.carter@brightapps.co.uk",
                YearlySalary = 90000
            },
            new Job
            {
                Id = 12,
                Title = "UI/UX Designer",
                Description =
                    "Design intuitive and visually appealing interfaces for web and mobile products. The role involves user research, wireframing, prototyping, and collaborating with developers to translate design concepts into functional interfaces.",
                Location = "Copenhagen, Denmark",
                StartDate = new DateTime(2026, 5, 18),
                CompanyName = "DesignFlow",
                Recruiter = "Lars Petersen",
                RecruiterEmail = "lars.petersen@designflow.dk",
                YearlySalary = 68000
            },
            new Job
            {
                Id = 13,
                Title = "Cloud Solutions Engineer",
                Description =
                    "Help organizations migrate and operate workloads in cloud environments such as AWS or Azure. Responsibilities include architecture design, cost optimization, and implementing infrastructure as code for scalable deployments.",
                Location = "Dublin, Ireland",
                StartDate = new DateTime(2026, 7, 1),
                CompanyName = "Nimbus Tech",
                Recruiter = "Sean Murphy",
                RecruiterEmail = "sean.murphy@nimbustech.ie",
                YearlySalary = 87000
            },
            new Job
            {
                Id = 14,
                Title = "Game Developer",
                Description =
                    "Join our creative development team to build immersive gameplay experiences. You will work with modern game engines, optimize performance, and collaborate with artists and designers to deliver engaging interactive worlds.",
                Location = "Helsinki, Finland",
                StartDate = new DateTime(2026, 6, 10),
                CompanyName = "Arctic Games",
                Recruiter = "Janne Korhonen",
                RecruiterEmail = "janne.k@arcticgames.fi",
                YearlySalary = 70000
            },
            new Job
            {
                Id = 15,
                Title = "Database Administrator",
                Description =
                    "Maintain and optimize relational databases supporting mission critical applications. Duties include monitoring performance, managing backups, tuning queries, and ensuring high availability and data integrity across environments.",
                Location = "Bratislava, Slovakia",
                StartDate = new DateTime(2026, 5, 30),
                CompanyName = "DataCore",
                Recruiter = "Ivana Kovac",
                RecruiterEmail = "ivana.kovac@datacore.sk",
                YearlySalary = 72000
            },
            new Job
            {
                Id = 16,
                Title = "IT Support Engineer",
                Description =
                    "Provide technical support to internal staff and maintain company IT infrastructure. Tasks include troubleshooting hardware and software issues, managing user accounts, and ensuring smooth daily operation of internal systems.",
                Location = "Vienna, Austria",
                StartDate = new DateTime(2026, 4, 15),
                CompanyName = "OfficeTech",
                Recruiter = "Stefan Hofer",
                RecruiterEmail = "stefan.hofer@officetech.at",
                YearlySalary = 48000
            },
            new Job
            {
                Id = 17,
                Title = "Business Analyst",
                Description =
                    "Analyze business processes and translate requirements into technical specifications. You will work closely with stakeholders to improve operational efficiency and support the delivery of technology driven solutions.",
                Location = "Frankfurt, Germany",
                StartDate = new DateTime(2026, 6, 20),
                CompanyName = "Insight Consulting",
                Recruiter = "Julia Fischer",
                RecruiterEmail = "julia.fischer@insightconsult.de",
                YearlySalary = 75000
            },
            new Job
            {
                Id = 18,
                Title = "Site Reliability Engineer",
                Description =
                    "Ensure the reliability and scalability of large scale production systems. Responsibilities include monitoring infrastructure, responding to incidents, improving observability, and automating operational processes.",
                Location = "Barcelona, Spain",
                StartDate = new DateTime(2026, 7, 5),
                CompanyName = "ScaleOps",
                Recruiter = "Carlos Ruiz",
                RecruiterEmail = "c.ruiz@scaleops.es",
                YearlySalary = 86000
            },
            new Job
            {
                Id = 19,
                Title = "Embedded Systems Engineer",
                Description =
                    "Develop firmware and low level software for embedded hardware platforms. You will work with microcontrollers, communication protocols, and real time operating systems in collaboration with hardware engineering teams.",
                Location = "Stuttgart, Germany",
                StartDate = new DateTime(2026, 8, 1),
                CompanyName = "MicroTech Systems",
                Recruiter = "Thomas Bauer",
                RecruiterEmail = "t.bauer@microtech.de",
                YearlySalary = 83000
            },
            new Job
            {
                Id = 20,
                Title = "Technical Writer",
                Description =
                    "Create clear and comprehensive documentation for software products and developer APIs. You will collaborate with engineers and product managers to produce guides, tutorials, and technical references for users.",
                Location = "Remote",
                StartDate = new DateTime(2026, 5, 10),
                CompanyName = "DocuCraft",
                Recruiter = "Rachel Evans",
                RecruiterEmail = "rachel.evans@docucraft.io",
                YearlySalary = 62000
            },
            new Job
            {
                Id = 21,
                Title = "Blockchain Developer",
                Description =
                    "Build decentralized applications and smart contracts using modern blockchain frameworks. The role involves designing secure contract logic, integrating blockchain APIs, and maintaining distributed ledger infrastructure.",
                Location = "Lisbon, Portugal",
                StartDate = new DateTime(2026, 7, 15),
                CompanyName = "ChainWave",
                Recruiter = "Miguel Santos",
                RecruiterEmail = "miguel.santos@chainwave.pt",
                YearlySalary = 92000
            },
            new Job
            {
                Id = 22,
                Title = "AI Research Engineer",
                Description =
                    "Conduct applied research in artificial intelligence and help transform research prototypes into scalable software solutions. You will collaborate with academic partners and internal engineering teams on innovative AI projects.",
                Location = "Zurich, Switzerland",
                StartDate = new DateTime(2026, 9, 1),
                CompanyName = "FutureMind Labs",
                Recruiter = "Nina Roth",
                RecruiterEmail = "nina.roth@futuremind.ch",
                YearlySalary = 110000
            },
            new Job
            {
                Id = 23,
                Title = "Network Engineer",
                Description =
                    "Design and maintain secure network infrastructure supporting enterprise services. Responsibilities include configuring routers and switches, monitoring traffic, and ensuring reliable connectivity across multiple sites.",
                Location = "Hamburg, Germany",
                StartDate = new DateTime(2026, 6, 25),
                CompanyName = "NetSecure",
                Recruiter = "Oliver Brandt",
                RecruiterEmail = "oliver.brandt@netsecure.de",
                YearlySalary = 73000
            },
            new Job
            {
                Id = 24,
                Title = "AR/VR Developer",
                Description =
                    "Develop immersive augmented and virtual reality applications for enterprise and consumer use. The role includes working with 3D environments, interaction systems, and performance optimization for real time rendering.",
                Location = "Paris, France",
                StartDate = new DateTime(2026, 7, 20),
                CompanyName = "VisionXR",
                Recruiter = "Claire Dubois",
                RecruiterEmail = "claire.dubois@visionxr.fr",
                YearlySalary = 84000
            },
            new Job
            {
                Id = 25,
                Title = "Technical Project Manager",
                Description =
                    "Lead cross functional software projects from planning through delivery. You will coordinate teams, manage timelines, mitigate risks, and ensure successful implementation of complex technical initiatives.",
                Location = "Vienna, Austria",
                StartDate = new DateTime(2026, 6, 1),
                CompanyName = "StratEdge",
                Recruiter = "Patrick Leitner",
                RecruiterEmail = "patrick.leitner@stratedge.at",
                YearlySalary = 91000
            },
            new Job
            {
                Id = 26,
                Title = "Junior Software Developer",
                Description =
                    "Entry level role for developers eager to grow their skills in modern software engineering. You will work alongside experienced engineers to implement features, fix bugs, and learn best practices in professional development environments.",
                Location = "Graz, Austria",
                StartDate = new DateTime(2026, 5, 1),
                CompanyName = "NextGen Software",
                Recruiter = "Lisa Maier",
                RecruiterEmail = "lisa.maier@nextgen.at",
                YearlySalary = 45000
            },
            new Job
            {
                Id = 27,
                Title = "Systems Engineer",
                Description =
                    "Design and maintain complex enterprise systems that support critical business operations. Responsibilities include infrastructure planning, integration of services, and ensuring system stability and performance.",
                Location = "Milan, Italy",
                StartDate = new DateTime(2026, 6, 15),
                CompanyName = "EuroSystems",
                Recruiter = "Marco Bianchi",
                RecruiterEmail = "marco.bianchi@eurosystems.it",
                YearlySalary = 77000
            },
            new Job
            {
                Id = 28,
                Title = "E-commerce Platform Engineer",
                Description =
                    "Develop scalable backend systems powering high traffic e-commerce platforms. You will implement payment integrations, optimize checkout flows, and ensure high availability during peak demand periods.",
                Location = "Warsaw, Poland",
                StartDate = new DateTime(2026, 7, 1),
                CompanyName = "ShopScale",
                Recruiter = "Adam Zielinski",
                RecruiterEmail = "adam.zielinski@shopscale.pl",
                YearlySalary = 74000
            },
            new Job
            {
                Id = 29,
                Title = "Analytics Engineer",
                Description =
                    "Bridge the gap between data engineering and analytics by designing robust data models and transformation pipelines. The role focuses on making clean, well structured data available for business intelligence and reporting.",
                Location = "Amsterdam, Netherlands",
                StartDate = new DateTime(2026, 8, 10),
                CompanyName = "MetricFlow",
                Recruiter = "Sanne Visser",
                RecruiterEmail = "sanne.visser@metricflow.nl",
                YearlySalary = 79000
            },
            new Job
            {
                Id = 30,
                Title = "Platform Engineer",
                Description =
                    "Build and maintain internal developer platforms that improve engineering productivity. Responsibilities include creating automation tools, maintaining CI/CD pipelines, and ensuring consistent development environments across teams.",
                Location = "Oslo, Norway",
                StartDate = new DateTime(2026, 7, 10),
                CompanyName = "NordPlatform",
                Recruiter = "Anders Larsen",
                RecruiterEmail = "anders.larsen@nordplatform.no",
                YearlySalary = 88000
            }
        );

        base.OnModelCreating(builder);
    }
}