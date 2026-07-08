namespace EnterpriseHub.Api.Contracts;

public record CargoDto(int Id, string Title, string Level, decimal BaseSalary, int EmployeeCount);

public record GuardarCargoRequest(string Title, string Level, decimal BaseSalary);
