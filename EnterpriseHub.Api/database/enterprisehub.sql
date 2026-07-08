CREATE DATABASE EnterpriseHubDb;
GO

USE EnterpriseHubDb;
GO

CREATE TABLE Departments (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(120) NOT NULL,
    ManagerName NVARCHAR(120) NOT NULL,
    MonthlyBudget DECIMAL(18,2) NOT NULL
);
GO

CREATE TABLE JobPositions (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(120) NOT NULL,
    Level NVARCHAR(60) NOT NULL,
    BaseSalary DECIMAL(18,2) NOT NULL
);
GO

CREATE TABLE Employees (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(80) NOT NULL,
    LastName NVARCHAR(80) NOT NULL,
    Email NVARCHAR(160) NOT NULL,
    HireDate DATE NOT NULL,
    Status NVARCHAR(40) NOT NULL,
    DepartmentId INT NOT NULL,
    JobPositionId INT NOT NULL,
    CONSTRAINT FK_Employees_Departments FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    CONSTRAINT FK_Employees_JobPositions FOREIGN KEY (JobPositionId) REFERENCES JobPositions(Id)
);
GO

CREATE TABLE LeaveRequests (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    EmployeeId INT NOT NULL,
    StartDate DATE NOT NULL,
    EndDate DATE NOT NULL,
    Reason NVARCHAR(240) NOT NULL,
    Status NVARCHAR(40) NOT NULL,
    CONSTRAINT FK_LeaveRequests_Employees FOREIGN KEY (EmployeeId) REFERENCES Employees(Id) ON DELETE CASCADE
);
GO

INSERT INTO Departments (Name, ManagerName, MonthlyBudget) VALUES
('Tecnologia', 'Carla Mendoza', 18000),
('Finanzas', 'Luis Andrade', 12000),
('Operaciones', 'Andrea Velasco', 15000);
GO

INSERT INTO JobPositions (Title, Level, BaseSalary) VALUES
('Desarrollador Full Stack', 'Senior', 1850),
('Analista Financiero', 'Semi Senior', 1450),
('Coordinador Operativo', 'Senior', 1600);
GO

INSERT INTO Employees (FirstName, LastName, Email, HireDate, Status, DepartmentId, JobPositionId) VALUES
('Sofia', 'Guerrero', 'sofia.guerrero@enterprisehub.com', '2024-02-05', 'Activo', 1, 1),
('Mateo', 'Salazar', 'mateo.salazar@enterprisehub.com', '2023-09-18', 'Activo', 2, 2);
GO

INSERT INTO LeaveRequests (EmployeeId, StartDate, EndDate, Reason, Status) VALUES
(1, '2026-07-14', '2026-07-16', 'Capacitacion externa', 'Aprobada'),
(2, '2026-08-01', '2026-08-03', 'Asuntos personales', 'Pendiente');
GO
