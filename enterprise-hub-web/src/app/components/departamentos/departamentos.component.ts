import { Component, OnInit } from '@angular/core';

import { Departamento } from '../../models';
import { ServicioApi } from '../../services/servicio-api.service';

@Component({
  selector: 'app-departamentos',
  templateUrl: './departamentos.component.html',
  standalone: false
})
export class DepartamentosComponent implements OnInit {
  departments: Departamento[] = [];
  departmentForm = this.createForm();
  feedback = '';

  constructor(private readonly servicioApi: ServicioApi) {}

  ngOnInit(): void {
    this.loadDepartments();
  }

  saveDepartment(): void {
    this.servicioApi.saveDepartment(this.departmentForm).subscribe({
      next: () => {
        this.feedback = 'Departamento guardado correctamente.';
        this.departmentForm = this.createForm();
        this.loadDepartments();
      },
      error: () => (this.feedback = 'No fue posible guardar el departamento.')
    });
  }

  editDepartment(departamento: Departamento): void {
    this.departmentForm = { ...departamento };
    this.feedback = 'Editando departamento seleccionado.';
  }

  deleteDepartment(id: number): void {
    this.servicioApi.deleteDepartment(id).subscribe({
      next: () => {
        this.feedback = 'Departamento eliminado.';
        this.loadDepartments();
      },
      error: () => (this.feedback = 'No se pudo eliminar. Verifica si tiene empleados asociados.')
    });
  }

  resetForm(): void {
    this.departmentForm = this.createForm();
    this.feedback = '';
  }

  private loadDepartments(): void {
    this.servicioApi.getDepartments().subscribe((data) => (this.departments = data));
  }

  private createForm(): Partial<Departamento> {
    return { name: '', managerName: '', monthlyBudget: 0 };
  }
}
