export interface Departamento {
  id: number;
  name: string;
  managerName: string;
  monthlyBudget: number;
  employeeCount: number;
}

export interface Cargo {
  id: number;
  title: string;
  level: string;
  baseSalary: number;
  employeeCount: number;
}

export interface Empleado {
  id: number;
  firstName: string;
  lastName: string;
  email: string;
  hireDate: string;
  status: string;
  departmentId: number;
  departmentName: string;
  jobPositionId: number;
  jobPositionTitle: string;
}

export interface SolicitudPermiso {
  id: number;
  employeeId: number;
  employeeName: string;
  startDate: string;
  endDate: string;
  reason: string;
  status: string;
  totalDays: number;
}
