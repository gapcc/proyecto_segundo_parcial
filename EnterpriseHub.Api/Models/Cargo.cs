namespace EnterpriseHub.Api.Models;

public class Cargo
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Level { get; set; } = string.Empty;
    public decimal BaseSalary { get; set; }
    public ICollection<Empleado> Employees { get; set; } = [];
}
