import { Component, OnInit } from '@angular/core';
import { forkJoin } from 'rxjs';

import { ServicioApi } from '../../services/servicio-api.service';
import { Departamento, Empleado, Cargo, SolicitudPermiso } from '../../models';

@Component({
  selector: 'app-panel',
  templateUrl: './panel.component.html',
  styleUrl: './panel.component.css',
  standalone: false
})
export class PanelComponent implements OnInit {
  departments: Departamento[] = [];
  positions: Cargo[] = [];
  employees: Empleado[] = [];
  leaveRequests: SolicitudPermiso[] = [];

  constructor(private readonly servicioApi: ServicioApi) {}

  ngOnInit(): void {
    forkJoin({
      departments: this.servicioApi.getDepartments(),
      positions: this.servicioApi.getJobPositions(),
      employees: this.servicioApi.getEmployees(),
      leaveRequests: this.servicioApi.getLeaveRequests()
    }).subscribe(({ departments, positions, employees, leaveRequests }) => {
      this.departments = departments;
      this.positions = positions;
      this.employees = employees;
      this.leaveRequests = leaveRequests;
    });
  }
}
