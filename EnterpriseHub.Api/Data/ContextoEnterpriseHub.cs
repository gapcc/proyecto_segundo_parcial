using EnterpriseHub.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EnterpriseHub.Api.Data;

public class ContextoEnterpriseHub(DbContextOptions<ContextoEnterpriseHub> options) : DbContext(options)
{
    public DbSet<Departamento> Departments => Set<Departamento>();
    public DbSet<Cargo> JobPositions => Set<Cargo>();
    public DbSet<Empleado> Employees => Set<Empleado>();
    public DbSet<SolicitudPermiso> LeaveRequests => Set<SolicitudPermiso>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Departamento>(entity =>
        {
            entity.Property(item => item.Name).HasMaxLength(120);
            entity.Property(item => item.ManagerName).HasMaxLength(120);
            entity.Property(item => item.MonthlyBudget).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Cargo>(entity =>
        {
            entity.Property(item => item.Title).HasMaxLength(120);
            entity.Property(item => item.Level).HasMaxLength(60);
            entity.Property(item => item.BaseSalary).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.Property(item => item.FirstName).HasMaxLength(80);
            entity.Property(item => item.LastName).HasMaxLength(80);
            entity.Property(item => item.Email).HasMaxLength(160);
            entity.Property(item => item.Status).HasMaxLength(40);

            entity.HasOne(item => item.Departamento)
                .WithMany(item => item.Employees)
                .HasForeignKey(item => item.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(item => item.Cargo)
                .WithMany(item => item.Employees)
                .HasForeignKey(item => item.JobPositionId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<SolicitudPermiso>(entity =>
        {
            entity.Property(item => item.Reason).HasMaxLength(240);
            entity.Property(item => item.Status).HasMaxLength(40);

            entity.HasOne(item => item.Empleado)
                .WithMany(item => item.LeaveRequests)
                .HasForeignKey(item => item.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
