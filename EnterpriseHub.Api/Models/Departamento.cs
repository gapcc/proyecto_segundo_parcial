namespace EnterpriseHub.Api.Models;

public class Departamento
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string ManagerName { get; set; } = string.Empty;
    public decimal MonthlyBudget { get; set; }
    public ICollection<Empleado> Employees { get; set; } = [];
}
