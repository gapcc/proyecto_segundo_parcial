using EnterpriseHub.Api.Models;

namespace EnterpriseHub.Api.Data;

public static class DatosIniciales
{
    public static void Initialize(ContextoEnterpriseHub context)
    {
        if (context.Departments.Any())
        {
            return;
        }

        var departments = new[]
        {
            new Departamento { Name = "Tecnologia", ManagerName = "Carla Mendoza", MonthlyBudget = 18000m },
            new Departamento { Name = "Finanzas", ManagerName = "Luis Andrade", MonthlyBudget = 12000m },
            new Departamento { Name = "Operaciones", ManagerName = "Andrea Velasco", MonthlyBudget = 15000m }
        };

        var positions = new[]
        {
            new Cargo { Title = "Desarrollador Full Stack", Level = "Senior", BaseSalary = 1850m },
            new Cargo { Title = "Analista Financiero", Level = "Semi Senior", BaseSalary = 1450m },
            new Cargo { Title = "Coordinador Operativo", Level = "Senior", BaseSalary = 1600m }
        };

        context.Departments.AddRange(departments);
        context.JobPositions.AddRange(positions);
        context.SaveChanges();

        var employees = new[]
        {
            new Empleado
            {
                FirstName = "Sofia",
                LastName = "Guerrero",
                Email = "sofia.guerrero@enterprisehub.com",
                HireDate = new DateTime(2024, 2, 5),
                Status = "Activo",
                DepartmentId = departments[0].Id,
                JobPositionId = positions[0].Id
            },
            new Empleado
            {
                FirstName = "Mateo",
                LastName = "Salazar",
                Email = "mateo.salazar@enterprisehub.com",
                HireDate = new DateTime(2023, 9, 18),
                Status = "Activo",
                DepartmentId = departments[1].Id,
                JobPositionId = positions[1].Id
            }
        };

        context.Employees.AddRange(employees);
        context.SaveChanges();

        context.LeaveRequests.AddRange(
            new SolicitudPermiso
            {
                EmployeeId = employees[0].Id,
                StartDate = new DateTime(2026, 7, 14),
                EndDate = new DateTime(2026, 7, 16),
                Reason = "Capacitacion externa",
                Status = "Aprobada"
            },
            new SolicitudPermiso
            {
                EmployeeId = employees[1].Id,
                StartDate = new DateTime(2026, 8, 1),
                EndDate = new DateTime(2026, 8, 3),
                Reason = "Asuntos personales",
                Status = "Pendiente"
            });

        context.SaveChanges();
    }
}
