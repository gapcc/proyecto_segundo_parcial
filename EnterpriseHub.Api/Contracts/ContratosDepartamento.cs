namespace EnterpriseHub.Api.Contracts;

public record DepartamentoDto(int Id, string Name, string ManagerName, decimal MonthlyBudget, int EmployeeCount);

public record GuardarDepartamentoRequest(string Name, string ManagerName, decimal MonthlyBudget);
