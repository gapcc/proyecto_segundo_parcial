import { Component, OnInit } from '@angular/core';

import { ServicioApi } from '../../services/servicio-api.service';
import { Departamento, Empleado, Cargo } from '../../models';

@Component({
  selector: 'app-empleados',
  templateUrl: './empleados.component.html',
  standalone: false
})
export class EmpleadosComponent implements OnInit {
  employees: Empleado[] = [];
  departments: Departamento[] = [];
  positions: Cargo[] = [];
  employeeForm = this.createForm();
  feedback = '';

  constructor(private readonly servicioApi: ServicioApi) {}

  ngOnInit(): void {
    this.loadCatalogs();
    this.loadEmployees();
  }

  saveEmployee(): void {
    this.servicioApi.saveEmployee(this.employeeForm).subscribe({
      next: () => {
        this.feedback = 'Empleado guardado correctamente.';
        this.employeeForm = this.createForm();
        this.loadEmployees();
      },
      error: () => (this.feedback = 'No fue posible guardar el empleado.')
    });
  }

  editEmployee(empleado: Empleado): void {
    this.employeeForm = { ...empleado, hireDate: empleado.hireDate.slice(0, 10) };
    this.feedback = 'Editando empleado seleccionado.';
  }

  deleteEmployee(id: number): void {
    this.servicioApi.deleteEmployee(id).subscribe(() => {
      this.feedback = 'Empleado eliminado.';
      this.loadEmployees();
    });
  }

  resetForm(): void {
    this.employeeForm = this.createForm();
    this.feedback = '';
  }

  private loadCatalogs(): void {
    this.servicioApi.getDepartments().subscribe((data) => (this.departments = data));
    this.servicioApi.getJobPositions().subscribe((data) => (this.positions = data));
  }

  private loadEmployees(): void {
    this.servicioApi.getEmployees().subscribe((data) => (this.employees = data));
  }

  private createForm(): Partial<Empleado> {
    return {
      firstName: '',
      lastName: '',
      email: '',
      hireDate: new Date().toISOString().slice(0, 10),
      status: 'Activo',
      departmentId: 0,
      jobPositionId: 0
    };
  }
}
