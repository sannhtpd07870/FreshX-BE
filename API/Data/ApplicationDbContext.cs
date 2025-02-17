﻿using Microsoft.EntityFrameworkCore;
using API.Models;
using API.Server.Models;
using NuGet.Protocol;

namespace API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<AccountCus> AccountCus { get; set; }
        public DbSet<AccountEmp> AccountEmp { get; set; }
        public DbSet<Advice> Advice { get; set; }
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<DiagnosisSymptom> DiagnosisSymptom { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<Exercise> Exercise { get; set; }
        public DbSet<Insurance> Insurance { get; set; }
        public DbSet<Meal> Meal { get; set; }
        public DbSet<MedicalRecord> MedicalRecord { get; set; }
        public DbSet<Note> Note { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Symptom> Symptom { get; set; }
        // Thêm DbSet cho ChatMessage và ChatSession
        public DbSet<ChatMessage> ChatMessages { get; set; }
        public DbSet<ChatSession> ChatSessions { get; set; }
        public DbSet<Feedback> Feedback { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // AccountCus configuration
            modelBuilder.Entity<AccountCus>()
                .HasOne(ac => ac.Customer)
                .WithMany(c => c.AccountCus)
                .HasForeignKey(ac => ac.CustomerId);

            // AccountEmp configuration
            modelBuilder.Entity<AccountEmp>()
                .HasOne(ae => ae.Employee)
                .WithMany(e => e.AccountEmp)
                .HasForeignKey(ae => ae.EmployeeId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa nhân viên khi xóa AccountEmp

            modelBuilder.Entity<AccountEmp>()
                .HasOne(ae => ae.Role)
                .WithMany(r => r.AccountEmp)
                .HasForeignKey(ae => ae.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa vai trò khi xóa AccountEmp

            // Advice configuration
            modelBuilder.Entity<Advice>()
                .HasOne(a => a.Diagnosis)
                .WithMany(d => d.Advices)
                .HasForeignKey(a => a.DiagnosisId);

            // Appointment configuration
            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.Customer)
                .WithMany(c => c.Appointments)
                .HasForeignKey(ap => ap.CustomerId);
            modelBuilder.Entity<Appointment>()
                .HasOne(ap => ap.Employee)
                .WithMany(e => e.Appointments)
                .HasForeignKey(ap => ap.EmployeeId);
            // DiagnosisSymptom configuration
            modelBuilder.Entity<DiagnosisSymptom>()
                .HasKey(ds => new { ds.DiagnosisId, ds.SymptomId });
            modelBuilder.Entity<DiagnosisSymptom>()
                .HasOne(ds => ds.Diagnosis)
                .WithMany(d => d.DiagnosisSymptoms)
                .HasForeignKey(ds => ds.DiagnosisId);
            modelBuilder.Entity<DiagnosisSymptom>()
                .HasOne(ds => ds.Symptom)
                .WithMany(s => s.DiagnosisSymptoms)
                .HasForeignKey(ds => ds.SymptomId);

            // Exercise configuration
            modelBuilder.Entity<Exercise>()
                .HasOne(e => e.Diagnosis)
                .WithMany(d => d.Exercises)
                .HasForeignKey(e => e.DiagnosisId);

            // Meal configuration
            modelBuilder.Entity<Meal>()
                .HasOne(m => m.Diagnosis)
                .WithMany(d => d.Meals)
                .HasForeignKey(m => m.DiagnosisId);

            // MedicalRecord configuration
            modelBuilder.Entity<MedicalRecord>()
                .HasOne(mr => mr.Customer)
                .WithMany(c => c.MedicalRecords)
                .HasForeignKey(mr => mr.CustomerId);

            // Note configuration
            modelBuilder.Entity<Note>()
                .HasOne(n => n.MedicalRecord)
                .WithMany(mr => mr.Notes)
                .HasForeignKey(n => n.MedicalRecordId);

            // Role configuration
            modelBuilder.Entity<Role>()
                .HasMany(r => r.AccountEmp)
                .WithOne(ae => ae.Role)
                .HasForeignKey(ae => ae.RoleId)
                .OnDelete(DeleteBehavior.Restrict); // Tránh xóa vai trò khi xóa AccountEmp

            // Symptom configuration
            modelBuilder.Entity<Symptom>()
                .HasMany(s => s.DiagnosisSymptoms)
                .WithOne(ds => ds.Symptom)
                .HasForeignKey(ds => ds.SymptomId);

            // Cấu hình mối quan hệ giữa ChatMessage và ChatSession
            modelBuilder.Entity<ChatMessage>()
                .HasOne(cm => cm.ChatSession)
                .WithMany(cs => cs.ChatMessages)
                .HasForeignKey(cm => cm.ChatSessionId);

            // Cấu hình các thuộc tính khác cho ChatMessage nếu cần
            modelBuilder.Entity<ChatMessage>()
                .Property(cm => cm.MessageType)
                .IsRequired()
                .HasMaxLength(50);

            // Cấu hình các thuộc tính khác cho ChatSession nếu cần
            modelBuilder.Entity<ChatSession>()
                .HasMany(cs => cs.ChatMessages)
                .WithOne(cm => cm.ChatSession)
                .HasForeignKey(cm => cm.ChatSessionId);
            //Feedback
            modelBuilder.Entity<Feedback>().HasOne(f => f.AccountCus).WithMany(a => a.Feebacks).
                HasForeignKey(a => a.AccountId);
        }
    }
}
