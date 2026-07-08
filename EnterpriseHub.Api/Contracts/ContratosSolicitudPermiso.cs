namespace EnterpriseHub.Api.Contracts;

public record SolicitudPermisoDto(
    int Id,
    int EmployeeId,
    string EmployeeName,
    DateTime StartDate,
    DateTime EndDate,
    string Reason,
    string Status,
    int TotalDays);

public record GuardarSolicitudPermisoRequest(
    int EmployeeId,
    DateTime StartDate,
    DateTime EndDate,
    string Reason,
    string Status);
