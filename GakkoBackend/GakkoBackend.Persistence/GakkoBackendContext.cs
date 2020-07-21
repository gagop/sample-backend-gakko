using System;
using GakkoBackend.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GakkoBackend.Persistence
{
    public partial class GakkoBackendContext : DbContext
    {
        public GakkoBackendContext()
        {
        }

        public GakkoBackendContext(DbContextOptions<GakkoBackendContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Candidate> Candidate { get; set; }
        public virtual DbSet<Curriculum> Curriculum { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Semestr> Semestr { get; set; }
        public virtual DbSet<StudiesModeDict> StudiesModeDict { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Candidate>(entity =>
            {
                entity.HasKey(e => e.IdCandidate)
                    .HasName("Candidate_pk");

                entity.Property(e => e.IdCandidate).ValueGeneratedNever();

                entity.HasOne(d => d.IdCandidateNavigation)
                    .WithOne(p => p.Candidate)
                    .HasForeignKey<Candidate>(d => d.IdCandidate)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Candidate_Person");

                entity.HasOne(d => d.IdCurriculumNavigation)
                    .WithMany(p => p.Candidate)
                    .HasForeignKey(d => d.IdCurriculum)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Candidate_Curriculum");
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
                    .HasConstraintName("Curriculum_Semestr");

                entity.HasOne(d => d.IdStudiesModeNavigation)
                    .WithMany(p => p.Curriculum)
                    .HasForeignKey(d => d.IdStudiesMode)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Curriculum_StudiesModeDict");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.IdEmployee)
                    .HasName("Employee_pk");

                entity.Property(e => e.IdEmployee).ValueGeneratedNever();

                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.RefreshToken)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.RefreshTokenExpDate).HasColumnType("datetime");

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithOne(p => p.Employee)
                    .HasForeignKey<Employee>(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("Employee_Person");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.HasKey(e => e.IdNews)
                    .HasName("News_pk");

                entity.Property(e => e.IdNews).ValueGeneratedNever();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasMaxLength(500);

                entity.HasOne(d => d.IdEmployeeNavigation)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.IdEmployee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("News_Employee");
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.IdPerson)
                    .HasName("Person_pk");

                entity.Property(e => e.IdPerson).ValueGeneratedNever();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasMaxLength(80);
            });

            modelBuilder.Entity<Semestr>(entity =>
            {
                entity.HasKey(e => e.IdSemestr)
                    .HasName("Semestr_pk");

                entity.Property(e => e.IdSemestr).ValueGeneratedNever();

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<StudiesModeDict>(entity =>
            {
                entity.HasKey(e => e.IdStudiesMode)
                    .HasName("StudiesModeDict_pk");

                entity.Property(e => e.IdStudiesMode).ValueGeneratedNever();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
