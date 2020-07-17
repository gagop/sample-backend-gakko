using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using GakkoBackend.Entities;

namespace GakkoBackend.Persistence
{
    public partial class StudentAppDbContext : DbContext
    {
        public StudentAppDbContext()
        {
        }

        public StudentAppDbContext(DbContextOptions<StudentAppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<CountryDict> CountryDict { get; set; }
        public virtual DbSet<Curriculum> Curriculum { get; set; }
        public virtual DbSet<Faculty> Faculty { get; set; }
        public virtual DbSet<LanguageDict> LanguageDict { get; set; }
        public virtual DbSet<LanguageDictTeacher> LanguageDictTeacher { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Semester> Semester { get; set; }
        public virtual DbSet<SpecializationDict> SpecializationDict { get; set; }
        public virtual DbSet<StatusDict> StatusDict { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<StudentGroup> StudentGroup { get; set; }
        public virtual DbSet<StudentGroupStudent> StudentGroupStudent { get; set; }
        public virtual DbSet<StudentStatusDict> StudentStatusDict { get; set; }
        public virtual DbSet<StudiesMode> StudiesMode { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<SubjectCurriculum> SubjectCurriculum { get; set; }
        public virtual DbSet<Teacher> Teacher { get; set; }
        public virtual DbSet<TeacherSubject> TeacherSubject { get; set; }
        public virtual DbSet<TitleDict> TitleDict { get; set; }
        public virtual DbSet<TitleTeacher> TitleTeacher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.HasKey(e => e.IdCertificate)
                    .HasName("Certificate_pk");

                entity.Property(e => e.IdCertificate).ValueGeneratedNever();

                entity.Property(e => e.Level)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany(p => p.Certificate)
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Certificate_LanguageDict");
            });

            modelBuilder.Entity<CountryDict>(entity =>
            {
                entity.HasKey(e => e.IdCountry)
                    .HasName("CountryDict_pk");

                entity.Property(e => e.IdCountry).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Curriculum>(entity =>
            {
                entity.HasKey(e => e.IdCurriculum)
                    .HasName("Curriculum_pk");

                entity.Property(e => e.IdCurriculum).ValueGeneratedNever();

                entity.HasOne(d => d.IdSemestrNavigation)
                    .WithMany(p => p.Curriculum)
                    .HasForeignKey(d => d.IdSemestr)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Curriculum_Semester");

                entity.HasOne(d => d.IdStudiesModeNavigation)
                    .WithMany(p => p.Curriculum)
                    .HasForeignKey(d => d.IdStudiesMode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Curriculum_StudiesModeDict");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.HasKey(e => e.IdFaculty)
                    .HasName("Faculty_pk");

                entity.Property(e => e.IdFaculty).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.HasOne(d => d.IdChiefNavigation)
                    .WithMany(p => p.Faculty)
                    .HasForeignKey(d => d.IdChief)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Faculty_Teacher");
            });

            modelBuilder.Entity<LanguageDict>(entity =>
            {
                entity.HasKey(e => e.IdLanguage)
                    .HasName("LanguageDict_pk");

                entity.Property(e => e.IdLanguage).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LanguageDictTeacher>(entity =>
            {
                entity.HasKey(e => new { e.IdTeacher, e.IdLanguage })
                    .HasName("LanguageDict_Teacher_pk");

                entity.ToTable("LanguageDict_Teacher");

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany(p => p.LanguageDictTeacher)
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LanguageDict_Teacher_LanguageDict");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.LanguageDictTeacher)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("LanguageDict_Teacher_Teacher");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("Person_pk");

                entity.Property(e => e.IdPerson).ValueGeneratedNever();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.PayoutAccount)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.RefreshTokenExp).HasColumnType("datetime");

                entity.Property(e => e.Salt)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.UniversityEmail)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.Person)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Person_CountryDict");
            });

            modelBuilder.Entity<Semester>(entity =>
            {
                entity.HasKey(e => e.IdSemestr)
                    .HasName("Semester_pk");

                entity.Property(e => e.IdSemestr).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<SpecializationDict>(entity =>
            {
                entity.HasKey(e => e.IdSpecialization)
                    .HasName("SpecializationDict_pk");

                entity.Property(e => e.IdSpecialization).ValueGeneratedNever();

                entity.Property(e => e.Description).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<StatusDict>(entity =>
            {
                entity.HasKey(e => e.IdStatus)
                    .HasName("StatusDict_pk");

                entity.Property(e => e.IdStatus).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.IdStudent)
                    .HasName("Student_pk");

                entity.Property(e => e.IdStudent).ValueGeneratedNever();

                entity.Property(e => e.IndexNumber)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithOne(p => p.Student)
                    .HasForeignKey<Student>(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_Person");
            });

            modelBuilder.Entity<StudentGroup>(entity =>
            {
                entity.HasKey(e => e.IdStudentGroup)
                    .HasName("StudentGroup_pk");

                entity.Property(e => e.IdStudentGroup).ValueGeneratedNever();

                entity.Property(e => e.GroupNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCurriculumNavigation)
                    .WithMany(p => p.StudentGroup)
                    .HasForeignKey(d => d.IdCurriculum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentGroup_Curriculum");
            });

            modelBuilder.Entity<StudentGroupStudent>(entity =>
            {
                entity.HasKey(e => new { e.IdStudent, e.IdStudentGroup })
                    .HasName("StudentGroup_Student_pk");

                entity.ToTable("StudentGroup_Student");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.StudentGroupStudent)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentGroup_Student_Student");

                entity.HasOne(d => d.IdStudentGroupNavigation)
                    .WithMany(p => p.StudentGroupStudent)
                    .HasForeignKey(d => d.IdStudentGroup)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudentGroup_Student_StudentGroup");
            });

            modelBuilder.Entity<StudentStatusDict>(entity =>
            {
                entity.HasKey(e => new { e.IdStatus, e.IdStudent })
                    .HasName("Student_StatusDict_pk");

                entity.ToTable("Student_StatusDict");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.IdStatusNavigation)
                    .WithMany(p => p.StudentStatusDict)
                    .HasForeignKey(d => d.IdStatus)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_StatusDict_StatusDict");

                entity.HasOne(d => d.IdStudentNavigation)
                    .WithMany(p => p.StudentStatusDict)
                    .HasForeignKey(d => d.IdStudent)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Student_StatusDict_Student");
            });

            modelBuilder.Entity<StudiesMode>(entity =>
            {
                entity.HasKey(e => e.IdStudiesMode)
                    .HasName("StudiesMode_pk");

                entity.Property(e => e.IdStudiesMode).ValueGeneratedNever();

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany(p => p.StudiesMode)
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudiesModeDict_Language");

                entity.HasOne(d => d.IdSpecializationNavigation)
                    .WithMany(p => p.StudiesMode)
                    .HasForeignKey(d => d.IdSpecialization)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("StudiesModeDict_Specialization");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.HasKey(e => e.IdSubject)
                    .HasName("Subject_pk");

                entity.Property(e => e.IdSubject).ValueGeneratedNever();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5);

                entity.Property(e => e.Ects).HasColumnName("ECTS");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdLanguageNavigation)
                    .WithMany(p => p.Subject)
                    .HasForeignKey(d => d.IdLanguage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject_LanguageDict");
            });

            modelBuilder.Entity<SubjectCurriculum>(entity =>
            {
                entity.HasKey(e => new { e.IdSubject, e.IdCurriculum })
                    .HasName("Subject_Curriculum_pk");

                entity.ToTable("Subject_Curriculum");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne(d => d.IdCurriculumNavigation)
                    .WithMany(p => p.SubjectCurriculum)
                    .HasForeignKey(d => d.IdCurriculum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject_Curriculum_Curriculum");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.SubjectCurriculum)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Subject_Curriculum_Subject");
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.IdTeacher)
                    .HasName("Teacher_pk");

                entity.Property(e => e.IdTeacher).ValueGeneratedNever();

                entity.HasOne(d => d.IdFacultyNavigation)
                    .WithMany(p => p.Teacher)
                    .HasForeignKey(d => d.IdFaculty)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Faculty");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithOne(p => p.Teacher)
                    .HasForeignKey<Teacher>(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Person");
            });

            modelBuilder.Entity<TeacherSubject>(entity =>
            {
                entity.HasKey(e => new { e.IdTeacher, e.IdSubject })
                    .HasName("Teacher_Subject_pk");

                entity.ToTable("Teacher_Subject");

                entity.HasOne(d => d.IdSubjectNavigation)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.IdSubject)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Subject_Subject");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.TeacherSubject)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Teacher_Subject_Teacher");
            });

            modelBuilder.Entity<TitleDict>(entity =>
            {
                entity.HasKey(e => e.IdTitle)
                    .HasName("TitleDict_pk");

                entity.Property(e => e.IdTitle).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCountryNavigation)
                    .WithMany(p => p.TitleDict)
                    .HasForeignKey(d => d.IdCountry)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("TitleDict_CountryDict");
            });

            modelBuilder.Entity<TitleTeacher>(entity =>
            {
                entity.HasKey(e => new { e.IdTitle, e.IdTeacher })
                    .HasName("Title_Teacher_pk");

                entity.ToTable("Title_Teacher");

                entity.Property(e => e.IssuedAt).HasColumnType("datetime");

                entity.HasOne(d => d.IdTeacherNavigation)
                    .WithMany(p => p.TitleTeacher)
                    .HasForeignKey(d => d.IdTeacher)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Title_Teacher_Teacher");

                entity.HasOne(d => d.IdTitleNavigation)
                    .WithMany(p => p.TitleTeacher)
                    .HasForeignKey(d => d.IdTitle)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Title_Teacher_TitleDict");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
