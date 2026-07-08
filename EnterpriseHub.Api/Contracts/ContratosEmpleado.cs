namespace EnterpriseHub.Api.Contracts;

public record EmpleadoDto(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    DateTime HireDate,
    string Status,
    int DepartmentId,
    string DepartmentName,
    int JobPositionId,
    string JobPositionTitle);

public record GuardarEmpleadoRequest(
    string FirstName,
    string LastName,
    string Email,
    DateTime HireDate,
    string Status,
    int DepartmentId,
    int JobPositionId);
