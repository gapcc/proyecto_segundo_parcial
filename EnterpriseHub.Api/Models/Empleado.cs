namespace EnterpriseHub.Api.Models;

public class Empleado
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime HireDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public int JobPositionId { get; set; }
    public Departamento? Departamento { get; set; }
    public Cargo? Cargo { get; set; }
    public ICollection<SolicitudPermiso> LeaveRequests { get; set; } = [];
}
